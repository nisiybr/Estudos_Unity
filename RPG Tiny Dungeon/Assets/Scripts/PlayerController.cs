using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	
	private transitionController _TC;
	
	//public bool isDoor;
	private Rigidbody2D rbPlayer;
	private SpriteRenderer srPlayer;
	private Animator animPlayer;
	private bool isWalking;
	private bool isAttacking;

	public float horizontal;
	public float vertical;

	public float playerVelocity;
	public bool isLookingRight;

	public Transform Raypoint;

	public LayerMask interacao;

	public GameObject slashFrontPrefab;
	public GameObject slashBackPrefab;
	public GameObject slashRightPrefab;
	public GameObject slashLeftPrefab;
	public int idDirecao; //0 - front, 1 - back, 2 - sides


	public List<int> doorKeys = new List<int>();
	//public int[] doorKeys;

	// Use this for initialization
	void Start () {
		idDirecao = 0; // Player começa olhando para baixo
		_TC = FindObjectOfType(typeof(transitionController)) as transitionController;
		
		rbPlayer = GetComponent<Rigidbody2D>();
		srPlayer = GetComponent<SpriteRenderer>();
		animPlayer = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		movimentar();
		atacar();
		atualizarAnimator();
		testaColisaoRaycast();
		
	}
	void movimentar(){
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");
 
		Debug.DrawRay(Raypoint.position, new Vector2(horizontal,vertical) * 0.11f, Color.red);


		rbPlayer.velocity = new Vector2(horizontal*playerVelocity,vertical*playerVelocity);
	}
	void atacar(){
		if(Input.GetButtonDown("Fire1")){
			isAttacking = true;
		}
	}
	public void slash()
	{
		GameObject slashPrefab = null;
		switch(idDirecao)
		{
			case 0:
				slashPrefab = slashFrontPrefab;
			break;
			case 1:
				slashPrefab = slashBackPrefab;
			break;
			case 2:
				if(isLookingRight)
				{
					slashPrefab = slashRightPrefab;
				}
				else 
				{
					slashPrefab = slashLeftPrefab;
				}
			break;
		}
		GameObject temp = Instantiate(slashPrefab,transform.position,transform.localRotation);
		Destroy(temp, 0.5f);
	}
	void atualizarAnimator(){
		//controle animacao do walk
		if(horizontal != 0 || vertical != 0 )
			isWalking = true;
		else isWalking = false;
		animPlayer.SetBool("walk",isWalking);

		//chama a animacao de ataque, que deve ativar o colisor do ataque
		if(isAttacking)
		{
			animPlayer.SetTrigger("attack");
			isAttacking = false;
		}


		//Controla qual layer deve ser utilizada
		//Layer 0 =  front
		//Layer 1 =  back
		//Layer 2 =  side
		
		if (vertical > 0)
		{
			idDirecao = 1;
			animPlayer.SetLayerWeight(1,1);
			animPlayer.SetLayerWeight(2,0);
			srPlayer.flipX = false;
		}
		if (vertical < 0)
		{
			idDirecao = 0;
			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,0);
			srPlayer.flipX = false;
		}		
		if (horizontal < 0)
		{
			idDirecao = 2;
			isLookingRight = false;
			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,1);
			srPlayer.flipX = true;
		}		
		if (horizontal > 0)
		{
			idDirecao = 2;
			isLookingRight = true ;
			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,1);
			srPlayer.flipX = false;
		}		
	}
	private void OnTriggerEnter2D(Collider2D col) {
		switch(col.tag)
		{
			case "colectable":
				if(col.gameObject.GetComponent<ColectableController>().type == "key")
				{
					doorKeys.Add(col.gameObject.GetComponent<ColectableController>().idKey);
				}
				Destroy(col.gameObject);
			break;
		}
	}
	void testaColisaoRaycast()
	{
		RaycastHit2D hit = Physics2D.Raycast(Raypoint.position, new Vector2(horizontal,vertical),0.11f, interacao);
		if(hit && !_TC.isDoor)
		{
			doorController temp = hit.transform.gameObject.GetComponent<doorController>();

			if(!temp.doorLocked)
			{
				_TC.isDoor = true;
				temp.doorClosed = false;
				temp.atualizaSprite();  
				_TC.startFade(temp);
			}
			else
			{
				if(temp.idKey == 0 && doorKeys.Count > 0)
				{
					_TC.isDoor = true; 
					temp.doorClosed = false; 
					temp.doorLocked = false;
					temp.atualizaSprite();  
					_TC.startFade(temp);
				}
				else
				{
					if(doorKeys.Exists(element => element == temp.idKey))
					{
						_TC.isDoor = true; 
						temp.doorClosed = false; 
						temp.doorLocked = false;
						temp.atualizaSprite();  
						_TC.startFade(temp);
					}
				}
			}			
		}
	}
}

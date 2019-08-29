using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		
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
			
			animPlayer.SetLayerWeight(1,1);
			animPlayer.SetLayerWeight(2,0);
			srPlayer.flipX = false;
		}
		if (vertical < 0)
		{
			
			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,0);
			srPlayer.flipX = false;
		}		
		if (horizontal < 0)
		{

			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,1);
			srPlayer.flipX = true;
		}		
		if (horizontal > 0)
		{
			
			animPlayer.SetLayerWeight(1,0);
			animPlayer.SetLayerWeight(2,1);
			srPlayer.flipX = false;
		}		
	}
	void testaColisaoRaycast()
	{
		RaycastHit2D hit = Physics2D.Raycast(Raypoint.position, new Vector2(horizontal,vertical),0.11f, interacao);
		if(hit && !_TC.isDoor)
		{
			_TC.isDoor = true; 

			doorController temp = hit.transform.gameObject.GetComponent<doorController>();
			_TC.startFade(temp);
			

		}
	}
}

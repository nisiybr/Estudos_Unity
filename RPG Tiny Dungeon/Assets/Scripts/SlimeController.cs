using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

	private PlayerController _PC;
	public GameObject explosaoPrefab;

	private Rigidbody2D slimeRb;
	public Vector2 direcaoMovimento;
	public float velocidadeMovimento;
	public float tempoMinMovimento;
	public float tempoMinEspera;
	public float tempoMaxMovimento;
	public float tempoMaxEspera;

	public LayerMask	mascaraPlayer;
	public bool isPlayerLock;
	public bool chaveIsLock;
	public float areaPercepcao;
	public Color pursuitColor;
	public Color originalColor;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		_PC = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		slimeRb = GetComponent<Rigidbody2D >();
		StartCoroutine("moverSlime");
	}
	
	// Update is called once per frame
	void Update () {
		slimeRb.velocity = direcaoMovimento * velocidadeMovimento;

		if(isPlayerLock)
		{
			chaveIsLock = true;
			direcaoMovimento = Vector3.Normalize(_PC.transform.position - transform.position)*3;
			spriteRenderer.color = pursuitColor;
		}
		else
		{
			if(chaveIsLock)
			{
				direcaoMovimento = new Vector2(0,0);
				StartCoroutine("moverSlime");
				spriteRenderer.color = originalColor;
				chaveIsLock = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		switch(col.tag)
		{
			case "slash":
				Instantiate(explosaoPrefab,transform.position,transform.localRotation);	
				if(this.tag == "keyMonsters")
				{
					this.GetComponentInParent<doorController>().retiraDaList(this.gameObject);
					if(this.GetComponentInParent<doorController>().monsters.Count == 0)
					{
						
						this.GetComponentInParent<doorController>().doorSpecialEvent 	= false; 
						this.GetComponentInParent<doorController>().doorClosed 			= false; 
						this.GetComponentInParent<doorController>().doorLocked 			= false;					
						this.GetComponentInParent<doorController>().atualizaSprite();
					}
				}
				Destroy(this.gameObject);		
			break;
		}
	}


	IEnumerator moverSlime()
	{

		direcaoMovimento = new Vector2(0,0);
		yield return new WaitForSeconds(Random.Range(tempoMinEspera,tempoMaxEspera));

		if(!isPlayerLock)
		{
			
			int x = Random.Range(-1,2);//sorteia -1, 0 , 2 
			int y = Random.Range(-1,2);//sorteia -1, 0 , 2 
			direcaoMovimento = new Vector2 (x,y);

			yield return new WaitForSeconds(Random.Range(tempoMinMovimento,tempoMaxMovimento));

		}
		StartCoroutine("moverSlime");
	}

	private void FixedUpdate() {
		isPlayerLock = Physics2D.OverlapCircle(transform.position,0.6f,mascaraPlayer);
	}
}

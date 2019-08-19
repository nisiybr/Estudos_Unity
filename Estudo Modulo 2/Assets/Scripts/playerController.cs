using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private gameController _GC;
	
	[Header("VARIABLES PLAYER")]
	public 		bool 			isFaceToRight;
	public 		float 			horizontal;
	public 		float 			vertical;
	public		Transform 		groundCheckL;
	public		Transform 		groundCheckR;
	public	 	Rigidbody2D 	rbPlayer;
	private 	Animator		animPlayer;
	public 		bool 			isGrounded;
	public		bool 			isFlying;
	public		bool 			isSwimming;
	public		bool 			isSubmerso;
	public 		Transform 		maoPersonagem;

	public		bool 			isLoadingHeadHit;
	public		bool 			isAbleToThrow;
	public		bool 			isAbleToHit;
	public		bool 			isAbleToFly;

	[Header("STATS PLAYER")]
	public 		int 			HP;
	public		float 			defesa;
	public		float 			ataqueBase;
	public		float 			forcaPulo;
	public		float 			forcaPuloNado;
	public 		float 			velocityPlayer;
	public 		float 			velocityPlayerSubmerso;
	public 		float 			velocityPlayerMultiplier;
	public		float 			forcaArremeso;
	public		float 			attackSpeedBall; //Delay entre tiros
	public		float 			attackSpeedHammer; //Delay entre marteladas
	public		float			gravityDefault;
	public		float			gravityFlying;
	public		float			gravitySwimming;
	public		float			timeToFly;

	[Header("COLLIDERS PLAYER")]
	public		Collider2D 		colliderDefault;
	public		Collider2D 		colliderSwimming;


	[Header("MASCARA CHAO")]
	public		LayerMask		whatIsGround;

	[Header("PREFAB TIRO")]
	public		GameObject 		ballPrefab;


	[Header("POWER UPS")]
	public		bool 			haveHammer;
	public		bool 			haveBalls;
	public 		bool			haveCloak;






	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(gameController)) as gameController;

		rbPlayer = GetComponent<Rigidbody2D> ();
		animPlayer = GetComponent<Animator> ();

		rbPlayer.gravityScale = gravityDefault;

		//srPlayer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkIfGrounded ();

		if (!isSwimming ) 
		{

				atacar ();
		
			if (!isLoadingHeadHit) 
			{
				movimentar ();
				pular ();
				voar ();
			}

		} 
		else 
		{
			nadar ();
		}


		UpdateAnimator ();
	}
	void checkIfGrounded(){
		isGrounded = Physics2D.OverlapArea (groundCheckL.position, groundCheckR.position,whatIsGround);
	}
	void movimentar()
	{
		horizontal = Input.GetAxis ("Horizontal");
		rbPlayer.velocity = new Vector2 (velocityPlayer * horizontal * velocityPlayerMultiplier, rbPlayer.velocity.y);

		correr ();

		//Verifica para qual lado deve-se vira  o personagem
		if (horizontal < 0 && isFaceToRight) {
			flip ();	
		} else if (horizontal > 0 && !isFaceToRight) {
			flip ();
		}
			
	}
	void correr()
	{
		if(Input.GetButton("Fire3")){
			velocityPlayerMultiplier = 2;	
		}
		if(Input.GetButtonUp("Fire3")){
			velocityPlayerMultiplier = 1;	
		}
	}
	void flip()
	{
		Vector3 myScale;
		myScale = this.transform.localScale;
		myScale.x *= -1;
		this.transform.localScale = myScale;
		isFaceToRight = !isFaceToRight;
	}
	void pular()
	{
		if(Input.GetButtonDown("Jump") && isGrounded ){
			rbPlayer.AddForce(new Vector2 (0f, forcaPulo));			
		}

	}
	void nadar()
	{
		horizontal = Input.GetAxis ("Horizontal");
		rbPlayer.velocity = new Vector2 (velocityPlayerSubmerso * horizontal * velocityPlayerMultiplier, rbPlayer.velocity.y);

		vertical = Input.GetAxis ("Vertical");
		if (isSwimming && isSubmerso) {
			print("1");
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, velocityPlayerSubmerso * vertical * velocityPlayerMultiplier);
		}
		if (isSwimming && !isSubmerso && vertical < 0) {
			print("2");
			rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, velocityPlayerSubmerso * vertical * velocityPlayerMultiplier);
		}
		if (isSwimming && !isSubmerso && Input.GetButtonDown("Jump")) {
			print("3");
			rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
			rbPlayer.AddForce(new Vector2 (0f, forcaPuloNado));	
		}

		if (horizontal < 0 && isFaceToRight) {
			flip ();	
		} else if (horizontal > 0 && !isFaceToRight) {
			flip ();
		}
			
					


	}
	void voar()
	{
		if(Input.GetButtonDown("Jump") && !isGrounded && isAbleToFly && haveCloak){
			isFlying = true;
			rbPlayer.gravityScale = gravityFlying;
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, 0.1f);
		}
		if(Input.GetButtonUp("Jump") && !isGrounded && isFlying){
			isFlying = false;
			isAbleToFly = false;
			StartCoroutine("ControlIsAbletoFly");
			rbPlayer.gravityScale = gravityDefault;
		}
		if(isGrounded){
			isFlying = false;	
			rbPlayer.gravityScale = gravityDefault;
		}

	}
	void atacar()
	{
		if(Input.GetButtonDown("Fire1") && haveBalls && isAbleToThrow){
			StartCoroutine ("ControlIsAbletoThrow");
			isFlying = false;
			animPlayer.SetTrigger ("Fire1");
			rbPlayer.gravityScale = gravityDefault;
		}
		if(Input.GetButtonDown("Fire2") && haveHammer && isAbleToHit){
			HitControl ();
			isFlying = false;
			animPlayer.SetTrigger ("Fire2");
			rbPlayer.gravityScale = gravityDefault;
		}
		if(Input.GetButtonDown("HeadHit") && isAbleToHit && isGrounded && !isSwimming){
			isLoadingHeadHit = true;
			rbPlayer.velocity = new Vector2 (0f, 0f);
			rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			HitControl ();
			animPlayer.SetBool ("HeadHitLoad",true);
		}
		if(Input.GetButtonUp("HeadHit") && isGrounded && !isSwimming){
			isLoadingHeadHit = false;
			rbPlayer.velocity = new Vector2 (0f, 0f);
			animPlayer.SetBool ("HeadHitLoad",false);
		}

	}
	void UpdateAnimator()
	{		
		animPlayer.SetBool("isWalking", horizontal != 0f ? true : false);
		animPlayer.SetBool ("isGrounded", isGrounded); 
		animPlayer.SetFloat ("velocityY", rbPlayer.velocity.y);
		animPlayer.SetBool ("isAbleToThrow", isAbleToThrow);
		animPlayer.SetBool ("isAbleToHit", isAbleToHit);
		animPlayer.SetBool ("isFlying", isFlying);
		animPlayer.SetBool ("isSwimming", isSwimming);
		animPlayer.SetBool ("isSubmerso", isSubmerso);
	}
	void atirarBola()		
	{
		GameObject temp = Instantiate (ballPrefab);
		temp.transform.position = maoPersonagem.transform.position;
		temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(isFaceToRight?forcaArremeso:(forcaArremeso*-1),0f));
	}
	void HitControl()
	{		
		isAbleToHit = false;
		StartCoroutine ("ControlIsAbletoHit");
	}
	public void releaseX()
	{
		rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	public void atualizarItens(int idItem){
		switch(idItem)
		{
			case 0://hammer
			haveHammer = true;
			break;
			case 1://bolinha
			haveBalls = true;
			break;
			case 2://capa
			haveCloak = true;
			break;
		}
			
	}
	public void trataColetavel(int c){
		switch(c)
		{
			case 0: 
			case 1: 
			case 2: 
				atualizarItens(c); //ativa hammer bolinha ou cloak
			break;			
			case 998: //Ring Normal
				_GC.coletarRings(1,10);
			break;
			case 999: //Ring Plus
				_GC.coletarRings(5,100);
			break;
		}
	}



	//------------------------------------------------------------------
	//------------------------COROUTINES--------------------------------
	//------------------------------------------------------------------
	IEnumerator ControlIsAbletoThrow ()
	{		
		isAbleToThrow = false;
		yield return new WaitForSeconds (attackSpeedBall);
		isAbleToThrow = true;
	}
	IEnumerator ControlIsAbletoHit ()
	{
		yield return new WaitForSeconds (attackSpeedHammer);
		isAbleToHit = true;
	}
	IEnumerator ControlIsAbletoFly ()
	{
		yield return new WaitForSeconds (timeToFly);
		isAbleToFly = true;
	}



	//------------------------------------------------------------------
	//------------------------COLLISIONS--------------------------------
	//------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D col)
	{
		switch (col.gameObject.tag) {
		case "agua":
			{
				isSwimming = true;
				isFlying = false;
				rbPlayer.gravityScale = gravitySwimming;
				rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, -0.2f);
				colliderDefault.enabled = false;
				colliderSwimming.enabled = true;
				break;
			}
		case "submerso":
			{
				isFlying = false;
				isSubmerso = true;
				rbPlayer.gravityScale = gravitySwimming;
				break;
			}
		case "itemLoja":
			{
				col.SendMessage("abrirLoja", 0 , SendMessageOptions.DontRequireReceiver);
				break;
			}
		case "coletavel":
			{
				col.SendMessage("coletavel", 0 , SendMessageOptions.DontRequireReceiver);
				break;
			}
		case "coletavelG":
			{
				trataColetavel(col.GetComponent<coletavelController>().idColetavel);
				Destroy(col.gameObject);				
				break;
			}
		}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		switch (col.gameObject.tag) {
		case "agua":
			{
				isFlying = false;
				isSwimming = false;
				rbPlayer.gravityScale = gravityDefault;
				colliderDefault.enabled = true;
				colliderSwimming.enabled = false;
				break;
			}
		case "submerso":
			{
				isFlying = false;
				isSubmerso = false;
				rbPlayer.velocity = new Vector2 (0f, 0f);
				rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				break;
			}
		}

	}

}

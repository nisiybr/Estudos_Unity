using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private 	Rigidbody2D 	rbPlayer;
	private 	Animator 		animPlayer;
	private 	SpriteRenderer 	srPlayer;
	public 	float 			horizontal; 
	public 		float 			playerVelocity;
	public 		float 			velocityMultiplier;
	public 		float 			jumpForce;
	private 	bool 			turnedPlayer;
	private		gameController	_GC;
	public		AudioClip[]		audioClips;
	public		Transform		groundCheck;
	public		Transform		groundCheck2;
	public		bool			isGrounded;
	public		bool			isGrounded1;
	public		bool			isGrounded2;
	public		GameObject		playerExit;
	public 		float			exitVelocity;
	private		bool			keyCouroutine;
	private		bool			keyGeneric;
	private		bool			canDecrementVigor;
	private		bool			canIncrementVigor;
	// Use this for initialization
	void Start () {
		canDecrementVigor 	= true;
		canIncrementVigor 	= true;
		keyCouroutine		= false;
		rbPlayer 			= GetComponent<Rigidbody2D>();
		animPlayer 			= GetComponent<Animator>();
		srPlayer 			= GetComponent<SpriteRenderer>();
		turnedPlayer		= false;
		_GC					= FindObjectOfType(typeof(gameController)) as gameController;
	}
	// Update is called once per frame
	void Update () {
		if(_GC.Timer > 0 && _GC.isPlayerAlive && _GC.gamePhase == gameState.gamePlay)
		{
			movePlayer();
		}
		else 
		{
			if(!keyGeneric)
			{
				rbPlayer.velocity = new Vector2(0,rbPlayer.velocity.y); //Set player's lateral velocity without multiplier
				horizontal = 0;
				isGrounded = true;
			}
			keyGeneric = true;
		}	
		//End game
		if(_GC.gamePhase == gameState.levelPassed)	
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,playerExit.transform.position,exitVelocity*Time.deltaTime);
			if(!keyCouroutine)
			{
				horizontal = 1f;
				StartCoroutine("moveToExit");
				keyCouroutine = true;
			} 
		}	
		updateAnimator();
	}
	void movePlayer()
	{
		horizontal = Input.GetAxis("Horizontal"); //Recebe input de movimento lateral
		if(Input.GetButton("Fire3"))
		{	
			if(_GC.actualVigorPoints > 5)
			{
				rbPlayer.velocity = new Vector2(horizontal*playerVelocity*velocityMultiplier,rbPlayer.velocity.y); //Set player's lateral velocity with multiplier
			}
			else
			{
				rbPlayer.velocity = new Vector2(horizontal*playerVelocity,rbPlayer.velocity.y); //Set player's lateral velocity without multiplier
			}
			if(canDecrementVigor && horizontal != 0)
			{
				
				if(_GC.actualVigorPoints >= 0)
				{
					canDecrementVigor = false;
					StartCoroutine("decrementVigor");				
				}
			}
			else if (horizontal == 0 && canIncrementVigor && _GC.actualVigorPoints < 100)
			{
				canIncrementVigor = false;
				StartCoroutine("incrementVigor");
			}
		} 
		else 
		{
			rbPlayer.velocity = new Vector2(horizontal*playerVelocity,rbPlayer.velocity.y); //Set player's lateral velocity without multiplier
			if (canIncrementVigor && _GC.actualVigorPoints < 100)
			{
				canIncrementVigor = false;
				StartCoroutine("incrementVigor");
			}
		}
		//Verify if its necessary to turn player
		if(turnedPlayer == false && horizontal < 0)
		{
			invertPlayer();
		}
		else if(turnedPlayer == true && horizontal > 0)
		{
			invertPlayer();
		}
		//verify if the player will jump
		updateIsGrouded();
		if(Input.GetButtonDown("Jump") && isGrounded && transform.position.x <= 226.65f)
		{
			jump();
		}
	}
	//Set the Animator parameter to change animation
	void updateAnimator()
	{
		animPlayer.SetFloat("horizontal",horizontal);
		animPlayer.SetFloat("yVelocity",rbPlayer.velocity.y);
		animPlayer.SetBool("isGrounded",isGrounded);
	}
	//Invert player on X axis
	void invertPlayer()
	{
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
		turnedPlayer = !turnedPlayer;
	}
	void jump()
	{
		rbPlayer.AddForce(new Vector2(0f,jumpForce));
		_GC.playOneShot(_GC.audioSource[0],audioClips[0]);
	}
	void updateIsGrouded()
	{
		isGrounded1 = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x,groundCheck.position.y),0.02f);
		isGrounded2 = Physics2D.OverlapCircle(new Vector2(groundCheck2.position.x,groundCheck2.position.y),0.02f);
		if(isGrounded1 || isGrounded2)
		{
			isGrounded = true;
		} else 
		{
			isGrounded = false;
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(_GC.gamePhase == gameState.gamePlay)
		{	
			switch(col.gameObject.tag)
			{
				case "coletavel":
					updateCheeseAmount();
					Destroy(col.gameObject);
				break;
				case "shelfBreaker":
					_GC.breakShelf();
				break;
				case "triggerEndGame":
					if(_GC.CheeseAmount >= 100)
					{
						_GC.iscallCat = false;
						print("Chamar End Game");
						_GC.completeStage();
					}
				break;
				case "enemy":	
				
					srPlayer.enabled 	= false;
					
					if(_GC.isPlayerAlive)
					{
						_GC.playOneShot(_GC.audioSource[0],_GC.audioClips[2]);
					}
					_GC.isRatInMouth = true;
					_GC.failStage();
					_GC.isPlayerAlive 	= false;
				break;
				
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		switch(col.gameObject.tag)
		{
			case "coletavel":
				updateCheeseAmount();
				Destroy(col.gameObject);
			break;
			
		}
	}
	void updateCheeseAmount()
	{
		_GC.CheeseAmount +=1;
		_GC.playOneShot(_GC.audioSource[0],_GC.audioClips[0]);
	}
	IEnumerator moveToExit()
	{
		rbPlayer.velocity = new Vector2(0,0); //Set player's lateral velocity without multiplier
		isGrounded = true;
		rbPlayer.gravityScale = 0f;
		for (float actual = this.transform.localScale.x; actual >= 0.8f; actual = actual - 0.015f)
		{
			this.transform.localScale = new Vector3(actual,actual,actual);
			yield return new WaitForSeconds(0.13f);
		}
		horizontal = 0f;
		yield return new WaitForSeconds(1.2f);
		horizontal = 1f;
		StartCoroutine("changeTranparency");
		for (float actual = this.transform.localScale.x; actual >= 0.4f; actual = actual - 0.011f)
		{
			this.transform.localScale = new Vector3(actual,actual,actual);
			yield return new WaitForSeconds(0.12f);
		}
		
	}
	IEnumerator changeTranparency()
	{
		for(float f = 1f;f >= 0; f = f - 0.01f)
		{
			if(f < 0f)
			{
				f = 0f;
			}
			Color temp = srPlayer.color;
			temp.a = f;
			srPlayer.color = temp;
			yield return new WaitForSeconds(0.035f);
		}
	}
	IEnumerator decrementVigor()
	{
		
		if((_GC.actualVigorPoints - 0.5f) < 0)
		{
			_GC.actualVigorPoints = 0f;
		}
		else
		{
			_GC.actualVigorPoints -= 0.5f;
		}
		yield return new WaitForSeconds(0.05f);
		canDecrementVigor = true;
	}
	IEnumerator incrementVigor()
	{
		_GC.actualVigorPoints += 0.8f;
		yield return new WaitForSeconds(0.02f);
		canIncrementVigor = true;
	}
}
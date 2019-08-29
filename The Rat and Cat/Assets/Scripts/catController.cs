using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catController : MonoBehaviour {

	private 	Rigidbody2D 	rbCat;
	public 		float 			catVelocity;
	private		gameController	_GC;
	private 	Animator 		animCat;
	
	// Use this for initialization
	void Start () {
		rbCat 			= GetComponent<Rigidbody2D>();
		_GC				= FindObjectOfType(typeof(gameController)) as gameController;
		animCat 		= GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animCat.SetBool("isRatInMouth",_GC.isRatInMouth);
		if(_GC.catAtLeft)
		{	
			rbCat.velocity = new Vector2(catVelocity*1,rbCat.velocity.y); //Set player's lateral velocity without multiplier
		}
		else
		{
			rbCat.velocity = new Vector2(catVelocity*-1,rbCat.velocity.y); //Set player's lateral velocity without multiplier
		} 
	}
	void OnBecameInvisible()
	{
		if(_GC != null)
		{
			if(_GC.iscallCat)
			{
				_GC.StartCoroutine("callCat");
			}
			if(_GC.isRatInMouth)
			{
				_GC.failStage();
			}
		}
		Destroy(this.gameObject,2f);
	}
}

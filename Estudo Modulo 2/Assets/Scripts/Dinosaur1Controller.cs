using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur1Controller : MonoBehaviour {

	private 	ballController		_BC;
	private 	HammerController	_HC;
	private 	playerController	_PC;
	private 	bool				canTakeHit;

	[Header("STATS PLAYER")]
	public		float 			HP;
	public		float 			defesa;
	private Rigidbody2D rbDinosaur1;
	public float Dinosaur1velocity;
	public float Dinosaur1TimeToTurn;
	public GameObject prefabEffectMorte;

	// Use this for initialization
	void Start () {
		rbDinosaur1 = GetComponent<Rigidbody2D> ();
		StartCoroutine ("AIMovement");
		canTakeHit = true;
	}
	
	/*// Update is called once per frame
	void Update () {
		
	}*/

	void OnTriggerEnter2D(Collider2D col)
	{
		switch (col.tag) 
		{
		case "projetilPlayer":
			print ("Enemy1: Bolinha");
			_BC = FindObjectOfType (typeof(ballController)) as ballController;
			takeHit(_BC.ataqueCalculado);
			Destroy (col.gameObject,0.01f);
			break;
		case "hammerHit":
			print ("Enemy1: Marreta");
			_HC = FindObjectOfType (typeof(HammerController)) as HammerController;
			takeHit(_HC.ataqueCalculado);
			break;
		case "headHit":
			print ("Enemy1: Cabeçada");
			_PC = FindObjectOfType (typeof(playerController)) as playerController;
			takeHit (_PC.ataqueBase);
			if (_PC.transform.position.x <= transform.position.x) {
				rbDinosaur1.AddForce (new Vector2 (130f, 100f));	
			} else {
				rbDinosaur1.AddForce (new Vector2 (-130f, -100f));
			}
			break;
		}
	}
	void takeHit(float damage)
	{
		if (canTakeHit) {
			canTakeHit	=	false;
			Instantiate (prefabEffectMorte, transform.localPosition, transform.localRotation);
			if (HP - damage > 0) {
				print ("Enemy1: " + HP + "-" + damage + "=" + (HP - damage));
				HP -= damage;
			} else {
				morrer ();
			}
			print ("Enemy1: HP Atual: " + HP);
			StartCoroutine ("ControlCanTakeHit");
		}
	}
	void morrer()
	{
		StopCoroutine ("AIMovement");
		Instantiate (prefabEffectMorte,transform.localPosition,transform.localRotation);
		Destroy (this.gameObject);
	}
	IEnumerator AIMovement(){
		rbDinosaur1.velocity = new Vector2 (Dinosaur1velocity, rbDinosaur1.velocity.y);
		yield return new WaitForSeconds (Dinosaur1TimeToTurn);
		Dinosaur1velocity *= -1;
		flip ();
		StartCoroutine ("AIMovement");
	}
	IEnumerator ControlCanTakeHit(){
		yield return new WaitForEndOfFrame ();
		print("Coroutina ControlCanTakeHit: True");
		canTakeHit = true;
	}
	void flip()
	{
		Vector3 myScale;
		myScale = this.transform.localScale;
		myScale.x *= -1;
		this.transform.localScale = myScale;
	}
}

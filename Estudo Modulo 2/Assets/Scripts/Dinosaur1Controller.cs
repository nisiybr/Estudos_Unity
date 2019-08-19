using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur1Controller : MonoBehaviour {

	private 	ballController		_BC;
	private 	HammerController	_HC;
	private 	playerController	_PC;
	private 	bool				canTakeHit;

	[Header("STATS ENEMY")]
	public		float 			HP;
	public		float 			defesa;	
	private Rigidbody2D rbDinosaur1;
	public float Dinosaur1velocity;
	public GameObject prefabEffectMorte;

	// Use this for initialization
	void Start () {
		rbDinosaur1 = GetComponent<Rigidbody2D> ();
		canTakeHit = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		switch (col.tag) 
		{
		case "hitCollision":
			print ("Enemy1: Pé na Cabeça");
			_PC = FindObjectOfType (typeof(playerController)) as playerController;
			takeHit (_PC.ataqueBase, col);	

			break;
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
	void takeHit(float damage, Collider2D col)
	{
		if (canTakeHit) {
			canTakeHit	=	false;
			Rigidbody2D rb = col.gameObject.GetComponentInParent<Rigidbody2D>(); 
			rb.velocity = new Vector2(rb.velocity.x,0f);
			rb.AddForce (new Vector2 (0f, 500f));
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
		Instantiate (prefabEffectMorte,transform.localPosition,transform.localRotation);
		Destroy (this.gameObject);
	}
	IEnumerator ControlCanTakeHit(){
		yield return new WaitForEndOfFrame ();
		print("Coroutina ControlCanTakeHit: True");
		canTakeHit = true;
	}

}

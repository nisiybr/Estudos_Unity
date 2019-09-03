using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

	public GameObject explosaoPrefab;

	private Rigidbody2D slimeRb;
	private Vector2 direcaoMovimento;
	public float velocidadeMovimento;
	public float tempoMinMovimento;
	public float tempoMinEspera;
	public float tempoMaxMovimento;
	public float tempoMaxEspera;
	// Use this for initialization
	void Start () {
		slimeRb = GetComponent<Rigidbody2D >();
		StartCoroutine("moverSlime");
	}
	
	// Update is called once per frame
	void Update () {
		slimeRb.velocity = direcaoMovimento * velocidadeMovimento;
	}

	private void OnTriggerEnter2D(Collider2D col) {
		switch(col.tag)
		{
			case "slash":
				Instantiate(explosaoPrefab,transform.position,transform.localRotation);
				Destroy(this.gameObject);
			break;
		}
	}


	IEnumerator moverSlime()
	{
		yield return new WaitForSeconds(Random.Range(tempoMinEspera,tempoMaxEspera));
		int x = Random.Range(-1,2);//sorteia -1, 0 , 2 
		int y = Random.Range(-1,2);//sorteia -1, 0 , 2 

		direcaoMovimento = new Vector2 (x,y);

		yield return new WaitForSeconds(Random.Range(tempoMinMovimento,tempoMaxMovimento));

		direcaoMovimento = new Vector2(0,0);
		StartCoroutine("moverSlime");
	}
}

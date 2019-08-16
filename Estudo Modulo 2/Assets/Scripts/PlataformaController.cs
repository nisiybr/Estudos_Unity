using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour {

	public Transform[] 	positions;
	public Transform	abovePlatform;
	public float velocidadePlataforma;



	private int			indexNextPosition;

	// Use this for initialization
	void Start () {
		transform.position = positions [0].position;
		indexNextPosition = 1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, positions[indexNextPosition].position, velocidadePlataforma * Time.deltaTime);
		if (transform.position == positions[indexNextPosition].position) {
			if (positions.Length-1 == indexNextPosition) {
				indexNextPosition = 0;
			} else {
				indexNextPosition += 1;
			}
				
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		switch (col.gameObject.tag) {
		case "player":
			if (col.transform.position.y > abovePlatform.position.y) {
				col.transform.parent = this.transform;
			}
			break;
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		switch (col.gameObject.tag) {
		case "player":
			col.transform.parent = null;
			break;
		}
	}
}

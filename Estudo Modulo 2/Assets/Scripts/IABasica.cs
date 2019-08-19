using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasica : MonoBehaviour {

	public Transform[] 	positions;
	public float velocidadeIA;
	public 		bool 			olhandoDireita;



	private int			indexNextPosition;

	// Use this for initialization
	void Start () {
		transform.position = positions [0].position;
		indexNextPosition = 1;
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, positions[indexNextPosition].position, velocidadeIA * Time.deltaTime);
		if (transform.position == positions[indexNextPosition].position) {
			if (positions.Length-1 == indexNextPosition) {
				indexNextPosition = 0;
			} else {
				indexNextPosition += 1;
			}
				
		}

		if (transform.position.x > positions[indexNextPosition].position.x && olhandoDireita) {
			flip ();
		 	olhandoDireita = false;
		}	
		if (transform.position.x < positions[indexNextPosition].position.x && !olhandoDireita) {
		 	flip ();
		 	olhandoDireita = true;
		}
	}
	void flip()
	{
		Vector3 myScale;
		myScale = this.transform.localScale;
		myScale.x *= -1;
		this.transform.localScale = myScale;
	}
}

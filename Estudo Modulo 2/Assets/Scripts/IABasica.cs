using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasica : MonoBehaviour {

	public Transform[] 	positions;
	public float velocidadeIA;



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

	}
}

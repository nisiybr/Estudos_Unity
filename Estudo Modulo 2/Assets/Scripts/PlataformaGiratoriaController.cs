using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaGiratoriaController : MonoBehaviour {

	public Transform rotula;
	public Transform plataforma;
	public Transform	abovePlatform;
	public int incremento;
	private float anguloRotula;

	// Use this for initialization
	void Start () {
		StartCoroutine ("GiraRotula");
	}
	IEnumerator GiraRotula()
	{
		yield return new WaitForSeconds (0.01f);
		anguloRotula = rotula.eulerAngles.z;
		anguloRotula += incremento;
		if (anguloRotula <= -360 || anguloRotula >= 360) {
			anguloRotula = 0;
		}
		rotula.eulerAngles = (new Vector3 (0f, 0f, anguloRotula));
		plataforma.eulerAngles = (new Vector3 (0f, 0f,0f));
		StartCoroutine ("GiraRotula");
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		print ("Colidiu");
		switch (col.gameObject.tag) {
		case "player":
			print ("player");
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

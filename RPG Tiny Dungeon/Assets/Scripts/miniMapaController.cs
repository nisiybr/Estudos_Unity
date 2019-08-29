using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapaController : MonoBehaviour {
	public Transform mapa;
	public int idSalaAtual;

	public GameObject[] salasGamePlay;
	public GameObject[] salas;
	// Use this for initialization
	void Start () {
		idSalaAtual = 0;

		foreach (GameObject s in salasGamePlay)
		{
			s.SetActive(false);
		}
		salasGamePlay[0].SetActive(true);

		
		foreach (GameObject s in salas)
		{
			s.SetActive(false);
		}
		salas[idSalaAtual].SetActive(true);
	}
	public void updateMiniMapa(int idSala)
	{


		salas[idSala].SetActive(true);
		Vector2 posMapa = new Vector2(salas[idSala].transform.localPosition.x,salas[idSala].transform.localPosition.y)*-1;
		mapa.localPosition = posMapa;

		salasGamePlay[idSala].SetActive(true);
		salasGamePlay[idSalaAtual].SetActive(false);
		idSalaAtual = idSala;

	}
}

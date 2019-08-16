using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temaInfo : MonoBehaviour {
	
	private temaScene temaScene;
	private soundController SC;

	[Header("Configuração do Tema")]
	public int 		idTema;
	public string 	nomeTema;
	public Color 	corTema;
	public bool requerDependencia;
	public int qtdEstrelasAnterior;

	[Header("Configuração das Estrelas")]
	public float notaMin1Estrela;
	public float notaMin2Estrela;
	

	[Header("Configuração do Botão")]
	public Text 		idTemaTxt;
	public GameObject[] estrela;

	// Use this for initialization
	void Start () {

		temaScene = FindObjectOfType(typeof(temaScene)) as temaScene;

		idTemaTxt.text = idTema.ToString(); 
	
		estrelas();

		SC = FindObjectOfType (typeof(soundController)) as soundController;
	}
	
	public void selecionarTema()
	{
		SC.playButton ();
 		temaScene.nomeTemaTxt.text = nomeTema.ToString();
		temaScene.nomeTemaTxt.color = corTema;
		PlayerPrefs.SetInt("idTema",idTema);
		PlayerPrefs.SetString("nomeTema",nomeTema);
		PlayerPrefs.SetFloat("notaMin1Estrela",notaMin1Estrela);
		PlayerPrefs.SetFloat("notaMin2Estrela",notaMin2Estrela);
		temaScene.btnPlay.interactable = true;
	}

	public void estrelas()
	{
		
		foreach(GameObject s in estrela)
		{
			s.SetActive(false);
		}

		

		int nEstrelas = 0;


		if(PlayerPrefs.HasKey("estrelasLevel" + idTema.ToString()))
		{
				nEstrelas = PlayerPrefs.GetInt("estrelasLevel" + idTema.ToString());
		}

		

		for(int i = 0; i < nEstrelas; i++)
		{
			estrela[i].SetActive(true);
		}
		if (requerDependencia) {	
			if (idTema > 1) {
				int nEstrelaAnterior;
				int levelAnterior;
				levelAnterior = idTema - 1;
				if (PlayerPrefs.HasKey ("estrelasLevel" + levelAnterior.ToString ())) {
					nEstrelaAnterior = PlayerPrefs.GetInt ("estrelasLevel" + levelAnterior.ToString ());
				} else {
					nEstrelaAnterior = 0;
				}
				if (nEstrelaAnterior < qtdEstrelasAnterior) {
					this.gameObject.GetComponent<Button> ().interactable = false;
				} else {
					this.gameObject.GetComponent<Button> ().interactable = true;
				}


			}
		}
			
	}
}

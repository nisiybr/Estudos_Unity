using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleController : MonoBehaviour {

	public Text txtInstrucoes;

	private loadXMLFile loadXMLFile;
	
	// Use this for initialization
	void Start () {
		loadXMLFile = FindObjectOfType(typeof(loadXMLFile)) as loadXMLFile;

		txtInstrucoes.text = loadXMLFile.interface_titulo[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void alterarIdioma (string idioma){
		PlayerPrefs.SetString("idioma",idioma);
		loadXMLFile.loadXMLData();

		txtInstrucoes.text = loadXMLFile.interface_titulo[0];
	}
}

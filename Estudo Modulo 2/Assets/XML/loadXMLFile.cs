using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class loadXMLFile : MonoBehaviour {

	public string arquivoXML; //nome do arquivo a ser carregado
	public List<string>  interface_titulo;
	public List<string>  interface_loja;

	public string	idioma; //padrão é pt-br, setado na interface do Unity
	// Use this for initialization
	void Awake() {
		loadXMLData();
	}
	public void loadXMLData(){

		if(PlayerPrefs.GetString("idioma") != "")
		{
			idioma = PlayerPrefs.GetString("idioma");
		}
		
		interface_titulo.Clear();
		interface_loja.Clear();

		TextAsset xmlData = (TextAsset) Resources.Load(idioma + "/" + arquivoXML); //Aponta para o Arquivo XML que será lido
	
		XmlDocument xmlDocument = new XmlDocument(); // Construtor

		xmlDocument.LoadXml(xmlData.text);

		foreach (XmlNode node in xmlDocument["language"].ChildNodes)
		{
			string nodeName = node.Attributes["name"].Value;
			
			foreach (XmlNode n in node["textos"].ChildNodes)
			{
				switch(nodeName)
				{
					case "interface_titulo":
						interface_titulo.Add(n.InnerText);
					break;
					case "interface_loja":
						interface_loja.Add(n.InnerText);
					break;
				}
			}	

		}

	}
	
}

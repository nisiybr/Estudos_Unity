using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LetterToLetter : MonoBehaviour {
	public Text texto;
	public string frase;
	public string[] frases;
	// Use this for initialization
	void Start () {
		print(frase.Length);
		StartCoroutine ("incrementaLetra");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator incrementaLetra()
	{
		texto.text = "";
		for (int j = 0; j < frases.Length; j++) 
		{
			
			for (int i = 0; i < frases[j].Length; i++) 
			{
				texto.text += frases [j][i];
			//	if (frases [j][i] != ' ')
					yield return new WaitForSeconds (0.04f);
			//	else
			//		yield return new WaitForSeconds (0.45f);			
			}
			texto.text += "\n";
			yield return new WaitForSeconds (1.5f);
		}	
	}
}

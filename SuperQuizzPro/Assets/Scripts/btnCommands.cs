using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnCommands : MonoBehaviour {

	private soundController SC;
	private fade fade;
	void Start () {

		SC = FindObjectOfType (typeof(soundController)) as soundController;
		fade = FindObjectOfType (typeof(fade)) as fade;
	}

	// Use this for initialization
	public void loadScene(string scene)
	{	
		string mudarMusica = scene.Substring (0, 1);
		if (mudarMusica == "S") {
			SC.AudioMusic.clip = SC.Musics [0];
			SC.AudioMusic.Play ();	
		}

		StartCoroutine ("transicao", scene.Substring (1, scene.Length - 1));

	}

	public void reloadScene()
	{
		int idTema = PlayerPrefs.GetInt("idTema");
		if(idTema != 0 )
		{
			SceneManager.LoadScene(idTema.ToString());
		}
	}
	public void sair() //funciona para windows ou mobile
	{
		Application.Quit();
	}
	IEnumerator transicao(string nomeCena){
		fade.fadeIn ();
		yield return new WaitWhile(()  => fade.fume.color.a < 0.9f );
		SceneManager.LoadScene(nomeCena);
	}




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour {


	public void changeScene(string scene)
	{
		SceneManager.LoadScene (scene);
	}
	public void activeObject(GameObject objetoSelected)
	{
		objetoSelected.SetActive (true);
	}
	public void deactiveObject(GameObject objetoSelected)
	{
		objetoSelected.SetActive (false);
	}
	public void Quitgame()
	{
		Application.Quit();
	}
}

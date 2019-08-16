using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {

	public AudioSource 	AudioMusic, AudioFX;
	public AudioClip 	somAcerto, somErro, somBotao, som3Stars;
	public AudioClip[] 	Musics ;

	void Awake()
	{ 
		DontDestroyOnLoad (this.gameObject);

		
	}

	// Use this for initialization
	void Start () {
		carregarPreferencias ();
		AudioMusic.clip = Musics [0];
		AudioMusic.Play ();

	}

	public void playAcerto (){
		AudioFX.PlayOneShot (somAcerto);
	}
	public void playErro (){
		AudioFX.PlayOneShot (somErro);
	}
	public void playButton(){
		AudioFX.PlayOneShot (somBotao);
	}
	public void playVinheta(){
		AudioFX.PlayOneShot (som3Stars);
	}
	public void carregarPreferencias()
	{

		if (PlayerPrefs.GetInt ("valoresDefault") == 0) {
			PlayerPrefs.SetInt ("valoresDefault", 1);
			PlayerPrefs.SetInt ("onOffMusica", 1);
			PlayerPrefs.SetInt ("onOffEfeitos", 1);
			PlayerPrefs.SetFloat ("volumeMusica", 1);
			PlayerPrefs.SetFloat ("volumeEfeitos", 1);
			print ("teste");
		}
		int onOffMusica = PlayerPrefs.GetInt ("onOffMusica");
		int onOffEfeitos = PlayerPrefs.GetInt ("onOffEfeitos");
		float volumeMusica = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		bool tocarMusica = false;
		bool tocarEfeitos = false;

		if (onOffMusica == 1) {
			tocarMusica = true;
		}
		if (onOffEfeitos == 1) {
			tocarEfeitos = true;
		}

		AudioMusic.mute = !tocarMusica;
		AudioFX.mute = !tocarEfeitos;

		AudioMusic.volume = volumeMusica;
		AudioFX.volume = volumeEfeitos;


	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioSource SourceMusic;
	public AudioSource SourceFX;

	public AudioClip mTitle;
	public AudioClip mStart;
	public AudioClip mGrass;
	public AudioClip mWater;


	// Use this for initialization
	void Start () {
		valoresIniciais ();
	}

	void valoresIniciais(){
		if (!PlayerPrefs.HasKey("valoresIniciais")) {
			PlayerPrefs.SetInt("valoresIniciais",1);
			PlayerPrefs.SetFloat ("volumeMusica", 1f);
			PlayerPrefs.SetFloat ("volumeFX", 1f);
		}

		SourceMusic.volume = PlayerPrefs.GetFloat ("volumeMusica");
		SourceFX.volume = PlayerPrefs.GetFloat ("volumeFX");

		SourceMusic.clip = mTitle;
		SourceMusic.Play ();
		SourceMusic.loop = true;
	}
	public IEnumerator changeMusic (AudioClip clip)
	{
		print ("Aloha");
		float volumeAtual = SourceMusic.volume;
		for (float v = volumeAtual; v > 0; v -= 0.015f) {
			SourceMusic.volume = v;
			yield return new WaitForEndOfFrame ();
		}

		SourceMusic.clip = clip;
		SourceMusic.loop = true;
		SourceMusic.Play ();


		for (float v = 0; v < volumeAtual; v += 0.015f) {
			SourceMusic.volume = v;
			yield return new WaitForEndOfFrame ();
		}
	}
}

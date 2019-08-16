using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class options : MonoBehaviour {

	public GameObject PainelPrincipal;
	public GameObject PainelOptions;


	private soundController SC;

	public Toggle 	toggleMusic, toggleFX;
	public Slider	sliderMusic, sliderFX;

	void Start () {
		SC = FindObjectOfType (typeof(soundController)) as soundController;
		carregarPreferencias ();
		PainelPrincipal.SetActive (true);
		PainelOptions.SetActive (false);



	}
	// Update is called once per frame
	void Update () {
		
	}
	public void configuracoes(bool onOff)
	{
		SC.playButton ();
		PainelPrincipal.SetActive (!onOff);
		PainelOptions.SetActive (onOff);
	}

	public void resetScore()
	{

		int onOffMusica = PlayerPrefs.GetInt ("onOffMusica");
		int onOffEfeitos = PlayerPrefs.GetInt ("onOffEfeitos");
		float volumeMusica = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("valoresDefault", 1);
		PlayerPrefs.SetInt ("onOffMusica", onOffMusica);
		PlayerPrefs.SetInt ("onOffEfeitos", onOffEfeitos);
		PlayerPrefs.SetFloat ("volumeMusica", volumeMusica);
		PlayerPrefs.SetFloat ("volumeEfeitos", volumeEfeitos);

	}

	public void mutarMusica()
	{
		SC.AudioMusic.mute = !toggleMusic.isOn;
		if (toggleMusic.isOn == true) {
			PlayerPrefs.SetInt ("onOffMusica", 1);
		} else {
			PlayerPrefs.SetInt ("onOffMusica", 0);
		}
			
	}
	public void mutarEffects()
	{
		SC.AudioFX.mute = !toggleFX.isOn;
		if (toggleFX.isOn == true) {
			PlayerPrefs.SetInt ("onOffEfeitos", 1);
		} else {
			PlayerPrefs.SetInt ("onOffEfeitos", 0);
		}
	}
	public void volumeMusica()
	{
		SC.AudioMusic.volume = sliderMusic.value;
		PlayerPrefs.SetFloat ("volumeMusica", sliderMusic.value);
	}
	public void volumeEffects()
	{
		SC.AudioFX.volume = sliderFX.value;
		PlayerPrefs.SetFloat ("volumeEfeitos", sliderFX.value);

	}

	public void carregarPreferencias()
	{
		int onOffMusica = PlayerPrefs.GetInt ("onOffMusica");
		int onOffEfeitos = PlayerPrefs.GetInt ("onOffEfeitos");
		float volumeM = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		bool tocarMusica = false;
		bool tocarEfeitos = false;

		if (onOffMusica == 1) {
			tocarMusica = true;
		}
		if (onOffEfeitos == 1) {
			tocarEfeitos = true;
		}

		toggleMusic.isOn = tocarMusica;
		toggleFX.isOn = tocarEfeitos;

		sliderMusic.value = volumeM;
		sliderFX.value = volumeEfeitos;

	}


}

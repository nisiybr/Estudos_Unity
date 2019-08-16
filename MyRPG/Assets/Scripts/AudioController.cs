using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
	public Slider 		MusicVolumeSlider;
	public Toggle 		MusicVolumeToggle;
	public AudioSource	MusicAudioSource;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("MusicVolume")) {
			MusicAudioSource.volume = PlayerPrefs.GetFloat ("MusicVolume");
			MusicVolumeSlider.value = PlayerPrefs.GetFloat ("MusicVolume");
		} else {
			MusicAudioSource.volume = 1;
		}
		if (PlayerPrefs.HasKey ("MusicOn")) {
			if (PlayerPrefs.GetInt ("MusicOn") == 1) {
				MusicAudioSource.mute = false;
				MusicVolumeToggle.isOn = true;
			} else if(PlayerPrefs.GetInt ("MusicOn") == 0){
				MusicAudioSource.mute = true;
				MusicVolumeToggle.isOn = false;
			}				
		} else {
			MusicAudioSource.mute = false;
			MusicVolumeToggle.isOn = true;
		}								
	}
	
	// Update is called once per frame
	public void OnChangeMusicVolume () {
		MusicAudioSource.volume = MusicVolumeSlider.value;
	}
	public void OnToggleMusicPressed () {
		MusicAudioSource.mute = !MusicVolumeToggle.isOn;
	}
	public void saveOptions()
	{
		int MusicOn;

		if (MusicAudioSource.mute) {
			MusicOn = 0;
		} else {
			MusicOn = 1;
		}


		PlayerPrefs.SetFloat("MusicVolume",MusicAudioSource.volume);
		PlayerPrefs.SetInt("MusicOn",MusicOn);
	}

}

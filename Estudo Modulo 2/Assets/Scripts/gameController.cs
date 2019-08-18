using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	[Header("UI")]
	public Text extraLifeTxt;
	public Text coinsTxt;
	public Text scoreTxt;

	public int rings; 
	public int score; 

	// Use this for initialization
	void Start () {
		rings = 0;
	}
	public void coletarRings(int r, int s){
		rings += r;

		coinsTxt.text = rings.ToString();
		addScore(s);
	}
	public void addScore(int s){
		score += s;
		scoreTxt.text = score.ToString();
	}
	

}


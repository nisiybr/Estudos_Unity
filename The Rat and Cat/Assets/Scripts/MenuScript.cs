using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	
	public Text txtPressStart;
	public Color colorPressStartInitial;
	public Color colorPressStartFinal;	
	public bool isGoingOpaque;
	private TransitionController _TC;

// Use this for initialization
	void Start () {
		isGoingOpaque = true;
		StartCoroutine("piscaTxt");
		_TC = FindObjectOfType (typeof(TransitionController)) as TransitionController;
		_TC.cenaPara = "gamePlay";
	}
		
	IEnumerator piscaTxt()
	{
		if(isGoingOpaque)
		{
			for(float f = 1f;f > 0; f = f - 0.05f)
			{
				if(f < 0f)
				{
					f = 0f;
				}
				Color temp = txtPressStart.color;
				temp.a = f;
				txtPressStart.color = temp;
				yield return new WaitForSeconds(0.1f);
			}
		}
		else
		{
			for(float f = 0f;f < 1; f = f + 0.05f)
			{
				if(f > 1f)
				{
					f = 1f;
				}
				Color temp = txtPressStart.color;
				temp.a = f;
				txtPressStart.color = temp;
				yield return new WaitForSeconds(0.1f);
			}
		}
		isGoingOpaque = !isGoingOpaque;
		StartCoroutine("piscaTxt");
	}
}

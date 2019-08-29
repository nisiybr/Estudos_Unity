using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum gameState {gamePlay,levelPassed,levelFailed}
public class gameController : MonoBehaviour {

	public 		AudioSource[] 	audioSource;
	public		AudioClip[]		audioClips;
	public		int				CheeseAmount;
	public		Text			txtCheeseAmount;
	public		int				TimeLimit;
	public		int				timeCallCat;
	public		int				timeCommingCat;
	public		bool			spawnCat;
	public		int				Timer;
	public		Text			txtTimer;
	public		Text			txtTimerCanvas;
	public		Text			txtRank;
	public		GameObject		prefabCat;
	public		Transform		spawnCatLeft;
	public		Transform		spawnCatRight;
	public	 	bool			catAtLeft;
	public		bool			isPlayerAlive;
	public		bool			isRatInMouth;
	public		bool			iscallCat;
	public		GameObject[]	shelfToBeBreaked;
	public		gameState		gamePhase;

	public		float			vigorPoints;
	public		float			actualVigorPoints;
	public		GameObject		barraVigor;
	public		Color			barraVigorColor;
	public		Color			barraVigorColorRed;

	public		GameObject		levelPassedPanel;
	public		GameObject		levelFailedPanel;
	public		float			velocityPanel;
	private 	TransitionControllerGamePlay _TC;

	
	// Use this for initialization
	void Start () {
		CheeseAmount = 0;
		Timer = TimeLimit;
		catAtLeft = true;
		iscallCat = true;
		isPlayerAlive = true;
		isRatInMouth = false;
		updateUI();
		StartCoroutine("updateTimer");
		StartCoroutine("callCat");
		_TC = FindObjectOfType (typeof(TransitionControllerGamePlay)) as TransitionControllerGamePlay;

	}
	
	// Update is called once per frame
	void Update () {
		updateUI();
		if(gamePhase == gameState.levelPassed)
		{
			levelPassedPanel.transform.localPosition = Vector3.MoveTowards(levelPassedPanel.transform.localPosition,new Vector3(0,0,0),velocityPanel*Time.deltaTime);
		}	
		else if(gamePhase == gameState.levelFailed)
		{
			levelFailedPanel.transform.localPosition = Vector3.MoveTowards(levelFailedPanel.transform.localPosition,new Vector3(0,0,0),velocityPanel*Time.deltaTime);
		}

	}
	public void playOneShot(AudioSource source,AudioClip audio)
	{
		source.PlayOneShot(audio);
	}
	void updateUI()
	{
		float scale = actualVigorPoints/vigorPoints;
		barraVigor.transform.localScale = new Vector3(scale,barraVigor.transform.localScale.y,barraVigor.transform.localScale.z);
		if(actualVigorPoints <= 20f )
		{
			barraVigor.GetComponent<Image>().color = barraVigorColorRed;
		}
		else
		{
			barraVigor.GetComponent<Image>().color = barraVigorColor;
		}
		txtCheeseAmount.text = CheeseAmount.ToString() + "/100";
		txtTimer.text = Timer.ToString();
	}
	void updateCanvas()
	{
		txtTimerCanvas.text = "Your Time: " + Timer.ToString();
		txtRank.text = "Rank: " + calculateRank(Timer);		
	}
	public void loadScene(string sceneName)
	{
		_TC.cenaPara = sceneName;
		_TC.callFadeOut ();
	}
	IEnumerator callCat()
	{
		int rand;
		rand = Random.Range(1,100);
		print("Iniciando Coroutina...");
		yield return new WaitForSeconds(timeCallCat);
		if(iscallCat)
		{
			print("Gato mia...");
			playOneShot(audioSource[0],audioClips[1]);
			yield return new WaitForSeconds(timeCommingCat);
			if(iscallCat)
			{
				print("Gato aparece...");
				GameObject temp = Instantiate(prefabCat); 
				if(rand <= 50)
				{			
					catAtLeft = true;
					temp.transform.position = spawnCatLeft.position;		
				}
				else 
				{
					catAtLeft = false;
					temp.transform.position = spawnCatRight.position;	
					Vector3 myscale = temp.transform.localScale;
					myscale.x *= -1;
					temp.transform.localScale = myscale;
				}
			}
		}
		
	}
	IEnumerator updateTimer()
	{
		yield return new WaitForSeconds(1);
		if (Timer>0)
		{
			Timer--;
		}
		else if (Timer<=0)
		{
			failStage();	
		}
				
		StartCoroutine("updateTimer");
		if(Timer <= 20)
		{
			txtTimer.color = Color.red;
		}
		
	}
	public void failStage()
	{
		gamePhase = gameState.levelFailed;
		StopCoroutine("updateTimer");
		levelFailedPanel.SetActive(true);		
		print("Level Failed");
		
	}
	public void completeStage()
	{
		StopCoroutine("updateTimer");
		levelPassedPanel.SetActive(true);
		updateCanvas();
		gamePhase = gameState.levelPassed;
		print("Level Passed");
	}
	public void breakShelf()
	{
		shelfToBeBreaked[0].SetActive(false);
		shelfToBeBreaked[1].SetActive(true);
	}
	private string calculateRank(int time)
	{	
		if(time >= 90)
		{
			return "S+";
		}
		else if(time >= 80)
		{
			return "S";
		}
		else if(time >= 70)
		{
			return "A";
		}
		else if(time >= 50)
		{
			return "B";
		}
		else if(time >= 40)
		{
			return "C";
		}
		else if(time >= 30)
		{
			return "D";
		}
		else 
		{
			return "E";
		}
	}
	private void movePanel(Transform panel, Vector3 destination, float velocityPanel)
	{
		panel.transform.localPosition =  Vector3.MoveTowards(panel.transform.localPosition, destination,velocityPanel * Time.deltaTime);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //biblioteca importada para utilização do canvas
using System; //biblioteca importada para utilização da Classe Array

public class DiceController : MonoBehaviour {

	//Painel de Log
	public GameObject contentBox;


	public GameObject prefabFirstLine;
	public Text roundFirstLine;
	public Text redScoreFirstLine;
	public Text yellowScoreFirstLine;

	public GameObject prefabOtherLines;
	public Text roundOtherLines;
	public GameObject PrefabRedScoreOtherLines;
	public Text redScoreOtherLines;
	public Text redLossOtherLines;
	public GameObject PrefabYellowScoreOtherLines;
	public Text yellowScoreOtherLines;
	public Text yellowLossOtherLines;

	//Painel de Dados
	public GameObject painelRolarDados;
	public Text roundNumber;
	public Text redLoses;
	public Text yellowLoses;
	public Color red;
	public Color yellow;
	public Color padrao;
	public Image[] DiceBackground;

	//Botão Jogar Dados
	public Button throwDicesButton;

	//Botão Nova Batalha
	public Button newBattleButton;

	public SpriteRenderer[] redDices; 
	public SpriteRenderer[] yellowDices;
	public Sprite[] redFaces;
	public Sprite[] yellowFaces;
	public InputField[] contadorExercito;
	public InputField[] contadorTanque;
	public InputField[] contadorResultado;
	public int[] redScore;
	public int[] yellowScore;
	public int resultadoConfrontoRed;
	public int resultadoConfrontoYellow;
	public int qtdDadosAtaque;
	public int qtdDadosDefesa;

	private int numeroSorteado;
	private bool isBattleBegan;

	private int roundCount;
	private bool resetRoundCount;



	// Use this for initialization
	void Start () {
		painelRolarDados.gameObject.SetActive (false);
		roundCount = 0;
		resetRoundCount = false;
		throwDicesButton.interactable = false;
		newBattleButton.interactable = false;
	}
	
	public void ThrowDices()
	{	
		//trava campos de texto após a primeira batalha
		if (!isBattleBegan) {
			isBattleBegan = true;
			foreach (InputField i in contadorExercito) {
				i.interactable = false;
			}
			foreach (InputField i in contadorTanque) {
				i.interactable = false;
			}
			foreach (InputField i in contadorResultado) {
				i.interactable = false;
			}
			newBattleButton.interactable = true;
		}

		//Manter Round Count
		if (resetRoundCount) {
			roundCount = 0;
			resetRoundCount = false;
		}


		//Primeiro round de cada batalha
		if (roundCount == 0) {
			roundFirstLine.text = "Round " + roundCount.ToString();
			redScoreFirstLine.text = contadorResultado [0].text;
			yellowScoreFirstLine.text = contadorResultado [1].text;
			Instantiate (prefabFirstLine, contentBox.transform);
		}
			
		roundCount++;
		roundNumber.text = "Round "+roundCount.ToString ();//Painel de Rolagem de Dados

		resultadoConfrontoRed = 0;
		resultadoConfrontoYellow = 0;


		//Quantos dados de ataque serão considerados
		if (int.Parse(contadorResultado [0].text) > 3) {
			qtdDadosAtaque = 3;
		}
		else if (int.Parse(contadorResultado [0].text) == 3) {
			qtdDadosAtaque = 2;
		}
		else if(int.Parse(contadorResultado [0].text) == 2){
			qtdDadosAtaque = 1;
		}
		else if(int.Parse(contadorResultado [0].text) <= 1){
			qtdDadosAtaque = 0;
		}

		//Quantos dados de defesa serão considerados
		if (int.Parse(contadorResultado [1].text) >= 3) {
			if (qtdDadosDefesa > qtdDadosAtaque) {
				qtdDadosDefesa = qtdDadosAtaque;
			} else {
				qtdDadosDefesa = 3;
			}
		}
		else if (int.Parse(contadorResultado [1].text) == 2) {
			if (qtdDadosDefesa > qtdDadosAtaque) {
				qtdDadosDefesa = qtdDadosAtaque;
			} else {
				qtdDadosDefesa = 2;
			}
		}
		else if(int.Parse(contadorResultado [1].text) == 1){
			if (qtdDadosDefesa > qtdDadosAtaque) {
				qtdDadosDefesa = qtdDadosAtaque;
			} else {
				qtdDadosDefesa = 1;
			}					
		}
		else if(int.Parse(contadorResultado [1].text) <= 0){
			if (qtdDadosDefesa > qtdDadosAtaque) {
				qtdDadosDefesa = qtdDadosAtaque;
			} else {
				qtdDadosDefesa = 0;
			}			
		}

	

		//carrega o array de resultados vermelhos de ataque
		int contador = 0;
		foreach (int i in redScore) {
			numeroSorteado = UnityEngine.Random.Range (1, 7);
			if (contador < qtdDadosAtaque) {
				redScore [contador] = numeroSorteado;
			} else {
				redScore [contador] = 0;
			}
			contador++;
		}


		//carrega o array de resultados amarelos de defesa
		contador = 0;
		foreach (int i in yellowScore) {
			numeroSorteado = UnityEngine.Random.Range (1, 7);
			if (contador < qtdDadosDefesa) {
				yellowScore [contador] = numeroSorteado;
			}
			else {
			yellowScore [contador] = 0;
			}
			contador++;
		}

		//Ordena Arrays de Forma Crescente
		Array.Sort (redScore);
		Array.Sort (yellowScore);

		//Ordena Arrays de Forma  Decrescente (é obrigatório ordenar por ordem crescente primeiro)
		Array.Reverse (redScore);
		Array.Reverse (yellowScore);



		//Calcula confrontos
		for(int i = 0; i < qtdDadosAtaque && i < qtdDadosDefesa; i++)
		{				
			if (redScore[i] > yellowScore[i]) {
				resultadoConfrontoYellow -= 1;
				DiceBackground [i].color = red;
			} else {
				resultadoConfrontoRed -= 1;
				DiceBackground [i].color = yellow;
			}
		}

		atualizaExercitos (0,resultadoConfrontoRed);
		atualizaExercitos (1,resultadoConfrontoYellow);


		//Atualiza Sprite Dados Vermelhos
		contador = 0;
		foreach (SpriteRenderer s in redDices) {			
			if (contador <= (qtdDadosAtaque - 1)) {
				s.sprite = redFaces [redScore [contador] - 1];
			} else {
				s.enabled = false;
			}
			contador++;
		}


		//Atualiza Sprite Dados Amarelos
		contador = 0;
		foreach (SpriteRenderer s in yellowDices) {		
		
			if (contador <= (qtdDadosDefesa - 1)) {
				s.sprite = yellowFaces [yellowScore [contador] - 1];
			} else {
				s.enabled = false;
			}
			contador++;
		}

		redLoses.text = "Ataque perdeu "+resultadoConfrontoRed.ToString()+ (resultadoConfrontoRed<-1?" exércitos":" exército");
		yellowLoses.text = "Ataque perdeu "+resultadoConfrontoYellow.ToString()+ (resultadoConfrontoYellow<-1?" exércitos":" exército");
							
		painelRolarDados.gameObject.SetActive (true);

		roundOtherLines.text = "Round " + roundCount.ToString(); //Scroll View de Log
		redScoreOtherLines.text = contadorResultado [0].text; 
		yellowScoreOtherLines.text = contadorResultado [1].text; 
		redLossOtherLines.text = resultadoConfrontoRed.ToString();
		yellowLossOtherLines.text = resultadoConfrontoYellow.ToString();
		GameObject temp = Instantiate (prefabOtherLines, contentBox.transform);
		Instantiate (PrefabRedScoreOtherLines, temp.transform);
		Instantiate (PrefabYellowScoreOtherLines, temp.transform);

	}
	public void newBattle()
	{
		resetRoundCount = true;

		foreach (Transform child in contentBox.transform) {
			GameObject.Destroy(child.gameObject);
		}

		isBattleBegan = false;
		foreach (InputField i in contadorExercito) {
			i.text = "0";
			i.interactable = true;

		}
		foreach (InputField i in contadorTanque) {
			i.text = "0";
			i.interactable = true;

		}
		foreach (InputField i in contadorResultado) {
			i.text = "0";
		}


		foreach (SpriteRenderer s in redDices) {						
			s.enabled = true;
		}
		foreach (SpriteRenderer s in yellowDices) {						
			s.enabled = true;
		}
			
		controlaThrowDicesButton ();
		newBattleButton.interactable = false;
	}
	public void calcular(int team)	//0 - Red  e 1 - Yellow
	{
		int qtdExercito = int.Parse(contadorExercito[team].text);
		int qtdTanque = int.Parse(contadorTanque[team].text);
		int resultado = qtdExercito + (qtdTanque*10);
		contadorResultado [team].text = resultado.ToString ();
	}
	public void controlaThrowDicesButton()
	{
		if (int.Parse (contadorResultado [0].text) > 1 && int.Parse (contadorResultado [1].text) > 0) {

			throwDicesButton.interactable = true;
		} else {
			throwDicesButton.interactable = false;
		}

	}
	public void atualizaExercitos(int team, int baixas) 	//0 - Red  e 1 - Yellow
	{
		int qtdExercito = int.Parse (contadorExercito [team].text);
		int qtdTanque = int.Parse (contadorTanque [team].text);


		qtdExercito = qtdExercito + baixas; //baixas já está negativo, então soma-se
		if (qtdExercito < 0) {
			qtdTanque = qtdTanque - 1;
			qtdExercito = qtdExercito + 10; 
		}


		contadorExercito[team].text = qtdExercito.ToString ();
		contadorTanque[team].text = qtdTanque.ToString ();
		calcular (team);
	}
	public void OkButton()
	{
		painelRolarDados.SetActive (false);
		foreach (Image i in DiceBackground) {
			i.color = padrao;
		}
	}

}

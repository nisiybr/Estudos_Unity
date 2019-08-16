using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class modoJogo1 : MonoBehaviour {

	[Header("Configuração dos Textos")]
	public 	Text	temaTxt;
	public 	Text	perguntaTxt;
	public 	Image	perguntaIMG;
	public 	Text	infoTxt;
	public	Image	alternativaAImg;
	public	Image	alternativaBImg;
	public	Image	alternativaCImg;
	public	Image	alternativaDImg;

	[Header("Textos Alternativas")]
	public Text altATxt;
	public Text altBTxt;
	public Text altCTxt;
	public Text altDTxt;


	[Header("Configuração das Barras")]
	public	GameObject	barraProgresso;
	public	GameObject	barraTempo;

	[Header("Configuração dos Botões")]
	public	Color	corAcerto;
	public	Color	corErro;
	public 	Button[] botoes;


	[Header("Configuração das Perguntas")]
	public 	string[] 	perguntas;
	public 	string[] 	correta;
	public 	int 		idResponder;
	public List<int>	listaPerguntas; 
	public 	int 		qtdPerguntas;
	public	Sprite[] 	perguntasImg;

	[Header("Configuração das Alternativas")]
	public string [] 	alternativasA;
	public Sprite[] 	alternativaAimg;
	public string [] 	alternativasB;
	public Sprite[] 	alternativaBimg;
	public string [] 	alternativasC;
	public Sprite[] 	alternativaCimg;
	public string [] 	alternativasD;
	public Sprite[] 	alternativaDimg;

	[Header("Configuração do Modo de Jogo")]
	public bool perguntasComImagem;
	public bool utilizarAlternativas;
	public bool utilizarAlternativasComImagem;
	public bool perguntasAleatorias;
	public bool jogarComTempo ;
	public float tempoResposta;
	public float tempo;
	public bool mostrarCorreta;
	public int qtdPiscar;

	[Header("Configuração das Notas")]
	public int 		qtdPerguntasRespondidas;
	public int 		totalPerguntas;
	public int 		qtdAcertos;
	public int 		qtdErros;
	public float	notaMin1Estrela,notaMin2Estrela;
	public int		nEstrelas;
	
	[Header("Configuração dos Painéis")]
	public GameObject[] paineis;
	public Text 		notaFinalTxt;
	public float		notaFinal;
	public Text			mensagens1Txt;
	public Text			mensagens2Txt;
	public GameObject[]	estrela;

	[Header("Configuração das Mensagens")]
	public string[] mensagens1;
	public string[] mensagens2;

	private int idBtnCorreto;
	private int idTema;
	private string nomeTema;
	private bool exibindoCorreta;
	private soundController soundController;

	// Use this for initialization
	void Start () {

		soundController = FindObjectOfType (typeof(soundController)) as soundController;

		idTema 				= PlayerPrefs.GetInt("idTema");	
		notaMin1Estrela 	= PlayerPrefs.GetFloat("notaMin1Estrela");	
		notaMin2Estrela 	= PlayerPrefs.GetFloat("notaMin2Estrela");	
		nomeTema 			= PlayerPrefs.GetString("nomeTema");


		temaTxt.text = nomeTema;
		paineis[0].SetActive(true);
		paineis[1].SetActive(false);

		idResponder = 0;
		montarListaPerguntas();
		qtdPerguntasRespondidas = 0;
		totalPerguntas = perguntas.Length;
		qtdAcertos = 0;
		qtdErros = 0;
		atualizaInfo();
		
		if(!jogarComTempo)
		{
			barraTempo.SetActive(false);
			
		}
		else	
		{
			barraTempo.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void montarListaPerguntas()
	{
		if (perguntasAleatorias == true) 
		{
			bool	addPergunta = true;

			if (qtdPerguntas > perguntas.Length) 
			{
				qtdPerguntas = perguntas.Length;
			}

			while (listaPerguntas.Count < qtdPerguntas) 
			{
				addPergunta = true;
				int rand = Random.Range (0, perguntas.Length);

				foreach (int Idp in listaPerguntas) 
				{
					if (rand == Idp) 
					{
						addPergunta = false;
					}
				}

				if (addPergunta == true) 
				{
					listaPerguntas.Add (rand);
				}
			}
		
		} 
		else 
		{
			for (int i = 0; i < perguntas.Length; i++) 
			{
				listaPerguntas.Add (i);
			}
		}

		if (perguntasComImagem)
		{
			perguntaIMG.sprite = perguntasImg [listaPerguntas [idResponder]];
		} 
		else 
		{
			perguntaTxt.text = perguntas[listaPerguntas[idResponder]];
		}

			

		if (utilizarAlternativas) {
			altATxt.text = alternativasA [listaPerguntas [idResponder]];
			altBTxt.text = alternativasB [listaPerguntas [idResponder]];
			altCTxt.text = alternativasC [listaPerguntas [idResponder]];
			altDTxt.text = alternativasD [listaPerguntas [idResponder]];
		} else if (utilizarAlternativasComImagem) 
		{
			alternativaAImg.sprite = alternativaAimg [listaPerguntas [idResponder]];
			alternativaBImg.sprite = alternativaBimg [listaPerguntas [idResponder]];
			alternativaCImg.sprite = alternativaCimg [listaPerguntas [idResponder]];
			alternativaDImg.sprite = alternativaDimg [listaPerguntas [idResponder]];
		}


		if(jogarComTempo)
		{			
			StopCoroutine("contaTempo");
			tempo = tempoResposta;
			atualizaTempo();
			StartCoroutine("contaTempo");
		}
	}	

	public void responder(string alternativa)
	{
		if (exibindoCorreta == true) {
			return;
		}
		
		StopCoroutine("contaTempo");
		
		if(correta[listaPerguntas[idResponder]] == alternativa)
		{
			print("Correto!!!");
			soundController.playAcerto ();
			qtdAcertos++;
			//botoes[0].colors.normalColor = corAcerto;
		}		
		else
		{
			print("Errou!!!");
			soundController.playErro ();
			qtdErros++;
			//botoes[1].colors.normalColor = corErro;
		}
		qtdPerguntasRespondidas++;

		switch (correta [listaPerguntas[idResponder]]) 
		{
			case "A":
				idBtnCorreto = 0;
				break;
		
			case "B":
				idBtnCorreto = 1;
				break;		

			case "C":
				idBtnCorreto = 2;
				break;

			case "D":
				idBtnCorreto = 3;
				break;
		}

		if (mostrarCorreta == true) //verifica se no Settings está marcado para identificar ao jogador qual a resposta correta, após a jogada.
		{
			foreach (Button b in botoes) {
				b.image.color = corErro;
			}
			exibindoCorreta = true;
			botoes [idBtnCorreto].image.color = corAcerto;
			StartCoroutine ("mostrarAlternativaCorreta");
		} 
		else 
		{
			avancarPergunta();
		}


	}

	public void avancarPergunta()
	{

		EventSystem.current.SetSelectedGameObject (null);

		idResponder++;			
		if(idResponder < perguntas.Length)
		{	
			montarListaPerguntas();			
		}
		else 
		{
			calularNotaFinal();
		}
		atualizaInfo();
					
	}
	public void atualizaInfo()
	{	
		Vector3 myScale = new Vector3 ((idResponder*1.0f)/(totalPerguntas*1.0f),barraProgresso.transform.localScale.y,barraProgresso.transform.localScale.z);
		barraProgresso.transform.localScale = myScale;
		infoTxt.text = "Respondendo pergunta "+(idResponder+1)+" de "+totalPerguntas+" perguntas";
	}	
	public void atualizaTempo()
	{	
		barraTempo.transform.localScale = new Vector3(tempo/tempoResposta,barraTempo.transform.localScale.y);
	}	
	IEnumerator contaTempo()
	{		
		yield return new WaitForSeconds(0.05f);
		tempo-= 0.05f;
		if(tempo <= 0)
		{
			tempo = 0;
			qtdErros++;
			qtdPerguntasRespondidas++;
			avancarPergunta();
		}
		else
		{
			StartCoroutine("contaTempo");
		}
		atualizaTempo();
	}	
	public void calularNotaFinal()
	{
		int qtdEstrelasRecorde;
		idResponder = perguntas.Length-1;

		foreach(GameObject s in estrela)
		{
			s.SetActive(false);
		}

		paineis[0].SetActive(false);
		paineis[1].SetActive(true);
		notaFinal = ((float)qtdAcertos/(float)qtdPerguntasRespondidas)*10f;
		notaFinal = Mathf.Round(notaFinal*100f)/100f;
		notaFinalTxt.text = notaFinal.ToString();

		


		if(notaFinal == 10)
		{
			nEstrelas = 3;
			soundController.playVinheta ();
		}
		else if(notaFinal >= notaMin2Estrela)
		{
			nEstrelas = 2;
		}
		else if(notaFinal >= notaMin1Estrela)
		{
			nEstrelas = 1;
		}	
		else nEstrelas = 0;

		mensagens1Txt.text = mensagens1[nEstrelas];
		mensagens2Txt.text = mensagens2[nEstrelas];

		for(int i = 0; i < nEstrelas; i++)
		{
			estrela[i].SetActive(true);
		}

	
		if(PlayerPrefs.HasKey("estrelasLevel" + idTema.ToString()))
		{
				qtdEstrelasRecorde = PlayerPrefs.GetInt("estrelasLevel" + idTema.ToString());
		}
		else
		{	
				qtdEstrelasRecorde = 0;
		}
		
		if(nEstrelas > qtdEstrelasRecorde)		
		{
			PlayerPrefs.SetInt("estrelasLevel" + idTema.ToString(),nEstrelas);
		}
	}

	IEnumerator mostrarAlternativaCorreta()
	{

		for (int i = 0; i < qtdPiscar; i++) {
			botoes [idBtnCorreto].image.color = corAcerto;
			yield return new WaitForSeconds (0.2f);
			botoes [idBtnCorreto].image.color = Color.white;
			yield return new WaitForSeconds (0.2f);
		}

		foreach (Button b in botoes) {
			b.image.color = Color.white;
		}
		exibindoCorreta = false;
		avancarPergunta();
	}
	
}

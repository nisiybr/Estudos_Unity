using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum direcaoNave
{
	esquerda, direita, cima, baixo
}


public enum gameState
{
	intro, gamePlay	
}

public enum tagBullets
{
	playerShot,enemyShot
}
public class GameController : MonoBehaviour {
	private TransitionController _TC;
	public	playerController _PC;

	[Header("Config. Limite Movimento")]
	public Transform LimiteSuperior;
	public Transform LimiteInferior;
	public Transform LimiteEsquerda;
	public Transform LimiteDireita;

	[Header("Config. Tiros")]
	public GameObject[] prefabBullet;

	[Header("Config. Explosão")]
	public GameObject prefabExplosao; 	

	[Header("Config. Player")]
	public GameObject playerPrefab;
	public Vector3 posPlayer;
	public bool isPlayerAlive;
	public int vidas;
	public Transform SpawnPlayer;
	public float tempoRespawn;
	public float tempoInvulnerabilidade;
	public Color corPlayerInvulneravel;
	public float delayPiscar;
	public int vidas_utilizadas = 0;
	public GameObject barraPontosVidaAtual;	


	[Header("Config. Camera")]
	public Camera mainCamera;
	public Transform LimiteCamEsquerda;
	public Transform LimiteCamDireito;
	public float velocidadeLateralCamera;
	public float velocidadeVerticalCamera;

	[Header("Config. Mapa")]
	public Transform Mapa;
	public float velocidadeMapa;
	public Transform posFinalFase;

	[Header("Config. Game")]
	public gameState currentState;
	public float tamanhoInicialNave;
	public float tamanhoOrigemNave;
	public Transform posicaoInicialNave;
	public Transform posicaoDecolagemNave;
	public float velocidadeDecolagem;
	public float velocidadeAtual;
	public bool isDecolar;
	public Color corInicialFumaca;
	public Color corFinalFumaca;
	public Text	txtVidas;
	public Text	txtScore;
	public int score;
	public int qtdBoss;

	[Header("Config. Audios")]
	public AudioSource FxAudioSource;
	public AudioSource FxAudioSource2;
	public AudioClip FxTiro;
	public AudioClip FxExplosao;
	public AudioClip FxHit;
	public AudioClip FxCoin;
	public AudioClip FxHealth;



	// Use this for initialization
	void Start () {
	
		_TC = FindObjectOfType (typeof(TransitionController)) as TransitionController;
		//_PC = FindObjectOfType(typeof(playerController)) as playerController;
		StartCoroutine("introFase");
		isPlayerAlive = true;
		txtVidas.text = "x" + vidas.ToString(); 
		txtScore.text = score.ToString(); 
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerAlive)
		{
			posPlayer = _PC.transform.position;
			limitarMovimentoPlayer();
		}
		if(Mapa.transform.position.y > posFinalFase.transform.position.y && currentState == gameState.gamePlay)
		{
			Mapa.transform.Translate(Vector3.down * velocidadeMapa * Time.deltaTime);
		}	
		if(isDecolar && currentState == gameState.intro)
		{
			_PC.transform.position = Vector3.MoveTowards(_PC.transform.position,posicaoDecolagemNave.transform.position,velocidadeAtual*Time.deltaTime);
			if(_PC.transform.position == posicaoDecolagemNave.position)
			{
				StartCoroutine("introSubir");
				currentState = gameState.gamePlay;
			}
			_PC.fumacaSr.color = Color.Lerp(corInicialFumaca,corFinalFumaca,15 * velocidadeAtual * Time.deltaTime);			
		}
	}
	void LateUpdate()
	{
		//Vector3 posicaoDestinoCamera = new Vector3(mainCamera.transform.position.x, posFinalCamera.transform.position.y,-10);
		//mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position,posicaoDestinoCamera,velocidadeVerticalCamera*Time.deltaTime);	
		//controlePosicaoCamera();
		atualizaBarraVidaPlayer();
		if(qtdBoss == 4)
		{	
			_TC.cena = "MissionSuccessful";
			_TC.CallFadeout ();
		}
	}
	void limitarMovimentoPlayer()
	{
		
		if(posPlayer.y > LimiteSuperior.position.y )
		{
			_PC.transform.position = new Vector3(posPlayer.x,LimiteSuperior.position.y,0);
		}
		else if(posPlayer.y < LimiteInferior.position.y )
		{
			_PC.transform.position = new Vector3(posPlayer.x,LimiteInferior.position.y,0);
		}

		if(posPlayer.x > LimiteDireita.position.x )
		{
			_PC.transform.position = new Vector3(LimiteDireita.position.x,posPlayer.y,0);
		}
		else if(posPlayer.x < LimiteEsquerda.position.x )
		{
			_PC.transform.position = new Vector3(LimiteEsquerda.position.x,posPlayer.y,0);
		}
		
	}

	void controlePosicaoCamera()
	{
		if(mainCamera.transform.position.x > LimiteCamEsquerda.position.x && mainCamera.transform.position.x < LimiteCamDireito.position.x)
		{
			 moverCamera();
		}
		else if (mainCamera.transform.position.x <=LimiteCamEsquerda.position.x && posPlayer.x > LimiteCamEsquerda.position.x)
		{
			moverCamera();
		}
		else if (mainCamera.transform.position.x >= LimiteCamDireito.position.x && posPlayer.x < LimiteCamDireito.position.x)
		{
			moverCamera();
		}
	}	
	void moverCamera()
	{
		Vector3 posicaoDestinoCamera = new Vector3(posPlayer.x,mainCamera.transform.position.y,mainCamera.transform.position.z);
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,posicaoDestinoCamera,velocidadeLateralCamera*Time.deltaTime);
	}
	public string retornaTag(tagBullets tag)
	{	
		string retorno = null;
		switch(tag)
		{
		case tagBullets.playerShot:
			retorno = "playerShot";
			break;	
		case tagBullets.enemyShot:
			retorno = "enemyShot";
			break;	
		}
		return retorno;
	}
	public void carregaScene(string sceneName)
	{	
		SceneManager.LoadScene(sceneName);
	}
	public void chamaAguardaRespawn()
	{
		vidas -= 1;
		vidas_utilizadas +=1;
		if(vidas >= 0)
		{
			txtVidas.text = "x" + vidas.ToString(); 
		}
		StartCoroutine("aguardaRespawn");
	}
	
	IEnumerator aguardaRespawn()
	{
		yield return new WaitForSeconds(tempoRespawn);
		Instantiate(playerPrefab,SpawnPlayer);
		yield return new WaitForEndOfFrame();
		_PC.StartCoroutine("invencivel");
		isPlayerAlive = true;
	}
	IEnumerator introFase()
	{
		yield return new WaitForSeconds(2f);
	
		_PC.fumacaSr.color = corInicialFumaca;
		_PC.transform.position = posicaoInicialNave.transform.position;
		_PC.transform.localScale = new Vector3(tamanhoInicialNave,tamanhoInicialNave,tamanhoInicialNave);
		yield return new WaitForSeconds(2);
		isDecolar = true;
		for(velocidadeAtual=0; velocidadeAtual < velocidadeDecolagem; velocidadeAtual+= 0.3f)
		{	
			yield return new WaitForSeconds(0.1f);
		}
	}
	IEnumerator Aguardar()
	{
		
		for(float s = tamanhoInicialNave; s < tamanhoOrigemNave ; s += 0.02f )
		{
			_PC.transform.localScale = new Vector3(s,s,s);
			_PC.sombra.transform.localPosition = Vector3.MoveTowards(_PC.sombra.transform.localPosition,_PC.posFinalSombra.localPosition,Time.deltaTime*5f);
			yield return new WaitForSeconds(0.1f);	
		}
		
	}
	IEnumerator introSubir()
	{
		
		for(float s = tamanhoInicialNave; s < tamanhoOrigemNave ; s += 0.02f )
		{
			_PC.transform.localScale = new Vector3(s,s,s);
			_PC.sombra.transform.localPosition = Vector3.MoveTowards(_PC.sombra.transform.localPosition,_PC.posFinalSombra.localPosition,Time.deltaTime*5f);
			yield return new WaitForSeconds(0.1f);	
		}
		
	}
	public void Pontuar(int pontos)
	{	
		score = score + pontos;
		txtScore.text = score.ToString();
	}
	public void atualizaBarraVidaPlayer()
	{
		Vector3 barraScale = barraPontosVidaAtual.GetComponent<RectTransform>().localScale;
		barraScale.x = _PC.pontosVidaAtual/_PC.pontosVidaDefault;
		barraPontosVidaAtual.GetComponent<RectTransform>().localScale = barraScale;
		
	}
	public void playOneShot(AudioClip audio)
	{
		FxAudioSource.PlayOneShot(audio);
	}
	public void playOneShot2(AudioClip audio)
	{
		FxAudioSource2.PlayOneShot(audio);
	}
}

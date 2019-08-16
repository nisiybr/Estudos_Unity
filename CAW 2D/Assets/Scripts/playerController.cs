using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	private TransitionController _TC;
	private GameController _GC;
	private Rigidbody2D rbPlayer;

	private SpriteRenderer playerSr;
	public SpriteRenderer fumacaSr;
	public float velocidadePlayer;
	public Transform arma;
	public float velocidadeTiro;
	public float delayTiro; //tempo entre um tiro e outro
	public bool waitToShot; //controla se o personagem pode atirar ou não
	public int idBullet;
	public tagBullets tagBullet;
	public GameObject sombra;
	public Transform posFinalSombra;
	public float pontosVidaDefault; //Quantidade de Vida Inicial
	public float pontosVidaAtual;	//Quantidade de Vida Atual
	

	// Use this for initialization
	void Start () {
		_TC = FindObjectOfType (typeof(TransitionController)) as TransitionController;
		_GC = FindObjectOfType(typeof(GameController)) as GameController;
		_GC._PC = this;
		rbPlayer = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		if(_GC.vidas_utilizadas > 0)
		{
			sombra.transform.position = posFinalSombra.position;
		}

	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical"); 
		bool turbo = Input.GetButton("Fire3");
		
		//Movimentação do Avião
		if(_GC.currentState == gameState.gamePlay)
		{
			if(turbo)
			{
				rbPlayer.velocity = new Vector2(horizontal*velocidadePlayer*1.5f,vertical*velocidadePlayer);
			} else {
				rbPlayer.velocity = new Vector2(horizontal*velocidadePlayer,vertical*velocidadePlayer);
			}
	
			//Comandos para atirar
			if(Input.GetButton("Fire1") && !waitToShot)
			{
				Atirar(_GC.prefabBullet[idBullet],_GC.retornaTag(tagBullet)); 
			}
		
		}
	}
	void Atirar(GameObject prefab,string tag)
	{
		waitToShot = true;
		_GC.playOneShot(_GC.FxTiro);
		StartCoroutine("controlWaitToShot");
		GameObject temp = Instantiate(prefab);
		temp.transform.tag = tag;
		temp.transform.position = arma.position;
		temp.GetComponent<Rigidbody2D>().velocity = new Vector2 (0,velocidadeTiro);
		Destroy(temp.gameObject,5);		
	}
	IEnumerator controlWaitToShot()
	{
		yield return new WaitForSeconds(delayTiro);
		waitToShot = false;
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		switch(col.gameObject.tag)
		{
			case "Enemy":
			pontosVidaAtual = 0;
			explodir();
			break;
			case "Boss":
			pontosVidaAtual = 0;
			explodir();
			break;
			case "enemyShot":
			tomarDano(col.gameObject.GetComponent<idBullet>().dano);
			print("Deu Dano " + col.gameObject.GetComponent<idBullet>().dano);			
			Destroy(col.gameObject);
			print("Destruir");
			break;		
			case "coletavel":
			switch(col.GetComponent<idColetavel>().nomeItem)
				{
					case "CaixaBomba":
						print("Pegou Caixa Bomba"); 
					break;	
					case "CaixaVida":
						_GC.playOneShot2(_GC.FxHealth);
						if(pontosVidaAtual + 10 > pontosVidaDefault)
						{
							pontosVidaAtual = pontosVidaDefault;
						}
						else
						{
							pontosVidaAtual += 10;
						}
					break;
					case "CaixaMoeda":
						{
							_GC.playOneShot2(_GC.FxCoin);
							_GC.Pontuar(1000);
							print("Pegou Caixa Moeda");
						}
					break;
				}
			Destroy(col.gameObject);
			break;	
		}
	}
	void explodir()
	{
		_GC.isPlayerAlive = false;
		_GC.playOneShot(_GC.FxExplosao);
		GameObject temp = Instantiate(_GC.prefabExplosao,transform.position,transform.rotation);	
		Destroy(temp.gameObject,0.5f);
		Destroy(this.gameObject);
		if(_GC.vidas >0)
		{	
			_GC.chamaAguardaRespawn();
		}
		else
		{
			_TC.cena = "gameOver";
			_TC.CallFadeout ();
		}
	}
	IEnumerator invencivel()
	{
		StartCoroutine("piscarPlayer");
		Collider2D col = GetComponent<Collider2D>();
		col.enabled = false;
		Color corOriginal = playerSr.color;
		playerSr.color = _GC.corPlayerInvulneravel;
		yield return new WaitForSeconds(_GC.tempoInvulnerabilidade);
		playerSr.color = corOriginal;
		col.enabled = true;	
		StopCoroutine("piscarPlayer");
		playerSr.enabled = true;
		fumacaSr.enabled = true;
	}
	IEnumerator piscarPlayer()
	{
		yield return new WaitForSeconds(_GC.delayPiscar);
		playerSr.enabled = !playerSr.enabled;
		fumacaSr.enabled = !fumacaSr.enabled;
		StartCoroutine("piscarPlayer");
	}

	void tomarDano(int dano)
	{
		_GC.playOneShot2(_GC.FxHit);
		if(pontosVidaAtual - dano < 0)
		{
			pontosVidaAtual = 0f;
		}
		else
		{
			pontosVidaAtual -= dano;
		}
	
		if(pontosVidaAtual<=0)
		{
			explodir();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy2 : MonoBehaviour {

	public GameController _GC;
	
	public direcaoNave direcao;
	public float velocidadeMovimento; //velocidade de movimento do inimigo
	public float pontoInicialCurva; //ponto x ou y onde a curva é iniciada
	public float grausCurva; //quantidade em graus que o inimigo ira fazer a curva
	private bool isCurva;	//booleano que define se o inimigo já pode fazer a curva
	public float incrementar; // quantidade de graus a ser incrementado por loop do update.
	private float incrementado; // quantidade de graus que já está incrementado
	private float rotacaoZ; //rotacao a ser setada no inimigo naquele loop do update
	public GameObject[] Loot;	
	public int pontos; 


	public Transform arma;	//tranform da arma
	public float velocidadeTiro;//velocidade do tiro do inimigo
	public float delayTiro; // quantidade de tempo que o inimigo vai demorar para atirar apos entrar na cena
	public int idBullet; //id da bala utilizada
	public tagBullets tagBullet; //tag em que a bala sera alocada

	

	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(GameController)) as GameController;
		rotacaoZ = transform.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		if(direcao == direcaoNave.cima)
		{
			if(transform.position.y <= pontoInicialCurva && !isCurva)
			{
				isCurva = true;
			}
		}
		else if(direcao == direcaoNave.baixo)
		{
			if(transform.position.y >= pontoInicialCurva && !isCurva)
			{
				isCurva = true;
			}
		}
		else if(direcao == direcaoNave.direita)
		{
			if(transform.position.x <= pontoInicialCurva && !isCurva)
			{
				isCurva = true;
			}
		}
		else if(direcao == direcaoNave.esquerda)
		{
			if(transform.position.x >= pontoInicialCurva && !isCurva)
			{
				isCurva = true;
			}
		}
		if(isCurva && incrementado < grausCurva)
		{
			rotacaoZ += incrementar;
			transform.rotation = Quaternion.Euler(0,0,rotacaoZ);
			
			if(incrementar <0)
			{
				incrementado += incrementar *-1;
			} else 
			{
				incrementado += incrementar;	
			}
		}

	
		transform.Translate(Vector3.down * velocidadeMovimento * Time.deltaTime);
	}
	public void Atirar(GameObject prefab,string tag)
	{
		GameObject temp = Instantiate(prefab,arma.position,transform.localRotation);
		temp.transform.tag = tag;
		temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * velocidadeTiro;
	}
	void OnBecameVisible()
	{	
		StartCoroutine("controleTiro");
	}
	IEnumerator controleTiro()
	{
		yield return new WaitForSeconds(delayTiro);
		Atirar(_GC.prefabBullet[idBullet],_GC.retornaTag(tagBullet));
		StartCoroutine("controleTiro");
	}	
	void explodir()
	{
		//	if(this.tag == "Boss")
			//{
		//		_GC.qtdBoss++;
		//	}
			_GC.Pontuar(pontos);
			GameObject temp = Instantiate(_GC.prefabExplosao,transform.position,transform.localRotation);	
			Destroy(temp.gameObject,0.5f);
			Destroy(this.gameObject);
	}
	void SpawnLoot()
	{
		int idItem = 0;
		int rand = Random.Range(0,100);
		if(rand < 30)
		{
			rand = Random.Range(0,100);
			if(rand > 85)
			{
				idItem = 2;
			}
			else if(rand > 50)
			{
				idItem = 1;
			}
			else 
			{
				idItem = 0;
			}
			GameObject temp =  Instantiate(Loot[idItem],transform.position,Loot[idItem].transform.localRotation);
			temp.transform.parent = _GC.Mapa;
		}
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		switch(col.gameObject.tag)
		{
			case "player":
			SpawnLoot();
			explodir();
			break;	
			case "playerShot":
			SpawnLoot();
			Destroy(col.gameObject);
			explodir();
			break;		
		}
	}
}

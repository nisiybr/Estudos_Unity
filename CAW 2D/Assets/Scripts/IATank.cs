using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATank : MonoBehaviour {
	private GameController _GC;
	public float delayTiro; // quantidade de tempo que o inimigo vai demorar para atirar apos entrar na cena
	public int idBullet; //id da bala utilizada
	public tagBullets tagBullet; //tag em que a bala sera alocada
	public Transform arma;	//tranform da arma
	public float velocidadeTiro;//velocidade do tiro do inimigo
	public GameObject[] Loot;	
	public int pontos;
	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnBecameVisible()
	{	
		StartCoroutine("controleTiro");
	}
	IEnumerator controleTiro()
	{
		yield return new WaitForSeconds(delayTiro);
		//if(transform.position.y >= _GC.posPlayer.y)
		//{	
			Atirar(_GC.prefabBullet[idBullet],_GC.retornaTag(tagBullet));
		//}
		StartCoroutine("controleTiro");
	}	
	void Atirar(GameObject prefab,string tag)
	{
		//Faz a mira do inimigo para o usuario
		arma.up = _GC.posPlayer - transform.position;
		GameObject temp = Instantiate(prefab,arma.position,arma.localRotation);
		temp.transform.tag = tag;
		temp.GetComponent<Rigidbody2D>().velocity = arma.up * velocidadeTiro;		
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
			GameObject temp =  Instantiate(Loot[idItem],transform.position,transform.localRotation);
			temp.transform.parent = _GC.Mapa;
		}
		
	}
	void explodir()
	{
			_GC.Pontuar(pontos);
			GameObject temp = Instantiate(_GC.prefabExplosao,transform.position,transform.localRotation);
			temp.transform.parent = _GC.Mapa;	
			Destroy(temp.gameObject,0.5f);
			Destroy(this.gameObject);
	}
void OnTriggerEnter2D(Collider2D col)
	{
		switch(col.gameObject.tag)
		{	
			case "playerShot":
			SpawnLoot();
			Destroy(col.gameObject);
			explodir();
			break;		
		}
	}
}

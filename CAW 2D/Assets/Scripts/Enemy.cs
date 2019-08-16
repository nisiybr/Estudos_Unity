using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	private GameController _GC;
	public GameObject[] Loot;
	public Transform arma;
	public float velocidadeTiro;
	public float[] delayTiro; //tempo entre um tiro e outro
	public int idBullet;
	public bool podeAtirar;
	public bool isAlive;

	public tagBullets tagBullet; //tag em que a bala sera alocada
	
	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(GameController)) as GameController;
		isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnBecameVisible()
	{	
		StartCoroutine("controlWaitToShot");
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

	void explodir()
	{
			isAlive = false;
			GameObject temp = Instantiate(_GC.prefabExplosao,transform.position,transform.localRotation);	
			Destroy(temp.gameObject,0.5f);
			Destroy(this.gameObject);
	}

	void SpawnLoot()
	{
		int idItem = 0;
		int rand = Random.Range(0,100);
		if(rand < 50)
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

	void Atirar(GameObject prefab,string tag)
	{


		//Faz a mira do inimigo para o usuario
		arma.right = _GC.posPlayer - transform.position;
		GameObject temp = Instantiate(prefab,arma.position,arma.localRotation);
		temp.transform.tag = tag;
		temp.GetComponent<Rigidbody2D>().velocity = arma.right * velocidadeTiro;		
	}
	IEnumerator controlWaitToShot()
	{
		yield return new WaitForSeconds(Random.Range(delayTiro[0],delayTiro[1]));
		
		//Tiro do inimigo
		//if(transform.position.y )
		//if(transform.position.y >= _GC.posPlayer.y)
		//{
			Atirar(_GC.prefabBullet[idBullet],_GC.retornaTag(tagBullet));
		//}
		StartCoroutine("controlWaitToShot");
	}
}

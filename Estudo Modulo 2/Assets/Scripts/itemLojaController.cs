using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemLojaController : MonoBehaviour {
	// Use this for initialization

	private lojaController _lC;
	private SpriteRenderer itemSr;
	public int idItem;



	void Start () {
		_lC = FindObjectOfType(typeof (lojaController)) as lojaController;
		itemSr = GetComponent<SpriteRenderer>();

		itemSr.sprite = _lC.spriteItem[idItem];

	}
	
	
	public void abrirLoja(){
		_lC.abrirLoja(idItem);
	}
}
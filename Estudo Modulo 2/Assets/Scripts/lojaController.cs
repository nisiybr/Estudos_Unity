using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lojaController : MonoBehaviour {
	// Use this for initialization

	public GameObject painelLoja;
	private playerController _PC;

	public Sprite[] spriteItem;
	public string[] nomeItem;
	public int[] precoItem;

	private int idItem;

	public Image icoItem;
	public Text txtNomeItem;
	public Text txtPreco;

	void Start () {
		_PC = FindObjectOfType(typeof(playerController)) as playerController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void abrirLoja(int idItemLoja)
	{
		idItem = idItemLoja;
		icoItem.sprite = spriteItem[idItem];
		txtNomeItem.text = nomeItem[idItem];
		txtPreco.text = precoItem[idItem].ToString();

		Time.timeScale = 0; //Pausa o jogo		
		painelLoja.SetActive(true);
	}

	public void fecharLoja()
	{
		Time.timeScale = 1; //Retorna ao jogo		
		painelLoja.SetActive(false);
	}

	public void comprarItem(){
		_PC.atualizarItens(idItem);
		fecharLoja();
	}

}
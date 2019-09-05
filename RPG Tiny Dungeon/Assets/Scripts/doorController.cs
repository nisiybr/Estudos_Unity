using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

	public Transform saida;
	public Transform posicaoCamera;
	public int idSala;

	public bool doorClosed;
	public bool doorLocked;

	public int idKey;
	
	private SpriteRenderer srPorta;

	public Sprite portaTrancada;
	public Sprite portaFechada;
	public Sprite portaAberta;

	private void Start() {
		srPorta = GetComponent<SpriteRenderer>();
		atualizaSprite();
	}
	public void atualizaSprite()
	{
		if(doorLocked)
		{
			srPorta.sprite = portaTrancada;
		}
		else if(doorClosed)
		{
			srPorta.sprite = portaFechada;
		}
		else
		{
			srPorta.sprite = portaAberta;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class temaScene : MonoBehaviour {

	public Text 			nomeTemaTxt;
	public Button			btnPlay;
	private soundController SC;

	[Header("Configuração da Paginação")]
	public GameObject[] 	btnPaginacao;
	public GameObject[] 	paineltemas;
	public int 				idPaginaAtual;	

	private fade fade;

	// Use this for initialization
	void Start () {

		SC = FindObjectOfType (typeof(soundController)) as soundController;
		fade = FindObjectOfType (typeof(fade)) as fade;

		//Seta página inicial = 0
		idPaginaAtual = 0;
		//Desabilita todos os painéis
		foreach (GameObject b in paineltemas) {
			b.SetActive (false);
		}
		//Desabilita os botões de navegação
		foreach (GameObject b in btnPaginacao) {
			b.SetActive (false);
		}
		//Começa só com a primeira página
		paineltemas [idPaginaAtual].SetActive (true);

		atualizaBotoesNavegacao ();


	}
	
	public void jogar()
	{
		SC.playButton ();
		SC.AudioMusic.clip = SC.Musics [1];
		SC.AudioMusic.Play ();

		int idTema = PlayerPrefs.GetInt("idTema");
		if(idTema != 0 )
		{
			StartCoroutine ("transicao", idTema.ToString ());
			//SceneManager.LoadScene(idTema.ToString());
		}
	}
	void atualizaBotoesNavegacao()
	{

		nomeTemaTxt.text = "Selecione o tema"; //Zera o tema apresentado na tela
		nomeTemaTxt.color = Color.white; //Volta a cor para branco
		btnPlay.interactable = false;//serve para desabilitar o botão assim que carrega a scene

		//Se houver mais de uma pagina, habilita os botões de 
		if (paineltemas.Length > 1) {			
			if(idPaginaAtual > 0)
				btnPaginacao[0].SetActive (true);
			if(idPaginaAtual == 0)
				btnPaginacao[0].SetActive (false);
			if(idPaginaAtual < (paineltemas.Length-1))	
				btnPaginacao[1].SetActive (true);
			if(idPaginaAtual == (paineltemas.Length-1))	
				btnPaginacao[1].SetActive (false);

		} else {
			foreach (GameObject b in btnPaginacao) {
				b.SetActive (false);
			}
		}			
	}
	public void btnPagina(int i)
	{
		SC.playButton ();

		paineltemas [idPaginaAtual].SetActive (false);
		paineltemas [idPaginaAtual+i].SetActive (true);
		idPaginaAtual = idPaginaAtual + i;

		atualizaBotoesNavegacao();
	}

	IEnumerator transicao(string nomeCena){
		fade.fadeIn ();
		yield return new WaitWhile(()  => fade.fume.color.a < 0.9f );
		SceneManager.LoadScene(nomeCena);
	}


}

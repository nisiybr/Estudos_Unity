using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionController : MonoBehaviour {

	public Transform mainCamera;
	private doorController door;
 	private PlayerController _PC;

	private miniMapaController _MM;
	public bool isDoor;

	public Animator animator;

	private ShadowController _SC;

	void Start() {
		_PC = FindObjectOfType(typeof(PlayerController))	 as PlayerController;
		_MM = FindObjectOfType(typeof(miniMapaController)) as miniMapaController;
		_SC = _PC.GetComponent<ShadowController>();
		mainCamera = Camera.main.transform;
	}
	public void startFade(doorController portaS) //passa como parâmetro o script da porta
	{
		door = portaS;	
		animator.SetTrigger("FadeOut");
	}
	public void onFadeComplete()
	{
		_PC.transform.position = door.saida.position;
		mainCamera.position = new Vector3(door.posicaoCamera.position.x,door.posicaoCamera.position.y,-10);
		_MM.updateMiniMapa(door.idSala);
		_SC.updateLightList();
		isDoor = false;
		animator.SetTrigger("FadeIn");

	}
}

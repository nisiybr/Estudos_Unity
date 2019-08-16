using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffSet : MonoBehaviour {

	public MeshRenderer mrMeshRenderer;
	public float velocidadeOffSet;

	// Update is called once per frame
	void Update()
	{
		mrMeshRenderer.materials [0].SetTextureOffset("_MainTex",new Vector2(mrMeshRenderer.materials [0].GetTextureOffset ("_MainTex").x + velocidadeOffSet,mrMeshRenderer.materials [0].GetTextureOffset ("_MainTex").y));

	}
}
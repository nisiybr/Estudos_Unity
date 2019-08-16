using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {
	
	private MeshRenderer mr;
	private Vector2 initialOffset;
	public float incremento;
	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer>();
		initialOffset = mr.material.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
		initialOffset = new Vector2(initialOffset.x+incremento,initialOffset.y);
		mr.material.SetTextureOffset("_MainTex",initialOffset);
	}
}

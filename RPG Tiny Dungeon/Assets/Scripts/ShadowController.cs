using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour {
	public GameObject shadowPrefab;
	public Transform shadowPosition;
	public FonteLuz[] fonteLuz;
	public List<Transform> shadows;

	public List<Transform> Lights;
	// Use this for initialization

	public float minScale;
	public float maxScale;

	public bool isStarted;

	void Start () {
		
	
	}
	private void OnEnable()
	{
		updateLightList ();
	}

	public void updateLightList()
	{
		foreach (Transform t in shadows)
		{
			Destroy(t.gameObject);				
		}

		shadows.Clear();
		Lights.Clear();


		fonteLuz = FindObjectsOfType(typeof(FonteLuz)) as FonteLuz[];

		for(int i=0; i < fonteLuz.Length;i++)
		{
			Lights.Add(fonteLuz[i].transform);
			GameObject tempShadow = Instantiate(shadowPrefab, shadowPosition.position, transform.localRotation, this.transform);
			shadows.Add(tempShadow.transform);
		}

	}
	// Update is called once per frame
	void LateUpdate () {
		for(int i = 0;i < fonteLuz.Length;i++)
		{
			Vector3	direcao = Lights[i].position - transform.position;
			shadows[i].up  = direcao * -1;

			float distancia = Vector3.Distance(Lights[i].position,transform.position);
			

			if(distancia < minScale)
			{
				distancia = minScale;
			}
			else if(distancia > maxScale)
			{
				distancia = maxScale;
			}

			shadows[i].localScale = new Vector3(shadows[i].localScale.x,distancia,shadows[i].localScale.z);
		}
	}
}

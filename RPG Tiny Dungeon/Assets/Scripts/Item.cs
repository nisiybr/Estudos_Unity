using UnityEngine;

[CreateAssetMenu(fileName = "new item",menuName = "Inventario/Item")]

public class Item : ScriptableObject {
	public int id = 0; 
	public string name = "Novo Item";
	public Sprite icon = null;

}

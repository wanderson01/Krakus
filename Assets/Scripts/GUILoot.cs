using UnityEngine;
using System.Collections;

public class GUILoot : MonoBehaviour {

	public int textSize;
	new private GUIText guiText;

	void Start () {
		guiText = GetComponent<GUIText> ();
	}
	
	void Update () {
		UpdateLootCount ();
	}

	void UpdateLootCount() {
		guiText.text = "Gold: " + GameData.Gold + " Iron: " + GameData.Iron + " Stone: " + GameData.Stone;
		guiText.fontSize = Mathf.Min (Screen.height, Screen.width) / textSize;
	}
}

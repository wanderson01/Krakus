using UnityEngine;
using System.Collections;

public class GUILoot : MonoBehaviour {

	public int textSize;

	void Update () {
		UpdateLootCount ();
	}

	void UpdateLootCount() {
		guiText.text = "Gold: " + GameData.currentGame.Gold + " Iron: " + GameData.currentGame.Iron + " Stone: " + GameData.currentGame.Stone;
		guiText.fontSize = Mathf.Min (Screen.height, Screen.width) / textSize;
	}
}

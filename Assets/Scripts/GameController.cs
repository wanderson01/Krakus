using UnityEngine;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour {

	void Awake () {
		print ("Game Controller");
		StartGame ();
	}

	public static void StartGame(){

		if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){
			SaveLoadGameData.Load ();
		}
	}

	public static void EndGame(){

		SaveLoadGameData.Save ();
		Application.LoadLevel (Application.loadedLevel);
	}
}

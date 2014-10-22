using UnityEngine;
using System.Collections;
using System.IO;
[System.Serializable]
public class GameControl : MonoBehaviour {

	void Awake () {

		StartGame ();
	}

	public static void StartGame(){

		if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){
			SaveLoadGameData.Load ();
		}
		else {
			StartNewGame();
		}
	}

	public static void StartNewGame(){

		GameData.currentGame.SpawnPoint = GameObject.FindWithTag ("Start").transform.position;
	}

	public static void GameOver(){

		SaveLoadGameData.Save ();
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public static void AddGold(int amount){
		
		GameData.currentGame.Gold += amount;
	}
	
	public static void AddIron(int amount){
		
		GameData.currentGame.Iron += amount;
	}
	
	public static void AddStone(int amount){
		
		GameData.currentGame.Stone += amount;
	}
	
	public static void AddGrimoire(int amount){
		
		GameData.currentGame.Grimoire += amount;
	}
}

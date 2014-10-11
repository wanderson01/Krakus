using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData {
		
	public static GameData currentGame;

	public static int Gold { get; private set;}
	public static int Iron { get; private set;}
	public static int Stone { get; private set;}
	public static int Grimoire { get; private set;} // Provisoriamente int.
	public static Vector3 SpawnPoint { get; set;}
	public static int spawnId { get; set;}

/*	public GameData(){

	}*/

	public static void AddGold(int amount){
		
		Gold += amount;
	}

	public static void AddIron(int amount){
		
		Iron += amount;
	}

	public static void AddStone(int amount){
		
		Stone += amount;
	}

	public static void AddGrimoire(int amount){
		
		Grimoire += amount;
	}
}

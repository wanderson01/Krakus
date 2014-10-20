using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadGameData {
	
//	public static List<GameData> savedGames = new List<GameData>();

	public static void Save(){

//		savedGames.Add (GameData.currentGame);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");

		GameData.currentGame.allObjectsId = GetAllObjectsId ();
		bf.Serialize (file, GameData.currentGame);
		file.Close();
	}

	public static void Load(){

		if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){
			
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);

			GameData.currentGame = (GameData)bf.Deserialize(file);

			foreach(GameObject obj in GetAllObjects()){

				if (!GameData.currentGame.allObjectsId.Contains(obj.GetInstanceID())){
					Debug.Log ("teste");
					GameObject.Destroy(obj);
				}
			}

		/*	List<GameObject> all = new List<GameObject>();
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof (GameObject)))
			{
				if (gos.Contains(obj))
				{
					continue;
				}
				gos.Add(obj);
				m_instanceMap[obj.GetInstanceID()] = obj;
			}*/

			file.Close();
		}
	}

	public static void Delete(){

		if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){

			File.Delete(Application.persistentDataPath + "/savedGames.gd");
		}
	}

	public static IList<int> GetAllObjectsId(){
		
		GameObject[] allLoots = GameObject.FindGameObjectsWithTag("Loot");
		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] allBreakable = GameObject.FindGameObjectsWithTag("Breakable");

		IList<int> allObjectsID = new List<int> ();
				
		for (int i = 0; i < allLoots.Length; i++){
			allObjectsID.Add (allLoots[i].GetInstanceID());
		}
		for (int i = 0; i < allEnemies.Length; i++){
			allObjectsID.Add (allEnemies[i].GetInstanceID());
		}
		for (int i = 0; i < allBreakable.Length; i++){
			allObjectsID.Add (allBreakable[i].GetInstanceID());
		}
		return allObjectsID;
	}

	public static IList<GameObject> GetAllObjects(){
		
		GameObject[] allLoots = GameObject.FindGameObjectsWithTag("Loot");
		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] allBreakable = GameObject.FindGameObjectsWithTag("Breakable");

		IList<GameObject> allObjects = new List<GameObject> ();
		
		for (int i = 0; i < allLoots.Length; i++){
			allObjects.Add (allLoots[i]);
		}
		for (int i = 0; i < allEnemies.Length; i++){
			allObjects.Add (allEnemies[i]);
		}
		for (int i = 0; i < allBreakable.Length; i++){
			allObjects.Add (allBreakable[i]);
		}
		return allObjects;
	}



/*	private static void GetData(){

		BaseStats playerStats = GameObject.FindWithTag ("Player").GetComponent<BaseStats> ();

		GameData.currentGame.Gold = GameControl.Gold;
		GameData.currentGame.Iron = GameControl.Iron;
		GameData.currentGame.Stone = GameControl.Stone;
		GameData.currentGame.Grimoire = GameControl.Grimoire;
		GameData.currentGame.SpawnId = GameControl.SpawnId;
		GameData.currentGame.SpawnPoint = GameControl.SpawnPoint;
		GameData.currentGame.player_maxHealth = playerStats.maxHealth;
		GameData.currentGame.player_currentHealth = playerStats.currentHealth;
		GameData.currentGame.player_attackSpeed = playerStats.attackSpeed;
		GameData.currentGame.player_movementSpeed = playerStats.movementSpeed;
		GameData.currentGame.player_damageEffect = playerStats.damageEffect;
	}
	
	private static void SetData(){

		BaseStats playerStats = GameObject.FindWithTag ("Player").GetComponent<BaseStats> ();

		GameControl.Gold = GameData.currentGame.Gold;
		GameControl.Iron = GameData.currentGame.Iron;
		GameControl.Stone = GameData.currentGame.Stone;
		GameControl.Grimoire = GameData.currentGame.Grimoire;
		GameControl.SpawnId = GameData.currentGame.SpawnId;
		GameControl.SpawnPoint = GameData.currentGame.SpawnPoint;
		playerStats.maxHealth = GameData.currentGame.player_maxHealth;
		playerStats.currentHealth = GameData.currentGame.player_currentHealth;
		playerStats.attackSpeed = GameData.currentGame.player_attackSpeed;
		playerStats.movementSpeed = GameData.currentGame.player_movementSpeed;
		playerStats.damageEffect = GameData.currentGame.player_damageEffect;
	}*/
}

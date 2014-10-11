using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadGameData {
	
	public static List<GameData> savedGames = new List<GameData>();

	public static void Save(){

		savedGames.Add (GameData.currentGame);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");

		bf.Serialize (file, SaveLoadGameData.savedGames);
		file.Close();
	}

	public static void Load(){

		if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){
			
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);

			SaveLoadGameData.savedGames = (List<GameData>)bf.Deserialize(file);
			file.Close();
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData{
	public static string saveFileName = Application.persistentDataPath + "/savedGames.gd";

	public static GameData currentGame = new GameData ();

	public int player_maxHealth{ get; set;}
	public int player_currentHealth{ get; set;}
	public float player_attackSpeed{ get; set;}
	public float player_movementSpeed{ get; set;}
	public BaseStats.DamageEffect player_damageEffect{ get; set;}
	public int Gold { get; set;}
	public int Iron { get; set;}
	public int Stone { get; set;}
	public int Grimoire { get; set;} // Provisoriamente int.
	public int SpawnId { get; set;}
	public IList<int> allObjectsId;

	private SerializeVector3 _SpawnPoint;
	public Vector3 SpawnPoint {
		get {
			if (_SpawnPoint == null) {
				return Vector3.zero;
			} else {
				return (Vector3)_SpawnPoint;
			}
		} 
		set {
			_SpawnPoint = (SerializeVector3)value;
		}
	}
}

[System.Serializable]
public class SerializeVector3 {
	
	private float x;
	private float y;
	private float z;
	
	public SerializeVector3(Vector3 vec3) {
		this.x = vec3.x;
		this.y = vec3.y;
		this.z = vec3.z;
	}
	
	public static implicit operator SerializeVector3(Vector3 vec3) {
		return new SerializeVector3(vec3);
	}
	public static explicit operator Vector3(SerializeVector3 serial_vec3) {
		return new Vector3(serial_vec3.x, serial_vec3.y, serial_vec3.z);
	}
}

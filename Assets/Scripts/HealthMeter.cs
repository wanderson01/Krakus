using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour {

	public GameObject player;
	public int playerHealth;
	public int lastHealth;


	void Start () {

		lastHealth = player.GetComponent<BaseCharacter> ().maxHealth;
	}
	
	void Update () {

		CheckHealthChange ();
	}

	void CheckHealthChange(){

		playerHealth = player.GetComponent<BaseCharacter> ().currentHealth;

		if (playerHealth != lastHealth){

			UpdateHealthMeter();
			lastHealth = playerHealth;
		}
	}

	void UpdateHealthMeter(){

		if (playerHealth == 5){
			foreach(Transform child in transform){
				child.GetComponent<GUITexture>().enabled = true;
			}
		}
		else if (playerHealth == 4){
			transform.FindChild("Head").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Left").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Arm Right").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Bottom").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Top").GetComponent<GUITexture>().enabled = true;
		}
		else if (playerHealth == 3){
			transform.FindChild("Head").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Left").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Right").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Bottom").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Top").GetComponent<GUITexture>().enabled = true;
		}
		else if (playerHealth == 2){
			transform.FindChild("Head").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Left").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Right").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Bottom").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Top").GetComponent<GUITexture>().enabled = false;
		}
		else if (playerHealth == 1){
			transform.FindChild("Head").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Left").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Arm Right").GetComponent<GUITexture>().enabled = false;
			transform.FindChild("Chest Bottom").GetComponent<GUITexture>().enabled = true;
			transform.FindChild("Chest Top").GetComponent<GUITexture>().enabled = false;
		}
		else if (playerHealth <= 0){
			foreach(Transform child in transform){
				child.GetComponent<GUITexture>().enabled = false;
			}
		}
	}
}

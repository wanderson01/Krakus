using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour {

	public GameObject player;
	public Sprite[] health;
	private int playerHealth;
	private int lastHealth;
	private int maxHealth;
	private SpriteRenderer spriteRenderer;

	void Start () {

		maxHealth = player.GetComponent<BaseStats> ().maxHealth;
		lastHealth = player.GetComponent<BaseStats> ().maxHealth;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	void Update () {

		CheckHealthChange ();
	}

	void CheckHealthChange(){

		playerHealth = player.GetComponent<BaseStats> ().currentHealth;

		if (playerHealth != lastHealth){

			UpdateHealthMeter();
			lastHealth = playerHealth;
		}
	}

	void UpdateHealthMeter(){

		if (playerHealth == maxHealth){
			spriteRenderer.sprite = health[0];
		}
		else if (playerHealth == maxHealth - 1){
			spriteRenderer.sprite = health[1];
		}
		else if (playerHealth == maxHealth - 2){
			spriteRenderer.sprite = health[2];
		}
		else if (playerHealth == maxHealth - 3){
			spriteRenderer.sprite = health[3];
		}
		else if (playerHealth == maxHealth - 4){
			spriteRenderer.sprite = health[4];
		}
		else if (playerHealth == maxHealth - 5){
			spriteRenderer.sprite = health[5];
		}
	}
}

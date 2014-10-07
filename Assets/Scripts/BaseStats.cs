using UnityEngine;
using System.Collections;

public class BaseStats : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public bool invulnerable = false;
	public float attackSpeed;
	public float movementSpeed;
	public int damage;
	public DamageEffect damageEffect;

	void Start () {
		currentHealth = maxHealth;
	}

	void Update () {
		CheckHealth ();
	}

	public enum DamageEffect{

		Physical,
		Fire,
		Ice
	}

	public void ReceiveDamage(int damage){
		
		if (currentHealth > 0 && !invulnerable) {
			currentHealth -= damage;
		}
	}
	
	void CheckHealth(){
		
		if (currentHealth <= 0){
			Die ();
		}
	}

	void Die(){

		if (this.gameObject.tag == "Player"){
			GameController.EndGame();
		}
		Destroy (this.gameObject);
	//	transform.FindChild ("Arthur").gameObject.SetActive (false);

	}
}

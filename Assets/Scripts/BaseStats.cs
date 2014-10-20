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
	private bool dead = false;

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
		
		if (currentHealth <= 0 && !dead){
			Die ();
		}
	}

	void Die(){

		dead = true;
		if (this.gameObject.tag == "Player"){
			GameControl.GameOver();
		}
		Destroy (this.gameObject);
	//	transform.FindChild ("Arthur").gameObject.SetActive (false);

	}
}

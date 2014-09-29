using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public float attackSpeed;
	public float movementSpeed;
	public int damage;
	public string damageEffect;

	void Start () {
		currentHealth = maxHealth;
	}

	void Update () {
		CheckHealth ();
	}

	public void ReceiveDamage(int damage){
		
		if (currentHealth > 0) {
			currentHealth -= damage;
		}
	}
	
	void CheckHealth(){
		
		if (currentHealth <= 0){
			Die ();
		}
	}

	void Die(){
		print ("Dead.");
		Destroy (this.gameObject);
	//	transform.FindChild ("Arthur").gameObject.SetActive (false);
	//	Application.LoadLevel ("GameOver");
	}
}

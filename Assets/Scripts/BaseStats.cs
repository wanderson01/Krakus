using UnityEngine;
using System.Collections;

public class BaseStats : MonoBehaviour {

	//public float damageCoolDownMS = 0f;
	public GameObject damageTimerBase;
	public GameObject []damageTimer = new GameObject[3];
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

	public DamageType getDamage() {
		DamageType ret = new DamageType();
		ret.damage = damage;
		ret.effect = damageEffect;
		return ret;
	}

	void Update () {
		CheckHealth ();
	}

	public enum DamageEffect{

		Physical = 0,
		Fire = 1,
		Ice = 2
	}

	bool damageEnabled(DamageEffect damage) {
		bool ret = false;
		
		if (!invulnerable) {
			ret = true;
			if (damageTimerBase != null) {
				int timerIndex = (int) damage;
				GameObject timerObject = damageTimer [timerIndex];
				if (timerObject != null) {
					ret = false;
					if (timerObject.GetComponent<CoolDownTimer>().isTimerEnded ()) {
						Destroy (timerObject);
						timerObject = null;
					}
				}
				if (timerObject == null) {
					damageTimer[timerIndex] = timerObject = Instantiate (damageTimerBase) as GameObject;
					timerObject.transform.parent = this.transform;
					timerObject.name = "Timer" + damage;
					ret = true;
				}
			}
		}

		return ret;
	}

	public void ReceiveDamage(int damage){
		ReceiveDamage(new DamageType(damage, DamageEffect.Physical));
	}

	public void ReceiveDamage(DamageType damageType){
		if (currentHealth > 0 && damageEnabled(damageType.effect)) {
			currentHealth -= damageType.damage;
		}
	}

	// Para ser chamado por Endless Pit e coisas semelhantes
	public void InstaDeath() {
		currentHealth = 0;
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

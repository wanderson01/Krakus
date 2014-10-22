using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int damage;
	public GameObject wielder;
	public BaseStats wielderBase;

	void Start() {
		if (wielder != null) {
			wielderBase = wielder.GetComponent<BaseStats>();
		}
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Enemy"){
			print (col);
			col.GetComponent<BaseStats>().ReceiveDamage(new DamageType(damage, wielderBase.damageEffect));
		}
	}
}

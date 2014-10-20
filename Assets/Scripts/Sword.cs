using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int damage;

	void OnTriggerEnter(Collider col){

		if (col.tag == "Enemy"){
			col.GetComponent<BaseStats>().ReceiveDamage(damage);
		}
	}
}

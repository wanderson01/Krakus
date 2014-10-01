using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int damage;

	void OnTriggerEnter(Collider col){

		if (col.tag == "Enemy"){
			print (col);
			col.GetComponent<BaseCharacter>().ReceiveDamage(damage);
		}
	}
}

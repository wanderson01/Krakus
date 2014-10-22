using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public GameObject fireObj;
	public GameObject iceObj;
	private BaseStats baseStats;
	public GameObject wielder;

	void Start () {
		baseStats = wielder.GetComponent<BaseStats> ();
	}

	public void CastMagic(){

		if (baseStats.damageEffect != BaseStats.DamageEffect.Physical){
			Instantiate (MagicEffect(), transform.position, transform.rotation);
		}
	}

	GameObject MagicEffect() {
		if (baseStats.damageEffect == BaseStats.DamageEffect.Fire) {
			return fireObj;
		} else if (baseStats.damageEffect == BaseStats.DamageEffect.Ice) {
			return iceObj;
		} else {
			Debug.Log ("Error: No magic effect selected.");
			return null;
		}
	}
}

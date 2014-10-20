using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {
		
	public GameObject fireObj;
	public GameObject iceObj;
	public GameObject fireAndIceObj;
	private BaseStats baseStats;
	
	void Start () {
		
		baseStats = GetComponent<BaseStats> ();
	}
	
	public void CastMagic(){

		float offsetX = - 5;
		Vector2 position = new Vector2 (transform.position.x + offsetX, transform.position.y);

		GameObject magic = Instantiate (MagicEffect (), position, Quaternion.Euler (0, 180, 0)) as GameObject;
		magic.GetComponent<MagicProjectile> ().canHitPlayer = true;
	}
	
	GameObject MagicEffect(){
		
		if (baseStats.damageEffect == BaseStats.DamageEffect.Fire){
			return fireObj;
		}
		else if (baseStats.damageEffect == BaseStats.DamageEffect.Ice){
			return iceObj;
		}
		else {
			Debug.Log ("Error: No magic effect selected.");
			return null;
		}
	}
}

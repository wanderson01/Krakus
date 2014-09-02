using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour {

	public GameObject arrowPrefab;
	public float firingSpeed;
	private float _nextAttack = 0;
	private BaseUnit unit;
	private bool shooting;
	
	void Start () {

		unit = GetComponent<BaseUnit>();
	}

	void Update () {

		if (Input.GetKeyDown ("1")) {
			CreateArrow();
		}

	}

	void CreateArrow(){

		Vector3 arrowPosition = transform.FindChild ("Aim_Pivot").FindChild ("Arrow_Shooter").position;
		Quaternion arrowRotation = transform.FindChild ("Aim_Pivot").FindChild ("Arrow_Shooter").rotation;

		Instantiate (arrowPrefab, arrowPosition, arrowRotation);
	}

	void Firing(){

		if (shooting) {
			if (Time.time > _nextAttack) {

				CreateArrow ();
				_nextAttack = Time.time + firingSpeed;
			}
		}
	}










}
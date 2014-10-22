using UnityEngine;
using System.Collections;

public class TemperatureCounter : MonoBehaviour {
	public GameObject heatTimerObject;
	public GameObject freezeTimerObject;
	CoolDownTimer heatTime;
	CoolDownTimer freezeTime;

	void Start () {
		heatTimerObject = Instantiate (heatTimerObject) as GameObject;
		freezeTimerObject = Instantiate (freezeTimerObject) as GameObject;

		heatTime = heatTimerObject.GetComponent<CoolDownTimer> () as CoolDownTimer;
		freezeTime = freezeTimerObject.GetComponent<CoolDownTimer> () as CoolDownTimer;

		heatTimerObject.transform.parent = this.transform;
		freezeTimerObject.transform.parent = this.transform;


		heatTimerObject.name = "Heat Timer";
		freezeTimerObject.name = "Freeze Timer";
	}
	
	void Update () {
	
	}
}

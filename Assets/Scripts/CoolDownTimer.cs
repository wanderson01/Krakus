using UnityEngine;
using System.Collections;

public class CoolDownTimer : MonoBehaviour {
	public float timePass = 0f;
	public float initialCounter = 0f;
	
	public void Start () {
		timePass = initialCounter;
	}
	
	public void Update () {
		timePass -= Time.deltaTime;
	}
	
	public float setTime(float newTime) {
		return timePass = newTime;
	}

	public float getTime() {
		return timePass;
	}

	public void addTime(float extraTime) {
		timePass += extraTime;
	}

	public bool isTimerEnded() {
		return timePass <= 0;
	}
}

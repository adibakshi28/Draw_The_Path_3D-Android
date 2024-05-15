using UnityEngine;
using System.Collections;

public class TimerScalechanger : MonoBehaviour {

	public float timerScale=1;

	LevelDataController levelDataController;

	void Start () {
		levelDataController = GameObject.FindGameObjectWithTag ("levelDataController").GetComponent<LevelDataController> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			levelDataController.timerScale = timerScale;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			levelDataController.timerScale = 1;
		}
	}

}

using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public int type;
	public float timeIncrease = 2;

	LevelDataController levelDataController;

	void Start () {
		levelDataController = GameObject.FindGameObjectWithTag ("levelDataController").GetComponent<LevelDataController> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			switch (type) {      // 1 : Star ; 2 : Lightening switch : 3 : Laser Switch ; 4 : Time increse
			case 1:
				levelDataController.starsEarned++;
				Destroy (this.gameObject);
				break;
			case 2:
				levelDataController.SwitchOff (1);
				Destroy (this.gameObject);
				break;
			case 3:
				levelDataController.SwitchOff (2);
				Destroy (this.gameObject);
				break;
			case 4:
				levelDataController.timeToCompleteLevel += timeIncrease;
				Destroy (this.gameObject);
				break;
			}
		}
	}

}

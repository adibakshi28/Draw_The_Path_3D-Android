using UnityEngine;
using System.Collections;

public class LighteningController : MonoBehaviour {

	public GameObject particleSys1,particleSys2;

	LevelDataController levelDataController;

	void Awake () {
		levelDataController = GameObject.FindGameObjectWithTag ("levelDataController").GetComponent<LevelDataController> ();
		levelDataController.lighteningScript.Add (this.gameObject.GetComponent<LighteningController> ());
	}

	public void SwitchOff(){
		GetComponent<BoxCollider> ().enabled=false;
		GetComponent<LineRenderer>().enabled=false;
		particleSys1.SetActive (false);
		particleSys2.SetActive (false);
	}

}
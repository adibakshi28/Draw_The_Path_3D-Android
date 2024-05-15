using UnityEngine;
using System.Collections;

public class PathFollow : MonoBehaviour {

	public float speed=10;
	[HideInInspector]
	public float reachDistance=0;
	[HideInInspector]
	public GameObject followingObject;
	[HideInInspector]
	public bool motion =false;

	int currentPoint=0;
	LevelDataController levelControllerScript;

	void Start(){
		levelControllerScript = GetComponent<LevelDataController> ();
		followingObject = levelControllerScript.player;
	}

	void Update () {
		if (motion) {
			float distance = Vector3.Distance (levelControllerScript.pathCoodinates [currentPoint] , followingObject.transform.position);
     		followingObject.transform.position = Vector3.MoveTowards (followingObject.transform.position, levelControllerScript.pathCoodinates [currentPoint], Time.deltaTime * speed);

			if (distance <= reachDistance) {
				currentPoint++;
			}

			if (currentPoint >= levelControllerScript.pathCoodinates.Count) {
				motion = false;
				levelControllerScript.GameEnd ();
			}
		}
	}
}

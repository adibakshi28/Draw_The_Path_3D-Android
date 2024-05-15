using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchTest : MonoBehaviour {

	public GameObject player,pathRendrer;
	public float speed = 1;   
	public float maxLightIntensity = 1;
	public List<Vector3> pathCoodinates;     // required to be public for code execution
	[HideInInspector]
	public bool reached = false;

	bool touchEnded=false;
	Vector3 touchWorldCoodinates,trailCoodinates;
	Light ambientLight;

	void Start(){
		ambientLight = GetComponent<Light> ();
		ambientLight.intensity = 0;
	}

	void Update () {
		if (!touchEnded) {
			if (Input.touchCount != 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					
					touchWorldCoodinates = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch (0).position.x,Input.GetTouch (0).position.y,10));  // z coodinate is y position of camera
					touchWorldCoodinates.Set(touchWorldCoodinates.x,0,touchWorldCoodinates.z);        // required if camera is prespective 
					pathCoodinates.Add(touchWorldCoodinates);

					Debug.Log (touchWorldCoodinates);
				}
				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					touchEnded = true;
					StartCoroutine (MoveObject ());
					StartCoroutine (LightIncreasor ());
				}
			} 
		}
	}

	void LateUpdate(){
		pathRendrer.transform.position = touchWorldCoodinates;
	}

	IEnumerator LightIncreasor(){
			while (ambientLight.intensity < maxLightIntensity) {
			ambientLight.intensity += 0.05f;
			yield return new WaitForSeconds((0.04f));
		} 
		ambientLight.intensity = maxLightIntensity;
	}

	IEnumerator MoveObject(){
		
		yield return new WaitForSeconds((0.7f));

	   int currentCoodinateNo=0;

		while(currentCoodinateNo<pathCoodinates.Count){
			player.transform.position = pathCoodinates[currentCoodinateNo];
			currentCoodinateNo++;
			yield return new WaitForSeconds((0.02f/speed));      // default speed is 50 coodinates in 1 second
		}
	}

	public void GameEnd(){
		if (reached) {
			// Won
		} 
		else {
		 // Lost

		}
	}

}

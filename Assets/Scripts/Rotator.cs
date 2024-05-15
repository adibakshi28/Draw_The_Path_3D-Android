using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed = -1000;

	private Vector3 rot=new Vector3 (0,1,0);

	void Update () {
		transform.Rotate (rot*Time.deltaTime*speed);
	}
}

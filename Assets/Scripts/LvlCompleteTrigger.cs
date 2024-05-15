using UnityEngine;
using System.Collections;

public class LvlCompleteTrigger : MonoBehaviour {

	private Vector3 endCoodinate;

	void Start () {
		float endPixilX = Screen.width/53.6f;
		float endPixilY = Screen.height/23.5f;
		endCoodinate = Camera.main.ScreenToWorldPoint(new Vector3(endPixilX,endPixilY,10));  // z coodinate is y position of camera
		endCoodinate.Set(-endCoodinate.x,-0.3f,-endCoodinate.z);  //(12,-0.3,6.4)
		this.gameObject.transform.position = endCoodinate;
	}

}

using UnityEngine;
using System.Collections;

public class PlayerBehav : MonoBehaviour {

	public GameObject explosion;
	public AudioClip powerUpAudio;

	private Vector3 startingCoodinate;
	AudioSource aud;
	LevelDataController levelDataController;

	void Start () {
		levelDataController = GameObject.FindGameObjectWithTag ("levelDataController").GetComponent<LevelDataController> ();
		aud = GetComponent<AudioSource> ();
		float startingPixilX = Screen.width / 30f;
		float startingPixilY = Screen.height/14f;
		startingCoodinate = Camera.main.ScreenToWorldPoint(new Vector3(startingPixilX,startingPixilY,10));  // z coodinate is y position of camera
		startingCoodinate.Set(startingCoodinate.x,0,startingCoodinate.z);  // (-10.9,0,-6)
		this.gameObject.transform.position = startingCoodinate;     
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){ 
			levelDataController.Pause ();
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "enemy") {
			Instantiate (explosion, transform.position, Quaternion.identity);
			Handheld.Vibrate ();
			levelDataController.GameEnd ();
		}
		if (other.gameObject.tag == "lvlTrigger") {
			levelDataController.reached = true;
			levelDataController.GameEnd ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "bonus") {
			aud.clip = powerUpAudio;
			aud.Play ();
		}
	}
}

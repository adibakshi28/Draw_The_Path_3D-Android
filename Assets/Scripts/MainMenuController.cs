using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject exitPanel;

	AudioSource aud;

	void Start(){
		aud = GetComponent<AudioSource> ();
	}
		
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			exitPanel.SetActive (true);
			aud.Play ();
		}
	}

	public void PlayButton(){
		SimpleSceneFader.ChangeSceneWithFade("Level Selection");
		aud.Play ();
	}

	public void ExitButton(){
		exitPanel.SetActive (true);
	}
	public void ExitFinalButton (){
		Application.Quit ();
		return;
	}
	public void ExitReturnButton (){
		exitPanel.SetActive (false);
	}

}

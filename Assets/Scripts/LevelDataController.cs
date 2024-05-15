using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDataController : MonoBehaviour {

	public GameObject player, pathRendrer, levelCamera, playerCamera, startingText, timeUpText, observeText, tutorialBtn, canvas, pauseBtn;
	public float maxLightIntensity = 1;
	public Text timerText;
	public Slider timeSlider;
	public AudioClip clockSound, bassDrop, UISound;
	public GameObject[] levelDesign;
	[HideInInspector]
	public GameObject obsticals, tutorial,collectables;
	[HideInInspector]
	public float timerScale=1,timeToCompleteLevel=7,viewingTime=0.5f;
	[HideInInspector]
	public Color lvlEmissionColor;
	[HideInInspector]
	public List<Vector3> pathCoodinates;     // required to be public for code execution
	[HideInInspector]
	public bool reached = false,lightOff=false,countTime=false;   
	[HideInInspector]
	public int starsEarned=0;
	[HideInInspector]
	public List<LaserBehav> lasersScript;
	[HideInInspector]
	public List<LighteningController> lighteningScript; 

	GameObject levelElements;
	int currentLevel;
	bool touchEnded=false,touched=false;
	Vector3 touchWorldCoodinates,trailCoodinates,groundPosition=new Vector3(0,-1.5f,0);

	Light ambientLight;
	AudioSource aud;
	Animator levelCameraAnim;

	PathFollow pathFollowScript;
	UIController UIControllerScript;
	GameDataController gameDataController;

	void Start(){
		gameDataController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameDataController> ();
		pathFollowScript = GetComponent<PathFollow> ();
		UIControllerScript= canvas.GetComponent<UIController> ();
		ambientLight = GetComponent<Light> ();
		aud = GetComponent<AudioSource> ();
		levelCameraAnim = levelCamera.GetComponent<Animator> ();

		currentLevel = gameDataController.currentLevel;
		timeToCompleteLevel = gameDataController.lvlData [currentLevel - 1].timeToCompleteLevel;
		viewingTime = gameDataController.lvlData [currentLevel - 1].viewingTime;
	     
		timerText.text = timeToCompleteLevel.ToString ();
		timeSlider.maxValue = timeToCompleteLevel;
		timeSlider.value = timeSlider.maxValue;
		GenerateLevel (currentLevel);
	}

	void Update () {
		if ((countTime)&&(lightOff)) {
			timeToCompleteLevel -= Time.deltaTime*timerScale;
			timerText.text = (Mathf.Round (timeToCompleteLevel)).ToString ();
			timeSlider.value = timeToCompleteLevel;
			if ((aud.clip != clockSound) || (!aud.isPlaying)) {
				aud.clip = clockSound;
				aud.Play ();
				aud.loop = true;
			} 
			if (timeToCompleteLevel <= 0) {
				countTime = false;
				timerText.text = "0";
				timeSlider.value = timeSlider.minValue;
				timeUpText.SetActive (true);
				GameEnd ();
			}
		}

		if ((!touchEnded)&&(lightOff)) {
			if (Input.touchCount != 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					touchWorldCoodinates = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch (0).position.x,Input.GetTouch (0).position.y,10));  // z coodinate is y position of camera
					touchWorldCoodinates.Set(touchWorldCoodinates.x,0,touchWorldCoodinates.z);        // required if camera is prespective 
					pathCoodinates.Add(touchWorldCoodinates);
					touched = true;
				}
				if ((Input.GetTouch (0).phase == TouchPhase.Ended)&&(touched)) {
					touchEnded = true;
					StartCoroutine (MovePlayer ());
				}
			} 
		}
	}

	void LateUpdate(){
		if (touched) {
			pathRendrer.transform.position = touchWorldCoodinates;
		}
	}

	IEnumerator StartingLight(){
		ambientLight.intensity = maxLightIntensity;
		observeText.SetActive(true);
		yield return new WaitForSeconds(viewingTime);
		observeText.SetActive (false);
		while (ambientLight.intensity > 0) {
			ambientLight.intensity -= 0.05f;
			yield return new WaitForSeconds((0.08f));
		} 
		ambientLight.intensity = 0;
		obsticals.SetActive (false);
		pauseBtn.SetActive (false);
		lightOff = true;
		countTime = true;
		startingText.SetActive (true);
		yield return new WaitForSeconds((0.5f));
		startingText.SetActive (false);
	}

	IEnumerator LightIncreasor(){
		obsticals.SetActive (true); 
		pauseBtn.SetActive (true);
		while (ambientLight.intensity < maxLightIntensity) {
			ambientLight.intensity += 0.05f;
			yield return new WaitForSeconds((0.04f));
		} 
		ambientLight.intensity = maxLightIntensity;
	}

	IEnumerator MovePlayer(){
		countTime = false;
		StartCoroutine (LightIncreasor ());

		aud.Stop ();
		aud.clip = bassDrop;
		aud.loop = false;
		yield return new WaitForSeconds (0.5f);

		aud.Play ();
		levelCameraAnim.SetTrigger ("Switch");      // Level Camera Move animation 

		yield return new WaitForSeconds (1.5f);

		playerCamera.SetActive(true);   // camera switch
		levelCamera.SetActive(false);

		pathFollowScript.motion = true;
		countTime = true;
	}      
		

	public void GameEnd(){
		countTime = false;
		aud.Stop ();
		pathFollowScript.motion = false;
		pauseBtn.SetActive (false);
		player.transform.DetachChildren ();
		Destroy(player);
		if((reached)&&(timeToCompleteLevel>0)){
			starsEarned++;
			if(PlayerPrefs.GetInt ("levelReached")==currentLevel){
				PlayerPrefs.SetInt ("levelReached", currentLevel+1);  
			}
			if(PlayerPrefs.GetInt ("Level " + gameDataController.currentLevel.ToString () + " Stars")<starsEarned){    // setting highscore
				PlayerPrefs.SetInt ("Level " + gameDataController.currentLevel.ToString () + " Stars",starsEarned);
			} 
		}
		else {   //Lost
			starsEarned=0;
		}
		StartCoroutine (GameEndDisplay ());
	}
	IEnumerator GameEndDisplay(){
		yield return new WaitForSeconds(1);
		UIControllerScript.StarShow (starsEarned);
		gameDataController.showInterstitialAd ();            //  show interinstial ads
	}

	public void GenerateLevel(int currentLevel){
		if(levelDesign[currentLevel-1]!=null){
			levelElements = Instantiate (levelDesign [currentLevel - 1], groundPosition, Quaternion.identity)as GameObject;

			// Size change according to screen size
			Vector3 size;
			size = levelElements.transform.localScale;
			size=new Vector3((((91.8f*Screen.width)/(1.70666f*Screen.height))),6,100);
			levelElements.transform.localScale = size;
		
			obsticals = levelElements.transform.GetChild (0).transform.gameObject;          // obsticals should be the first child of every level design prefab
			if (levelElements.transform.GetChild (1).transform.gameObject.tag == "tutorial") {
				tutorial = levelElements.transform.GetChild (1).transform.gameObject;
				collectables = levelElements.transform.GetChild (2).transform.gameObject;
			} 
			else {
				collectables = levelElements.transform.GetChild (1).transform.gameObject;
			}
		}
		else{
			Debug.Log("error occered in LevelCreator script");	
		}

		if (tutorial != null) {
			StartCoroutine (ShowTutorial ());
		} 
		else {
			StartCoroutine (StartingLight ());
		}
	}
	IEnumerator ShowTutorial(){
		obsticals.SetActive (false);
		collectables.SetActive (false);
		pauseBtn.SetActive (false);
		tutorial.SetActive (true);
		yield return new WaitForSeconds(1.5f);
		tutorialBtn.SetActive (true);
	}

	public void TutorialBtn(){
		aud.clip = UISound;
		aud.loop = false;
		aud.Play ();
		tutorial.SetActive (false);
		obsticals.SetActive (true);
		collectables.SetActive (true);
		tutorialBtn.SetActive (false);
		pauseBtn.SetActive (true);
		StartCoroutine (StartingLight());
	}

	public void SwitchOff(int what){  //  1=lightening,2=laser,3=both
		
		int currentIndice = 0;

		if ((what == 1) || (what == 3)) {
			while (currentIndice < lighteningScript.Count) {
				lighteningScript[currentIndice].SwitchOff ();
				currentIndice++;
			}
		}
		
		currentIndice = 0;

			if ((what == 2) || (what == 3)) {
				while (currentIndice < lasersScript.Count) {
					lasersScript [currentIndice].SwitchOff ();
					currentIndice++;
				}
			}
		}
		

	public void Pause(){
		aud.Stop ();
		aud.clip = UISound;
		aud.loop = false;
		StartCoroutine (Pauser());
	}
	IEnumerator Pauser(){
		aud.Play ();
		UIControllerScript.PauseState (true);
		countTime = false;
		yield return new WaitForSeconds (0.05f);
		Time.timeScale = 0;
	}
	public void Resume(){
		Time.timeScale = 1;
		aud.Play ();
		UIControllerScript.PauseState (false);
		countTime = true;
	}
	public void ReplayButton(){
		Time.timeScale = 1;
		aud.Play ();
		if (currentLevel < 16) {
			StartCoroutine (SceneChanger ("Game Scene"));
		} 
		else {
			if (currentLevel < 31) {
				StartCoroutine (SceneChanger ("(Green) Game scene"));
			} 
			else {
				//		SceneManager.LoadScene ("Game Scene");
			}
		}
	}
	public void LevelSelectButton(){
		Time.timeScale = 1;
		aud.Play ();
		StartCoroutine (SceneChanger ("Level Selection"));
	}
	public void NextButton(){
		aud.Play ();
		gameDataController.currentLevel = gameDataController.currentLevel + 1;
		if ((currentLevel+1) < 16) {
			StartCoroutine (SceneChanger ("Game Scene"));
		} 
		else {
			if ((currentLevel+1) < 31) {
				StartCoroutine (SceneChanger ("(Green) Game scene"));
			} 
			else {
				//		SceneManager.LoadScene ("Game Scene");
			}
		}
	}

	IEnumerator SceneChanger(string scene){
		countTime = false;
		aud.clip = UISound;
		aud.loop = false;
		aud.Play ();
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene (scene);
	}

}

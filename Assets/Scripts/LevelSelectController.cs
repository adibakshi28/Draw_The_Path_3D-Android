using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {

	[System.Serializable]
	public class Level
	{
		public GameObject levelThumbnail; 
		[HideInInspector]
		public Text levelNo;
		[HideInInspector]
		public GameObject lockImage,star1, star2, star3;
		[HideInInspector]
		public Button levelButton;
		[HideInInspector]
		public Animator btnEmphAnim;
		[HideInInspector]
		public LvlBtnDataFeeder btnDataScript;
	}

	public GameObject levelPanel,nextPageBtm, previousPageBtn,exitPanel;
	public AudioClip levelBtnSound, pageBtnSound;
	public Level[] level;

	int totalPages,currentPageNo=1,unlockLvlsTill;
	AudioSource aud;
	RectTransform rtLevelPanel;
	GameDataController gameDataController;

	void Awake(){
		for (int lvl = 0; lvl <level.GetLength(0); lvl++) {
			level [lvl].btnDataScript = level [lvl].levelThumbnail.GetComponent<LvlBtnDataFeeder> ();

			level [lvl].levelButton = level [lvl].btnDataScript.levelButton;
			level [lvl].levelNo = level [lvl].btnDataScript.levelNo;
			level [lvl].lockImage = level [lvl].btnDataScript.lockImage;
			level [lvl].btnEmphAnim = level [lvl].btnDataScript.btnEmphAnim;
		
			level [lvl].star1 = level [lvl].btnDataScript.star1;
			level [lvl].star2 = level [lvl].btnDataScript.star2;
			level [lvl].star3 = level [lvl].btnDataScript.star3;
		}
	}

	void Start () {
		gameDataController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameDataController> ();
		gameDataController.showInterstitialAd ();            //  show interinstial ads

		aud = GetComponent<AudioSource> ();
		rtLevelPanel = levelPanel.GetComponent<RectTransform> ();

		if (PlayerPrefs.GetInt ("levelReached") <= level.GetLength (0)) {
			unlockLvlsTill = PlayerPrefs.GetInt ("levelReached");
		} 
		else {
			unlockLvlsTill = level.GetLength (0);
		}

		for (int lvlIndex = 0; lvlIndex < unlockLvlsTill; lvlIndex++) {
			UnlockLevel (lvlIndex);
		}
		StartCoroutine (BtnAnim ());                    // Animates new level ( called in from coroutine to avoid warning )

		gameDataController.totalLevels = level.GetLength (0);

		totalPages = levelPanel.transform.childCount;
		previousPageBtn.SetActive(false);     //  At starting First page is shown
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){ 
			ExitButton ();
		}
	}

	IEnumerator BtnAnim(){
		yield return new WaitForSeconds (0.4f);
		for (int page = 0; page < (unlockLvlsTill -1)/ 15; page++) {
			yield return new WaitForSeconds (0.2f);
			PageChangeButton (-1);
		}
		level [unlockLvlsTill - 1].btnEmphAnim.SetTrigger ("Emph");
	}

	public void LevelPlay(int levelToPlayBtn){
		aud.clip = levelBtnSound;
		aud.Play ();
		gameDataController.currentLevel = levelToPlayBtn;
		if (levelToPlayBtn < 16) {
			SceneManager.LoadScene ("Game Scene");
		} 
		else {
			if (levelToPlayBtn < 31) {
				SceneManager.LoadScene ("(Green) Game scene");
			} 
			else {
				// SceneManager.LoadScene ("(Yellow) Game scene");
			}
		}
	}

	public void PageChangeButton(int direction){      // -1 = Next   ,,   +1 = Previous
		aud.clip = pageBtnSound;
		aud.Play ();
		rtLevelPanel.anchoredPosition = new Vector2 ((rtLevelPanel.anchoredPosition.x+(1000*direction)), 0);
		currentPageNo = currentPageNo - direction;
		if (currentPageNo == 1) {
			previousPageBtn.SetActive(false);
		} else {
			previousPageBtn.SetActive(true);
		}
		if (currentPageNo == totalPages) {
			nextPageBtm.SetActive(false);
		} else {
			nextPageBtm.SetActive(true);
		}
	}


	void UnlockLevel(int lvlIndex){
		level [lvlIndex].levelButton.interactable = true;
		level [lvlIndex].lockImage.SetActive (false);
		level [lvlIndex].levelNo.text = (lvlIndex+1).ToString ();                  

		int starsEarned = PlayerPrefs.GetInt ("Level " + (lvlIndex + 1).ToString () + " Stars");
		switch (starsEarned) {
		case 0:
			level [lvlIndex].star1.SetActive (false);
			level [lvlIndex].star2.SetActive (false);
			level [lvlIndex].star3.SetActive (false);
			break;
		case 1:
			level [lvlIndex].star1.SetActive (true);
			level [lvlIndex].star2.SetActive (false);
			level [lvlIndex].star3.SetActive (false);
			break;
		case 2:
			level [lvlIndex].star1.SetActive (true);
			level [lvlIndex].star2.SetActive (true);
			level [lvlIndex].star3.SetActive (false);
			break;
		case 3:
			level [lvlIndex].star1.SetActive (true);
			level [lvlIndex].star2.SetActive (true);
			level [lvlIndex].star3.SetActive (true);
			break;
		}
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

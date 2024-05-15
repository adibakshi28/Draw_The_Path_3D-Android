using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameDataController : MonoBehaviour {

	[System.Serializable]
	public class LevelData
	{
		public float timeToCompleteLevel=7,viewingTime=0.5f;
	}

	public int currentVersion;                       // increment this field by 1 in every newer versions of the game release
	public string instiantialID;
	public LevelData[] lvlData;
	[HideInInspector]
	public int currentLevel,totalLevels;

	InterstitialAd interstitial;

	void Start () {
		if (PlayerPrefs.GetInt ("hasPlayed") < 50) {                  // (50 is used to ensure hasPlayed has a value less than 100 whic indicates that the game has been played previously...... expect that 100 and 50 as numbers ha no other significance)

			Debug.Log ("Application Running for the first time");

			//Game Settings
			PlayerPrefs.SetInt ("hasPlayed", 100);
			PlayerPrefs.SetInt ("firstTime", 1);              // if playing for the first time the 1 else 0 always
			PlayerPrefs.SetInt("timesLaunched",0);            //  keeps track of times the application is launched
			PlayerPrefs.SetInt ("coins", 0);                       
			PlayerPrefs.SetInt ("levelReached", 1);  
		} 
		else {
			PlayerPrefs.SetInt ("firstTime", 0);
		}
		PlayerPrefs.SetInt("timesLaunched",(PlayerPrefs.GetInt("timesLaunched")+1));     //  incriments by 1 every time the application is launched 

		if (!(PlayerPrefs.GetInt ("version") == currentVersion)) {
			PlayerPrefs.SetInt ("version", currentVersion);
			//  put all the new player pref statements or changes to previously existant in future versions here eg. new players , coin gifts etc
		}

		DontDestroyOnLoad (this.gameObject);

		RequestInterstitialAds();


		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;

		SceneManager.LoadScene ("Main Menu");

	}

	private void RequestInterstitialAds()
	{
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(instiantialID);

		//***Test***
	/*		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
			.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
			.Build();  */

		//***Production***
		AdRequest request = new AdRequest.Builder().Build();

		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}


	public void showInterstitialAd()
	{
		//Show Ad
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} 
		else {
			RequestInterstitialAds ();
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject pauseElements;
	public Text lvlOutcomeTxt,tipText;
	public string[] loserTips;
	public string[] winnerTips;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void PauseState(bool state){
		pauseElements.SetActive(state);
	}

	public void StarShow(int starsEarned){
		switch(starsEarned){
		case 0:
			anim.SetInteger ("Stars", 0);
			lvlOutcomeTxt.text = "FAILED";
			tipText.text = loserTips [Random.Range (0, loserTips.GetLength (0))];
			break;
		case 1:
			anim.SetInteger ("Stars", 1);
			lvlOutcomeTxt.text = "LEVEL COMPLETE";
			tipText.text = winnerTips [Random.Range (0, winnerTips.GetLength (0))];
			break;
		case 2:
			anim.SetInteger ("Stars", 2);
			lvlOutcomeTxt.text = "LEVEL COMPLETE";
			tipText.text = winnerTips [Random.Range (0, winnerTips.GetLength (0))];
			break;
		case 3:
			anim.SetInteger ("Stars", 3);
			lvlOutcomeTxt.text = "LEVEL COMPLETE";
			tipText.text = null;
			break;
		}
	}
}

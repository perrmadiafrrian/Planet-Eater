using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public UI_PlanetInfo infoSize;
	public Text highScore;
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy) {
			highScore.text = infoSize.getSize () + " kc";
		}
	}
}

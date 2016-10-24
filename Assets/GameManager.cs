using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	GvrViewer googleVR;
	void Start () {
		googleVR = GameObject.FindObjectOfType<GvrViewer> ();
		if (googleVR != null) {
			googleVR.VRModeEnabled = PlayerPrefs.GetInt (DataManager.VRMode,0) == 0 ? false : true;
		}
	}

	public void Play() {
		AsyncOperation ao = SceneManager.LoadSceneAsync ("Tes");
		ao.allowSceneActivation = true;
	}

	public void Restart() {
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().name);
	}

	public void Home() {
		AsyncOperation ao = SceneManager.LoadSceneAsync ("Menu");
		ao.allowSceneActivation = true;
	}
}

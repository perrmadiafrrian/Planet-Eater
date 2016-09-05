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

	void Update () {
	
	}
}

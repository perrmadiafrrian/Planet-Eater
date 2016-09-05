using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	public const string VRMode = "VRMode";

	void Start () {
	
	}

	public void SetVRMode (bool enabled) {
		PlayerPrefs.SetInt (VRMode,enabled?1:0);
	}
}

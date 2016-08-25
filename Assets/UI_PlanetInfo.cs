﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_PlanetInfo : MonoBehaviour {
	Text textSize;
	Player player;
	void Start () {
		textSize = transform.FindChild ("Size").GetComponent<Text>();
		player = GameObject.FindObjectOfType<Player> ();
	}

	void Update () {
		if (player != null) {
			float r = player.transform.localScale.x / 2f;
			int size = (int)(4f / 3f * Mathf.PI * r * r * r * 1000f);
			textSize.text = size + " kc";
		}
	}


}
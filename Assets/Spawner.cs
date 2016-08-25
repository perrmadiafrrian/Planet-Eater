﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnRadius = 300f;

	public int maxAsteroids = 100;
	public int maxPlanets = 20;

	public GameObject planetPrefab;

	public Material defaultMaterial;

	void Start () {
		Spawn ();
	}

	void Update () {
		
	}


	private Transform planets;
	private Transform asteroids;
	void Spawn() {
		planets = new GameObject ("_planets").transform;
		asteroids = new GameObject ("_asteroids").transform;

		StartCoroutine (SpawnAsteroid());
		StartCoroutine (SpawnPlanet());
	}

	IEnumerator SpawnAsteroid() {
		if (asteroids.childCount <= maxAsteroids) {
			yield return new WaitForSeconds (0.1f);

			GameObject g = GameObject.CreatePrimitive (PrimitiveType.Cube);
			g.name = "asteroid";
			g.tag = "Enemy";
			g.layer = 8; // Planet Layer
			g.GetComponent<Renderer> ().material = defaultMaterial;
			g.transform.SetParent (asteroids);
			g.transform.Rotate (new Vector3 (Random.Range(0,180),Random.Range(0,180),Random.Range(0,180)));
			float rand = Random.Range (0.7f,1.1f);
			g.transform.localScale = new Vector3 (rand,rand,rand);
			g.transform.position = Vector3.ClampMagnitude (new Vector3 (Random.Range (spawnRadius / 2f, -spawnRadius / 2f), 0f, Random.Range (spawnRadius / 2f, -spawnRadius / 2f)), spawnRadius / 2f);

			StartCoroutine (SpawnAsteroid ());
		} else {
			yield return new WaitForSeconds (1f);
			StartCoroutine (SpawnAsteroid ());
		}
	}

	IEnumerator SpawnPlanet() {
		if (planets.childCount <= maxPlanets) {
			yield return new WaitForSeconds (0.1f);

			GameObject g = Instantiate (planetPrefab);

			g.transform.SetParent (planets);
			g.transform.position = Vector3.ClampMagnitude (new Vector3 (Random.Range (spawnRadius / 2f, -spawnRadius / 2f), 0f, Random.Range (spawnRadius / 2f, -spawnRadius / 2f)), spawnRadius / 2f);
			float rand = Random.Range (1f,5f);
			g.transform.localScale = new Vector3 (rand,rand,rand);

			StartCoroutine (SpawnPlanet ());
		} else {
			yield return new WaitForSeconds (5f);
			StartCoroutine (SpawnPlanet ());
		}
	}
}

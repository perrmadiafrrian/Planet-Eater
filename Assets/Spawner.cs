using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnRadius = 350f;

	public int maxAsteroids = 50;
	public int maxPlanets = 10;

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
			yield return new WaitForSeconds (0.2f);

			GameObject g = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			g.name = "asteroid";
			g.layer = 8;
			g.GetComponent<Renderer> ().material = defaultMaterial;
			g.transform.SetParent (asteroids);
			g.transform.Rotate (new Vector3 (Random.Range(0,180),Random.Range(0,180),Random.Range(0,180)));
			float rand = Random.Range (0.7f,1.1f);
			g.transform.localScale = new Vector3 (rand,rand,rand);
			g.GetComponent<MeshRenderer>().materials[0].color = new Color(Random.Range (0f, 10f) / 10,
				Random.Range (0f, 10f) / 10, Random.Range (0f, 10f) / 10f);
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
			g.GetComponent<MeshRenderer> ().materials [0].color = new Color (Random.Range (0f, 10f) / 10,
				Random.Range (0f, 10f) / 10, Random.Range (0f, 10f) / 10f);
			StartCoroutine (SpawnPlanet ());
		} else {
			yield return new WaitForSeconds (5f);
			StartCoroutine (SpawnPlanet ());
		}
	}
}

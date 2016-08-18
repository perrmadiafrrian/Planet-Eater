using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	public Vector3 cameraOffset = new Vector3(0,2f,-3f);
	public float speed = 2f;

	private Ray ray;
	private RaycastHit hit;
	private float speedModifier = 1f;

	new private Transform camera;
	private Transform canvas;

	void Start () {
		if (Camera.main == null) {
			Debug.LogError ("Main Camera not found");
		} else {
			camera = Camera.main.transform;
		}

		canvas = GameObject.Find ("Canvas").transform;
		if (canvas == null) {
			Debug.LogError ("Canvas not found");
		} 
	}

	void Update () {
		// Move forwards 100u from the camera. 
		Move (camera.forward*100f + camera.position);
	}

	void Move(Vector3 targetLocation) {
		// Player Movement
		targetLocation.y = 0f;
		transform.position = Vector3.MoveTowards (transform.position, targetLocation, Time.deltaTime * speed * speedModifier);

		// Camera Target Location with offset (behind the player)
		Vector3 cameraTarget = transform.position - Vector3.ClampMagnitude ((targetLocation - transform.position), Mathf.Abs(cameraOffset.z));
		cameraTarget.y = cameraOffset.y;
		camera.parent.position = Vector3.MoveTowards (camera.parent.position, cameraTarget, speed * speedModifier * 1.1f * Time.deltaTime);
	}

	private GameObject gui;
	public void DoAction() {
		speedModifier = 0.2f;
		if (canvas != null) {
			gui = new GameObject ("GUI");
			gui.transform.SetParent (canvas);

			RectTransform r = gui.AddComponent<RectTransform> ();
			gui.AddComponent<Text> ().text = "Do Action";

			r.anchoredPosition = RectTransformUtility.WorldToScreenPoint (Camera.main, transform.position);
		}
	}

	public void EndAction() {
		speedModifier = 1f;
		GameObject.Destroy (gui);
	}
}

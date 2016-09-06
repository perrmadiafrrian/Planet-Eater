using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Radar : MonoBehaviour {

	public GameObject dangerPrefab;

	float detectionRate = 2f;
	float detectionRadius = 50f;

	Transform player;
	LayerMask planetMask;

	void Start () {
		planetMask = LayerMask.GetMask ("Planet");
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		StartCoroutine (Detect());
	}

	IEnumerator Detect() {
		if (player != null) {
			Collider[] nearbyPlanets = Physics.OverlapSphere (player.position, player.localScale.x + detectionRadius, planetMask);

			foreach (Collider c in nearbyPlanets) {
				if (c.transform.localScale.x > player.transform.localScale.x) {
					GameObject g = Instantiate (dangerPrefab);
					g.transform.SetParent (transform, false);

					Quaternion rot = Quaternion.FromToRotation (Camera.main.transform.forward, c.transform.position - player.position);
					g.transform.rotation = Quaternion.Euler (0f, 0f, rot.eulerAngles.y * -1f);

					StartCoroutine (AnimateDangerRadar (g.transform, c.transform));
				}
			}
		}

		yield return new WaitForSeconds (1f/detectionRate);

		StartCoroutine (Detect());
	}

	IEnumerator AnimateDangerRadar(Transform dangerSign, Transform enemy) {
		Image dangerSignImage = dangerSign.GetComponent<Image> ();
		float t = 0;
		while (player!=null && enemy!=null && t < 1f/detectionRate) {
			Quaternion rot = Quaternion.FromToRotation (Camera.main.transform.forward,enemy.position - player.position);
			dangerSign.rotation = Quaternion.Euler (transform.parent.rotation.eulerAngles.x,transform.parent.rotation.eulerAngles.y,rot.eulerAngles.y*-1f);
			dangerSignImage.color = new Color (1f,1f,1f,1f - (Vector3.Distance(player.position,enemy.position)/(detectionRadius+player.localScale.x)));
			t += Time.deltaTime;
			yield return null;
		}
		GameObject.Destroy (dangerSign.gameObject);
	}
}

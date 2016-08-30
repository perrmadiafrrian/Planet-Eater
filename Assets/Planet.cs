using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	private float movingSpd;
	private float planetSize;

	void Awake() {
		movingSpd = 5f * ((100f - transform.localScale.x) / 100f);
	}

	public float getMoveSpd() {
		return movingSpd;
	}

	public float getPlanetSize() {
		planetSize = transform.localScale.x;
		return planetSize;
	}

	public void Eat(GameObject target) {
		StartCoroutine(GrowAnim(getPlanetSize() + (target.transform.localScale.x / 4f)));
		movingSpd = 5f * ((100f - transform.localScale.x) / 100f);
		Destroy (target);
	}

	void OnCollisionEnter(Collision target) {
		if (target.transform.localScale.x < transform.localScale.x * .75f) {
			Eat (target.gameObject);
		}
	}

	IEnumerator GrowAnim(float size) {
		float t = 0f;
		float s = transform.localScale.x;
		while (t < .5f) {
			t = t + Time.deltaTime;
			float n = Mathf.Lerp (s, size, t * 2f);
			transform.localScale = new Vector3 (n, n, n);
			yield return null;
		}
	}
}

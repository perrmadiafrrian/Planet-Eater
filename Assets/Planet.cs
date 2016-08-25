using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	private float movingSpd;
	private float planetSize;

	public float getMoveSpd() {
		float sz = transform.localScale.x;
		movingSpd = 5f * ((100f - sz) / 100f);
		return movingSpd;
	}

	public float getPlanetSize() {
		planetSize = transform.localScale.x;
		return planetSize;
	}

	public void Eat(GameObject target) {
		transform.localScale = transform.localScale + (target.transform.localScale / 4);
		Destroy (target);
	}

	void OnCollisionEnter(Collision target) {
		if (target.transform.localScale.x < transform.localScale.x * .75f) {
			Eat (target.gameObject);
		}
	}
}

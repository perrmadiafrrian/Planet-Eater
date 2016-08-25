using UnityEngine;
using System.Collections;

public class AILogic : MonoBehaviour {

	private bool chasingOther;
	private float rad;
	public LayerMask planetMask;
	public Planet p;

	// Use this for initialization
	void Start () {
		chasingOther = false;
		rad = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!chasingOther) {
			StartCoroutine(SearchPlanet(transform.position, rad * 1.1f));
		}
		transform.Translate (transform.forward * Time.deltaTime * p.getMoveSpd());
	}

	IEnumerator SearchPlanet(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere (center, radius, planetMask);
		int i = 0;
		bool found = false;
		rad = radius;
		while (i < hitColliders.Length && !found) {
			if (hitColliders [i].transform.localScale.x < transform.localScale.x * .75f) {
				found = true;
				StartCoroutine (Chasing (hitColliders [i].gameObject));
				rad = transform.localScale.x;
			}
			yield return null;
		}
	}

	IEnumerator Chasing(GameObject target) {
		chasingOther = true;
		bool stopChasing = false;
		bool eatIt = false;
		while (target != null && Vector3.Distance(transform.position, target.transform.position) > 1f && !stopChasing) {
			transform.LookAt (target.transform);
			yield return null;
			if (Vector3.Distance (transform.position, target.transform.position) < (p.getPlanetSize() * .5f)) {
				stopChasing = true;
				eatIt = true;
			}
		}
		if (eatIt)
			p.Eat (target);
		chasingOther = false;
		yield return null;
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(0f, 0f, 1f, .3f);
		Gizmos.DrawSphere (transform.position, rad);
	}
}

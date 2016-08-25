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
	}

	IEnumerator SearchPlanet(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere (center, radius, planetMask);
		int i = 0;
		bool found = false;
		rad = radius;
		while (i < hitColliders.Length && !found) {
			if (hitColliders [i].transform.localScale.x < transform.localScale.x * .75f) {
				found = true;
				Debug.Log ("Chasing");
				StartCoroutine (Chasing (hitColliders [i].gameObject));
				rad = transform.localScale.x;
			}
			yield return null;
		}
	}

	IEnumerator Chasing(GameObject target) {
		chasingOther = true;
		bool stopChasing = false;
		while (Vector3.Distance(transform.position, target.transform.position) > (p.getPlanetSize() / 2.5f) && !stopChasing && target != null) {
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * p.getMoveSpd ());
			if (Vector3.Distance (transform.position, target.transform.position) > (p.getPlanetSize () / 2f)) {
				stopChasing = true;
				p.Eat (target);
			}
			yield return null;
		}
		chasingOther = false;
	}
}

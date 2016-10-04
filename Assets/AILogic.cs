using UnityEngine;
using System.Collections;

public class AILogic : MonoBehaviour {

	private bool chasingOther;
	private float rad;
	private bool running;
	private bool foundNewTarget;
	private GameObject closest;
	private bool searchNew;

	public LayerMask planetMask;
	public Planet p;

	// Use this for initialization
	void Start () {
		chasingOther = false;
		running = false;
		foundNewTarget = false;
		searchNew = false;
		rad = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!chasingOther && !running) {
			foundNewTarget = false;
			StartCoroutine(SearchPlanet(transform.position, rad * 1.1f));
		}
		transform.position = Vector3.MoveTowards (transform.position,transform.position + transform.forward, Time.deltaTime * p.getMoveSpd());
	}

	IEnumerator SearchPlanet(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere (center, radius, planetMask);
		int i = 0;
		bool found = false;
		rad = radius;
		while (i < hitColliders.Length && !found) {
			if (hitColliders[i] != null && closest == null && hitColliders[i].transform != transform) {
				closest = hitColliders [i].gameObject;
			} else if (hitColliders[i] != null && closest != null && hitColliders[i].transform != transform && Vector3.Distance (transform.position, closest.transform.position) > Vector3.Distance (transform.position, hitColliders [i].transform.position)) {
				closest = hitColliders [i].gameObject;
			}
			if (hitColliders [i] != null && hitColliders [i].transform.localScale.x < transform.localScale.x * .75f) {
				found = true;
				StartCoroutine (Chasing (hitColliders [i].gameObject));
				rad = transform.localScale.x;
			} else if (hitColliders [i] != null && hitColliders [i].transform.localScale.x > transform.localScale.x) {
				found = true;
				StartCoroutine (Running (hitColliders [i].gameObject));
				rad = transform.localScale.x;
			}
			yield return null;
		}
	}

	IEnumerator Chasing(GameObject target) {
		chasingOther = true;
		bool stopChasing = false;
		while (target != null && !foundNewTarget && Vector3.Distance(transform.position, target.transform.position) > 1f && !stopChasing) {
			Quaternion tar = Quaternion.LookRotation (target.transform.position - transform.position);
			transform.rotation = Quaternion.Lerp (transform.rotation, tar, 1f);
			CheckClosestEnemies (transform.position, Vector3.Distance(target.transform.position, transform.position));
			if (target.transform.localScale.x > transform.localScale.x)
				stopChasing = true;
			yield return null;
		}
		chasingOther = false;
		yield return null;
	}

	IEnumerator Running(GameObject target) {
		running = true;
		Quaternion tar = Quaternion.LookRotation (transform.position - target.transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, tar, 1f);
		CheckClosestEnemies (transform.position, Vector3.Distance(target.transform.position, transform.position));
		if (!foundNewTarget)
			yield return new WaitForSeconds (3f);
		else
			yield return null;
		running = false;
	}

	void CheckClosestEnemies(Vector3 center, float radius) {
		searchNew = true;
		Collider[] hitColliders = Physics.OverlapSphere (center, radius, planetMask);
		int i = 0;
		bool found = false;
		rad = radius;
		while (i < hitColliders.Length && !found) {
			if (closest == null && hitColliders[i] != null && hitColliders[i].transform != transform) {
				closest = hitColliders [i].gameObject;
				found = true;
			} else if (hitColliders [i] != null && closest != null && hitColliders[i].transform != transform && Vector3.Distance (transform.position, closest.transform.position) > Vector3.Distance (transform.position, hitColliders [i].transform.position)) {
				closest = hitColliders [i].gameObject;
				foundNewTarget = true;
				found = true;
			}
			i++;
		}
		searchNew = false;
	}

	void OnDrawGizmos() {
		if (searchNew)
			Gizmos.color = new Color (0f, 0f, 1f, .3f);
		else
			Gizmos.color = new Color (0f, 1f, 0f, .2f);

		Gizmos.DrawSphere (transform.position, rad);
	}
}

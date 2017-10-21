using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Zipline firedBy;
	public float speed;

	private Transform transform;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision collision) {
		firedBy.loc = collision.contacts[0].point;
		firedBy.isFlying = true;
		firedBy.FPC.canMove = false;
		firedBy.lineRenderer.enabled = enabled;
		firedBy.lineRenderer.SetPosition(1, firedBy.loc);
		Destroy(gameObject);
	}
}

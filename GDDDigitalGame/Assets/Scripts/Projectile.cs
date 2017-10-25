using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Zipline firedBy;
	public float speed;

	private Transform transform;
	//private BoxCollider boxColl;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
		//boxColl = GetComponent<BoxCollider>();
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody != firedBy.fpcRigidBody) {
			firedBy.loc = collision.contacts[0].point;
			firedBy.isFlying = true;
			firedBy.fpcRigidBody.velocity = Vector3.zero;
			firedBy.FPC.canMove = false;
			firedBy.lineRenderer.enabled = enabled;
			firedBy.lineRenderer.SetPosition(1, firedBy.loc);
			Destroy(gameObject);
		}
	}
}

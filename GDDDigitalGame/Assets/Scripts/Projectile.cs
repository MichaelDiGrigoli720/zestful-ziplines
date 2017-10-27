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
		if(collision.rigidbody != firedBy.fpcRigidBody) {
			if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player 2") {
				Zipline otherPlayerZip = collision.gameObject.GetComponent<Zipline>();
				otherPlayerZip.findLocation(transform.position + transform.forward * 2, transform.rotation);
				//otherPlayerZip.loc = (transform.position - otherPlayerZip.transform.position) + otherPlayerZip.transform.position;
				otherPlayerZip.isFlying = true;
				//otherPlayerZip.fpcRigidBody.velocity = Vector3.zero;
				//otherPlayerZip.FPC.canMove = false;
				Destroy(gameObject);
			}

			else {
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
}

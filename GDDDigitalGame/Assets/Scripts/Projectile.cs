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
}

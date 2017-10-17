using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	public Vector3 startPoint = Vector3.zero;

	private MonoBehaviour moveScript;
	private Transform t;
	private Vector3 initCamPos;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent("RigidbodyFirstPersonController") as MonoBehaviour;
		t = GetComponent<Transform>();
		initCamPos = t.GetChild(0).GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Respawn(float timeOut) {
		yield return new WaitForSeconds(timeOut);
		t.GetChild(0).GetComponent<Transform>().position = initCamPos;
		t.position = startPoint;
		moveScript.enabled = true;
	}
}

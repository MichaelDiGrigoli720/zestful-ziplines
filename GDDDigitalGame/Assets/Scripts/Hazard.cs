using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
    public GameObject text;

	// Use this for initialization
	void Start () {
		this.gameObject.tag = "Hazard";
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
        GameObject gObj = collision.gameObject;
        if (gObj.tag == "Player" || gObj.tag == "Player 2") {
            gObj.GetComponent<Death>().Kill(null);
		}
	}
}

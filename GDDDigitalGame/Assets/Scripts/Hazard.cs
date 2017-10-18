using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	// Use this for initialization
	void Start () {
		this.gameObject.tag = "Hazard";
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
        GameObject gObj = collision.gameObject;
        if (gObj.tag == "Player") {
			//Transform camTransform = gObj.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
			//camTransform.position -= camTransform.forward * 3;

			(gObj.GetComponent("RigidbodyFirstPersonController") as MonoBehaviour).enabled = false;
            IEnumerator coroute = gObj.GetComponent<Death>().Respawn(5.0f);
			StartCoroutine(coroute);
		}
	}
}

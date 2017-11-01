using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hazard : MonoBehaviour {
    private Text text;
    private int tim;

	// Use this for initialization
	void Start () {
		this.gameObject.tag = "Hazard";
        GameObject Canvas = GameObject.Find("Canvas");
        Transform child = Canvas.transform.Find("Text");
        text = child.GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
        GameObject gObj = collision.gameObject;
        if (gObj.tag == "Player" || gObj.tag == "Player 2") {
<<<<<<< HEAD
			//Transform camTransform = gObj.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
			//camTransform.position -= camTransform.forward * 3;

			(gObj.GetComponent("RigidbodyFirstPersonController") as MonoBehaviour).enabled = false;
            text.enabled = true;
            text.text = "You Are Dead. Respawn in " + 5 + " seconds.";
            IEnumerator coroute = gObj.GetComponent<Death>().Respawn(5.0f);
			StartCoroutine(coroute);
=======
            gObj.GetComponent<Death>().Kill(null);
>>>>>>> 1d45b92812262c21e0ad26692da82ea343e3b0ff
		}
	}
}

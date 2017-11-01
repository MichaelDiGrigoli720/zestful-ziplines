using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {
	public Vector3 startPoint = Vector3.zero;

    private bool isDead;
    private Text text;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = IsDead; }
    }

	private MonoBehaviour moveScript;
	//private Zipline zipScript;
    private Transform t;
	private Vector3 initCamPos;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent("RigidbodyFirstPersonController") as MonoBehaviour;
        isDead = false;
        //zipScript = GetComponent<Zipline>();

        t = GetComponent<Transform>();
		initCamPos = t.position;
        GameObject Canvas = GameObject.Find("Canvas");
        Transform child = Canvas.transform.Find("Text");
        text = child.GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Respawn(float timeOut) {
        isDead = true;
		yield return new WaitForSeconds(timeOut);
        //t.GetChild(0).GetComponent<Transform>().position = initCamPos;
        text.enabled = false;
        t.position = startPoint;
		moveScript.enabled = true;
        //zipScript.IsFlying = true;
        GetComponent<Rigidbody>().useGravity = true;
        isDead = false;
    }

    
}

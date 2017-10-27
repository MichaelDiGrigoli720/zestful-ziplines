using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	public Vector3 startPoint = Vector3.zero;

    private bool isDead;

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
	}

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Respawn(float timeOut) {
        isDead = true;
		yield return new WaitForSeconds(timeOut);
		//t.GetChild(0).GetComponent<Transform>().position = initCamPos;
        t.position = startPoint;
		moveScript.enabled = true;
        //zipScript.IsFlying = true;
        GetComponent<Rigidbody>().useGravity = true;
        isDead = false;
    }

    
}

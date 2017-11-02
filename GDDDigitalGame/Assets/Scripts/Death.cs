using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {
	public Vector3 startPoint = Vector3.zero;
    public GameObject gameManager;
    private gameMan GameManager;


    public bool isDead;
    public Text text;

    public float time = 5.0f;


    public bool IsDead
    {
        get { return isDead; }
        set { isDead = IsDead; }
    }

	private MonoBehaviour moveScript;
    private Transform t;
	private Vector3 initCamPos;

	// Use this for initialization
	void Start () {
        GameManager = gameManager.GetComponent<gameMan>();
		moveScript = GetComponent("RigidbodyFirstPersonController") as MonoBehaviour;
        isDead = false;

        t = GetComponent<Transform>();
		initCamPos = t.position;
    }

	// Update is called once per frame
	void Update () {
        if(isDead)
        {
            text.text = "You Are Dead. Respawn in " + ((int)time+1) + " seconds.";
            time -= Time.deltaTime;
        }

	}

    public void Kill()
    {
        GameManager.incrementPlayerScore(gameObject);

        if (!isDead)
        {
            time = 5.0f;

            (gameObject.GetComponent("RigidbodyFirstPersonController") as MonoBehaviour).enabled = false;
            text.text = "You Are Dead. Respawn in " + time + " seconds.";
            text.enabled = true;
            IEnumerator coroute = Respawn(time);
            StartCoroutine(coroute);
        }
    }

	public IEnumerator Respawn(float timeOut) {
        isDead = true;
        yield return new WaitForSeconds(timeOut);
        text.enabled = false;
        t.position = startPoint;
		moveScript.enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        isDead = false;
    }


}

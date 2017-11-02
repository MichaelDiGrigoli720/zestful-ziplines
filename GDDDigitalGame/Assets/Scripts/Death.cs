using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {
	public Vector3 startPoint = Vector3.zero;
    public GameObject gameManager;
    private gameMan GameManager;
    
<<<<<<< HEAD
    private bool isDead;
    private Text text;
=======
    public Text text;
    public bool isDead;
    public float tim = 5.0f;
>>>>>>> e208657246f7d939259ba8b2e7826a6b0b55b91a

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
        GameManager = gameManager.GetComponent<gameMan>();
		moveScript = GetComponent("RigidbodyFirstPersonController") as MonoBehaviour;
        isDead = false;
        //zipScript = GetComponent<Zipline>();

        t = GetComponent<Transform>();
		initCamPos = t.position;
    }

	// Update is called once per frame
	void Update () {
        if(isDead)
        {
            text.text = "You Are Dead. Respawn in " + ((int)tim+1) + " seconds.";
            tim -= Time.deltaTime;
        }

	}

    public void Kill()
    {
<<<<<<< HEAD
    
        GameManager.incrementPlayerScore(gameObject);
=======
        if (!isDead)
        {
            tim = 5.0f;
            if (playerWhoKilled != null)
                GameManager.incrementPlayerScore(playerWhoKilled);
>>>>>>> e208657246f7d939259ba8b2e7826a6b0b55b91a

            (gameObject.GetComponent("RigidbodyFirstPersonController") as MonoBehaviour).enabled = false;
            text.text = "You Are Dead. Respawn in " + tim + " seconds.";
            text.enabled = true;
            IEnumerator coroute = Respawn(tim);
            StartCoroutine(coroute);
        }
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

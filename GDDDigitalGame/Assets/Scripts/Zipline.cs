using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Zipline : MonoBehaviour
{
	public Camera cam; //Player Camera
	public GameObject projectile; //Projectile prefab
	public int maxRange = 99999;
	public bool isFlying; //Checks if they are in the air
	public Vector3 loc; //Location of the hit point
	public float speed = 10.0f; //Speed of the movement along the zipline
	public float reloadTime = 1.0f; //Time before being able to shoot again
	public Transform ziplineStart; //Zipline starting point (Hand for now)
	public RigidbodyFirstPersonController FPC; // The FPS controller
	public LineRenderer lineRenderer; //Zipline line

	private float elapsedTime = 0.0f; //Time since last shot
	public Rigidbody fpcRigidBody; // the FPC Rigidbody component
	private GameObject curProj; //The current projectile shot

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		fpcRigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Mouse0) && !isFlying && elapsedTime == 0.0f)
		{
			findLocation();
			elapsedTime = reloadTime;
		}

		if(isFlying)
		{
			fpcRigidBody.useGravity = false;
			//transform.position = Vector3.MoveTowards(transform.position, loc, speed * Time.deltaTime);

			fpcRigidBody.velocity = Vector3.zero;
			Vector3 pathNorm = loc - transform.position;
			pathNorm = pathNorm.normalized;
			fpcRigidBody.velocity = pathNorm * speed;

			lineRenderer.SetPosition(0, ziplineStart.position);
			//elapsedtime += Time.deltaTime;

			if (Vector3.Distance(transform.position, loc) < 5 && !Input.GetKey(KeyCode.Mouse0))
			{
				isFlying = false;
				FPC.canMove = true;
				lineRenderer.enabled = false;
			}
		}

		else if(!isFlying && fpcRigidBody.useGravity == false)
		{
			//fpcRigidBody.useGravity = true;
			fpcRigidBody.drag = 5;
		}

		if (Input.GetKey(KeyCode.LeftControl) && isFlying)
		{
			isFlying = false;
			FPC.canMove = true;
			lineRenderer.enabled = false;
			fpcRigidBody.drag = 5;
		}

		if(elapsedTime != 0)
		{
			elapsedTime -= Time.deltaTime;

			if(elapsedTime <= 0)
			{
				elapsedTime = 0;
			}
		}
	}

	public void findLocation()
	{
		curProj = (GameObject)Instantiate(projectile, FPC.transform.position + (cam.transform.forward * 4) + new Vector3(0.0f, 0.75f, 0.0f), cam.transform.rotation);
		curProj.GetComponent<Projectile>().firedBy = this;
		Destroy(curProj, 5.0f);
	}
}

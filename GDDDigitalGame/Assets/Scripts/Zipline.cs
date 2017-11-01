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
	private RaycastHit hit;
	private bool doCast = false;
	private bool hooked = false;
	//private Vector3 hookedLoc = Vector3.zero;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		fpcRigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
        if(this.gameObject.tag == "Player" && !gameObject.GetComponent<Death>().IsDead)
        {
            if ((Input.GetAxis("Fire1") == 1) && !isFlying && elapsedTime == 0.0f)
            {
				spawnProj(cam.transform.position, cam.transform.rotation);
                //findLocation(FPC.transform.position + (cam.transform.forward * 4) + new Vector3(0.0f, 0.75f, 0.0f), cam.transform.rotation);
                elapsedTime = reloadTime;
            }
        }

        if (this.gameObject.tag == "Player 2" && !gameObject.GetComponent<Death>().IsDead)
        {
            if ((Input.GetAxis("Fire2") == 1) && !isFlying && elapsedTime == 0.0f)
            {
				spawnProj(cam.transform.position, cam.transform.rotation);
				doCast = true;
                elapsedTime = reloadTime;
            }
        }

        if(doCast)
			findLocation();

        if (isFlying)
		{
			fpcRigidBody.useGravity = false;
			//transform.position = Vector3.MoveTowards(transform.position, loc, speed * Time.deltaTime);

			fpcRigidBody.velocity = Vector3.zero;
			Vector3 pathNorm = loc - transform.position;
			pathNorm = pathNorm.normalized;
			fpcRigidBody.velocity = pathNorm * speed;

            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            lineRenderer.SetPosition(0, ziplineStart.position);
			//elapsedtime += Time.deltaTime;

			if (Vector3.Distance(transform.position, loc) < 5 && !Input.GetButton("Fire1"))
			{
				isFlying = false;
				FPC.canMove = true;
				lineRenderer.enabled = false;
				hooked = false;
			}
		}

		else if(!isFlying && fpcRigidBody.useGravity == false)
		{
			//fpcRigidBody.useGravity = true;
			fpcRigidBody.drag = 5;
		}

		if ((Input.GetAxis("ZipDisengage") == 1) && isFlying && gameObject.tag == "Player")
		{
			isFlying = false;
			FPC.canMove = true;
			lineRenderer.enabled = false;
			fpcRigidBody.drag = 5;
			hooked = false;
		}
        else if ((Input.GetAxis("ZipDisengage2") == 1) && isFlying && gameObject.tag == "Player 2")
        {
            isFlying = false;
            FPC.canMove = true;
            lineRenderer.enabled = false;
            fpcRigidBody.drag = 5;
            hooked = false;
        }

        if (elapsedTime != 0)
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
		//curProj.GetComponent<Projectile>().firedBy = this;

		if(hooked)
		{
			//loc = hit.point;
			//isFlying = true;
			//fpcRigidBody.velocity = Vector3.zero;
			//FPC.canMove = false;
			//lineRenderer.enabled = true;
			//doCast = false;
			if(curProj != null)
				lineRenderer.SetPosition(1, curProj.transform.position);
		}
		else
		{
            if(curProj == null)
            {
                return;
            }
			if(Physics.Raycast(cam.transform.position, curProj.transform.position - cam.transform.position, out hit))
			{
				if(hit.collider.tag == "Player" || hit.collider.tag == "Player 2")
				{
                    if (hit.collider.GetComponent<advancedMovement>().collided == true && !hit.collider.GetComponent<Death>().isDead)
                    {
                        hit.collider.GetComponent<Death>().Kill(gameObject);
                        return;
                    }

					Zipline otherPlayerZip = hit.collider.gameObject.GetComponent<Zipline>();
					RaycastHit hit2;
					if(Physics.Raycast(otherPlayerZip.cam.transform.position, curProj.transform.position - cam.transform.position, out hit2))
					{
						otherPlayerZip.loc = hit2.point;
						otherPlayerZip.isFlying = true;
						otherPlayerZip.fpcRigidBody.velocity = Vector3.zero;
						otherPlayerZip.FPC.canMove = false;
						otherPlayerZip.lineRenderer.SetPosition(1, curProj.transform.position);
						otherPlayerZip.lineRenderer.enabled = true;
						otherPlayerZip.hooked = true;
						otherPlayerZip.spawnProj(otherPlayerZip.transform.position, curProj.transform.rotation);
						otherPlayerZip.doCast = true;
						doCast = false;
					}
				}

				else if(hit.collider.tag != "Projectile")
				{
					loc = hit.point;
					isFlying = true;
					fpcRigidBody.velocity = Vector3.zero;
					FPC.canMove = false;
					lineRenderer.enabled = true;
					lineRenderer.SetPosition(1, hit.point);
					doCast = false;
				}
			}
		}
	}

	private void spawnProj(Vector3 pos, Quaternion rot) {
		curProj = (GameObject)Instantiate(projectile, pos + (cam.transform.forward * 4) + new Vector3(0.0f, 0.75f, 0.0f), rot);
		doCast = true;
		Destroy(curProj, 5.0f);
	}
}

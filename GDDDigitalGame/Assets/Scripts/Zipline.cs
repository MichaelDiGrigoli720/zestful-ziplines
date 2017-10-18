using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Zipline : MonoBehaviour
{
    public Camera cam;
    public RaycastHit hit;

    public LayerMask cullingMask;

    public int maxRange = 99999;
    public bool IsFlying;
    public Vector3 loc;

    public float reloadTime = 1.0f;
    private float elapsedTime = 0.0f;

    public float speed = 10;
    
    //Zipline starting point (Hand for now)
    public Transform ziplineStart;

    public RigidbodyFirstPersonController FPC;
    private Rigidbody fpcRigidBody;

    //Zipline line
    public LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        fpcRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.DrawRay(cam.transform.position, Vector3.Lerp(transform.position, loc, (elapsedtime / maxHangTime)), Color.green);
        if (Input.GetKey(KeyCode.Mouse0) && !IsFlying && elapsedTime == 0.0f)
        {
            print("Finding Loc");
            findLocation();
            elapsedTime = reloadTime;
        }

        if(IsFlying)
        {
            fpcRigidBody.useGravity = false;
            transform.position = Vector3.MoveTowards(transform.position, loc, speed * Time.deltaTime);

            lineRenderer.SetPosition(0, ziplineStart.position);

            //elapsedtime += Time.deltaTime;

            if (Vector3.Distance(transform.position, loc) < 5 && !Input.GetKey(KeyCode.Mouse0))
            {
                IsFlying = false;
                FPC.canMove = true;
                lineRenderer.enabled = false;
            }
        }

        else if(!IsFlying && fpcRigidBody.useGravity == false)
        {
            fpcRigidBody.useGravity = true;
        }

        if (Input.GetKey(KeyCode.Space) && IsFlying)
        {
            IsFlying = false;
            FPC.canMove = true;
            lineRenderer.enabled = false;
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
        print("Finding...");
        print(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxRange, cullingMask));
        //Raycasting to find where the zipline goes

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxRange, cullingMask))
        {
            //Get the location of the hit
            loc = hit.point;

            //Set the player to flying
            IsFlying = true;

            print(IsFlying + " " + loc);

            //Make the player unable to move
            FPC.canMove = false;

            //Draw the line
            lineRenderer.enabled = enabled;
            lineRenderer.SetPosition(1, loc);
        }
    }
}

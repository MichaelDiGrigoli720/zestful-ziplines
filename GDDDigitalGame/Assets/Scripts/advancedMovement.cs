using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class advancedMovement : MonoBehaviour {

    private float distToGround;
    private bool collided;
    private bool spaceDown;
    private bool wallRunning;
    private float myY;
    private float thrust;
    private Rigidbody rb;
    private float jumpForce = 30;
    public bool doubleJump;
    private Vector3 dir;
    private bool jumpCom;

    public void OnCollisionEnter(Collision c)
    {
        collided = true;
        // Calculate Angle Between the collision point and the player
        dir = c.contacts[0].point - transform.position;
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;

    }

    public void OnCollisionExit()
    {
        collided = false;

    }

    // Use this for initialization
    void Start () {
        Collider collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;
        thrust = 30f;
        doubleJump = true;
        jumpCom = false;
        rb = GetComponent<Rigidbody>();
    }

    bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit);
        return (hit.distance < distToGround + .01);
        
    }

    bool checkSpaceDown()
    {
        return Input.GetKey("space");
    }
    bool checkJump()
    {
        return Input.GetKeyDown("space");
    }

    void jump()
    {
        rb.drag = 0f;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector2 input = GetInput();
        Vector3 Dir = new Vector3(input.x * (thrust) * -1, 0, input.y * (thrust) * -1);
        rb.AddForce(Dir, ForceMode.Impulse);
        rb.AddForce(new Vector3(0f,jumpForce, 0f), ForceMode.Impulse);
        
    }
	
	// Update is called once per frame
	void Update () {
        spaceDown = checkSpaceDown();
        //Advanced movement is for aerial stuff
        if (!isGrounded())
        {
            jumpCom = checkJump();
            //Checks for start wallrun
            if(collided && !wallRunning && spaceDown)
            {
                wallRunning = true;
                myY = gameObject.transform.position.y;
                doubleJump = true;
                
            }
            if(!wallRunning && jumpCom && doubleJump)
            {
                jump();
                
                doubleJump = false;
            }
            //Makes wallrun work
            if (wallRunning)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, myY, gameObject.transform.position.z);
                rb.velocity = new Vector3(rb.velocity.x, 0f,rb.velocity.z);
            }
            //checks for end wall run
            if(wallRunning && !spaceDown)
            {
                
                wallRunning = false;
                rb.AddForce(dir * thrust, ForceMode.Impulse);
                jump();
                Debug.Log("endWallrun");
            }
            //checks if no longer colliding to end wallrun
            if (!collided)
            {
                wallRunning = false;
             
            }
               
        }
        if (isGrounded())
        {
            wallRunning = false;
            doubleJump = true;
        }


        

	}

    private Vector2 GetInput()
    {

        Vector2 input = new Vector2
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal"),
            y = CrossPlatformInputManager.GetAxis("Vertical")
        };
        
        return input;
    }
}

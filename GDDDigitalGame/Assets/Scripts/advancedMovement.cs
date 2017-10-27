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
    private Rigidbody rb;
    private Vector3 dir;
    private bool jumpCom;
    private Zipline zip;
    public bool doubleJump;

    public float thrust = 60;
    public float jumpForce = 60;
    public Camera cam;

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
        doubleJump = true;
        jumpCom = false;
        rb = GetComponent<Rigidbody>();
        zip = this.gameObject.GetComponent<Zipline>();
    }

    bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit);
        return (hit.distance < distToGround + .01);
        
    }

    bool checkSpaceDown()
    {
        if(gameObject.tag == "Player")
        {
            return Input.GetButton("Jump");
        } else if (gameObject.tag == "Player 2")
        {
            return Input.GetButton("Jump2");
        }

        return false;
    }
    bool checkLCtrlDown()
    {
        if (gameObject.tag == "Player")
        {
            return Input.GetButton("WallDisengage");
        }
        else if (gameObject.tag == "Player 2")
        {
            return Input.GetButton("WallDisengage2");
        }

        return false;
    }
    bool checkJump()
    {
        if (this.gameObject.tag == "Player")
        {
            return Input.GetButtonDown("Jump");
        }
        else if (this.gameObject.tag == "Player 2")
        {
            return Input.GetButtonDown("Jump2");
        }

        return false;
    }

    void jump()
    {
        rb.drag = 0f;
        rb.velocity = new Vector3(0f, 0f, 0f);
        Vector2 input = GetInput();
        Vector3 jumpVec = new Vector3(0, jumpForce, 0);
        Vector3 Dir = cam.transform.forward * thrust * input.y + cam.transform.right * thrust * input.x + jumpVec;
        rb.AddForce(Dir, ForceMode.Impulse);
        
    }
	
	// Update is called once per frame
	void Update () {
        spaceDown = !checkSpaceDown(); // this is specifically for wallrunning
        bool lctrlDown = checkLCtrlDown();
        //Advanced movement is for aerial stuff
        if (!isGrounded() && !gameObject.GetComponent<Death>().IsDead)
        {
            jumpCom = checkJump();
            //Checks for start wallrun
            if(collided && !wallRunning && spaceDown)
            {
                wallRunning = true;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                doubleJump = true;
                
            }
            if(!wallRunning && jumpCom && doubleJump && !zip.isFlying)
            {
                jump();
                
                doubleJump = false;
            }
            //Makes wallrun work
            if (wallRunning)
            {
                rb.useGravity = false;
                rb.AddForce(Physics.gravity * 0.5f);
                
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x, myY, gameObject.transform.position.z);
                //rb.velocity = new Vector3(rb.velocity.x, 0f,rb.velocity.z);
            }
            if(!wallRunning && !zip.isFlying)
            {
                rb.useGravity = true;
            }
            //checks for end wall run
            if(wallRunning && !spaceDown)
            {
                wallRunning = false;
                rb.AddForce(dir * thrust, ForceMode.Impulse);
                jump();
                
            }
            if(wallRunning && lctrlDown)
            {
                wallRunning = false;
                rb.AddForce(dir * (thrust / 10), ForceMode.Impulse);
                
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
        Vector2 input = new Vector2();

        if (this.gameObject.tag == "Player")
        {
            input = new Vector2
            {
                x = CrossPlatformInputManager.GetAxis("Horizontal1"),
                y = CrossPlatformInputManager.GetAxis("Vertical1")
            };
        }
        else if (this.gameObject.tag == "Player 2")
        {
            input = new Vector2
            {
                x = CrossPlatformInputManager.GetAxis("Horizontal2"),
                y = CrossPlatformInputManager.GetAxis("Vertical2")
            };
        }

        return input;
    }
}

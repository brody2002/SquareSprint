using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxMover : MonoBehaviour
{

    public float jumpForce = 100;
    public float horizontalVelocity = 5;

    private int layerMask;

    public bool canJump = true;
    private float v = 0;
    private Vector2 position;

    Rigidbody2D body;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // include only layer 6
        layerMask = 1 << 6;
    }
    // Update is called once per frame
    /*void Update()
    {
        //jump if we can jump and we are touching the ground and the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }*/

    void FixedUpdate()
    {
        // set a constant velocity to the right
        var velocity = body.velocity;
        Vector3 newVelocity = new Vector3(horizontalVelocity, velocity.y);
        body.velocity = newVelocity;
        
    }

    public void Jump(){
        var velocity = body.velocity;
        var newVelocity = new Vector2(velocity.x, 0);
        //jump only if we are touching the ground
        if (isTouchingGround())
        {
            body.velocity = newVelocity;
            v = jumpForce;
            Vector3 tempVect = new Vector3(0, v, 0);
            body.AddRelativeForce(tempVect);
        }
    }

    // check if we are touching the ground by raycasting down
    public bool isTouchingGround(){
        float extraheight = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraheight, layerMask);
        return hit.collider != null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : PhysicBase
{
    bool facingRight;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = 0;
        if (Input.GetAxis("Horizontal") > 0 && !hitright)
        {
            move.x = 5f;
            if (hitleft)
            {
                this.hitleft = false;
            }
            if (!facingRight)
            {
                flip();
            }
        }
        else if (Input.GetAxis("Horizontal") < 0 && !hitleft)
        {
            if (hitright)
            {
                this.hitright = false;
            }
            move.x = -5f;
            if (facingRight)
            {
                flip();
            }
        }
        else { this.desiredx = 0; GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);  }

        if (Input.GetButton("Jump") && hitdown) {
            hitdown = false;
            //Vector2 move = new Vector3(0f, 50f);
            //Movement(move);
            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 175));
            //transform.position += move * Time.fixedDeltaTime;
        }

    }

    public void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public Vector2 move;
    public float moveForce = 5f;
    // public float desiredy;
    public Vector2 velocity;
    public bool hitup;
    public bool hitdown;
    public bool hitright;
    public bool hitleft;
    public Rigidbody2D body;
    public ContactPoint2D[] contactPoints;
    bool facingRight;
    [SerializeField] float jumpForce = 10f;
    //bool jump;

    // Start is called before the first frame update
    public virtual void Start()
    {
        hitup = false;
        hitdown = false;
        hitright = false;
        hitleft = false;
        body = GetComponent<Rigidbody2D>();
        Debug.Log("Hello Depression");
        contactPoints = new ContactPoint2D[4];
        facingRight = true;
    }

    private void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Movement(readInput());
        jump();
        velocity = body.velocity;
    }

    //Deberia manejar el movimiento en x y el movimiento en y, PlayerScript solo debe mandar los cambios. Se puede hacer abstract?

    public void Movement(float direction)
    {
        //direction.x = direction.x * Time.fixedDeltaTime;
        /*if (direction.y == 0)
        {
            direction.y = body.velocity.y;
        }*/

        // Move by setting the rigidbody's velocity
        //body.velocity = direction;
        direction *= Time.fixedDeltaTime;
        transform.position += new Vector3(direction,0,0);

    }

    float readInput() {
        if (Input.GetAxis("Horizontal") > 0 && !hitright)
        {
            if (hitleft)
            {
                this.hitleft = false;
            }
            if (!facingRight)
            {
                flip();
            }
            return moveForce;
        }
        else if (Input.GetAxis("Horizontal") < 0 && !hitleft)
        {
            if (hitright)
            {
                this.hitright = false;
            }
            if (facingRight)
            {
                flip();
            }
            return -moveForce;
        }
        else {
            //body.velocity = new Vector2(0, body.velocity.y);
            return 0f;
        }
    }

    public void jump() {
        if (Input.GetButton("Jump") && hitdown)
        {
            hitdown = false;
            //move.y = 10f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Checks for side collisions
    private void sideChecking(int contactCount)
    {

        for (int i = 0; i < contactCount; ++i)
        {
            Debug.Log("Contact in X: " + contactPoints[i].normal.x + " Y: " + contactPoints[i].normal.y);
            Debug.Log("contact: " + contactPoints[i].collider.gameObject.name);
            Debug.Log("Number of Contacts: " + contactCount);

            /*contactPoint[x].normal.x/y devuelve el lado del objeto contra el que estoy colisionando
              Si pego contra el lado izquierdo de un objeto con mi lado derecho, lo que se retorna es un -1, el lado
              izquierdo de lo que estoy pegando. 

              Los siguientes if-elses entonces estan "invertidos". Si y me devuelve un -1 es porque estoy colisionado contra
              el lado de abajo del objeto, por lo tanto estoy pegando con el lado superior de mi personaje.
            */

            if (contactPoints[i].normal.y == 1)
            {
                hitdown = true;
            }

            if (contactPoints[i].normal.x == -1)
            {
                //print("The object collided with the right side of the ball!");
                hitright = true;
            }
            else if (contactPoints[i].normal.x == 1)
            {
                //print("The object collided with the left side of the ball!");
                hitleft = true;
            }

        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {

        Debug.Log("I am " + gameObject.name);
        // Debug.Log("Collision item rigidbody is: " + col.rigidbody.tag);
        Debug.Log("Collision item otherrigidbody is: " + col.otherRigidbody.gameObject.tag);


        //ContactPoint2D[] contactPoints = new ContactPoint2D[col.contactCount];
        int contactCount = col.GetContacts(contactPoints);
        sideChecking(contactCount);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("I am " + gameObject.name);
        // Debug.Log("Collision item rigidbody is: " + col.rigidbody.tag);
        Debug.Log("Collision item otherrigidbody is: " + col.otherRigidbody.gameObject.tag);


        int contactCount = col.GetContacts(contactPoints);
        sideChecking(contactCount);
    }

    //Checks if any collision is still occurring from sides and returns movements if not
    private void OnCollisionExit2D(Collision2D col)
    {
        int contactCount = col.GetContacts(contactPoints);
        if (contactCount != 0) //There are collisions happening, check them and set the rest to false
        {
            for (int i = 0; i < contactCount; ++i)
            {
                Debug.Log("Contact in X: " + contactPoints[i].normal.x + " Y: " + contactPoints[i].normal.y);
                Debug.Log("contact: " + contactPoints[i].collider.gameObject.name);
                Debug.Log("Number of Contacts: " + contactCount);

                if (contactPoints[i].normal.y == 1)
                {
                    hitdown = true;
                }
                else
                {
                    hitdown = false;
                }

                if (contactPoints[i].normal.x == -1)
                {
                    //print("The object collided with the right side of the ball!");
                    hitright = true;
                }
                else
                {
                    hitright = false;
                }

                if (contactPoints[i].normal.x == 1)
                {
                    //print("The object collided with the left side of the ball!");
                    hitleft = true;
                }
                else
                {
                    hitdown = false;
                }

            }
        }
        else
        { //collisionCount is 0, therefore no collisions are happening
            hitdown = hitup = hitright = hitleft = false;
        }

    }

}

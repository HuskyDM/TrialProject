using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PhysicBase : MonoBehaviour
{
    public Vector2 move;
    public float desiredx;
    public float desiredy;
    public Vector2 velocity;
    public bool hitup;
    public bool hitdown;
    public bool hitright;
    public bool hitleft;
    public Rigidbody2D body;
    public ContactPoint2D[] contactPoints;

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
    }

    private void FixedUpdate()
    {       
        
        Movement(new Vector2(move.x, 0));
        velocity = body.velocity;
    }

    public void Movement(Vector2 move)
    {
        move *= Time.fixedDeltaTime;
        transform.position += (Vector3)(move); //Deberia ser add force para que las fisicas funcionen
        //GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(move.x, 0));
    }

   
    void Update()
    {
       
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

    

    private void hittingDown() {
        if (velocity.y == 0)
        {
            // move.y = 0;
            //velocity.y = 0;
            hitdown = true;
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
        else { //collisionCount is 0, therefore no collisions are happening
            hitdown = hitup = hitright = hitleft = false;
        }
    
    }

}

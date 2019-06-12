using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public Vector2 move;
    public float desiredx = 9f;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * desiredx;
    }

    // Update is called once per frame
    /*void Update()
    {
        velocity += Physics2D.gravity * Time.deltaTime;
        velocity.x = desiredx;
        move = velocity * Time.deltaTime;
        transform.position += (Vector3)(move);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}

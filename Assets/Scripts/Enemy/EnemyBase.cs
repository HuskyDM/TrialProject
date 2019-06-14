using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [SerializeField]int health;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if PlayerBullet touched it

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("This enemy's health is " + health);
    }

    void checkZeroHealth() {
        if (health == 0) {
            Destroy(this);
        }
    }
}

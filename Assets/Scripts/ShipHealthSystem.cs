using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthSystem : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float health = 5;
    public float maxHealth = 5;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }
}

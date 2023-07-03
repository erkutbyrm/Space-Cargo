using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float currentSpeed = 0f;
    public float maxSpeed = 20f;
    public float accelPercentage = 2f;
    public float decelPercentage = 1f;
    public Rigidbody2D rigidBody;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
            );
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    

    private void FixedUpdate()
    {
        if (movement.y > 0 && currentSpeed < maxSpeed)
        {
            currentSpeed += accelPercentage * Time.deltaTime;
        }
        else if (movement.y < 0)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= decelPercentage * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
        }
        else
        {
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
            }
        }
        rigidBody.MovePosition(rigidBody.position + (Vector2)transform.right * currentSpeed * Time.fixedDeltaTime);
    }
}

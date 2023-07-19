using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMace : MonoBehaviour
{
    public float speed = 2;
    int dir = 1;

    public Transform rightCheck;
    public Transform leftCheck;

   
    void FixedUpdate()
    {
        // Move horizontally
        transform.Translate(Vector2.right * speed * dir * Time.fixedDeltaTime);

        // If is not touching the ground on the right side, change direction to the left
        if (Physics2D.Raycast(rightCheck.position, Vector2.down, 2) == false)
        {
            dir = -1;
        }

        // If is not touching the ground on the left side, change direction to the right
        if (Physics2D.Raycast(leftCheck.position, Vector2.down, 2) == false)
        {
            dir = 1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Input : MonoBehaviour
{
    public const int numRays = 32;
    private float[] distances = new float[numRays];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numRays; i++)
        {
            distances[i] = 0;
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.down;
        const float rotationIncrement = 180.0f / (numRays-1); // degrees
        Quaternion rotation = Quaternion.Euler(0,0, rotationIncrement);
        // now we ray cast numRays of rays starting from the bottom in even increments up to the top
        for (int i = 0; i < numRays; i++)
        {
            Color hitColor;
            var hit = Physics2D.Raycast(transform.position, direction, 999999999, (1 << 6) | (1 << 7));
            if(hit.collider != null)
            {
                distances[i] = hit.distance;
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.layer == 6)
                    {
                        hitColor = Color.blue;
                    }
                    else if (hit.collider.gameObject.layer == 7)
                    {
                        hitColor = Color.red;
                    } else
                    {
                        hitColor = Color.black;
                    }

                    Debug.DrawLine(transform.position, hit.point, hitColor);
                }

            }
            else
            {
                distances[i] = 999999999;
                Debug.DrawRay(transform.position, direction, Color.green);
            }
            // now rotate the direction vector by rotationIncrement
            direction = rotation * direction;
        }
    }
}

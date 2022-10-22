using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float positionX;
    bool moveright = true;

    public float movedistance = 5;
    public float platformspeed;

    void Start()
    {
        int random = Random.Range(0, 10);
        if (random > 5)
        {
            moveright = true;
        }
        else {
            moveright = false;
        }
        positionX = transform.position.x;
    }

    void Update()
    {
        if (moveright)
        {
            transform.position += new Vector3(platformspeed * Time.deltaTime, 0, 0);

            if (transform.position.x > positionX + movedistance)
            {
                moveright = false;
            }
        }
        else if (!moveright){
            transform.position -= new Vector3(platformspeed * Time.deltaTime, 0, 0);
            if (transform.position.x < positionX - movedistance) {
                moveright = true;
            }
        }
    }
}

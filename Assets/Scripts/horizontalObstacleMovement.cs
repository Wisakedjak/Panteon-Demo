using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalObstacleMovement : MonoBehaviour
{
    public bool isLeft = false, isRight = true;
    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "rightLine")
        {
            isRight = false;
            isLeft = true;
        }

        if (collision.gameObject.tag == "leftLine")
        {
            isLeft = false;
            isRight = true;
        }


    }

    void FixedUpdate()
    {
        //sawParent.gameObject.transform.Rotate(50, 0, 0);
        if (isLeft == true)
        {
            this.gameObject.transform.Translate(3f * Time.deltaTime, 0, 0);

        }
        else if (isRight == true)
        {
            this.gameObject.transform.Translate(-3f * Time.deltaTime, 0, 0);
        }
    }
}

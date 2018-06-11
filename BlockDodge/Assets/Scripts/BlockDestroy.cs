using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    float camHeightHalf, rotateAngle;

    private void Start()
    {
        //Calculate half the cam height
        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;

        rotateAngle = Random.Range(-1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Block")
        {
            transform.Rotate(new Vector3(0f, 0f, 1f), rotateAngle);
        }

        //If the GO move 2 units below the screen, player survived this wave. So destroy it and update the score
        if (transform.position.y < (-camHeightHalf - 2f))
        {
            Destroy(gameObject);

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateScore(10);
        }

    }
}

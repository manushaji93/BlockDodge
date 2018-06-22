using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    float camHeightHalf;

    private void Start()
    {
        //Calculate half the cam height
        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {

        //If the GO move 2 units below the screen, player survived this wave. So destroy it and update the score
        if (transform.position.y < (-camHeightHalf - 2f))
        {
            Destroy(gameObject);

            if (gameObject.tag != "Life" && gameObject.tag != "Shield" && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notInGame)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateScore(10);
            }
        }

    }
}

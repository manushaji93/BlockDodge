using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour {

    float camHeightHalf;

    private void Start()
    {
        
        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;

    }

    // Update is called once per frame
    void Update () {
		
        if (transform.position.y < (-camHeightHalf-2f))
        {
            Destroy(gameObject);

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().UpdateScore(10);
        }

	}
}

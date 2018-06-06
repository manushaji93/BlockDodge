using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour {

    public Transform[] blocks;

    public GameObject blockPrefab;

    float timeToSpawn, spawnRate;

    int randomInt;
	// Use this for initialization
	void Start () {

        timeToSpawn = 2f;
        spawnRate = 1f;

	}
	
	// Update is called once per frame
	void Update () {
		
        if (Time.time >= timeToSpawn)
        {
            SpawnWave();

            timeToSpawn = Time.time + spawnRate;

        }

	}

    void SpawnWave()
    {
        randomInt = Random.Range(0, blocks.Length);

        for (int i = 0; i < blocks.Length; i++)
        {
            if (i != randomInt)
            {
                GameObject block =  Instantiate(blockPrefab, blocks[i].position,Quaternion.identity);
                block.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour {

    public Transform[] blocks;

    public GameObject blockPrefab, lifePrefab;

    GameManager gm;

    float timeToSpawn, spawnRate, lifeSpawnedAt;

    int spacePos, lifePos, lifeChance;

    bool spawnedLife;
	// Use this for initialization
	void Start () {
        lifeSpawnedAt = 0f;
        timeToSpawn = 2f;
        spawnRate = 1f;

        spawnedLife = false;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= lifeSpawnedAt + (spawnRate * 1.5f))
            spawnedLife = false;

        if (Time.time >= timeToSpawn)
        {
            SpawnWave();

            timeToSpawn = Time.time + spawnRate;

        }

	}

    void SpawnWave()
    {
        spacePos = Random.Range(0, blocks.Length);
        lifeChance = Random.Range(0 , 100);
        lifePos = Random.Range(0, blocks.Length);

        while (lifePos == spacePos)
        {
            lifePos = Random.Range(0, blocks.Length);
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (i != spacePos)
            {
                if (i == lifePos && gm.lives < 3 && lifeChance < 25 && !spawnedLife)
                {
                    GameObject life = Instantiate(lifePrefab, blocks[i].position, Quaternion.identity);
                    life.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    spawnedLife = true;
                    lifeSpawnedAt = Time.time;
                }
                else
                {
                    GameObject block = Instantiate(blockPrefab, blocks[i].position, Quaternion.identity);
                    block.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                }
            }

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{

    //Array of spawn points.
    public Transform[] blocks;

    public GameObject blockPrefab, lifePrefab;

    GameManager gm;

    float timeToSpawn, spawnRate, lifeSpawnedAt;

    int spacePos, lifePos, lifeChance;

    bool spawnedLife;
    // Use this for initialization
    void Start()
    {

        //Last time a life collectible was spawned.
        lifeSpawnedAt = 0f;

        //Time to spawn the next wave of blocks.
        timeToSpawn = 2f;

        //How often should we spawn the block waves.
        spawnRate = 1f;

        //Not spawned a life collectible yet.
        spawnedLife = false;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        //Used to spawn a life collectible only if one and a half block wave has passed.
        if (Time.time >= lifeSpawnedAt + (spawnRate * 1.5f))
            spawnedLife = false;

        //Spawn the next wave based on the spawn rate.
        if (Time.time >= timeToSpawn)
        {
            SpawnWave();

            timeToSpawn = Time.time + spawnRate;

        }

    }

    void SpawnWave()
    {
        //Get a position to spawn a space in the wave.
        spacePos = Random.Range(0, blocks.Length);

        //Chance of sawning a life collectible.
        lifeChance = Random.Range(0, 100);

        //Get a position to spawn a life collectible in the wave.
        lifePos = Random.Range(0, blocks.Length);

        //Re-roll if the position for the life collectible is the same as the position for the space.
        while (lifePos == spacePos)
        {
            lifePos = Random.Range(0, blocks.Length);
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (i != spacePos)
            {
                //If the player has less than 3 lives left, and is eligible to spawn a life collectible at this position, spawn it.
                if (i == lifePos && gm.lives < 3 && lifeChance < 25 && !spawnedLife)
                {
                    GameObject life = Instantiate(lifePrefab, blocks[i].position, Quaternion.identity);
                    life.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    spawnedLife = true;
                    lifeSpawnedAt = Time.time;
                }

                //Otherwise spawn a block instead.
                else
                {
                    GameObject block = Instantiate(blockPrefab, blocks[i].position, Quaternion.identity);
                    block.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                }
            }

        }
    }

}

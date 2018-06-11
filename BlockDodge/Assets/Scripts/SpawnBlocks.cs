using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{

    //Array of spawn points.
    public Transform[] blocks;

    public GameObject blockPrefab, lifePrefab;

    GameManager gm;

    InitialSetup isScript;

    float timeToSpawn, lifeSpawnedAt, velocity , twoSpacingUnit, fourSpacingUnit;
    public float spawnRate;

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
        spawnRate = 2f;

        //Not spawned a life collectible yet.
        spawnedLife = false;

        //Gravity on the blocks. Use this to control how fast they fall down.
        velocity = 5f;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        isScript = GameObject.Find("Initial Setup").GetComponent<InitialSetup>();

        twoSpacingUnit = isScript.spacingUnit * 2f;

        fourSpacingUnit = isScript.spacingUnit * 4f;

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
            //float v = Random.Range(velocity - 2f, velocity + 2f);

            if (i != spacePos)
            {
                //If the player has less than 3 lives left, and is eligible to spawn a life collectible at this position, spawn it.
                if (i == lifePos && gm.lives < 3 && lifeChance < 25 && !spawnedLife)
                {
                    GameObject life = Instantiate(lifePrefab, blocks[i].position, Quaternion.identity);
                    life.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocity;
                    spawnedLife = true;
                    lifeSpawnedAt = Time.time;
                    life.transform.localScale = new Vector2(twoSpacingUnit, twoSpacingUnit);
                }

                //Otherwise spawn a block instead.
                else
                {
                    GameObject block = Instantiate(blockPrefab, blocks[i].position, Quaternion.identity);
                    block.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocity;
                    block.transform.localScale = new Vector2(fourSpacingUnit, twoSpacingUnit);
                }
            }

        }
    }

}

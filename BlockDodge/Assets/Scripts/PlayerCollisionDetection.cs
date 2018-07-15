using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {

    public GameObject lifePlusOnePrefab, shieldTextPrefab, shieldObj, shipDestroyedPSPrefab;

    float timeSinceLastCollision;

    public int shieldHitCount;

    GameManager gameManagerGO;
    SpawnBlocks sb;

    // Use this for initialization
    void Start()
    {
        gameManagerGO = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sb = GameObject.Find("Blocks Spawner").GetComponent<SpawnBlocks>();

        shieldHitCount = 0;

        timeSinceLastCollision = Time.time;
    }

    //Hit a block, lose a life/end the game.
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D myCollider = collision.otherCollider;

        if (myCollider.name != "Shield")
        {
            //To provide half a wave delay between collision detection so that hitting 2 blocks in one wave does not reduce two lives.
            if (Time.time >= (timeSinceLastCollision + (sb.spawnRate / 2)))
            {
                timeSinceLastCollision = Time.time;
                Instantiate(shipDestroyedPSPrefab, transform.position, Quaternion.identity);
                gameManagerGO.PlayerDied();
                Destroy(gameObject);
            }
        }
        else if (myCollider.name == "Shield")
        {
            Destroy(collision.gameObject);
            shieldHitCount += 1;

            if (shieldHitCount == 3)
            {
                shieldObj.SetActive(false);
                shieldHitCount = 0;
            }
        }
    }

    //Looks like a collectible
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Life")
        {
            Instantiate(lifePlusOnePrefab, collision.transform.position, Quaternion.identity);
            gameManagerGO.lives += 1;
            gameManagerGO.UpdateLife();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Shield")
        {
            Instantiate(shieldTextPrefab, collision.transform.position, Quaternion.identity);
            shieldObj.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}

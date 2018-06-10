using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public GameObject lifePlusOnePrefab;

    float movePos, speed, playableAreaHalfWidth, timeSinceLastCollision;

    GameManager gameManagerGO;
    SpawnBlocks sb;

    Rigidbody2D myRB;

    // Use this for initialization
    void Start()
    {
        gameManagerGO = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sb = GameObject.Find("Blocks Spawner").GetComponent<SpawnBlocks>();

        playableAreaHalfWidth = 5f;
        speed = 15f;
        timeSinceLastCollision = Time.time;

        myRB = GetComponent<Rigidbody2D>();

        InitialLoading();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        movePos = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        movePos = transform.position.x + movePos;

        //Make sure that the player can only move within the playable area.
        movePos = Mathf.Clamp(movePos, -playableAreaHalfWidth, playableAreaHalfWidth);

        myRB.MovePosition(new Vector2(movePos, transform.position.y));
    }

    //Hit a block, lose a life/end the game.
    void OnCollisionEnter2D(Collision2D collision)
    {
        //To provide half a wave delay between collision detection so that hitting 2 blocks in one wave does not reduce two lives.
        if (Time.time >= (timeSinceLastCollision + (sb.spawnRate / 2)))
        {
            timeSinceLastCollision = Time.time;
            gameManagerGO.EndGame();
        }
    }

    //Looks like a collectible
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(lifePlusOnePrefab, collision.transform.position, Quaternion.identity);
        gameManagerGO.lives += 1;
        gameManagerGO.UpdateLife();
        Destroy(collision.gameObject);
    }

    void InitialLoading()
    {
        //Hide all images and load the correct values at the start of the scene.
        GameObject.Find("You Died Image").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Game Over Text").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("LivesImage0").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("LivesImage1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("LivesImage2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Score Text").GetComponent<Text>().text = "x" + gameManagerGO.score.ToString();
        GameObject.Find("Lives Text").GetComponent<Text>().text = "x" + gameManagerGO.lives.ToString();

    }

}

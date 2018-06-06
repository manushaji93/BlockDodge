using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float movePos, speed, playableAreaHalfWidth;

    GameManager gameManagerGO;

    Rigidbody2D myRB;

	// Use this for initialization
	void Start () {

        gameManagerGO = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        playableAreaHalfWidth = 5f;
        speed = 15f;

        myRB = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        movePos = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        movePos = transform.position.x + movePos;
        movePos = Mathf.Clamp(movePos, -playableAreaHalfWidth, playableAreaHalfWidth);

        myRB.MovePosition(new Vector2(movePos, transform.position.y));
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameManagerGO.EndGame();
    }

}

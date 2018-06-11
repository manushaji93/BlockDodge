using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movePos;
    float playableAreaHalfWidth, camHeightHalf, camWidthHalf, playableAreaBoundsLeft, playableAreaBoundsRight;

    InitialSetup isScript;

    GameManager gmGO;

    public bool isNotInGame;

    // Use this for initialization
    void Start()
    {
        isScript = GameObject.Find("Initial Setup").GetComponent<InitialSetup>();
        gmGO = GameObject.Find("Game Manager").GetComponent<GameManager>();

        isNotInGame = false;

        transform.localScale = new Vector2(isScript.spacingUnit * 4f, isScript.spacingUnit * 4f / 3f);

        //Calculate half the cam height
        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;

        //Calculate half the cam width
        camWidthHalf = camHeightHalf * GameObject.Find("Main Camera").GetComponent<Camera>().aspect;

        playableAreaHalfWidth = camWidthHalf;

        playableAreaBoundsLeft = -playableAreaHalfWidth + (isScript.spacingUnit * 3f);

        playableAreaBoundsRight = playableAreaHalfWidth - (isScript.spacingUnit * 3f);

        transform.position = new Vector3(transform.position.x, -camHeightHalf + 1f, transform.position.z);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isNotInGame)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

                // get the touch position from the screen touch to world point
                movePos = Camera.main.ScreenToWorldPoint(touch.position);

            }
            else
                movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            movePos.z = 0f;

            //Make sure that the player can only move within the playable area.
            movePos.x = Mathf.Clamp(movePos.x, playableAreaBoundsLeft, playableAreaBoundsRight);

            movePos.y = -camHeightHalf + 1f;

            transform.position = movePos;
        }

    }

}

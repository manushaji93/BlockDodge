using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movePos, tempPos;
    float playableAreaHalfWidth, camHeightHalf, camWidthHalf, playableAreaBoundsLeft, playableAreaBoundsRight, playableAreaBoundsUp, playableAreaBoundsDown, posOffsetX, posOffsetY;

    InitialSetup isScript;

    GameManager gmGO;

    public bool isNotInGame;

    // Use this for initialization
    void Start()
    {
        isScript = GameObject.Find("Initial Setup").GetComponent<InitialSetup>();
        gmGO = GameObject.Find("Game Manager").GetComponent<GameManager>();

        isNotInGame = false;

        transform.localScale = new Vector2(isScript.spacingUnit * 4f, isScript.spacingUnit * 4f);

        //Calculate half the cam height
        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;

        //Calculate half the cam width
        camWidthHalf = camHeightHalf * GameObject.Find("Main Camera").GetComponent<Camera>().aspect;

        playableAreaHalfWidth = camWidthHalf;

        playableAreaBoundsLeft = -playableAreaHalfWidth + (isScript.spacingUnit * 3f);

        playableAreaBoundsRight = playableAreaHalfWidth - (isScript.spacingUnit * 3f);

        playableAreaBoundsUp = camHeightHalf - (transform.localScale.y / 2) - 0.5f;

        playableAreaBoundsDown = -camHeightHalf + (transform.localScale.y / 2) + 0.5f;

        transform.position = new Vector3(transform.position.x, -camHeightHalf + 1f, transform.position.z);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!isNotInGame)
        //{
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            // get the touch position from the screen touch to world point
            if (touch.phase == TouchPhase.Began)
            {
                if (isNotInGame)
                {
                    gmGO.UnpauseGame();
                    isNotInGame = false;
                }

                tempPos = Camera.main.ScreenToWorldPoint(touch.position);

                posOffsetX = tempPos.x - transform.position.x;

                posOffsetY = tempPos.y - transform.position.y;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                movePos = Camera.main.ScreenToWorldPoint(touch.position);
                //movePos = new Vector3(movePos.x - posOffsetX, movePos.y, movePos.z);
                movePos = new Vector3(movePos.x - posOffsetX, movePos.y - posOffsetY, movePos.z);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                gmGO.PauseGame();

                isNotInGame = true;
            }

        }

#elif UNITY_STANDALONE || UNITY_WEBPLAYER

        movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

#endif

        movePos.z = 0f;

        //Make sure that the player can only move within the playable area.
        movePos.x = Mathf.Clamp(movePos.x, playableAreaBoundsLeft, playableAreaBoundsRight);

        //movePos.y = -camHeightHalf + 1f;
        movePos.y = Mathf.Clamp(movePos.y, playableAreaBoundsDown, playableAreaBoundsUp);

        transform.position = movePos;
        //}

    }

}

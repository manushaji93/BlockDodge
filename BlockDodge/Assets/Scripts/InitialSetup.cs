using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialSetup : MonoBehaviour {

    GameManager gameManagerGO;

    float camHeightHalf, camWidth;

    public float spacingUnit, spawnRate, blockVelocity;

    // Use this for initialization
    void Start () {

        camHeightHalf = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;
        camWidth = camHeightHalf * GameObject.Find("Main Camera").GetComponent<Camera>().aspect * 2f;

        spacingUnit = camWidth / 26f;

        float spawnPointY = camHeightHalf + 2f;

        GameObject.Find("Spawn Point0").transform.position = new Vector3(-(10f * spacingUnit), spawnPointY, 0f);
        GameObject.Find("Spawn Point1").transform.position = new Vector3(-(5f * spacingUnit), spawnPointY, 0f);
        GameObject.Find("Spawn Point2").transform.position = new Vector3(0f * spacingUnit, spawnPointY, 0f);
        GameObject.Find("Spawn Point3").transform.position = new Vector3(5f * spacingUnit, spawnPointY, 0f);
        GameObject.Find("Spawn Point4").transform.position = new Vector3(10f * spacingUnit, spawnPointY, 0f);

        gameManagerGO = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        RectTransform scoreRT = GameObject.Find("Score").GetComponent<RectTransform>();
        RectTransform livesRT = GameObject.Find("Lives").GetComponent<RectTransform>();
        RectTransform totalScoreRT = GameObject.Find("Total Score").GetComponent<RectTransform>();

        //Hide all images and load the correct values at the start of the scene.
        GameObject.Find("You Died Image").GetComponent<Image>().enabled = false;
        GameObject.Find("Game Over Text").GetComponent<Image>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerCollisionDetection>().shieldObj.SetActive(false);
        GameObject.Find("LivesImage0").GetComponent<Image>().enabled = false;
        GameObject.Find("LivesImage1").GetComponent<Image>().enabled = false;
        GameObject.Find("LivesImage2").GetComponent<Image>().enabled = false;
        GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Score Text").GetComponent<Text>().text = "x" + gameManagerGO.score.ToString();
        GameObject.Find("Score Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(scoreRT.anchoredPosition.x, scoreRT.anchoredPosition.y - scoreRT.rect.height);
        GameObject.Find("Lives Text").GetComponent<Text>().text = "x" + gameManagerGO.lives.ToString();
        GameObject.Find("Lives Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(livesRT.anchoredPosition.x, livesRT.anchoredPosition.y - livesRT.rect.height);
        GameObject.Find("Total Score Text").GetComponent<Text>().text = "x" + gameManagerGO.overallScore.ToString();
        GameObject.Find("Total Score Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(totalScoreRT.anchoredPosition.x, livesRT.anchoredPosition.y - livesRT.rect.height);
        gameManagerGO.targetScore = gameManagerGO.targetWaves * 4 * 10;

        if (gameManagerGO.level == 1)
        {
            GameObject.Find("Total Score").GetComponent<Text>().enabled = false;
            GameObject.Find("Total Score Text").GetComponent<Text>().enabled = false;
        }
        else
        {
            GameObject.Find("Total Score").GetComponent<Text>().enabled = true;
            GameObject.Find("Total Score Text").GetComponent<Text>().enabled = true;
        }

        /* Using quadratic bezier curve to calculate the required spawn rate and blocks speed:
         * 
         * int y = (1 - t) * (1 - t) * startY + 2 * (1 - t) * t * bezierY + t * t * endY;
         * 
         * where,
         * y = value to be calculated
         * t = 1 / Max. Number of levels
         * startY = value of y in level 1
         * endY = value of y in last level
        */


        spawnRate = (Mathf.Pow(1 - (1 / (gameManagerGO.levelLimit) * gameManagerGO.level), 2) * gameManagerGO.spawnIntervalStart) + (2 * (1 - (1 / (gameManagerGO.levelLimit) * gameManagerGO.level)) * (1 / (gameManagerGO.levelLimit) * gameManagerGO.level) * gameManagerGO.spawnIntervalStart) + (Mathf.Pow((1 / (gameManagerGO.levelLimit) * gameManagerGO.level), 2) * gameManagerGO.spawnIntervalLimit);

        Debug.Log("Spawn rate is " + spawnRate + " for level " + gameManagerGO.level + ".");

        blockVelocity = (Mathf.Pow(1 - (1 / (gameManagerGO.levelLimit) * gameManagerGO.level), 2) *gameManagerGO.startVelocity)+(2 * (1 - (1 / (gameManagerGO.levelLimit) * gameManagerGO.level))*(1 / (gameManagerGO.levelLimit) * gameManagerGO.level)*gameManagerGO.startVelocity) + (Mathf.Pow((1 / (gameManagerGO.levelLimit) * gameManagerGO.level), 2) * gameManagerGO.endVelocity);

        Debug.Log("Block velocity is " + blockVelocity + " for level " + gameManagerGO.level + ".");
    }

}

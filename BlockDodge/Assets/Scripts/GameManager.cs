using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    float slowTimeFactor; //How much to slow down the time by.

    Text scoreText, livesText;

    public int score, lives;

    //Loaded only once when the game starts.
    private void Awake()
    {
        DontDestroyOnLoad(this);

        score = 0;
        lives = 3;

        SceneManager.LoadScene("Level1");
    }

    void Start()
    {

        slowTimeFactor = 10f;

    }

    public void EndGame()
    {
        StartCoroutine(FinishGame());
    }

    // Update is called once per frame
    IEnumerator FinishGame()
    {
        //Slow down the time.
        Time.timeScale = 1 / slowTimeFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowTimeFactor;

        //Display the 'You died' text and lives
        GameObject.Find("You Died Image").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = true;

        lives -= 1;

        //To display the current number of lives.
        if (lives != 0)
        {
            for (int i = 0; i <= lives; i++)
            {
                GameObject.Find("LivesImage" + i.ToString()).GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        //To wait for specified seconds based on the time slow factor.
        yield return new WaitForSeconds(1f / slowTimeFactor);

        //Remove the 'life' image at the end and update the life counter.
        GameObject.Find("LivesImage" + lives.ToString()).GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Lives Text").GetComponent<Text>().text = "x" + lives.ToString();

        yield return new WaitForSeconds(1f / slowTimeFactor);

        //You are all out of lives, end the game.
        if (lives == 0)
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
            GameOver();
        }
        //You have lives left, reload the scene with the score and remaining lives.
        else
        {
            //Return the timescale to normal.
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.fixedDeltaTime * slowTimeFactor;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UpdateScore(int scoreVal)
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();

        score = score + scoreVal;

        scoreText.text = "x" + score.ToString();
    }

    public void UpdateLife()
    {
        livesText = GameObject.Find("Lives Text").GetComponent<Text>();

        livesText.text = "x" + lives.ToString();
    }

    void GameOver()
    {
        Debug.Log("Game is over.");
    }
}

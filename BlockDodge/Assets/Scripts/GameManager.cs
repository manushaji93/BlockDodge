using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    float slowTimeFactor;

    Text scoreText, livesText;

    public int score, lives;

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
        Time.timeScale = 1/slowTimeFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowTimeFactor;

        GameObject.Find("You Died Image").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = true;

        lives -= 1;

        if (lives != 0)
        {
            for (int i = 0; i <= lives; i++)
            {
                GameObject.Find("LivesImage" + i.ToString()).GetComponent<SpriteRenderer>().enabled = true;
            }
        }


        yield return new WaitForSeconds(1f / slowTimeFactor);

        GameObject.Find("LivesImage" + lives.ToString()).GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Lives Text").GetComponent<Text>().text = "x" + lives.ToString();

        yield return new WaitForSeconds(1f / slowTimeFactor);

        if (lives == 0)
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
            GameOver();
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.fixedDeltaTime * slowTimeFactor;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }     
    }

    public void UpdateScore (int scoreVal)
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

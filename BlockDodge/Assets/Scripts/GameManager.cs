using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    float slowTimeFactor; //How much to slow down the time by.

    Text scoreText, livesText, totalScoreText;

    public int score, lives, level, targetWaves, targetScore, overallScore, scoreForRestartingLevel;

    PlayerMovement pmScript;

    public bool notInGame;

    //Loaded only once when the game starts.
    private void Awake()
    {
        DontDestroyOnLoad(this);

        score = 0;
        lives = 3;
        level = 1;
        targetWaves = 20;
        targetScore = score;
        overallScore = score;
        scoreForRestartingLevel = overallScore;
        notInGame = false;

        SceneManager.LoadScene("1");
    }

    void Start()
    {

        slowTimeFactor = 10f;

    }

    public void PlayerDied()
    {
        pmScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        pmScript.isNotInGame = true;
        notInGame = true;
        StartCoroutine(EndLevel());
    }

    // Update is called once per frame
    IEnumerator EndLevel()
    {
        //Slow down the time.
        Time.timeScale = 1 / slowTimeFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowTimeFactor;

        lives -= 1;

        //You are all out of lives, end the game.
        if (lives == 0)
        {

            GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = true;

            //To wait for specified seconds based on the time slow factor.
            yield return new WaitForSeconds(0.5f / slowTimeFactor);

            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;

            UpdateLife();

            GameOver();
            yield break;
        }

        //Display the 'You died' text and lives
        GameObject.Find("You Died Image").GetComponent<Image>().enabled = true;
        GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = true;

        //To display the current number of lives.
        if (lives != 0)
        {
            for (int i = 0; i <= lives; i++)
            {
                GameObject.Find("LivesImage" + i.ToString()).GetComponent<Image>().enabled = true;
            }
        }

        //To wait for specified seconds based on the time slow factor.
        yield return new WaitForSeconds(1f / slowTimeFactor);

        //Remove the 'life' image at the end and update the life counter.
        GameObject.Find("LivesImage" + lives.ToString()).GetComponent<Image>().enabled = false;
        GameObject.Find("Lives Text").GetComponent<Text>().text = "x" + lives.ToString();

        yield return new WaitForSeconds(1f / slowTimeFactor);

        
        //You have lives left, reload the scene with the score and remaining lives.
        if (lives != 0)
        {
            //Return the timescale to normal.
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.fixedDeltaTime * slowTimeFactor;

            overallScore = scoreForRestartingLevel;
            score = 0;
            notInGame = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UpdateScore(int scoreVal)
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        totalScoreText = GameObject.Find("Total Score Text").GetComponent<Text>();

        score = score + scoreVal;
        overallScore += scoreVal;

        scoreText.text = "x" + score.ToString();
        totalScoreText.text = "x" + overallScore.ToString();

        if (score >= targetScore)
        {
            score = 0;
            NextLevel();
        }
    }

    public void UpdateLife()
    {
        livesText = GameObject.Find("Lives Text").GetComponent<Text>();

        livesText.text = "x" + lives.ToString();
    }

    void GameOver()
    {
        //Display the 'Game Over' text.
        GameObject.Find("Game Over Text").GetComponent<Image>().enabled = true;
    }

    void NextLevel()
    {
        level += 1;
        targetWaves = targetWaves + (level * 2);
        scoreForRestartingLevel = overallScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void PauseGame()
    //{
    //    Time.timeScale = 0f;
    //    Time.fixedDeltaTime = 0f;

    //    GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = true;
    //}

    //public void UnpauseGame()
    //{
    //    Time.timeScale = 1f;
    //    Time.fixedDeltaTime = 1f;

    //    GameObject.Find("Game Over BG").GetComponent<SpriteRenderer>().enabled = false;
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    float slowTimeFactor;

    Text scoreText;

    public int score;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        score = 0;

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

        yield return new WaitForSeconds(1f / slowTimeFactor);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowTimeFactor;

        SceneManager.LoadScene("Level1");
    }

    public void UpdateScore (int scoreVal)
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();

        score = score + scoreVal;

        scoreText.text = "x" + score.ToString();
    }
}

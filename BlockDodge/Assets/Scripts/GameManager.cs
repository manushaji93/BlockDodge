using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    float slowTimeFactor;

	// Use this for initialization
	void Start () {

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

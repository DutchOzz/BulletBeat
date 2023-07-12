using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class uiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject restartText;
    [SerializeField] private GameObject pauzeText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject winText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject p1;
    private bool isGameOver = false;
    private bool isPauzed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //Disables panel if active
        gameOverPanel.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.GetComponent<Character>().dead && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }

        //If game is over
        if (isGameOver)
        {
            //If Q is hit, quit the game
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }

        else {
            if (Input.GetKeyDown(KeyCode.P)) {
                if (isPauzed) {
                    Time.timeScale = 1;
                    isPauzed = false;
                    pauzeText.SetActive(false);
                }
                else {
                    Time.timeScale = 0;
                    isPauzed = true;
                    pauzeText.SetActive(true);
                }
            }
        }
    }

    //controls game over canvas and there's a brief delay between main Game Over text and option to restart/quit text
    private IEnumerator GameOverSequence()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        
        yield return new WaitForSeconds(3.0f);

        restartText.SetActive(true);
    }

    public void WonGame(){
        //SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
        Time.timeScale = 0;
        winPanel.SetActive(true);
        winText.SetActive(true);
    }

    public void UpdateScoreCounter(int score)
    {
        ScoreText.text = "" + score;
    }
}
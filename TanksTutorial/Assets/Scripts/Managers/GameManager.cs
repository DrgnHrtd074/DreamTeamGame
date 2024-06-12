using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;
    public TextMeshProUGUI m_MessageText;
    public TextMeshProUGUI m_TimerText;

    public GameObject m_HighScorePanel;
    public TextMeshProUGUI m_HighScoresText;
    public GameObject m_HealthBar;

    public Button m_NewGameButton;
    public Button m_HighScoresButton;

    public GameObject[] m_Tanks;

    public Image healthBar;
    public float healthAmount = 100f;
    private float m_gameTime = 0;

    public float GameTime {  get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;

    public GameState State {  get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";
        healthBar.fillAmount = healthAmount;

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HealthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (m_GameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }
    }

    private void GameStateStart()
    {
        if(Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
            /*m_GameState = GameState.Playing;

            for (int i = 0;i < m_Tanks.Length; i++)
            {
                m_Tanks[i].SetActive(true);
            }*/

        }
    }
    private void GameStatePlaying()
    {
        bool isGameOver = false;

        m_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(m_gameTime);

        m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                    (seconds / 60), (seconds % 60));

        if (IsPlayerDead() == true)
        {
            //Debug.Log("You Lose");
            m_MessageText.text = "TRY AGAIN";
            isGameOver = true;
        }
        else if (OneTankLeft() == true)
        {
            //Debug.Log("You Win");
            m_MessageText.text = "WINNER!";
            isGameOver = true;
            m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
            m_HighScores.SaveScoresToFile();
        }

        if (isGameOver == true)
        {
            m_GameState = GameState.GameOver;

            m_NewGameButton.gameObject.SetActive(true);
            m_HighScoresButton.gameObject.SetActive(true);
            m_HealthBar.gameObject.SetActive(false);

        }
    }
    private void GameStateGameOver()
    {
        if(Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
            /*m_gameTime = 0;
            m_GameState = GameState.Playing;

            for (int i = 0; i < m_Tanks.Length;i++)
            {
                m_Tanks[i].SetActive(true);
            }*/
        }
    }

    public void OnNewGame()
    {
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(false);

        m_HealthBar.gameObject.SetActive(true);
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        m_gameTime = 0;
        m_GameState = GameState.Playing;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(true);
        }
    }

    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2} : {1 :D2}\n",
            (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }
    private bool IsPlayerDead()
    {

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }

        return false;
    }
}

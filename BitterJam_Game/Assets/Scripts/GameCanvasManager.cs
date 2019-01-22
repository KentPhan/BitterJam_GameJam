using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameStates
{
    START,
    PLAY,
    GAMEOVER
}


public class GameCanvasManager : MonoBehaviour
{
    public RectTransform StartScreen;
    public RectTransform GameOverScreen;
    public GameObject ShitStainParent;


    public TextMeshProUGUI ScoreValueText;
    public TextMeshProUGUI MessValueText;
    public TextMeshProUGUI FinalScoreValueText;
    public TextMeshProUGUI FinalMessValueText;
    public TextMeshProUGUI TimerTextValue;
    public float RateOfExplosions = 5.0f;
    public float TimeLimit = 30.0f;


    public static GameCanvasManager Instance;

    public List<ShitExplosionScript> ShitEmitters;


    private int m_Score = 0;
    private int m_MessV = 0;
    private GameStates m_CurrentState = GameStates.START;
    private float m_CurrentExplosionTimer = 0.0f;
    private float m_CurrentTime;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentState == GameStates.START)
        {
            if (Input.GetButtonDown("Submit"))
                StartGame();
        }
        else if (m_CurrentState == GameStates.PLAY)
        {



            // Explosions
            if (m_CurrentExplosionTimer <= 0.0f)
            {
                ShitEmitters[Random.Range(0, ShitEmitters.Count - 1)].Shard();
                m_CurrentExplosionTimer = RateOfExplosions;
            }
            else
            {
                m_CurrentExplosionTimer -= Time.deltaTime;
            }


            TimerTextValue.text = m_CurrentTime.ToString("0.00");
            m_CurrentTime -= Time.deltaTime;

            // Current Time
            if (m_CurrentTime <= 0.0f)
            {
                m_CurrentTime = 0.0f;
                TimerTextValue.text = m_CurrentTime.ToString("0.00");
                GameOver();
                return;
            }

        }
        else if (m_CurrentState == GameStates.GAMEOVER)
        {
            if (Input.GetButtonDown("Submit"))
            {
                GoToStart();
                Reset();
            }
        }
    }


    public void IncreaseScore(int value)
    {
        m_Score += value;
        ScoreValueText.text = m_Score.ToString();
    }

    public void IncreaseMess(int value)
    {
        m_MessV += value;
        MessValueText.text = m_MessV.ToString();
    }

    public void Reset()
    {
        m_Score = 0;
        m_MessV = 0;
        IncreaseScore(0);
        IncreaseMess(0);
        m_CurrentState = GameStates.START;
        StartScreen.gameObject.SetActive(true);
        m_CurrentExplosionTimer = 0.0f;
        m_CurrentTime = TimeLimit;
        TimerTextValue.text = m_CurrentTime.ToString("0.00");
        GameOverScreen.gameObject.SetActive(false);

        foreach (Transform child in ShitStainParent.transform)
        {
            Destroy(child.gameObject);
        }

    }

    public GameStates GetState()
    {
        return m_CurrentState;
    }

    public void StartGame()
    {
        m_CurrentState = GameStates.PLAY;
        StartScreen.gameObject.SetActive(false);
    }

    public void GoToStart()
    {
        m_CurrentState = GameStates.START;
        StartScreen.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        m_CurrentState = GameStates.GAMEOVER;
        GameOverScreen.gameObject.SetActive(true);
        FinalMessValueText.text = m_MessV.ToString();
        FinalScoreValueText.text = m_Score.ToString();

    }
}

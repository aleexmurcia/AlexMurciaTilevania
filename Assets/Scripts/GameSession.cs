using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] int playerLives;
    [SerializeField] TextMeshProUGUI puntuazioaTextua;
    [SerializeField] int playerScore = 0;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timerAmount = 30f;
    float initialTimerAmount;
    float timer = 1.5f;
    bool isReloading = false; 
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public static int LastScoreStatic = 0;


    void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            initialTimerAmount = timerAmount;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelTimer levelTimer = FindFirstObjectByType<LevelTimer>();

        if (levelTimer != null)
        {
            timerAmount = levelTimer.levelTime;
        }
        else
        {
            Debug.LogWarning("⚠️ Este nivel no tiene LevelTimer");
        }

        isReloading = false;
    }


    void Start()
    {
        puntuazioaTextua.text = playerScore.ToString();
    }

    void Update()
    {

        if(playerLives > numOfHearts)
        {
            playerLives = numOfHearts;
        }

        for(int i=0; i < hearts.Length; i++)
        {
            if(i < playerLives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        timerAmount -= Time.deltaTime;
        
        int minutes = Mathf.FloorToInt(timerAmount / 60);
        int seconds = Mathf.FloorToInt(timerAmount % 60);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);

        if(timerAmount < 0 && !isReloading)
        {
            timerAmount = 0;
            isReloading = true;
            DeathProcess();
        }
    }

    public void DeathProcess()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void IncreaseScore(int coinValue)
    {
        playerScore += coinValue;
        ChangeScoreText();
    }

    void ChangeScoreText()
    {
        puntuazioaTextua.text = playerScore.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        StartCoroutine(DelayOnReloading());
    }

    public int GetScore()
    {
        return playerScore;
    }


    IEnumerator DelayOnReloading()
    {
        yield return new WaitForSeconds(timer);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void ResetGameSession()
    {
        LastScoreStatic = playerScore;
        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
        levelManager.LoadGameOver();
        Destroy(gameObject);
    }
}

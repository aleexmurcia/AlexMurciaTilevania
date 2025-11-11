using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI bizitzakTextua;
    [SerializeField] TextMeshProUGUI puntuazioaTextua;
    [SerializeField] int playerScore = 0;
    float timer = 1.5f;


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
        }
    }

    void Start()
    {
        bizitzakTextua.text = playerLives.ToString();
        puntuazioaTextua.text = playerScore.ToString();
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
        bizitzakTextua.text = playerLives.ToString();
        StartCoroutine(DelayOnReloading());
    }

    IEnumerator DelayOnReloading()
    {
        yield return new WaitForSeconds(timer);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void ResetGameSession()
    {
        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
        //ceneManager.LoadScene(0);
        levelManager.LoadGameOver();
        Destroy(gameObject);
    }
}

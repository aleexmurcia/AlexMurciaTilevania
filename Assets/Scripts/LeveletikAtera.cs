using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeveletikAtera : MonoBehaviour
{
    [SerializeField] float itxaronDenbora = 1f;
    [SerializeField] string lastLevelName = "Level3";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        FindAnyObjectByType<PertsonaiMugimendua>().bizirikDago = false;
        StartCoroutine(HandleLevelEnd());
    }

    IEnumerator HandleLevelEnd()
    {
        yield return new WaitForSeconds(itxaronDenbora);

        if (SceneManager.GetActiveScene().name == lastLevelName)
        {
            GameSession gameSession = FindFirstObjectByType<GameSession>();

            if (gameSession != null)
            {
                GameSession.LastScoreStatic = gameSession.GetScore();
                Destroy(gameSession.gameObject);
            }

            FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
            SceneManager.LoadScene("GameFinished");
        }
        else
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
        SceneManager.LoadScene(nextSceneIndex);
    }
}

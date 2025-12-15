using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = GameSession.LastScoreStatic.ToString();
    }
}

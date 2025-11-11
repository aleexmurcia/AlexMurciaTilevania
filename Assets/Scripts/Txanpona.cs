using UnityEngine;

public class Txanpona : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinValue = 100;
    bool isPicked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPicked)
        {
            isPicked = true;
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            FindAnyObjectByType<GameSession>().IncreaseScore(coinValue);
            Destroy(gameObject);
        }
    }
}

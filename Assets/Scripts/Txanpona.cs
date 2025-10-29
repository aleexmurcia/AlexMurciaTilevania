using UnityEngine;

public class Txanpona : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(coinSound);
        }
    }
}

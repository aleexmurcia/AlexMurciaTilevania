using UnityEngine;
using UnityEngine.UIElements;

public class EtsaiaMugitzen : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isMiniBoss = false;
    [SerializeField] private GameObject mushroomPrefab;
    CapsuleCollider2D gorputzaCollider2D;
    BoxCollider2D oinakCollider2D;
    Rigidbody2D nireRigidbody2D;
    private int life;

    void Start()
    {
        gorputzaCollider2D = GetComponent<CapsuleCollider2D>();
        oinakCollider2D = GetComponent<BoxCollider2D>();
        nireRigidbody2D = GetComponent<Rigidbody2D>();

        if (isMiniBoss)
        {
            life = 2;
        }
        else
        {
            life = 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed *= -1;
        changeDirection();
    }

    void changeDirection()
    {
        transform.localScale = new Vector2(-(transform.localScale.x), transform.localScale.y);
    }

    void Update()
    {
        nireRigidbody2D.linearVelocity = new Vector2(speed, nireRigidbody2D.linearVelocity.y);
    }

    public void TakeDamage()
    {
        life--;

        if (life <= 0)
        {
            if (isMiniBoss)
            {
                DropMushroom();
            }
            Destroy(gameObject);
        }
    }

    void DropMushroom()
    {
        if (isMiniBoss)
        {
            Vector3 spawnPos = oinakCollider2D.bounds.center;
            spawnPos.y = gorputzaCollider2D.bounds.min.y + 0.2f; 
            Instantiate(mushroomPrefab, spawnPos, Quaternion.identity);
        }
    }
}

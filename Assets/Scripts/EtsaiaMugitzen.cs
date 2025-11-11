using UnityEngine;
using UnityEngine.UIElements;

public class EtsaiaMugitzen : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    CapsuleCollider2D gorputzaCollider2D;
    BoxCollider2D oinakCollider2D;
    Rigidbody2D nireRigidbody2D;
    void Start()
    {
        gorputzaCollider2D = GetComponent<CapsuleCollider2D>();
        oinakCollider2D = GetComponent<BoxCollider2D>();
        nireRigidbody2D = GetComponent<Rigidbody2D>();
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
}

using UnityEngine;
using System.Collections;

public class MushroomPower : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedBoost = 5f;
    [SerializeField] private float jumpBoost = 5f;
    [SerializeField] private float scaleMultiplier = 1.5f;
    [SerializeField] private float boostDuration = 5f;
    [SerializeField] private float lifetime = 5f;

    Rigidbody2D nireRigidbody2D;
    CapsuleCollider2D gorputzaCollider2D;
    BoxCollider2D oinakCollider2D;

    void Start()
    {
        gorputzaCollider2D = GetComponent<CapsuleCollider2D>();
        oinakCollider2D = GetComponent<BoxCollider2D>();
        nireRigidbody2D = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        nireRigidbody2D.linearVelocity = new Vector2(speed, nireRigidbody2D.linearVelocity.y);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PertsonaiMugimendua player = other.GetComponent<PertsonaiMugimendua>();
            if (player != null)
            {
                player.StartCoroutine(ApplyPower(player));
                Destroy(gameObject);

            }
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyPower(PertsonaiMugimendua player)
    {
        // Guardar valores originales
        float originalSpeed = player.speed;
        float originalJump = player.jumpSpeed;
        float originalScaleMultiplier = player.scaleMultiplier;

        // Aplicar boost
        player.speed += speedBoost;
        player.jumpSpeed += jumpBoost;
        player.scaleMultiplier = scaleMultiplier;

        // Acelerar música
        AudioSource music = FindObjectOfType<AudioSource>();
        float originalPitch = 1f;
        if (music != null)
        {
            originalPitch = music.pitch;
            music.pitch = 1.3f;
        }

        // Mantener boost mientras dura el tiempo
        float timer = 0f;
        while (timer < boostDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Restaurar valores originales
        player.speed = originalSpeed;
        player.jumpSpeed = originalJump;
        player.scaleMultiplier = originalScaleMultiplier;

        if (music != null)
        {
            music.pitch = originalPitch;
        }
    }
}

using System;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PertsonaiMugimendua : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] public float speed = 1f;
    [SerializeField] public float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] GameObject starPrefab;
    [SerializeField] Transform shootPoint;
    Rigidbody2D nireRigidbody2D;
    Animator animator;
    CapsuleCollider2D gorputzaCollider2D;
    BoxCollider2D oinakCollider2D;
    float gravityScaleAtStart;
    public bool bizirikDago = true;
    public float scaleMultiplier = 1f;
    float lastDirection = 1f; 



    void Start()
    {
        nireRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gorputzaCollider2D = GetComponent<CapsuleCollider2D>();
        oinakCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = nireRigidbody2D.gravityScale;
        bizirikDago = true;
    }
    void OnMove(InputValue value)
    {
        if(!bizirikDago) return;
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!bizirikDago) return;
        if (!oinakCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Moving"))) return;
        if (value.isPressed)
        {
            nireRigidbody2D.linearVelocity = new Vector2(nireRigidbody2D.linearVelocity.x, jumpSpeed);
        }
    }

    void EskaileraIgo()
    {
        if (!gorputzaCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            nireRigidbody2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("isGoingUp", false);
            return; 
        }
        nireRigidbody2D.linearVelocity = new Vector2(nireRigidbody2D.linearVelocity.x, moveInput.y * climbSpeed);
        bool playerHasVerticalSpeed = Mathf.Abs(nireRigidbody2D.linearVelocity.y) > Mathf.Epsilon;
        animator.SetBool("isGoingUp", playerHasVerticalSpeed);
        nireRigidbody2D.gravityScale = 0f;
    }

    void Run()
    {
        nireRigidbody2D.linearVelocity = new Vector2(moveInput.x * speed, nireRigidbody2D.linearVelocity.y);
        bool playerHasHorizontalSpeed = Mathf.Abs(nireRigidbody2D.linearVelocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void Update()
    {
        Run();
        FlipSprite();
        EskaileraIgo();
        Die();
    }

    void FlipSprite()
    {
        if (Mathf.Abs(nireRigidbody2D.linearVelocity.x) > Mathf.Epsilon)
        {
            lastDirection = Mathf.Sign(nireRigidbody2D.linearVelocity.x);
        }

        transform.localScale = new Vector3(
            lastDirection * scaleMultiplier,
            1f * scaleMultiplier,
            1f
        );
    }

    void Die()
    {
        if(!bizirikDago){ return; }
        if (gorputzaCollider2D.IsTouchingLayers(LayerMask.GetMask("Etsaiak", "Spikes")) || oinakCollider2D.IsTouchingLayers(LayerMask.GetMask("Etsaiak", "Spikes")))
        {
            animator.SetTrigger("hiltzen");
            bizirikDago = false;
            nireRigidbody2D.linearVelocity = new Vector2(0f, 15f);

            CameraShake.Instance.Shake(50f, 2f);
            FindAnyObjectByType<GameSession>().DeathProcess();
        }
    }

    void OnAttack()
    {
        if (!bizirikDago) return;
        Instantiate(starPrefab, shootPoint.position, shootPoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            transform.SetParent(null);
        }
    }
}

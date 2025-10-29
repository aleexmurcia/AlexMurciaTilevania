using System;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PertsonaiMugimendua : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] GameObject starPrefab;
    [SerializeField] Transform shootPoint;
    Rigidbody2D nireRigidbody2D;
    Animator animator;
    CapsuleCollider2D gorputzaCollider2D;
    BoxCollider2D oinakCollider2D;
    float gravityScaleAtStart;
    bool bizirikDago = true;


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
        if (!oinakCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
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
        bool playerHasHorizontalSpeed = Mathf.Abs(nireRigidbody2D.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(nireRigidbody2D.linearVelocity.x), 1f);
        }
    }

    void Die()
    {
        if(!bizirikDago){ return; }
        if (gorputzaCollider2D.IsTouchingLayers(LayerMask.GetMask("Etsaiak", "Spikes")) && oinakCollider2D.IsTouchingLayers(LayerMask.GetMask("Etsaiak", "Spikes")))
        {
            bizirikDago = false;
            animator.SetTrigger("hiltzen");
            nireRigidbody2D.linearVelocity = new Vector2(5f, 15f);
        }
    }

    void OnAttack()
    {
        if (!bizirikDago) return;
        Instantiate(starPrefab, shootPoint.position, shootPoint.rotation);
    }
}

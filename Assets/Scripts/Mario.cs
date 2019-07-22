using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    private Transform checkTransform;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isJump = true;

    public float jumpSpeed;
    public float moveSpeed;

    private void Awake()
    {
        checkTransform = transform.Find("CheckPosition");
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isJump = !Physics2D.Linecast(transform.position, checkTransform.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.localScale = horizontal == 0 ? transform.localScale : horizontal > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        rigidbody2D.velocity = new Vector2(moveSpeed * horizontal, rigidbody2D.velocity.y);

        if (!isJump && Input.GetButtonDown("Jump"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }

        animator.SetBool("PlayerJump", isJump);
        animator.SetFloat("PlayerSpeed", Mathf.Abs(rigidbody2D.velocity.x));
    }
}

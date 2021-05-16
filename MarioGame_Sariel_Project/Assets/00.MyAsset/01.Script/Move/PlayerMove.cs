using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveController
{
    [SerializeField] float jumpPower;
    [SerializeField] float maxSpeed, zeroSpeedRate;
    [SerializeField] float groundCheckDist = 1f;

    float velocityLerpTemp = 0f;
    float jumpTimer = 0f;

    [SerializeField] bool isGround = true;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(PlayerSystem.Instance.PlayerHP.CurrentHP > 0)
        {
            Move();
            if (Input.GetButtonDown("Jump") && isGround) Jump();
            if (Input.GetButtonUp("Jump")) jumpTimer = 0f;
        }
    }

    protected override void Move()
    {
        CheckLandingPlatform();
        //밑 함수의 h 벡터값 정의
        float h = Input.GetAxis("Horizontal");

        //워킹 함수
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rigid.AddForce(Vector2.right * h * moveSpeed, ForceMode2D.Impulse);
            if (velocityLerpTemp != 0) velocityLerpTemp = 0f;
        }
        else
        {
            VelocityZero();
        }

        Flip(h);
        LimitVelocity();
    }

    private void Flip(float h)
    {
        if(h > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(h < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void Jump()
    {
        jumpTimer += Time.deltaTime;

        if(jumpTimer <= 0.2f)
        //점프 함수;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    protected override void AccelerateSpeed()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void VelocityZero()
    {
        velocityLerpTemp += Time.deltaTime;
        rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, 0, velocityLerpTemp * zeroSpeedRate), rigid.velocity.y);
    }

    void LimitVelocity()
    {
        //걷기 속도 제한
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    void CheckLandingPlatform()
    {
        //레이캐스트 빔 디버깅
        //Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
        //랜딩 플랫폼
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, groundCheckDist, LayerMask.GetMask("L_Ground"));

        if (rayHit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}

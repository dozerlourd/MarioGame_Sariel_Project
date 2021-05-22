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
    [SerializeField] float maxJumpTimer = 0.3f;

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
            if (Input.GetButton("Jump") && isGround) Jump();
            if (Input.GetButtonUp("Jump")) { isGround = true; jumpTimer = 0; }
        }
    }

    protected override void Move()
    {
        CheckingPlatform();
        //밑 함수의 h 벡터값 정의
        float h = Input.GetAxis("Horizontal");

        //워킹 함수
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
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

        if (jumpTimer <= maxJumpTimer)
        {
            //점프 함수;
            //rigid.AddForce(Vector2.up * jumpPower * Time.deltaTime, ForceMode2D.Impulse);
            rigid.velocity = Vector2.up * jumpPower;
        }
        else { isGround = false; }
            
    }

    protected override void AccelerateSpeed() { }

    protected override void VelocityZero()
    {
        velocityLerpTemp += Time.deltaTime * 5;
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

    void CheckingPlatform()
    {
        //레이캐스트 빔 디버깅
        Debug.DrawRay(new Vector2(rigid.position.x - 0.075f, rigid.position.y - 0.1f), Vector3.right * 0.15f, Color.red);
        //랜딩 플랫폼
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(rigid.position.x - 0.075f, rigid.position.y - 0.1f), Vector3.right, groundCheckDist, LayerMask.GetMask("L_Ground"));

        isGround = rayHit.collider ? true : false;
    }
}

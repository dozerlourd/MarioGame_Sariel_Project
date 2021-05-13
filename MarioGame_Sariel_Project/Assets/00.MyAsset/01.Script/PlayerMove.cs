using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveController
{
    [SerializeField] public float jumpPower;
    [SerializeField] public float maxSpeed;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        CheckLandingPlatform();
        //밑 함수의 h 벡터값 정의
        float h = Input.GetAxisRaw("Horizontal");

        //워킹 함수
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        if(Input.GetButtonUp("Horizontal"))
        {

        }

        LimitVelocity();
    }

    protected override void AccelerateSpeed()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void VelocityZero()
    {
        
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
        if (rigid.velocity.y < 0)
        {


            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.6f)
                {

                }
            }
        }
    }
}

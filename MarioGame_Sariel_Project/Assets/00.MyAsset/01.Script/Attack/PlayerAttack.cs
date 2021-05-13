using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackController
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Attack(col);
    }

    protected override void OnAttack(GameObject enemy) { }

    protected override void OnAttack(GameObject enemy, AttackController attack)
    {
        attack.OnDamaged();
    }

    public override void OnDamaged(Vector2 targetPos)
    {
        //Change Layer
        gameObject.layer = 11;

        //view Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //void Oncollision2D 괄호 안의 Collision2D collision은 플레이어와 충돌한 충돌체의 collision을 나타냄.
        //그 함수 안에 있는 Ondamaged() 함수 안의 collision.transform.position이 곧 Vector2 targetPos이 되는 것이고, 
        //int dirc가 Vector2 targetPos값의 x값. 즉, collision.transform.position.x가 되는 것이다.
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        //Reaction Force
        rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);

        Invoke("OffDamaged", 1);
    }

    protected override void Attack(Collider2D col)
    {
        //피격
        if (col.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > col.gameObject.transform.position.y)
            {
                OnAttack(col.gameObject, col.GetComponent<AttackController>());
            }
            else
            {
                OnDamaged(col.transform.position);
            }
        }

        if (col.gameObject.tag == "Trap")
        {

        }
    }
}

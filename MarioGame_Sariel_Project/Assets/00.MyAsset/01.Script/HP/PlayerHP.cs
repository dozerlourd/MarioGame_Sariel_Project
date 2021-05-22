using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HPController
{
    [SerializeField] float knockbackPower;

    public override float TakeDamage { set => CurrentHP -= value; }

    protected override bool Damage(ref float setHP)
    {
        return false;
    }

    protected override void Death(ref float setHP)
    {
        
    }

    protected override bool OverHeal(ref float setHP)
    {
        return false;
    }

    protected override bool RecoveryHeal(ref float setHP)
    {
        return false;
    }

    protected override void RefreshUIElement()
    {
        
    }

    public override void OnDamaged(Vector2 targetPos)
    {
        TakeDamage = 1;

        if (CurrentHP >= 1)
        {
            Debug.Log("1");
            //Change Layer
            gameObject.layer = 11;

            //view Alpha
            PlayerSystem.Instance.SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

            //void Oncollision2D 괄호 안의 Collision2D collision은 플레이어와 충돌한 충돌체의 collision을 나타냄.
            //그 함수 안에 있는 Ondamaged() 함수 안의 collision.transform.position이 곧 Vector2 targetPos이 되는 것이고, 
            //int dirc가 Vector2 targetPos값의 x값. 즉, collision.transform.position.x가 되는 것이다.
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            //Reaction Force
            PlayerSystem.Instance.Rigidbody2D.AddForce(new Vector2(dirc, 1) * knockbackPower, ForceMode2D.Impulse);

            Invoke("OffDamaged", 1);
        }
        else
        {
            Debug.Log("0");
            //Sprite Alpha
            //PlayerSystem.Instance.SpriteRenderer.color = new Color(1, 1, 1, 0.4f);
            PlayerSystem.Instance.Animator.SetBool("IsDie",true);
            //Collider Disable
            PlayerSystem.Instance.Collider2D.enabled = false;
            //Die Effect Jump
            PlayerSystem.Instance.Rigidbody2D.AddForce(Vector2.up * knockbackPower, ForceMode2D.Impulse);
        }
    }

    protected override void OffDamaged()
    {
        gameObject.layer = 10;

        PlayerSystem.Instance.SpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}

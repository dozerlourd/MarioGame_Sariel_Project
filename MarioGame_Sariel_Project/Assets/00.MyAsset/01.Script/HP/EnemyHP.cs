using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : HPController
{
    [SerializeField] float deadPower;
    
    SpriteRenderer spriteRenderer;
    Collider2D col;
    Rigidbody2D rigid;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

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
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        col.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * deadPower, ForceMode2D.Impulse);
        //Destroy
        Invoke("DeActive", 5);
    }
}

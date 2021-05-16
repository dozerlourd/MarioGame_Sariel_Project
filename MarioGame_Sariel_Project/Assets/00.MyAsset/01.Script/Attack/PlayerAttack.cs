using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackController
{
    [SerializeField] float reactionPower = 3f;
    void OnCollisionEnter2D(Collision2D col)
    {
        Attack(col);
    }

    protected override void Attack(Collision2D col)
    {
        //피격
        if (col.gameObject.tag == "T_Enemy")
        {
            if (PlayerSystem.Instance.Rigidbody2D.velocity.y < 0 && transform.position.y > col.gameObject.transform.position.y)
            {
                Debug.Log(transform.position.y - col.transform.position.y);
                PlayerSystem.Instance.Rigidbody2D.AddForce(Vector2.up * reactionPower, ForceMode2D.Impulse);
                OnAttack(col.gameObject);

            }
            else
            {
                gameObject.GetComponent<HPController>().OnDamaged(col.transform.position);
            }
        }

        if (col.gameObject.tag == "T_Trap")
        {

        }
    }

    protected override void OnAttack(GameObject _obj)
    {
        _obj.GetComponent<HPController>().OnDamaged(transform.position);
    }
}

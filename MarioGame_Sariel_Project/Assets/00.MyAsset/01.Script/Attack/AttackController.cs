using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackController : MonoBehaviour
{
    protected virtual void Attack() { }
    protected virtual void Attack(Collider2D col) { }
    protected virtual void Attack(Collision2D col) { }
    protected virtual void Attack(GameObject obj) { }

    protected virtual void OnAttack() { }
    protected virtual void OnAttack(GameObject obj) { }
    protected virtual void OnAttack(GameObject obj, AttackController atk) { }
}

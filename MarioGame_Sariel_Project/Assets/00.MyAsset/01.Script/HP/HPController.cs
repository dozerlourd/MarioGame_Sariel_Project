using System;
using UnityEngine;

public abstract class HPController : MonoBehaviour
{
    bool isDeath = true;

    [SerializeField] float m_maxHP;
    float m_currentHP;

    public float MaxHP { get => m_maxHP; protected set => m_maxHP = value; }

    public float CurrentHP
    {
        get => m_currentHP; set
        {
            if (isDeath) return;
            if (m_currentHP < value) if (RecoveryHeal(ref value)) return;
            if (MaxHP < value) if (OverHeal(ref value)) return;
            if (m_currentHP > value) if (Damage(ref value)) return;
            m_currentHP = value; RefreshUIElement();
            if (m_currentHP <= 0)
            {
                Death(ref value);
                isDeath = true;
            }
        }
    }

    private void OnEnable()
    {
        isDeath = false;
        Generation();
        RefreshUIElement();
    }

    protected virtual void Generation()
    {
        isDeath = false;
        m_currentHP = MaxHP;
        RefreshUIElement();
    }

    public abstract float TakeDamage { set; }

    protected abstract bool RecoveryHeal(ref float setHP);
    protected abstract bool OverHeal(ref float setHP);
    protected abstract bool Damage(ref float setHP);
    protected abstract void RefreshUIElement();
    protected abstract void Death(ref float setHP);

    public virtual void OnDamaged() { }
    public virtual void OnDamaged(Vector2 vec) { }
    protected virtual void OffDamaged() { }
}

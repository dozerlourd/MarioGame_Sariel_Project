using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    protected abstract void Move();

    protected abstract void VelocityZero();

    protected abstract void AccelerateSpeed();
}

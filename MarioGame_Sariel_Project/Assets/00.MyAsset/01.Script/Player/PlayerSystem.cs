using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : SceneObject<PlayerSystem>
{
    [SerializeField] GameObject m_player;
    Animator m_animator;
    SpriteRenderer m_spriteRenderer;
    Collider2D m_collider2D;
    PlayerHP m_playerHP;
    PlayerAttack m_playerAttack;
    PlayerMove m_playerMove;
    Rigidbody2D m_rigid2D;

    public GameObject Player => m_player;
    public Animator Animator => m_animator = m_animator ? m_animator : Player.GetComponent<Animator>();
    public SpriteRenderer SpriteRenderer => m_spriteRenderer = m_spriteRenderer ? m_spriteRenderer : Player.GetComponent<SpriteRenderer>();
    public Collider2D Collider2D => m_collider2D = m_collider2D ? m_collider2D : Player.GetComponent<Collider2D>();
    public PlayerHP PlayerHP => m_playerHP = m_playerHP ? m_playerHP : Player.GetComponent<PlayerHP>();
    public PlayerAttack PlayerAttack => m_playerAttack = m_playerAttack ? m_playerAttack : Player.GetComponent<PlayerAttack>();
    public PlayerMove PlayerMove => m_playerMove = m_playerMove ? m_playerMove : Player.GetComponent<PlayerMove>();
    public Rigidbody2D Rigidbody2D => m_rigid2D = m_rigid2D ? m_rigid2D : Player.GetComponent<Rigidbody2D>();
}

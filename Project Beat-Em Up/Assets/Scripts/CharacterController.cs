using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_jumpForce;

    [Header("References")]
    [SerializeField] Rigidbody2D m_rigidbody;
    [SerializeField] SpriteRenderer m_spriteRenderer;


    protected void Move(Vector2 direction)
    {
        m_rigidbody.AddForce(direction * m_movementSpeed);
    }
    protected void Jump()
    {
        m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
    }
    protected void LookAtDirection(float direction)
    {
        // if the direction is left
        if (direction < 0)
        {
            m_spriteRenderer.flipX = false;
        }
        // if the direction is right
        else if(direction > 0)
        {
            m_spriteRenderer.flipX = true;
        }
    }

}

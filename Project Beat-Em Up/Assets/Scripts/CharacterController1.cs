using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController1 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_jumpForce;

    [Header("References")]
    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] SpriteRenderer m_spriteRenderer;


    protected void Move(Vector3 direction)
    {
        m_rigidbody.AddForce(direction * m_movementSpeed);
    }
    protected void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
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

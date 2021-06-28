using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (date)
 * 24/06/21 - Oliver - Changed from using rigidbody.addforce to transform.position += within Move() for smoother movement. Also added in Time.deltaTime to insure framerate independence.
 * Changed Move() and Jump() to virtual functions allowing addition character specific code to be added in inherited functions. Added an Animator property.
 * 
 */

public class CharacterController1 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float m_movementSpeed;
    [SerializeField] float m_jumpForce;

    [Header("References")]
    [SerializeField] protected Animator m_animator;
    [SerializeField] protected Rigidbody m_rigidbody;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;


    protected virtual void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * m_movementSpeed;
        //m_rigidbody.AddForce(direction * m_movementSpeed);
    }
    protected virtual void Jump()
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

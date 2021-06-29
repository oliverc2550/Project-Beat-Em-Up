using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (date)
 * 24/06/21 - Oliver - Changed from using rigidbody.addforce to transform.position += within Move() for smoother movement. Also added in Time.deltaTime to insure framerate independence.
 * Changed Move() and Jump() to virtual functions allowing addition character specific code to be added in inherited functions. Added an Animator property.
 * 29/06/21 - Oliver - Changed class name from CharacterController1 to CombatandMovementController. Moved Pickup and Drop to CombatandMovementController, made both methods virtual methods.
 * Thea - Moved the movement logic from the Move function to the PlayerController as Enemy doesn't use it. Enemy movement is done with NavMeshAgents
 */

public class CombatandMovementController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float m_movementSpeed;
    [SerializeField] protected float m_jumpForce;
    [SerializeField] protected LayerMask m_pickupLayer;
    [SerializeField] protected float m_pickupRange = 0.25f;
    [SerializeField] protected float m_normalAttackRange = 1.0f;
    [SerializeField] protected float m_normalAttackDamage = 1.0f;
    protected bool m_objPickedup;

    [Header("References")]
    [SerializeField] protected Animator m_animator;
    [SerializeField] protected Rigidbody m_rigidbody;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected Transform m_originPoint;
    [SerializeField] protected Transform m_normalAttackPoint;


    protected virtual void Move(Vector3 direction)
    {
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
            m_spriteRenderer.flipX = true;
        }
        // if the direction is right
        else if (direction > 0)
        {
            m_spriteRenderer.flipX = false;
        }
    }

    protected virtual void Pickup()
    {
        Collider[] colliders = Physics.OverlapSphere(m_originPoint.position, m_pickupRange, m_pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            nearbyObject.GetComponent<Rigidbody>().useGravity = false;
            nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            nearbyObject.transform.position = m_originPoint.position;
            nearbyObject.transform.parent = m_originPoint;
            m_objPickedup = true;
        }
    }

    protected virtual void Drop()
    {
        Collider[] colliders = Physics.OverlapSphere(m_originPoint.position, m_pickupRange, m_pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            nearbyObject.transform.parent = null;
            nearbyObject.GetComponent<Rigidbody>().useGravity = true;
            nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            m_objPickedup = false;
        }
    }

    protected virtual void NormalAttack()
    {

    }

    protected virtual void Block()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (date)
 * 24/06/21 - Oliver - Changed from using rigidbody.addforce to transform.position += within Move() for smoother movement. Also added in Time.deltaTime to insure framerate independence.
 * Changed Move() and Jump() to virtual functions allowing addition character specific code to be added in inherited functions. Added an Animator property.
 * 29/06/21 - Oliver - Changed class name from CharacterController1 to CombatandMovementController. Moved Pickup and Drop to CombatandMovementController, made both methods virtual methods.
 * Thea - Moved the movement logic from the Move function to the PlayerController as Enemy doesn't use it. Enemy movement is done with NavMeshAgents
 * 30/06/21 - Oliver - Refactored the Pickup() and Drop() methods into a single Interact() method. Added the NormalAttack(), NormalAttackEffects(), Block(), and BlockEffects() methods.
 * 01/07/21 - Oliver - Changed spriteRenderer.flipx to localscale.x = +/- to enable originpoints (used for object pickup & attacks) to flip with the rest of the character.
 * Thea - Inherited from IDamagable interface. Added logic and filled the functions that came from the interface
 * 2/07/21 - Thea - Added tooltips for the designer
 */

public class CombatandMovement : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] [Range(1,7)] protected float m_movementSpeed;
    [SerializeField] [Range(100, 300)] protected float m_jumpForce;
    [SerializeField] protected float m_pickupRange = 0.25f;
    [SerializeField] protected float m_normalAttackRange = 1.0f;
    [SerializeField] protected float m_normalAttackDamage = 1.0f;
    [SerializeField] protected LayerMask m_pickupLayer;
    [SerializeField] protected LayerMask m_enemyLayer;
    protected bool m_holdingObj;
    protected bool m_isAttacking;
    protected bool m_isBlocking;

    [Header("References")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")] 
    [SerializeField] protected Animator m_animator;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")] 
    [SerializeField] protected Rigidbody m_rigidbody;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")] 
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")] 
    [SerializeField] protected Transform m_originPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")] 
    [SerializeField] protected Transform m_normalAttackPoint;

    //Coming from the interface.
    public float health { get; set; }

    protected virtual void Move(Vector3 direction)
    {
    }
    protected virtual void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }
    protected void LookAtDirection(float direction)
    {
        Vector3 CharacterScale = transform.localScale;
        //Debug.Log(CharacterScale);
        // if the direction is left
        if (direction < 0)
        {
            //m_spriteRenderer.flipX = true;
            CharacterScale.x = -2;
            transform.localScale = CharacterScale;
        }
        // if the direction is right
        else if (direction > 0)
        {
            //m_spriteRenderer.flipX = false;
            CharacterScale.x = 2;
            transform.localScale = CharacterScale;
        }
        //Debug.Log(CharacterScale);
    }

    protected void Interact(ref bool holdingObj)
    {
        Collider[] colliders = Physics.OverlapSphere(m_originPoint.position, m_pickupRange, m_pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            if (holdingObj != true)
            {
                nearbyObject.GetComponent<Rigidbody>().useGravity = false;
                nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                nearbyObject.transform.position = m_originPoint.position;
                nearbyObject.transform.parent = m_originPoint;
                holdingObj = true;
                //Debug.Log(holdingObj);
            }
            else if (holdingObj == true)
            {
                nearbyObject.transform.parent = null;
                nearbyObject.GetComponent<Rigidbody>().useGravity = true;
                nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                holdingObj = false;
                //Debug.Log(holdingObj);
            }
        }
    }

    protected virtual void NormalAttackEffects()
    {

    }

    protected void NormalAttack(Transform attackPoint, float attackRange, LayerMask enemyLayer, float attackDamage)
    {
        Debug.Log("DefaultAttack");
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            Debug.Log("Attack Hit");
            Debug.Log("Dealt " + attackDamage + " to " + nearbyObject.gameObject);
            nearbyObject.gameObject.GetComponent<AttackHitDemo>().TakeDamage();
            NormalAttackEffects();

            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DealDamage(m_normalAttackDamage);
            }
        }
    }

    protected virtual void BlockEffects()
    {

    }

    protected void Block()
    {
        if (m_isBlocking == true)
        {
            Debug.Log("Reducing Damage Taken");
        }
    }

    //Mandatory functions, coming from the interface. If these functions are not added to this script, there will be an error. 
    //If these functions are not needed here anymore, they must be removed from the interface too

    public void DealDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            Die();
        }

        //todo: some particles, sounds and animations
    }

    public void Die()
    {
        //debug:
        Destroy(gameObject);
        //todo: some particles, sounds and animations
    }
}


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
 * 02/07/21 - Thea - Added tooltips for the designer
 * 04/07/21 - Oliver - Created instance of CombatandMovement due to github errors. Added fields for special attack functionality and boxcasting functionality.
 * Created the Attack() method so that Normal/SpecialAttack() could use it, to reduce duplicate code.
 */

public class Oliver_CombatandMovement : MonoBehaviour, IDamagable_Oliver
{
    [Header("Settings")]
    [SerializeField] [Range(50, 300)] protected float m_maxHealth;
    [SerializeField] [Range(1, 7)] protected float m_movementSpeed;
    [SerializeField] [Range(25, 300)] protected float m_jumpForce;
    [SerializeField] protected float m_pickupRange = 1.25f;
    [SerializeField] protected float m_normalAttackRange = 1.25f;
    [SerializeField] protected float m_normalAttackDamage = 1.0f;
    [SerializeField] protected float m_specialAttackRange = 0.75f;
    [SerializeField] protected float m_specialAttackDamage = 2.0f;
    [SerializeField] protected LayerMask m_pickupLayer;
    [SerializeField] protected LayerMask m_enemyLayer;
    [SerializeField] protected LayerMask m_collisionLayer;
    protected bool m_holdingObj;
    protected bool m_normalAttackActive;
    protected bool m_specialAttackActive;
    protected bool m_isBlocking;

    public GameObject heldObject;

    [Header("References")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Animator m_animator;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Collider m_collider;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Rigidbody m_rigidbody;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_interactPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_normalAttackPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_specialAttackPoint;

    //Coming from the interface.
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }

    protected virtual void Start()
    {
        ImaxHealth = m_maxHealth;
        IcurrentHealth = ImaxHealth;
        m_holdingObj = false;
        m_normalAttackActive = false;
        m_specialAttackActive = false;
        m_isBlocking = false;
        IisBlocking = m_isBlocking;
    }

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
            CharacterScale.x = -2;
            transform.localScale = CharacterScale;
        }
        // if the direction is right
        else if (direction > 0)
        {
            CharacterScale.x = 2;
            transform.localScale = CharacterScale;
        }
        //Debug.Log(CharacterScale);
    }

    protected void Interact(ref bool holdingObj)
    {
        Collider[] colliders = Physics.OverlapSphere(m_interactPoint.position, m_pickupRange, m_pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            if (holdingObj != true)
            {
                EquipItem(nearbyObject.gameObject);
                holdingObj = true;
                //Debug.Log(holdingObj);
            }
            else if (holdingObj == true)
            {
                UnequipHeldItem();
                holdingObj = false;
                //Debug.Log(holdingObj);
            }
        }
    }
    public void EquipItem(GameObject item)
    {
        heldObject = item;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        heldObject.transform.position = m_interactPoint.position;
        heldObject.transform.parent = m_interactPoint;
    }

    public void UnequipHeldItem()
    {
        heldObject = null;
        heldObject.transform.parent = null;
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    protected virtual void AttackEffects(GameObject gameObject)
    {

    }

    protected void Attack(Transform attackPoint, float attackRange, LayerMask enemyLayer, float attackDamage)
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            Debug.Log("Attack Hit");
            Debug.Log("Dealt " + attackDamage + " to " + nearbyObject.gameObject);
            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            nearbyObject.gameObject.GetComponent<IDamagable_Oliver>();
            if (nearbyObject.gameObject.GetComponent<IDamagable_Oliver>() != null)
            {
                nearbyObject.gameObject.GetComponent<IDamagable_Oliver>().TakeDamage(attackDamage);
                AttackEffects(nearbyObject.gameObject);
            }
        }
    }

    protected void NormalAttack(Transform normalAttackPoint, float normalAttackRange, LayerMask enemyLayer, float normalAttackDamage)
    {
        Debug.Log("Normal Attack");
        Attack(normalAttackPoint, normalAttackRange, enemyLayer, normalAttackDamage);
    }

    protected void SpecialAttack(Transform specialAttackPoint, float specialAttackRange, LayerMask enemyLayer, float specialAttackDamage)
    {
        Debug.Log("Special Attack");
        Attack(specialAttackPoint, specialAttackRange, enemyLayer, specialAttackDamage);
    }

    //Mandatory functions, coming from the interface. If these functions are not added to this script, there will be an error. 
    //If these functions are not needed here anymore, they must be removed from the interface too

    public void OnTakeDamage(float damage)
    {
        //todo: some particles, sounds and animations
    }

    public void Die()
    {
        //debug:
        Destroy(gameObject);
        //todo: some particles, sounds and animations
    }
}

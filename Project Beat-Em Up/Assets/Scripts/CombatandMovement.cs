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
 * 06/07/21 - Thea - Setting the currentHealth to the maxHealth in this script rather than the player script; Created EquipItem and UnequipItem functions 
 * so that they can be also called by the thiefEnemy; Added collider[] return type to the Attack function in order to know what are the hit objects; 
 * Stored held item as an object; Removed the Attack function and created an Attack animation event instead
 * 07/07/21 - Oliver - Merged Oliver_CombatandMovement with CombatandMovement. Some 06/07 changes have been reverted. Created AttackEffects() as a virtual method to fill the same role as
 * the collider[] Attack() method change from 06/07. Restored Attack() and moved SetNormalAttackBool(), SetSpecialAttackBool(), SetChargedAttackBool(), NormalAttackAnimEvent(), 
 * and SpecialAttackAnimEvent() from PlayerController so that these methods can be called by all inheriting classes via animation events. 
 * Added more variables to the virtual Start() method to allow them to also be easily called via inherited classes. Added Regions to help with readability.
 * 8/07/21 - Thea - Minor change at the LookAt function as the enemies weren't scaled properly
 * 09/07/21 - Oliver - Added reference to the UIController Script. Slightly refactored the Attack() Method to reduce some duplicate code as well as add in functionality to update newly
 * created Healthbars. Added ranges to all of the attack related properties to assist with designer ease of use.
 * 13/07/21 - Oliver - Added in functionality for PickupController within the Interact() method. Added in m_invulnerable and m_damageModifier to work with PickupController.
 * 14/07/21 - Thea - Edited LookAtDirection to flip sprite renderer instead of changing the scale, as we don't use weapons anymore and it was flipping the enemy ui on top of them.
 * 1/08/2 - Thea - Dynamically changing the position of the attack point and the charge attack point when the sprite is flipped
 */

public class CombatandMovement : MonoBehaviour, IDamagable
{
    [Header("References (DO NOT EDIT)")]
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Animator m_animator;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Rigidbody m_rigidbody;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_normalAttackPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_interactPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_specialAttackPoint;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Transform m_chargedAttackPoint;


    [Header("Settings")]
    [SerializeField] [Range(10, 300)] protected float m_maxHealth;
    [SerializeField] [Range(0, 10)] protected float m_movementSpeed;
    [SerializeField] protected string m_footstepSFX;
    [SerializeField] [Range(25, 300)] protected float m_jumpForce;
    [SerializeField] [Range(0, 3)] protected float m_pickupRange = 1.25f;
    [SerializeField] protected string m_normalAttackSound;
    [SerializeField] [Range(0, 10)] protected float m_normalAttackRange = 1.25f;
    [SerializeField] [Range(0, 50)] protected float m_normalAttackDamage = 1.0f;
    [SerializeField] protected string m_specialAttackSound;
    [SerializeField] [Range(0, 10)] protected float m_specialAttackRange = 0.75f;
    [SerializeField] [Range(0, 65)] protected float m_specialAttackDamage = 2.0f;

    [SerializeField] int m_scoreGainedOnSpecialAttack;
    [SerializeField] protected int m_scoreGainedOnChargedAttack;
    [SerializeField] protected LayerMask m_pickupLayer;
    [SerializeField] protected LayerMask m_targetLayer;
    [SerializeField] protected LayerMask m_collisionLayer;

    protected bool m_holdingObj;
    protected bool m_normalAttackActive;
    protected bool m_specialAttackActive;
    protected bool m_isBlocking;
    [HideInInspector] public bool m_invulnerable;

    private float m_normalAttackPointXpos;
    private float m_chargedAttackPointXpos;
    private Vector3 m_startScale;

    public GameObject heldObject;


    //Coming from the interface.
    public float ImaxHealth { get; set; }
    public float IcurrentHealth { get; set; }
    public bool IisBlocking { get; set; }
    public bool Iinvulnerable { get; set; }

    protected virtual void Start()
    {
        ImaxHealth = m_maxHealth;
        IcurrentHealth = ImaxHealth;
        m_holdingObj = false;
        m_normalAttackActive = false;
        m_specialAttackActive = false;
        m_isBlocking = false;
        IisBlocking = m_isBlocking;
        m_invulnerable = false;
        Iinvulnerable = m_invulnerable;
        m_startScale = transform.localScale;
        m_normalAttackPointXpos = m_normalAttackPoint.localPosition.x;

        if (m_chargedAttackPoint != null)
        {
            m_chargedAttackPointXpos = m_chargedAttackPoint.localPosition.x;
        }
    }
    #region Movement Methods
    protected virtual void Move(Vector3 direction)
    {
    }
    protected void PlayFootstep()
    {
        AudioManager.Instance.Play(m_footstepSFX);
    }
    protected virtual void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }
    protected void LookAtDirection(float direction)
    {

        //Vector3 CharacterScale = transform.localScale;
        //Debug.Log(CharacterScale);
        // if the direction is left
        if (direction < 0)
        {
            m_spriteRenderer.flipX = true;
            m_normalAttackPoint.localPosition = new Vector3(-m_normalAttackPointXpos, m_normalAttackPoint.localPosition.y, m_normalAttackPoint.localPosition.z);

            if (m_chargedAttackPoint != null)
            {
                m_chargedAttackPoint.localPosition = new Vector3(-m_chargedAttackPointXpos, m_chargedAttackPoint.localPosition.y, m_chargedAttackPoint.localPosition.z);
            }
            

            //CharacterScale.x = -m_startScale.x;
            //transform.localScale = CharacterScale;
        }
        // if the direction is right
        else if (direction > 0)
        {
            m_spriteRenderer.flipX = false;
            m_normalAttackPoint.localPosition = new Vector3(m_normalAttackPointXpos, m_normalAttackPoint.localPosition.y, m_normalAttackPoint.localPosition.z);

            if (m_chargedAttackPoint != null)
            {
                m_chargedAttackPoint.localPosition = new Vector3(m_chargedAttackPointXpos, m_chargedAttackPoint.localPosition.y, m_chargedAttackPoint.localPosition.z);
            }
            //CharacterScale.x = m_startScale.x;
            //transform.localScale = CharacterScale;
        }
        //Debug.Log(CharacterScale);
    }
    #endregion
    #region Interaction Methods
    public void Interact(ref bool holdingObj)
    {
        Collider[] colliders = Physics.OverlapSphere(m_interactPoint.position, m_pickupRange, m_pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            if (holdingObj != true)
            {
                EquipItem(nearbyObject.gameObject);
                holdingObj = true;
                //Debug.Log(holdingObj);
                PickupsController pickup = nearbyObject.gameObject.GetComponent<PickupsController>();
                if (pickup != null)
                {
                    pickup.PickupEffects(gameObject);
                }
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
    #endregion
    #region Attack Methods
    protected virtual void AttackEffects(GameObject gameObject)
    {

    }

    protected void Attack(Transform attackPoint, float attackRange, string sfxName, LayerMask targetLayer, float attackDamage)
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            // Checking if the nearby objects have damageable interface. If they do, they receive damage.
            IDamagable damagableTarget = nearbyObject.gameObject.GetComponent<IDamagable>();
            if (damagableTarget != null)
            {
                AudioManager.Instance.Play(sfxName); //Uses the instance of the AudioManager to play the associated attack sounds
                damagableTarget.TakeDamage(attackDamage);
                Debug.Log("Dealt " + attackDamage + " to " + damagableTarget);
                AttackEffects(nearbyObject.gameObject);
                if (targetLayer == LayerMask.GetMask("Player"))
                {
                    FindObjectOfType<UIController>().SetPlayerHealthBarPercent(damagableTarget.IcurrentHealth / damagableTarget.ImaxHealth);
                }
            }
        }
    }

    protected void NormalAttack(Transform normalAttackPoint, float normalAttackRange, string sfxName, LayerMask enemyLayer, float normalAttackDamage)
    {
       
        Attack(normalAttackPoint, normalAttackRange, sfxName, enemyLayer, normalAttackDamage);
    }

    protected void SpecialAttack(Transform specialAttackPoint, float specialAttackRange, string sfxName, LayerMask enemyLayer, float specialAttackDamage)
    {
        Debug.Log("Special Attack");
        Attack(specialAttackPoint, specialAttackRange, sfxName, enemyLayer, specialAttackDamage);
        FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnSpecialAttack);
    }

    //Following Methods needed due to limitations with Unity's Animation Events. Normal/SpecialAttack() have to be wrapped in a method to to the number of parameters they need.
    //SetNormal/SpecialAttackBool() methods needed as Unity Animation events are unable to directly toggle bools.
    protected void NormalAttackAnimEvent()
    {
        NormalAttack(m_normalAttackPoint, m_normalAttackRange, m_normalAttackSound, m_targetLayer, m_normalAttackDamage);
    }

    protected void SpecialAttackAnimEvent()
    {
        SpecialAttack(m_specialAttackPoint, m_specialAttackRange, m_specialAttackSound, m_targetLayer, m_specialAttackDamage);
    }

    protected void SetNormalAttackBool()
    {
        if (m_normalAttackActive == false)
        {
            m_normalAttackActive = true;
        }
        else
        {
            m_normalAttackActive = false;
        }
    }

    protected void SetSpecialAttackBool()
    {
        if (m_specialAttackActive == false)
        {
            m_specialAttackActive = true;
        }
        else
        {
            m_specialAttackActive = false;
        }
    }
    #endregion
    #region Interface Required Methods
    //Mandatory functions, coming from the interface. If these functions are not added to this script, there will be an error. 
    //If these functions are not needed here anymore, they must be removed from the interface too

    public virtual void OnTakeDamage(float damage)
    {
    }

    //Debug

    public void OnDrawGizmosSelected()
    {
        if (m_normalAttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(m_normalAttackPoint.position, m_normalAttackRange);
    }
    public virtual void Die()
    {
        //debug:
        Destroy(gameObject);
        //todo: some particles, sounds and animations
    }
    #endregion
}

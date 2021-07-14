using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Changelog
/*Inital Script and movement logic created by Thea (date)
 * 24/06/21 - Oliver - Changed local float variables h and v to declared Vector2 _input. _input.x/y used in same way that float h/v were.
 * Changed Input.GetKeyDown(Spacebar) to (Input.GetButtonDown("Jump") to make the base movement more platform agnostic.
 * Over rode the Move() function to add in m_animator controls for changing the animator state. Added a rigidbody velocity check to the if statement used to activate Jump().
 * This insures that the player cannot double jump and changes the animator state.
 * Created Pickup()/Drop(), Added OnDrawGizmosSelected() and all associated properties and methods.
 * 29/06/21 - Oliver - Changed class name from Player1 to PlayerController. Moved Pickup and Drop to CombatandMovementController, made both methods virtual methods.
 * Thea - Moved the movement logic from base character to here.
 * 30/06/21 - Oliver - Added in animator functionality for Attacking and Blocking. Added a SetAttackBool() Method to stop the the player from moving while attacking.
 * 01/07/21 - Oliver - Moved LookAtDirection to LateUpdate to enable localScale functionality in conjuction with Animator component. 
 * Created AttackAnimEvent() so that NormalAttack() can be called via animation event.
 * 04/07/21 - Oliver - Created instance of PlayerController due to github errors. Transitioned over from Unity's old input system to the new input system.
 * Due to change added methods OnMovement(), OnNormalAttack(), OnSpecialAttack(), OnChargedAttack(), OnInteract(), OnBlock() and OnJump(). Added SetSpecialAttackBool(), SetChargedAttackBool()
 * SpecialAttackAnimEvent() and ChargedAttackAnimEvent() to control additional attacks. Created IsGrounded() to check if the player had jumped and set the corresponding bool. Added FixedUpdate for physics.
 * Created m_maxCharge and m_currentCharge for ChargedAttack.
 * 07/07/21 - Oliver - Moved SetNormalAttackBool(), SetSpecialAttackBool(), SetChargedAttackBool(), NormalAttackAnimEvent(), and SpecialAttackAnimEvent() to CombatandMovement 
 * so that these methods can be called by all inheriting classes via animation events.
 * 09/07/21 - Oliver - Added in the functionality to update the UI Charge Bar.
 */

public class PlayerController : CombatandMovement
{
    private Vector2 m_input;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected PlayerInput m_playerInput;
    [Header("Player Settings ")]
    [SerializeField] protected float m_maxCharge = 75f;
    [SerializeField] protected float minimumChargeLevel = 25f;
    [SerializeField] protected float chargeGain = 0.5f;
    [SerializeField] protected float attackchargeDrain = 5f;
    [HideInInspector] public float m_currentCharge;
    private bool m_chargedAttackActive;
    protected bool m_isGrounded;

    protected override void Start()
    {
        base.Start();
        m_currentCharge = 0;
        m_chargedAttackActive = false;
        m_isGrounded = true;
    }

    protected bool IsGrounded(ref bool isGrounded)
    {
        if (Physics.BoxCast(m_collider.bounds.max, m_collider.bounds.extents, Vector3.down, transform.rotation, 1.2f, m_collisionLayer))
        {
            return isGrounded = true;
        }
        else
        {
            return isGrounded = false;
        }
    }

    protected override void Move(Vector3 direction)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_isBlocking == false)
        {
            Vector3 movement = direction * Time.deltaTime * m_movementSpeed;

            transform.position += movement;
        }
    }

    protected void SetChargedAttackBool()
    {
        if (m_chargedAttackActive == false)
        {
            m_chargedAttackActive = true;
        }
        else
        {
            m_chargedAttackActive = false;
        }
    }

    protected override void AttackEffects(GameObject gameObject)
    {
        if(m_currentCharge >= minimumChargeLevel)
        {
            gameObject.GetComponent<IDamagable>().TakeDamage(5f);
            m_currentCharge -= attackchargeDrain;
            m_uiController.SetChargeMeterPercent(m_currentCharge / m_maxCharge);
            Debug.Log(gameObject + " takes 5 shock damage");
        }
    }

    protected void ChargedAttack()
    {
        Debug.Log("ChargedAttack");
        m_currentCharge -= m_maxCharge;
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

    //Unity Input Systems Action Callbacks

    public void OnMovement(InputAction.CallbackContext value)
    {
        m_input = value.ReadValue<Vector2>();

        m_animator.SetFloat("InputX", m_input.x);
        m_animator.SetFloat("InputY", m_input.y);
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        Interact(ref m_holdingObj);
    }

    public void OnNormalAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false)
        {
            m_animator.SetTrigger("NormalAttack");
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false)
        {
            m_animator.SetTrigger("SpecialAttack");
        }
    }

    public void OnChargedAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_currentCharge >= m_maxCharge)
        {
            m_animator.SetTrigger("ChargedAttack");
        }
    }

    public void OnBlock(InputAction.CallbackContext value)
    {
        m_isBlocking = value.performed;

        if (m_isBlocking == true)
        {
            Debug.Log("Blocking");
            m_animator.SetBool("Block", true);
        }
        else if (m_isBlocking != true)
        {
            m_animator.SetBool("Block", false);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (m_isGrounded == true)
        {
            Jump();
        }
    }

    void Update()
    {
        Move(new Vector3(m_input.x, 0, m_input.y));
        m_animator.SetBool("Grounded", m_isGrounded);
        if(m_currentCharge <= m_maxCharge)
        {
            m_currentCharge += chargeGain * Time.deltaTime;
        }
        m_uiController.SetChargeMeterPercent(m_currentCharge / m_maxCharge);
    }

    private void FixedUpdate()
    {
        IsGrounded(ref m_isGrounded);
        //Debug.Log(IsGrounded(ref m_isGrounded));
    }

    void LateUpdate()
    {
        LookAtDirection(m_input.x);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changelog
/*Inital Script created by Thea (date)
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
 */

public class PlayerController : CombatandMovement
{
    private Vector2 _input;

    private void Start()
    {
        m_holdingObj = false;
        m_isAttacking = false;
        m_isBlocking = false;
    }

    protected void SetAttackBool()
    {
        if(m_isAttacking == false)
        {
            m_isAttacking = true;
        }
        else
        {
            m_isAttacking = false;
        }
    }

    protected override void Move(Vector3 direction)
    {
        if(m_isAttacking == false && m_isBlocking == false)
        {
            Vector3 movement = direction * Time.deltaTime * m_movementSpeed;

            transform.position += movement;
        }
    }

    protected void AttackAnimEvent()
    {
        NormalAttack(m_normalAttackPoint, m_normalAttackRange, m_enemyLayer, m_normalAttackDamage);
    }

    protected override void NormalAttackEffects()
    {

    }

    protected override void BlockEffects()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        if (m_originPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(m_originPoint.position, m_pickupRange);
    }

    //https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        m_animator.SetFloat("InputX", _input.x);
        m_animator.SetFloat("InputY", _input.y);

        //LookAtDirection(_input.x);
        Move(new Vector3(_input.x, 0, _input.y));

        if (Input.GetButtonDown("Fire1") && m_isAttacking == false)
        {
            m_animator.SetTrigger("Attack");
        }

        if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Blocking");
            m_animator.SetBool("Block", true);
            m_isBlocking = true;
            Block();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            Debug.Log("Not Blocking");
            m_animator.SetBool("Block", false);
            m_isBlocking = false;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(m_rigidbody.velocity.y) < 0.001f)
        {
            m_animator.SetBool("Grounded", false);
            Jump();
        }
        else if (Mathf.Abs(m_rigidbody.velocity.y) <= 0.001f)
        {
            m_animator.SetBool("Grounded", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact(ref m_holdingObj);
        }
    }

    void LateUpdate()
    {
        LookAtDirection(_input.x);
    }
}

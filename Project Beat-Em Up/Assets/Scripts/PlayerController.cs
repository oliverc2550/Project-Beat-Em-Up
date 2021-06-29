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
 */

public class PlayerController : CombatandMovementController
{
    private Vector2 _input;

    private void Start()
    {
        m_objPickedup = false;
    }

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
        if(direction.x != 0 || direction.z != 0)
        {
            m_animator.SetInteger("AnimState", 1);
            if (Mathf.Abs(m_rigidbody.velocity.y) >= 0.001f)
            {
                m_animator.SetInteger("AnimState", 0);
            }
        }
        else if(direction.x == 0 || direction.z == 0)
        {
            m_animator.SetInteger("AnimState", 0);
        }
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

        LookAtDirection(_input.x);
        Move(new Vector3(_input.x, 0 , _input.y));

        if (Input.GetButtonDown("Fire1"))
        {
            // TODO: play attack animation
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(m_objPickedup == false)
            {
                Pickup();
            }
            else if(m_objPickedup == true)
            {
                Drop();
            }
        }
    }
}

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
 * 
 */

public class Player1 : CharacterController1
{
    private Vector2 _input;
    [SerializeField] private Transform _originPoint;
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private float _pickupRange = 0.25f;
    [SerializeField] private bool _objPickedup;

    private void Start()
    {
        _objPickedup = false;
    }

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
        if(direction.x > 0 || direction.z > 0 || direction.x < 0 || direction.z < 0)
        {
            m_animator.SetInteger("AnimState", 2);
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

    private void Pickup()
    {
        Collider[] colliders = Physics.OverlapSphere(_originPoint.position, _pickupRange, _pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            nearbyObject.GetComponent<Rigidbody>().useGravity = false;
            nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            nearbyObject.transform.position = _originPoint.position;
            nearbyObject.transform.parent = _originPoint;
            _objPickedup = true;
        }
    }

    private void Drop()
    {
        Collider[] colliders = Physics.OverlapSphere(_originPoint.position, _pickupRange, _pickupLayer); //Get an array of colliders using Physics.OverlapSphere
        foreach (Collider nearbyObject in colliders) //Iterate over each collider in the list
        {
            nearbyObject.transform.parent = null;
            nearbyObject.GetComponent<Rigidbody>().useGravity = true;
            nearbyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _objPickedup = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (_originPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_originPoint.position, _pickupRange);
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
            if(_objPickedup == false)
            {
                Pickup();
            }
            else if(_objPickedup == true)
            {
                Drop();
            }
        }
    }
}

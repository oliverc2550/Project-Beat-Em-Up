using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;

//Changelog
/*Inital Script created by Thea (date)
 * 19/06/21 - Thea - Movement logic 
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
 * 09/08/21 - Thea - Gain score and shake the camera when charged attack is used
 * 15/08/21 - Thea - onNormalAttack event created in order to know when the player is going to attack and so that evemies can block it.
 * 17/08/21 - Oliver - Added in functionality for playerlives and changed OnInteract method to properly work with pickups
 */

public class PlayerController : CombatandMovement
{
    #region Variables
    //Variables serialized so that the designer could edit them in the editor
    private Vector2 m_input;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected PlayerInput m_playerInput;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] CinemachineVirtualCamera m_playerCamera;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected Collider m_collider;
    [Tooltip("Changing this might cause errors. Please DO NOT change this without consulting with a developer.")]
    [SerializeField] protected UIController m_uiController;
    [HideInInspector] public PickupsController m_pickupController;

    [Header("Player Settings ")]
    public int m_playerLives = 3;
    [SerializeField] [Range(0, 10)] protected float m_chargedAttackRange = 1.0f;
    [SerializeField] [Range(0, 65)] protected float m_chargedAttackDamage = 25.0f;
    [SerializeField] protected string m_chargedAttackSound;
    [SerializeField] protected float minimumChargeLevel = 25f;
    [SerializeField] protected float chargeGain = 0.5f;
    [SerializeField] protected float attackchargeDrain = 5f;
    [SerializeField] public float maxCharge = 75f;
    [HideInInspector] public float currentCharge;
    private bool m_chargedAttackActive;
    private float m_skinWidth = 0.1f;
    protected bool m_isGrounded;
    [HideInInspector] public bool m_pickupInRange;
    [HideInInspector] public bool m_isBossCamEnabled;
    [HideInInspector] public UnityEvent onNormalAttackEvent;
    #endregion
    //Overriden Start method
    protected override void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        base.Start();
        currentCharge = 0;
        m_chargedAttackActive = false;
        m_isGrounded = true;
        m_pickupInRange = false;
        m_uiController.UpdateLives(m_playerLives);
        m_pickupController = null;
    }
    //Custom method to check if the player is grounded or not
    protected bool IsGrounded(ref bool isGrounded)
    {
        if (Physics.BoxCast(m_collider.bounds.max, m_collider.bounds.extents, Vector3.down, transform.rotation, m_collider.bounds.extents.y + m_skinWidth, m_collisionLayer))
        {

            return isGrounded = true;
        }
        else
        {
            return isGrounded = false;
        }
    }
    //Overriden Move method
    protected override void Move(Vector3 direction)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_isBlocking == false && m_isBossCamEnabled == false)
        {
            Vector3 movement = -direction * Time.deltaTime * m_movementSpeed;

            transform.position += movement;
        }
    }
    //SetChargedAttackBool used in the same way the Normal and Special Attack versions are in the base class
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
    //ChargedAttack method used with Unity Animation event to trigger attack
    protected void ChargedAttack()
    {
        Debug.Log("ChargedAttack");

        Attack(m_chargedAttackPoint, m_chargedAttackRange, m_chargedAttackSound, m_targetLayer, m_chargedAttackDamage);
        currentCharge -= maxCharge;
        m_uiController.SetChargeMeterPercent(currentCharge / maxCharge);

        FindObjectOfType<ScoreManager>().AddScore(m_scoreGainedOnChargedAttack);
        m_playerCamera.transform.DOShakeRotation(1, 3);
    }
    //Coroutine to disable invulnerability after a set amount of time
    IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(5f);
        m_invulnerable = false;
        m_uiController.DisablePowerUpDisplay();
    }
    //Overriden Die method to reduce the players lives once they hit 0 hp
    public override void Die()
    {
        m_playerLives -= 1;
        m_uiController.UpdateLives(m_playerLives);
        if (m_playerLives <= 0)
        {
            m_uiController.EnableRestartMenu();
        }
        IcurrentHealth = ImaxHealth;
    }

    //Unity Input Systems Action Callbacks
    //Functions to be used with the new Unity Input System. Assign in the inspector through the Input component on the player
    public void OnMovement(InputAction.CallbackContext value)
    {
        m_input = value.ReadValue<Vector2>();

        m_animator.SetFloat("InputX", m_input.x);
        m_animator.SetFloat("InputY", m_input.y);
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if(m_pickupInRange == true && value.performed)
        {
            m_pickupController.PickupEffects(gameObject);
        }
    }

    public void OnNormalAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_isBlocking != true)
        {
            onNormalAttackEvent.Invoke();
            m_animator.SetTrigger("NormalAttack");
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_isBlocking != true && currentCharge >= attackchargeDrain)
        {
            m_animator.SetTrigger("SpecialAttack");
            currentCharge -= attackchargeDrain;
            m_uiController.SetChargeMeterPercent(currentCharge / maxCharge);
        }
    }

    public void OnChargedAttack(InputAction.CallbackContext value)
    {
        if (m_normalAttackActive == false && m_specialAttackActive == false && m_chargedAttackActive == false && m_isBlocking != true && currentCharge >= maxCharge)
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
            Debug.Log("Not Blocking");
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
    //Update used to move the player, gradually increase the players charge level and check if the player has an active invulnerability pickup active
    void Update()
    {
        Move(new Vector3(m_input.x, 0, m_input.y));
        m_animator.SetBool("Grounded", m_isGrounded);
        if(currentCharge <= maxCharge)
        {
            currentCharge += chargeGain * Time.deltaTime;
        }
        m_uiController.SetChargeMeterPercent(currentCharge / maxCharge);
        if(m_invulnerable == true)
        {
            StartCoroutine(InvulnerabilityTimer());
        }
    }
    //FixedUpdate used for ground checks
    private void FixedUpdate()
    {
        IsGrounded(ref m_isGrounded);
        //Debug.Log(IsGrounded(ref m_isGrounded));
    }
    //LateUpdate used to flip the character sprite if needed
    void LateUpdate()
    {
        LookAtDirection(m_input.x);
    }
}
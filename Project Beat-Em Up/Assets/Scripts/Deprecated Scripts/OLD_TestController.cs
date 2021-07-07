using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_TestController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float movementSpeed = 0.5f;
    public float jumpForce = 1f;
    public Transform glowLightPos;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AttackPos()
    {
        glowLightPos.position += Vector3.up * 1.35f;
    }

    public void IdlePos()
    {
        glowLightPos.position += Vector3.down * 1.35f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            animator.SetInteger("AnimState", 2);
            spriteRenderer.flipX = false;
            if (Mathf.Abs(rigidbody2D.velocity.y) >= 0.001f)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            animator.SetInteger("AnimState", 2);
            spriteRenderer.flipX = true;
            if (Mathf.Abs(rigidbody2D.velocity.y) >= 0.001f)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody2D.velocity.y) < 0.001f)
        {
            animator.SetBool("Grounded", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (Mathf.Abs(rigidbody2D.velocity.y) <= 0.001f)
        {
            animator.SetBool("Grounded", true);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }
}

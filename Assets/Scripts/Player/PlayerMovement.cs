using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Camera MainCam;
    public static Rigidbody2D rb;
    public float MovementSpeed = 0.75f;
    public Animator animator;
    Vector2 movement;
    public static bool facingRight = true;
    public static bool MovementAbility;

    public Image DashBar;
    private Vector2 DashSpeed;
    public float InitialDashSpeed;
    public float SlowModifier;
    private float LastUseTime;
    public float Cooldown;
    private bool Active = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        DashBar.fillAmount = CooldownState();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("speed", movement.sqrMagnitude*2);
        if (movement.x > 0 && !facingRight)
            Flip();
        else if (movement.x < 0 && facingRight)
            Flip();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= LastUseTime + Cooldown)
            Dash();
        if (Active)
        {
            SlowDown();
        }
    }

    public float CooldownState()
    {
        return ((Cooldown - (Time.time - LastUseTime)) / Cooldown);
    }

    public static void SetMovementAbility(bool setting)
    {
        MovementAbility = setting;
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void SlowDown()
    {
        this.GetComponentInParent<Rigidbody2D>().velocity -= DashSpeed / SlowModifier;
        DashSpeed = this.GetComponentInParent<Rigidbody2D>().velocity;
        if (this.GetComponentInParent<Rigidbody2D>().velocity.magnitude < 3f)
        {
            Active = false;
            PlayerMovement.MovementAbility = false;
            this.GetComponentInParent<Collider2D>().isTrigger = false;
        }

    }

    private void Dash()
    {
        PlayerMovement.SetMovementAbility(true);
        PlayerMovement.rb.velocity = (InitialDashSpeed * movement);
        this.GetComponentInParent<Collider2D>().isTrigger = true;
        DashSpeed = PlayerMovement.rb.velocity;
        LastUseTime = Time.time;
        Active = true;
    }
    public static void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if(!MovementAbility)
        {
            rb.velocity = (movement * MovementSpeed * Time.fixedDeltaTime * 20);
        }
        MainCam.transform.position = new Vector3(rb.position.x, rb.position.y, -10);
    }
}

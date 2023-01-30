using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Search;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletpreFab;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool isGrounded;
    private float LastShoot;
    private float raycastdist = 0.1f;
    private Animator Animator;
    private int Health=5;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal>0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Animator.SetBool("running",Horizontal!=0.0f);
        // Debug.DrawRay(transform.position,Vector3.down*raycastdist,Color.green);
        if (Physics2D.Raycast(transform.position, Vector3.down, raycastdist))
        {
            isGrounded = true;
        }
        else isGrounded = false;
       
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)&& isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space)   && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
       
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        // Debug.Log(Rigidbody2D.velocity);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x  == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(BulletpreFab,transform.position +direction*0.1f,Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if(Health ==0) Destroy(gameObject);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public AudioClip Sound;
    [SerializeField] private float Speed;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction*Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement john = collision.GetComponent<Collider2D>().GetComponent<JohnMovement>();
        GruntScript grunt = collision.GetComponent<Collider2D>().GetComponent<GruntScript>();
        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null)
        {
            grunt.Hit();
        }
        DestroyBullet();
    }
}

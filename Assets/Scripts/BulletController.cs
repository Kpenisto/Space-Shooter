/*
Author: Kyle Peniston
Date: 10/29/2024
Description: This script is used control the bullet logic and destroy the animation after 6 seconds to conserve memory. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float bulletSpeed = 10f;

    void Start()
    {
        //Add rigid body and destroy after 6 seconds
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        //Bullet speed and trajectory
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
}

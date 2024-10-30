/*
Author: Kyle Peniston
Date: 10/29/2024
Description: This script is used to handle the Asteroid movement controls and asteroid collision actions with the player and bullet.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AsterController : MonoBehaviour
{
    //Asteroid vars
    private float speed = 1.5f;
    public GameObject explodePrefab;
    private Vector3 targetPosition;
    private GameController gameController;
    private Vector3 moveDirection;
    private bool gameOver = false;

    void Start()
    {
        //Self Destruct in 12 seconds
        Destroy(gameObject, 12f);
        gameController = FindObjectOfType<GameController>();
        gameOver = gameController.gameOver;

        //Asteroids trajectory vars
        targetPosition = gameController.player.transform.position;
        moveDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        gameOver = gameController.gameOver;

        if (!gameOver)
        {
            // Move towards the initial target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            // Continue moving in the same direction once Game Over is true
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    public void SetTarget(Vector3 target)
    {
        //Player position
        targetPosition = target;
        moveDirection = (targetPosition - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //When asteroid comes in contact with bullet, destroy and display explosion animation
        if (other.CompareTag("Bullet"))
        {
            Instantiate(explodePrefab, transform.position, transform.rotation);
            Destroy(other.gameObject); // Destroy bullet
            Destroy(gameObject); // Destroy asteroid

            //Update Score
            gameController.updateScore();
        }
    }
}

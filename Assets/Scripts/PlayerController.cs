/*
Author: Kyle Peniston
Date: 10/29/2024
Description: This script is used to handle the Player movement controls, asteroid collision actions with the player,
and to instantiate the bullet using the "Space" bar
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Global Vars
    private Rigidbody2D rb2d;
    public Text winText;
    public float rotationSpeed = 200f;

    //GameController script
    private GameController gameController;

    //Game Objects
    public GameObject explodePrefab;
    public GameObject bulletPrefab;
    public GameObject background;

    void Start()
    {
        //Initialize components
        rb2d = GetComponent<Rigidbody2D>();
        gameController = background.GetComponent<GameController>();
    }

    void Update()
    {
        //Player rotation movement
        float h = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, 0, -h * Time.deltaTime);

        //Shooting action
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //On collision with Asteroid, remove player
        if (other.gameObject.CompareTag("Aster"))
        {
            //Remove player and start explode animation
            gameObject.SetActive(false);
            Instantiate(explodePrefab, transform.position, transform.rotation);

            //Destroy Asteroid that hit player
            Destroy(other.gameObject);

            //Set game over
            winText.text = "Game Over!";
            gameController.setGameOver(true);
        }
    }

    void Shoot()
    {
        //Instantiate and fire bullet
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}

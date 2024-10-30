/*
Author: Kyle Peniston
Date: 10/29/2024
Description: This script is used to handle the Game UI, asteroid generations, and game over actions. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Asteroids spawn vars
    public Transform asterPrefab;
    private float spawnTime = 1f;
    private float spawnDelay = 0f;
    public float spawnRadius = 10f;

    //UI Text Vars
    public Text timerText;
    public Text winText;
    public Text scoreText;
    private float timer = 0f;
    int maxSeconds = 60;
    int score = 0;
    public Button restartButton;
    public bool gameOver = false;

    //Turn player into trigger collider
    public Collider2D player;

    void Start()
    {
        //Set default start values
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
        timerText.text = maxSeconds.ToString();
        winText.text = "";
        scoreText.text = "Score: " + score.ToString();
        restartButton.gameObject.SetActive(false);
    }

    //Handle asteroids spawn positioning and trajectory
    private void Spawn()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius; // Circular spawn area
        Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
        Transform asteroid = Instantiate(asterPrefab, spawnPosition, Quaternion.identity);
        asteroid.GetComponent<AsterController>().SetTarget(player.transform.position);
    }

    //Handle UI updates over time
    void Update()
    {
        if (!gameOver)
        {
            timer += Time.deltaTime;
            int seconds = maxSeconds - (int)timer;

            //Stop timer at 0 seconds
            if (seconds <= 0)
            {
                seconds = 0;
            }

            timerText.text = seconds.ToString() + "s";

            //Win condition
            if (seconds == 0)
            {
                winText.text = "You Win!";
                setGameOver(true);
            }
        }
    }

    //Game over logic
    public void setGameOver(bool b)
    {
        //Turn player into a trigger
        player.isTrigger = true;
        gameOver = b;
        restartButton.gameObject.SetActive(true);
    }

    //Update score every asteroid collision with bullet
    public void updateScore()
    {
        if (!gameOver)
        {
            scoreText.text = "Score: " + (++score).ToString();
        }
    }

    //Reset scene
    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

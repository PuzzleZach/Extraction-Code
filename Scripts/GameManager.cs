using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TMP_Text timeRemaining;
    public TMP_Text currentWave;


    public GameObject helicopter;
    public Transform evacSpawnPoint;
    // Seconds left will later be modified by difficulty?
    private int normalModeTime = 100;
    private int hardModeTime = 200;
    private int secondsLeft;
    private int wave = 0;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("HardCoreDifficulty") == 1)
        {
            secondsLeft = hardModeTime;
        }
        else
        {
            secondsLeft = normalModeTime;
        }
        secondsLeft = 10; // heh
        currentWave.text = wave.ToString();
        timeRemaining.text = secondsLeft.ToString();
        InvokeRepeating("TimerDecrease", 1.0f, 1.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        if (secondsLeft < 1 && !gameOver)
        {
            EvacuateSequence();
        }
         
    }

    private void TimerDecrease()
    {
        if (!gameOver)
        {
            secondsLeft -= 1;
            timeRemaining.text = secondsLeft.ToString();
        }
    }

    public void AddWave()
    {
        wave++;
        currentWave.text = wave.ToString();
    }

    public void EvacuateSequence()
    {
        // Pause the countdown timer.
        gameOver = true;

        // Instantiate helicopter object.
        Instantiate(helicopter, evacSpawnPoint.position, helicopter.transform.rotation);
    }

    public void SetWinScene()
    {
        SceneManager.LoadScene(2);
    }

    public void SetLoseScene()
    {
        SceneManager.LoadScene(3);
    }
}

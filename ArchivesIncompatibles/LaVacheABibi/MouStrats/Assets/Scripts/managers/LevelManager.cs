using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    private int timeLeft;
    private string timerText;
    private List<bool> collectedObjectives;
    [SerializeField] Canvas canvas; 
    [SerializeField] string LevelName;
    [SerializeField] int timerLenght;
    [SerializeField] List<GameObject> objectiveList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timeLeft = timerLenght;
        StartCoroutine(StartCountdown());
        collectedObjectives = new List<bool>(objectiveList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        checkEndGame();
        checkObjectivesCollected();
    }

    private void checkEndGame()
    {
        if (timeLeft < 1)
        {
            LevelTimeout();
        }
    }


    private IEnumerator StartCountdown()
    {
        while (timeLeft > 0)
        {
            //Debug.Log("Countdown: " + formatTimer(timeLeft));
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
    }

    private void LevelTimeout()
    {
        //TODO END OF TIME
    }

    private string formatTimer(int time)
    {
        int secondes = time % 60;
        int minutes = time / 60;
        return minutes.ToString() + ":" + secondes.ToString();

    }

    public static LevelManager getInstance()
    {
        // # YOLO
        return instance;
    }


    private void checkObjectivesCollected()
    {
        foreach (var gameObject in objectiveList) {
            
        }
    }

    public int getTimeLeft()
    {
        return timeLeft;
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Gère tous les évènements dépassant le cadre d'une scène. Persistent d'une scène à l'autre
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameStateManager;
    [SerializeField] GameObject mouseManager;
    [SerializeField] GameObject inGameResourceManager;
    [SerializeField] List<Scene> levels;
    private int currentSceneIndex = 0;
    private static GameManager instance = null;

    private List<GameObject> persistentManagers;
    public static GameManager getInstance()
    {
        // YOLO, ne devrait pas être null
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        InitPersistentObjects();
    }

    void Update()
    {
        if (getCurrentScene().name == "MainMenu") {
            return;
        }
        CheckPauseMenu();
    }

    private void InitPersistentObjects()
    {
        persistentManagers = new List<GameObject>();
        persistentManagers.Add(gameStateManager);
        persistentManagers.Add(mouseManager);
        persistentManagers.Add(inGameResourceManager);

        for (int i = 0; i < persistentManagers.Count; i++) {
            DontDestroyOnLoad(persistentManagers[i]);
        }
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public Scene GetLevelByIndex(int index) {
        if(index > 0 && index < levels.Count)
        {
            return levels[index];
        }
        return levels[0];
    }

    public void LoadNextLevel()
    {
        ChangeCurrentLevel(LevelNavigator.NEXT);
        SceneManager.LoadScene(levels[currentSceneIndex].name);
    }

    private void ChangeCurrentLevel(LevelNavigator nav)
    {
        switch (nav)
        {
            case LevelNavigator.NEXT: { currentSceneIndex++; break; }
            case LevelNavigator.PREVIOUS: { currentSceneIndex++; break; }
        }
    }

    private void CheckPauseMenu() {
        // TODO : PAUSE MENU
    }
}

public enum LevelNavigator
{
    PREVIOUS,
    NEXT
}

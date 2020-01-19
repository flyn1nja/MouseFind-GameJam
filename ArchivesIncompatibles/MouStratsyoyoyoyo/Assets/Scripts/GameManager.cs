using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Gère tous les évènements dépassant le cadre d'une scène. Persistent d'une scène à l'autre
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inGameResourceManager;
    [SerializeField] GameObject levelFader;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] List<string> levels;
    private int currentSceneIndex = 0;
    private static GameManager instance = null;

    private Dictionary<string, GameObject> persistentManagers;

    public static bool gameIsPaused = false;

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

    public  GameObject GetManager(string key)
    {
        return persistentManagers[key];
    }

    private void InitPersistentObjects()
    {
        persistentManagers = new Dictionary<string,GameObject>();
        persistentManagers.Add("inGameResourceManager", inGameResourceManager);
        persistentManagers.Add("levelFader", levelFader);
        persistentManagers.Add("pauseScreen", pauseScreen);

        foreach (KeyValuePair<string,GameObject> manager in persistentManagers) {
            DontDestroyOnLoad(manager.Value);
        }
        DontDestroyOnLoad(pauseMenuUI);
        foreach (GameObject g in pauseMenuUI.GetComponentsInChildren<GameObject>()) {
            DontDestroyOnLoad(g);
        }
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public string GetLevelByIndex(int index) {
        if (index > 0 && index < levels.Count)
        {
            return levels[index];
        }
        return levels[0];
    }

    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(GetLevelByIndex(index));
    }

    public void LoadNextLevel()
    {
        ChangeCurrentLevel(LevelNavigator.NEXT);
        SceneManager.LoadScene(levels[currentSceneIndex]);
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

        if (Input.GetKeyDown(KeyCode.Escape) && getCurrentScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            if (gameIsPaused)
            {
                Debug.Log("Game quitted!");
                Application.Quit();
            }
            else
            {
                PauseGame();
            }
        }
        else if (Input.anyKeyDown && gameIsPaused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
            }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
}

public enum LevelNavigator
{
    PREVIOUS,
    NEXT
}

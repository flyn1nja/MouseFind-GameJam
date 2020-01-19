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
    [SerializeField] GameObject endScrenUI;
    [SerializeField] List<string> levels;
    private int currentSceneIndex = 0;
    private static GameManager instance = null;
    private bool endedGame = false;

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
        if (getCurrentScene().name == "MainMenu" && pauseScreen.transform.gameObject.activeSelf)
        {
            pauseScreen.transform.gameObject.SetActive(false);
        }

    void Update()
    {
        if (getCurrentScene().name == "MainMenu" && pauseScreen.transform.gameObject.activeSelf)
        {
            pauseScreen.transform.gameObject.SetActive(false);
        }

            if (endedGame && Input.GetKeyDown(KeyCode.Escape)) {
            endedGame = false;
            endScrenUI.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }

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

    public void ShowEndScreen(int points)
    {
        endedGame = true;
        endScrenUI.SetActive(true);
         GameObject results = endScrenUI.transform.GetChild(0).gameObject;
        switch (points) {
            case 0:
            case 1: { results.GetComponent<UnityEngine.UI.Text>().text = "The queen is happy with your devotion. Even though "+points+" cheese snacks ain't much, it's honest work."; break; }
            case 2:
            case 3: 
            case 4: { results.GetComponent<UnityEngine.UI.Text>().text = "The queen is very impressed by your devotion. Good job on getting those " + points + " cheese snacks!"; break; }
            case 5: 
            case 6: { results.GetComponent<UnityEngine.UI.Text>().text = "The queen is amazed by your display of talent. Congratulations for  " + points + " cheese snacks!"; break; }
            default: { results.GetComponent<UnityEngine.UI.Text>().text = "Good Job on this never-seen-before-performance. Here, get one more cheese. \n Score: " + points+1; break; }

        }
        results.GetComponent<UnityEngine.UI.Text>().color = Color.white;
    }
}

public enum LevelNavigator
{
    PREVIOUS,
    NEXT
}

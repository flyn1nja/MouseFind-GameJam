using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlayGame() => animator.SetTrigger("FadeIn");


    public void QuitGame()
    {
        Debug.Log("Game quitted!");
        Application.Quit();
    }    
}

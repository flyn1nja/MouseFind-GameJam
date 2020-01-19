using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void OnFadeComplete()
    {
        animator.SetTrigger("FadeOut");
    }


    public void FadeToKitchen()
    {
        GameManager.getInstance().LoadLevelByIndex(0);
    }
}

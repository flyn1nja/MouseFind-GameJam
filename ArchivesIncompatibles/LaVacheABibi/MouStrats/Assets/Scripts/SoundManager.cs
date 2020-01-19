using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] float fadeTime;
    [SerializeField] float timeBetweenFade;

    public AudioSource calmOst;
    public AudioSource stressingOst;
    public bool isCalmToStress;
    public bool canFade;
    public bool needFade;
    // Start is called before the first frame update
    void Start()
    {
        needFade = false;
        isCalmToStress = true;
        canFade = true;
        calmOst.volume = 1;
        stressingOst.volume = 0;
    }

    private void Update()
    {
        if(GameObject.Find("MouseManager").GetComponent<MouseManager>().ratOnAware == 0 && isCalmToStress == false)      //Need to check if it still works too 
        {
            needFade = true;
        }
        else if(GameObject.Find("MouseManager").GetComponent<MouseManager>().ratOnAware != 0 && isCalmToStress == true && canFade)
        {
            needFade = true;
        }
        //if (!canFade && needFade && isCalmToStress)      //needs to be sure it still works
        //{
        //    needFade = false;
        //}

        if(needFade && canFade)
        {
            if(isCalmToStress)
            {
                if (calmOst.volume > 0.05 || stressingOst.volume < 0.95)
                {
                    calmOst.volume -= Mathf.Lerp(0, 1, Time.deltaTime * fadeTime);
                    stressingOst.volume += Mathf.Lerp(0, 1, Time.deltaTime * fadeTime);
                }
                else
                {
                    stressingOst.volume = 1;
                    isCalmToStress = false;
                    needFade = false;
                    canFade = false;
                    Invoke("CanFadeAgain", timeBetweenFade);
                }
            }
            else
            {
                if (calmOst.volume < 0.95 || stressingOst.volume > 0.05)
                {
                    calmOst.volume += Mathf.Lerp(0, 1, Time.deltaTime * fadeTime);
                    stressingOst.volume -= Mathf.Lerp(0, 1, Time.deltaTime * fadeTime);
                }
                else
                {
                    calmOst.volume = 1;
                    stressingOst.volume = 0;
                    isCalmToStress = true;
                    needFade = false;
                    canFade = false;
                    Invoke("CanFadeAgain", timeBetweenFade);
                }
            }
        }

    }

    void CanFadeAgain()
    {
        Debug.Log("dsjhdgagdhja");
        canFade = true;
    }

}

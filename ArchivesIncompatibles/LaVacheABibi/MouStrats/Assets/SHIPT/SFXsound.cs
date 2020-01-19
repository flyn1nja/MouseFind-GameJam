using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpSound1;
    public AudioClip jumpSound2;
    public AudioClip special;

    private void Update()
    {
        if(this.gameObject.name == GameObject.Find("MouseManager").GetComponent<MouseManager>().currentMouse.transform.name)
        {
            if(Input.GetButtonDown("Jump"))
            {
                int random = Random.Range(0, 2);
                if(random == 1)
                {
                    audioSource.clip = jumpSound1;
                }
                else
                {
                    audioSource.clip = jumpSound2;
                }

                audioSource.Play();
            }
            if(Input.GetButtonDown("SpecialAttribute") && this.gameObject.name == "souris_fat")
            {
                audioSource.clip = special;
                audioSource.Play();
            }

            if (Input.GetButtonDown("SpecialAttribute") && this.gameObject.name == "souris_dash")
            {
                audioSource.clip = special;
                audioSource.Play();
            }

        }
    }

    public void CallCatapulte()
    {
        audioSource.clip = special;
        audioSource.Play();
    }
}

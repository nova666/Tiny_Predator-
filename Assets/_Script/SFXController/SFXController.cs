using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    float SFXDuration;

    private void Start()
    {
        if(gameObject.tag == "SoundSFX")
        {
            if (GetComponent<AudioSource>().clip != null)
            {
                SFXDuration = GetComponent<AudioSource>().clip.length;
                StartCoroutine(CheckTimeToDestroy(SFXDuration));
            }
        }
        
        if(GetComponent<ParticleSystem>() != null)
        {     
            SFXDuration = GetComponent<ParticleSystem>().main.duration;
            StartCoroutine(CheckTimeToDestroy(SFXDuration));
        }
    }

    IEnumerator CheckTimeToDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}

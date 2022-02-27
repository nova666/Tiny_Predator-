using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    List<GameObject> ListOfSounds;
    public enum Sound
    {
        BirdJump,
        Score,
        Lose,
        ButtonOver,
        ButtonClick
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        ListOfSounds = new List<GameObject>();
    }

    public void PlaySound(Sound sound)
    {
        GameObject FishJump = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Audio);
        FishJump.gameObject.SetActive(true);
        AudioSource audioSource = FishJump.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        Debug.Log("Sounds Being played " + audioSource.name);
        StartCoroutine(CheckSoundStatus(audioSource));
    }

    IEnumerator CheckSoundStatus(AudioSource SFX)
    {
        while (SFX.GetComponent<AudioSource>().isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        PoolManager.GetPoolManger().CoolObject(SFX.gameObject, PoolObjectType.Audio);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClipArray soundAudioClip in GameAssets.GetInstance().soundAudioClip)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.AudioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;

    }

    #region NOT USED CODE
    //void RemoveSound()
    //{
    //    for (int i = 0; i < ListOfSounds.Count; i++)
    //    {
    //        PoolManager.GetPoolManger().CoolObject(ListOfSounds[i], PoolObjectType.Audio);
    //        ListOfSounds.Remove(ListOfSounds[i]);
    //        i--;
    //    }

    //}
    #endregion
}

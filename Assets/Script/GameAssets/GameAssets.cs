using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    public Sprite pipeHeadSprite;
    public Transform pfPipeHead;
    public Transform pfPipeBody;
    public Transform pfGround;
    public Transform pfCloud_01;
    public Transform pfCloud_02;
    public Transform pfCloud_03;
    public Transform pfFish_01;
    public Transform pfFish_02;

    public SoundAudioClipArray[] soundAudioClip;

    [Serializable]
    public class SoundAudioClipArray
    {
        public SoundManager.Sound sound;
        public AudioClip AudioClip;
    }
}

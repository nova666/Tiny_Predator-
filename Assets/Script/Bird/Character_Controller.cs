using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suriyun;

public class Character_Controller : MonoBehaviour
{
    private const float JUMP_AMOUNT = 50f;
    private Rigidbody _BirdRb;

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    private static Character_Controller instance;
    Animator _Anim;
    public static Character_Controller GetInstance()
    {
        return instance;
    }

    int _FishCount = 0;
    public int FishCount
    {
        get { return _FishCount; }
    }

    private void Awake()
    {
        instance = this;
        _BirdRb = GetComponent<Rigidbody>();
        _Anim = GetComponentInChildren<Animator>();
        _BirdRb.isKinematic = true;
    }
    // Update is called once per frame
    void Update()
    {
        switch (GameManager.GetInstance().PlayerStatus)
        {
            default:
            case GameManager.State.Waiting:
                GameManager.GetInstance().PlayerStatus = GameManager.State.Waiting;
                UIScreenManager.GetInstance().ShowScreen();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    GameManager.GetInstance().PlayerStatus = GameManager.State.Playing;
                    UIScreenManager.GetInstance().ShowScreen();
                    _BirdRb.isKinematic = false;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case GameManager.State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    UIScreenManager.GetInstance().ShowScreen();
                    _Anim.SetInteger("animation", 22);
                    Jump();
                               
                }
                transform.eulerAngles = new Vector3(0, 0, _BirdRb.velocity.y * .5f);
                if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Fly"))
                {
                    _Anim.SetInteger("animation", 1);
                }

                break;
            case GameManager.State.GameOver:
                UIScreenManager.GetInstance().ShowScreen();
                break;
        }

    }

    private void Jump()
    {
        _BirdRb.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.GetInstance().PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Prey")
        {
            _FishCount += 10;
            if (other.GetComponent<Fish>())
            {
                other.GetComponent<Fish>().HasBeenEaten = true;
            }
            VFXManager.SpawningVFX(gameObject.transform);
            SoundManager.GetInstance().PlaySound(SoundManager.Sound.Score);
            Debug.Log(_FishCount);
        }
        else
        {
            //if (OnDied != null) OnDied(this, EventArgs.Empty);
            _BirdRb.isKinematic = true;
            SoundManager.GetInstance().PlaySound(SoundManager.Sound.Lose);
            GameManager.GetInstance().PlayerStatus = GameManager.State.GameOver;
            if (OnDied != null) OnDied(this, EventArgs.Empty);
        } 
    }


    //void RemoveVFX()
    //{
    //    PoolManager.GetPoolManger().CoolObject(vfx, PoolObjectType.Confetti);
    //}

    //vfx = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Confetti);
    //vfx.transform.position = gameObject.transform.position;
    //vfx.gameObject.SetActive(true);


}

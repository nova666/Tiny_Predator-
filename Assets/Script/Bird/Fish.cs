using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	bool _hasBeenEaten = false;
	public bool HasBeenEaten
    {
		get { return _hasBeenEaten; }
		set { _hasBeenEaten = value; }
	}
    private void OnEnable()
    {
        HasBeenEaten = false;
    }
    public void Jump()
    {

    }

    void Update()
	{

	}

    public void Jump(float speed)
    {
       
        SoundManager.GetInstance().PlaySound(SoundManager.Sound.BirdJump);
    }

    public float GetPositionX()
    {
        return gameObject.transform.position.x;
    }


}

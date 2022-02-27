using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallexEffect : MonoBehaviour
{
    // private float length, startpos;

    //public GameObject cam;
    [SerializeField] private RawImage[] _img;
    [SerializeField] private float _x, _y;
    [SerializeField] private float minSpeed = 1;

    void Update()
    {

        if (GameManager.GetInstance().PlayerStatus == GameManager.State.Waiting || GameManager.GetInstance().PlayerStatus == GameManager.State.GameOver)
            return;

        for (int i = 0; i < _img.Length; i++)
        {
            _img[i].uvRect = new Rect(_img[i].uvRect.position + new Vector2((i + minSpeed) *_x, (i + minSpeed) *_y) * Time.deltaTime, _img[i].uvRect.size);
        }

        //float temp = (cam.transform.position.x * (1 - parallexEffect));
        //float dist = (cam.transform.position.x * parallexEffect);
        //transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }
}

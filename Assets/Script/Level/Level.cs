using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE = 50f;
    const float OBJ_MOVE_SPEED = 10f;
    const float OBJ_DESTROY_X_POSITION = -30f;
    const float CLOUD_DESTROY_X_POSITION = -160f;
    const float CLOUD_SPAWN_X_POSITION = +160f;
    const float BIRD_X_POSITION = 0;

    private static Level instance;

    public static Level GetInstance()
    {
        return instance;
    }
    private List<Transform> cloudList;
    private List<Transform> groundList;
    private List<GameObject> _fishList;
    private float cloudSpawnTimer;
    private float fishSpawnTimer = 0;
    private float fishSpawnTimerMax;
    public GameObject _SpawningLocation;

    public void RetryLevel()
    {
        GameManager.GetInstance().Restart();
    }


    private void Awake()
    {
        instance = this;
        Score.Start();
        SpawnInitialGround();
        SpawnInitialCloud();
        _fishList = new List<GameObject>();
        fishSpawnTimerMax = 5f;
    }


    private void Update()
    {
     
        if (GameManager.GetInstance().PlayerStatus == GameManager.State.Playing && 
            GameManager.GetInstance().PlayerStatus != GameManager.State.GameOver)
        {
            HandleFishSpawning();
            HandleFishLifeCycle();
            HandleCloud();
            // Handle Background
        }
        else
        {
            ResetTimer();
        }


    }

    private void ResetTimer()
    {
        fishSpawnTimer = fishSpawnTimerMax;
    }

    private void HandleFishSpawning()
    {
        fishSpawnTimer -= Time.deltaTime;
        GameObject ob = null;
        if (fishSpawnTimer <= 0)
        {
            fishSpawnTimer += fishSpawnTimerMax;
            float probability = UnityEngine.Random.Range(0, 101);

            if (probability > 150)
            {
                ob = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Fish_02);
                #region Old Code
                //GameObject ob = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Fish_02);
                //ob.transform.position = new Vector3(_SpawningLocation.transform.position.x, _SpawningLocation.transform.position.y, 0);
                //ob.gameObject.SetActive(true);
                //ob.gameObject.GetComponent<Fish>().Jump(FISH_JUMPSPEED);
                //ob.gameObject.GetComponent<Fish>().Jump();
                //_fishList.Add(ob);
                #endregion
            }
            else
            {
                ob = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Fish_01);
                #region old Code
                //GameObject ob = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Fish_01);
                //Debug.Log(ob.transform.position);
                //ob.transform.position = new Vector3(_SpawningLocation.transform.position.x, _SpawningLocation.transform.position.y, 0);
                //Debug.Log(ob.transform.position);
                //ob.gameObject.SetActive(true);
                //ob.gameObject.GetComponent<Fish>().Jump();
                //_fishList.Add(ob);
                #endregion;
            }


            ob.transform.position = new Vector3(_SpawningLocation.transform.position.x, _SpawningLocation.transform.position.y, 0);
            ob.gameObject.SetActive(true);
            ob.gameObject.GetComponent<Fish>().Jump();
            _fishList.Add(ob);
            SoundManager.GetInstance().PlaySound(SoundManager.Sound.ButtonClick);
        }
    }

    private void HandleFishLifeCycle()
    {
        if (_fishList.Count <= 0)
            return;

        for (int i = 0; i < _fishList.Count; i++)
        {
            Fish fish = _fishList[i].GetComponent<Fish>();
            bool _HasFishFinishedTrajectory = fish.GetComponent<BezierFollow>()._reachedEnd;
            bool isToTheRightOfTheBird = fish.GetPositionX() > BIRD_X_POSITION;

            if (isToTheRightOfTheBird && fish.GetPositionX() <= BIRD_X_POSITION)
            {
                
            }

            if (fish.GetPositionX() < OBJ_DESTROY_X_POSITION || fish.HasBeenEaten || _HasFishFinishedTrajectory)
            {
                PoolManager.GetPoolManger().CoolObject(_fishList[i], PoolObjectType.Fish_01);
                _fishList.Remove(_fishList[i]);
                i--;
            }
        }
    }

    private Transform GetCloudPrefabTransform()
    {
        switch (UnityEngine.Random.Range(0, 3))
        {
            default:
            case 0: return GameAssets.GetInstance().pfCloud_01;
            case 1: return GameAssets.GetInstance().pfCloud_02;
            case 2: return GameAssets.GetInstance().pfCloud_03;
        }
    }

    private void HandleCloud()
    {
        cloudSpawnTimer -= Time.deltaTime;
        if(cloudSpawnTimer < 0)
        {
            float cloudSpawnTimerMax = 5f;
            cloudSpawnTimer = cloudSpawnTimerMax;
            float cloudY = 30f;
            Transform cloudTransform = Instantiate(GameAssets.GetInstance().pfCloud_01, new Vector3(CLOUD_SPAWN_X_POSITION, cloudY, 0), Quaternion.identity);
            cloudList.Add(cloudTransform);
        }


        for (int i = 0; i < cloudList.Count; i++)
        {
            Transform cloudTransform = cloudList[i];
            cloudList[i].position += new Vector3(-1, 0, 0) * OBJ_MOVE_SPEED * Time.deltaTime * .75f;
            if (cloudList[i].position.x < CLOUD_DESTROY_X_POSITION)
            {
                Destroy(cloudList[i].gameObject);
                cloudList.RemoveAt(i);
                i--;
            }  
        }
       
    }

    void SpawnInitialGround()
    {
        groundList = new List<Transform>();
        Transform groundTransform;
        float groundY = -47.5f;
        float groundWidth = 192f;
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(0, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth * 2f, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
    }

    void SpawnInitialCloud()
    {
        cloudList = new List<Transform>();
        Transform cloudTransform;
        float cloudY = 30f;
        cloudTransform = Instantiate(GetCloudPrefabTransform(), new Vector3(0, cloudY, 0), Quaternion.identity);
        groundList.Add(cloudTransform);
    }

}

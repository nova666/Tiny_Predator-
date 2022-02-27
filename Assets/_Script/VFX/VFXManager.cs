using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour {

    /// <summary>
    /// I am using this class to handle the visual effects in the game
    /// </summary>

    static VFXManager instance;
    public static VFXManager GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }


    static public void SpawningVFX(Transform target)
    {
        if (target == null)
            return;
        GameObject vfx;
        vfx = PoolManager.GetPoolManger().GetPoolObject(PoolObjectType.Confetti);
        vfx.transform.position = target.transform.position;
        vfx.gameObject.SetActive(true);
        instance.StartCoroutine(CheckStatusVFX(vfx));
    }

    static IEnumerator CheckStatusVFX(GameObject vfx)
    {

        while(vfx.GetComponent<ParticleSystem>().isPlaying && vfx != null)
        {
            yield return new WaitForEndOfFrame();
        }
       
        PoolManager.GetPoolManger().CoolObject(vfx, PoolObjectType.Confetti);
    }

}

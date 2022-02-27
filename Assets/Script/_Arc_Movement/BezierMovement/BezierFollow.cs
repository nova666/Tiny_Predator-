using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject[] routes;

    private GameObject[] waypoints;

    private int routeTogo;
    private float tParam;
    private float speedModifier;
    private bool coroutineAllowed;
    private Vector2 _DefaultPosition;

    public bool _reachedEnd { get; private set; }
    public Vector2 _capPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        AssignWaypoints();
        _DefaultPosition = transform.position;
        routeTogo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        _capPosition = _DefaultPosition;
        _reachedEnd = false;
        routeTogo = 0;
        tParam = 0f;
        speedModifier = 1f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeTogo));
        }
    }

    private void AssignWaypoints()
    {
        routes = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        _reachedEnd = false;

        Vector2 p0 = routes[routeNumber].transform.GetChild(0).position;
        Vector2 p1 = routes[routeNumber].transform.GetChild(1).position;
        Vector2 p2 = routes[routeNumber].transform.GetChild(2).position;
        Vector2 p3 = routes[routeNumber].transform.GetChild(3).position;

        while(tParam < 1)
        {
            speedModifier = 1f;
            speedModifier -= (transform.position.y * 0.01f);
            tParam += Time.deltaTime * speedModifier;

            _capPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;


            //transform.rotation = LookAt2D(new Vector3(_capPosition.x, _capPosition.y, 0) - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, LookAt2D(new Vector3(_capPosition.x, _capPosition.y, 0) - transform.position), 0.2f);
            transform.position = _capPosition; 
           // transform.eulerAngles = new Vector3(0, 0, transform.position.y * 1.8f);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeTogo += 1;

        if (routeTogo > routes.Length - 1)
            routeTogo = 0;
        coroutineAllowed = true;
        _reachedEnd = true;
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}

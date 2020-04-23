using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentindex = 0;
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentindex<=waypoints.Length)
        transform.position=Vector3.MoveTowards(transform.position, waypoints[currentindex].position, speed * Time.deltaTime);
        transform.LookAt(waypoints[currentindex]);
        if (Vector3.Distance(transform.position, waypoints[currentindex].position) <= Mathf.Epsilon)
        {
            currentindex++;
            if (currentindex >= waypoints.Length)
                currentindex = 0;
        }
    }
}

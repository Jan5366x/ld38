using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidAI : MonoBehaviour {

    private Transform target; //the enemy's target

    public float speed = 2f;
    private float minDistance = 0.1f;
    private float range;
 

    // Use this for initialization
    void Start()
    {

        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        range = Vector2.Distance(transform.position, target.position);
        Debug.DrawLine(transform.position, target.position);

        if (range > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}

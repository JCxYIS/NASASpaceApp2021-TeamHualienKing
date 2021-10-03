using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollingBG : MonoBehaviour
{    
    public Vector2 endPos;
    public float speed;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, speed);
        if((Vector2)transform.position == endPos)
            transform.position = startPos;
    }
} 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 20;
    public float acceleration = 0.1f;
    Vector2 nextMoveCommand;
    new Rigidbody2D rigidbody2D;

    SpriteRenderer spriteRenderer;

    enum State
    {
        Idle, Moving
    }

    State state = State.Idle;
    Vector2 start, end;
    Vector2 currentVelocity;
    float startTime;
    float distance;
    float velocity;

    /* -------------------------------------------------------------------------- */

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed *= GameManager.Instance.Difficulty.MoveSpeed_Scale;
    }

    void Update()
    {
        // nextMoveCommand = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if(nextMoveCommand.magnitude <= 0)
            IdleState();
        else
            MoveState();
    }
    

    /* -------------------------------------------------------------------------- */

    void IdleState()
    {
        if (nextMoveCommand != Vector2.zero)
        {
            start = transform.position;
            end = start + nextMoveCommand;
            distance = (end - start).magnitude;
            // UpdateAnimator(nextMoveCommand);
            velocity = 0;
            nextMoveCommand = Vector3.zero;
            state = State.Moving;
        }
    }

    void MoveState()
    {
        velocity = Mathf.Clamp01(velocity + Time.deltaTime * acceleration);
        rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, nextMoveCommand * speed, ref currentVelocity, acceleration, speed);            
        // UpdateAnimator(nextMoveCommand); 
        // spriteRenderer.flipX = rigidbody2D.velocity.x >= 0 ? true : false;
    }

    public void SetCommand(Vector2 moveCmd)
    {
        nextMoveCommand = moveCmd;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
        SetCommand(Vector2.zero);
    }
}
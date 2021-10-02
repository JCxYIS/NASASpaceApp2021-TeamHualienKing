using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] List<Rigidbody2D> HappyBalls;

    Player[] players;
    

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        players = GameObject.FindObjectsOfType<Player>();
        
        if(HappyBalls.Count == 0)
        {
            foreach(var p in players)
            {
                p.transform.position = Vector3.zero;
                p.GetComponent<Collider2D>().sharedMaterial = new PhysicsMaterial2D{bounciness = 1, friction = 0};
                p.PlayerController.SetVelocity(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 14.8763f);
            }
        }
        else
        {
            foreach(var ball in HappyBalls)
            {
                ball.GetComponent<Collider2D>().sharedMaterial = new PhysicsMaterial2D{bounciness = 1, friction = 0};
                ball.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 14.8763f;
            }
            foreach(var p in players)
            {
                Destroy(p.gameObject);
            }
        }        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("Landing");
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        if(players != null)
        foreach(var p in players)
        {
            Destroy(p.gameObject);
        }
    }
}
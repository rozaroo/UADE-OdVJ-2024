using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 3;
    private Vector3 velocity;
    Vector3 startPosition;
    GameManager gameManager;
    public static Ball Instance;
    private bool readyToLaunch = true;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        startPosition = new Vector3(FindObjectOfType<Player>().transform.position.x, FindObjectOfType<Player>().transform.position.y + 0.5f, 0);
        ResetBall();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToLaunch)
        {
            LaunchBall();
            readyToLaunch = false;
        }
        if (gameManager.lives > 0) transform.position += velocity * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone")) 
        { 
            gameManager.LoseHealth();
            ResetBall();
            readyToLaunch=true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            velocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }
        else
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }
    }
    public void ResetBall()
    {
        transform.position = new Vector3(FindObjectOfType<Player>().transform.position.x, FindObjectOfType<Player>().transform.position.y + 0.5f, 0);
        velocity = Vector3.zero;
    }
    void LaunchBall()
    {
        velocity = new Vector3(Random.Range(-1, 1f, 1.1f), 1, 0).normalized * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private BoxCollider2D box;
    private float sizeCollider;
    private float obstacleWidth;
    //[SerializeField] private float leftBound = -10f;

    GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();

        if (gameObject.CompareTag("Ground"))
        {
            box = GetComponent<BoxCollider2D>();
            sizeCollider = box.size.x;
        }
        else if (gameObject.CompareTag("Obstacle"))
        {
            obstacleWidth = GameObject.FindGameObjectWithTag("Column").GetComponent<BoxCollider2D>().size.x;
        }
       
    }

    
    void Update()
    {
        //transform.position = new Vector2(transform.position.x * -speed * Time.deltaTime, transform.position.y);

        if(!manager.gameOver)
            transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (gameObject.CompareTag("Ground"))
        {
            if (transform.position.x <= -sizeCollider)
            {
                transform.position = new Vector2(transform.position.x + 2 * sizeCollider, transform.position.y);
            }
        }

        /*else if (gameObject.CompareTag("Obstacle"))
        {
            if (transform.position.x < leftBound)
            {
                Destroy(gameObject);
            }
        }*/

       
    }
}

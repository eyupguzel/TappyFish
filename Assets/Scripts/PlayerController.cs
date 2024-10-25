using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float speed;
    private float angle;
    private readonly float maxAngle = 20;
    private readonly float minAngle = -60;
    private int score = 0;
    private int highScore = 0;

    public Text scoreText;
    public TextMeshProUGUI highScorePanel;
    public TextMeshProUGUI panelScore;

    public Sprite fishDied;
    private SpriteRenderer sp;

    Animator anim;

    GameManager manager;

    public AudioSource swim;
    public AudioSource hit;
    public AudioSource point;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindObjectOfType<GameManager>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        highScore = PlayerPrefs.GetInt("highscore");
        highScorePanel.text = highScore.ToString();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !manager.gameOver)
        {
            swim.Play();

            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.up * speed;
        }

    }

    private void FixedUpdate()
    {
        FishRotate();
    }

    private void FishRotate()
    {
        if (!manager.gameOver)
        {
            if (rb.velocity.y > 0)
            {
                if (angle < maxAngle)
                {
                    angle += 2;
                }
            }
            else if (rb.velocity.y < -1.2f)
            {
                if (angle >= minAngle)
                {
                    angle -= 1;
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
    private void HighScore()
    {
        if (highScore < score)
        {
            highScore = score;
            highScorePanel.text = highScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            score += 1;
            scoreText.text = $"{score}";
            point.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Column"))
        {
            if (!manager.gameOver)
            {
                manager.GameOver();
                GameOver();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        sp.sprite = fishDied;
        anim.enabled = false;
        manager.gameOverPanel.SetActive(true);
        panelScore.text = score.ToString();
        HighScore();
        hit.Play();
    }
}

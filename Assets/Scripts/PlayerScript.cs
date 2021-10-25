using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private int count;
    private int lives;
    private SpriteRenderer mySpriteRenderer;

    public GameObject loseTextObject;
    public GameObject winTextObject;
    public float speed;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI LivesText;
    public AudioSource musicSource;
    public AudioClip winSound;
    Animator anim; 


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;

        count = 0;

        rd2d = GetComponent<Rigidbody2D>();

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        anim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    void SetCountText()
    {
        CountText.text = "Bones: " + count.ToString() + " / 9 ";
        if (count >= 9)
        {
            winTextObject.SetActive(true);
            musicSource.clip = winSound;
            musicSource.Play();
            musicSource.loop = false;
        }
        else if (lives == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
        LivesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);

            mySpriteRenderer.flipX = true;

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);

            mySpriteRenderer.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }



    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count += 1;

            SetCountText();

            Destroy(collision.collider.gameObject);

            if (count == 5)
            {
                transform.position = new Vector3(0.0f, 50.0f, 0.0f);
                lives = 3;
                LivesText.text = "Lives: " + lives.ToString(); 
            }

        }

        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;

            SetCountText();

            Destroy(collision.collider.gameObject);

        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }


    }
}



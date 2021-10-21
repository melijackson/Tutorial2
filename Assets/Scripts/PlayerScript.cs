using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private int count;

    public GameObject winTextObject;
    public float speed;
    public TextMeshProUGUI CountText;



    // Start is called before the first frame update
    void Start()
    {
       count = 0; 
        
       rd2d = GetComponent<Rigidbody2D>(); 
       
       SetCountText();
       winTextObject.SetActive(false);
       

    }

    void SetCountText()
    {
        CountText.text = "Count: " + count.ToString();
        if(count >= 4)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
  
   void Update() 
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
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
            if(collision.collider.tag == "Coin")
            {
                count += 1;

                SetCountText();    

                Destroy(collision.collider.gameObject);
            }
        }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
               rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }
        }

    }

}

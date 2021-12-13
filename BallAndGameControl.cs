using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallAndGameControl : MonoBehaviour
{
    public float speed = 1f;
    Vector3 velocity;

    public Text pongScore;

    public bool right;
    public bool left;
    public bool up;
    public bool down;
    private Rigidbody2D rb2d;

    public int player1Score;
    public int player2Score;

    public GUISkin layout;


    // Start this game and randomly move the ball 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //Invoke("theBall", 2);
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15));
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }





    private void Update()
    {
        UpdateVelocity();
       UpdateScore(); // SCORE PART ONLY
        transform.position += velocity * Time.deltaTime;
    }
     //Change the speed and direction of the ball
    void UpdateVelocity()
    {
        if (right && up) velocity = new Vector3(1f, 1f, 0) * speed;
        else if (right && down) velocity = new Vector3(1f, -1f, 0) * speed;
        else if (left && up) velocity = new Vector3(-1f, 1f, 0) * speed;
        else if (left && down) velocity = new Vector3(-1f, -1f, 0) * speed;
    }

    void UpdateScore() // INCREASE SCORE
    {
        pongScore.text = player1Score.ToString("00") + "                      " + player2Score.ToString("00");
    }


    void OnGUI()
    {
        // THIS CHECKS EVERYTIME SOMETHING HAPPENS IF SOMEONE HAS WON.
        // TRIGGERS THE RESET IF SOMEONE HAS.
        GUI.skin = layout;
       

        // Restart Buitton will reset the game

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 10, 120, 24), "RESTART"))
        {
            player1Score = 0;
            player2Score = 0;
            ResetBall();
        }

        // Check to see if Player One has won
        if (player1Score == 3)
        {
            GUI.Label(new Rect(Screen.width / 2 - 65, 200, 2000, 1000), "PLAYER ONE WINS");
            ResetBall(); //Place the ball in the center

        }
        // Check to see if Player Two has won
        if (player2Score == 3)
        {
            GUI.Label(new Rect(Screen.width / 2 - 65, 200, 2000, 1000), "PLAYER TWO WINS");
            ResetBall(); //Place the ball in the center
        }
    }



    void ResetBall() // Where to put the ball when you reset the game
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // Detects collision in the ball. Tells the game where the ball should move next
        string sideStuff = collision.transform.name;

        if (sideStuff == "topWall" && up) { up = false; down = true; }
        if (sideStuff == "bottomWall" && down) { down = false; up = true; }
        if (sideStuff == "paddleRight" && right) { right = false; left = true; }
        if (sideStuff == "paddleLeft" && left) { left = false; right = true; }

        if (sideStuff == "rightWall" && right)
        {
            right = false;
            left = true;
            player1Score++;
            ResetBall();
        }
        if (sideStuff == "leftWall" && left)
        {
            left = false;
            right = true;
            player2Score++;
            ResetBall();

        }


        //Change velocity of the ball when hittinhg the paddle
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
        }



    }
}


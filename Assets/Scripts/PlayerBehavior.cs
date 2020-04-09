using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Vector3 velocity = new Vector3(1, 0, 0);
    public float Speed = 4.5f;

    private bool grounded = false;
    private bool isJumping = false;
    private float _jumpMagnitude = 6.5f;
    private Rigidbody2D myRB;
    float jumpDelay = 0.02f;
    float jumpTimer;
    Gamemanager gamemanager;
    Animator myAnimator;

    enum PlayerStates
    {
        Running,
        jumping,
        falling,
        die
    }

    PlayerStates myStates;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void FixedUpdate()
    {
        if (jumpTimer > Time.time && grounded )
        {
           
            Jump();
        }
    }


    // Update is called once per frame
    void Update()
    {
        Run();
        Gravity();
        CalculatePlayerInput();
        CalculateAnimations();


        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            jumpTimer = Time.time + jumpDelay;
           
        }
        
        if(this.transform.position.y <= -10f)
        {
            KillPlayer();
        }
        
       // Debug.Log(myRB.gravityScale);
    }

    private void Run()
    {
        this.transform.Translate(velocity * Speed * Time.deltaTime);
    }

    private void Jump()
    {
        
            myRB.AddForce(Vector2.up * _jumpMagnitude, ForceMode2D.Impulse);

            jumpTimer = 0;
            isJumping = true;
           
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            grounded = true;
            isJumping = false;
        }
    }

    private void Gravity()
    {
        if (isJumping)
        {
            myRB.gravityScale += 2 * Time.deltaTime;
        }
        else if (!isJumping)
        {
            myRB.gravityScale = 1;
        }

    }


    private void CalculatePlayerInput()
    {
        if (isJumping)
        {
            myStates = PlayerStates.jumping;
        }
       else if (grounded)
        {
            myStates = PlayerStates.Running;
        }

    }

    private void CalculateAnimations()
    {
        switch (myStates)
        {
            case PlayerStates.jumping:
                myAnimator.SetBool("isJumpingUp", true);
                StartCoroutine(fallingDown());
                break;
            case PlayerStates.Running:
                myAnimator.SetBool("isJumpingUp", false);
                myAnimator.SetBool("isFalling", false);
              
                break;
                
        }
    }

    IEnumerator fallingDown()
    {
        yield return new WaitForSeconds(0.2f);
        myAnimator.SetBool("isFalling", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            KillPlayer();
        }

        if (collision.transform.tag == "SilverCoin")
        {
            gamemanager.playerScore += 10;
        }

        if (collision.transform.tag == "BlueGem")
        {
            gamemanager.playerScore += 20;
        }


    }

    private void KillPlayer()
    {
        gamemanager.playerDead = true;
        gamemanager.playerLives -= 1;
        Destroy(this.gameObject);
    }

    

}

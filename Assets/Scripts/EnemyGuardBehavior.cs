using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardBehavior : MonoBehaviour
{
    enum GuardEnemyState
    {
        Idle,
        Walk, 
        Die
    }

    Animator myAnimator;

    Vector3 velocity;
    float walkingSpeed = 3.5f;
    int direction = 1;
    GuardEnemyState myStates;

    bool walkRestarted = false;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(direction, 0, 0);
        myAnimator = GetComponent<Animator>();
        myStates = GuardEnemyState.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(direction, 0, 0);
        CalculateEnemyAnimations();
        Walk();
    }

    private void CalculateEnemyAnimations()
    {
        switch (myStates)
        {
            case GuardEnemyState.Idle:
                myAnimator.SetBool("isIdle", true);
                if (!walkRestarted)
                {
                    walkRestarted = true;
                    StartCoroutine(restartWalkingSpeed());
                }
                
                break;
            case GuardEnemyState.Walk:
                myAnimator.SetBool("isIdle", false);
                walkingSpeed = 3.5f;
                break;
        }
    }

    private void CalculateEnemyActions()
    {

    }

    private void Walk()
    {
        this.transform.Translate(velocity * walkingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "LeftWall")
        {
            direction = 1;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            myStates = GuardEnemyState.Idle;
        }
        else if(collision.transform.tag == "RightWall")
        {
            direction = -1;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            myStates = GuardEnemyState.Idle;
        }
    }


    IEnumerator restartWalkingSpeed()
    {
        
        walkingSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        myStates = GuardEnemyState.Walk;
        walkingSpeed = 3.5f;
        walkRestarted = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    Vector3 bossVelocity;
    float bossSpeed;

    PlayerBehavior player;
    Gamemanager gamemanager;
    bool bossinPosition = false, canFire = true;

    public GameObject bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bossVelocity = new Vector3(1, 0, 0);
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!bossinPosition)
        {
            bossSpeed = gamemanager.bossSpeedController;
        }
        else
        {
            bossSpeed = player.Speed;

            if (canFire)
            {
                ShootBombs();
                StartCoroutine(bombFirerate());
            }

        }
       
        transform.Translate(bossVelocity * bossSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "BossLimit")
        {
            bossinPosition = true;
        }
    }

    private void ShootBombs()
    {
        canFire = false;
        Instantiate(bombPrefab, transform.position, transform.rotation);

    }

    IEnumerator bombFirerate()
    {
        yield return new WaitForSeconds(1f);
        canFire = true;
    }
}

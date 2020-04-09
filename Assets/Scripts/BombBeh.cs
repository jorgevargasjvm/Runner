using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBeh : MonoBehaviour
{

    Vector3 bombVelocity;
    float bombSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        bombVelocity = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bombVelocity * bombSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}

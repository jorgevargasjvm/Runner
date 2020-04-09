using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //Pickupsound
            //PickupEffect
            //ScoreUP
            Destroy(this.gameObject);
        }
    }


}

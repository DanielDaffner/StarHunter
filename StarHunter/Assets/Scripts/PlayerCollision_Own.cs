using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision_Own : MonoBehaviour
{

    public GameObject player;
 

    private void OnTriggerEnter(Collider other)
    {

     
            print("Collision");
        if (other.gameObject.tag == "Player" && other.gameObject !=transform.gameObject)
        {
            bool you =  other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;
          
            bool me = transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;

            if (you)
            {
                print("GIB KROOONE!!");
               // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = true;
                
            }

            else if (me && !you)
            {
                print("Krone Weg!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
            }

        }


    
    }
}



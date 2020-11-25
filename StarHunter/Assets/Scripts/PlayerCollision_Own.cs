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
           
            print("Du Krone?");
            bool tmp =  other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;
            print(tmp);

            if (tmp)
            {
                print("GIB KROOONE!!");
               // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = true;
                


            }

            print("Ich Krone?");
            tmp = transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;

            if (tmp)
            {
                print("Krone Weg!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;



            }


        }


    
    }
}



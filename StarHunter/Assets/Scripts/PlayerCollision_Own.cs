using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision_Own : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {

        print("Collision");
        if(col.gameObject.tag == "Player")
        {
            print("KROOONE!!");
        }

        if (col.gameObject.tag == "Cube")
        {
            print("Cuube!!");
        }
        
    }
}

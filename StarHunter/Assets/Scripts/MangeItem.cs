using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangeItem : MonoBehaviour
{

    float respawnTimer = 2f;
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.up,1*Time.deltaTime);
        if( !child.activeInHierarchy) {
            if (respawnTimer <= 0) {
                respawnTimer = 5f;
                child.SetActive(true);
                child.GetComponent<ItemScript>().refresh();
            } else {
                respawnTimer-= Time.deltaTime;
            }
        }
    }
}

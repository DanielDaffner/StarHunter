using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private const int itemTypeCount = 2;
    int itemType = 0;
    bool isTaken = false;
    bool spawning = false;
    int respawnTimer = 0;

    void Start()
    {
        itemType = Random.Range(0, itemTypeCount);
        print(itemType);
        print("asdfasdfasdf");
    }

    private void OnTriggerEnter(Collider other) {
        CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
        if (!isTaken && !player.hasItem) {
            isTaken = true;
            player.hasItem = true;
            player.itemType = itemType;
            //transform.gameObject.SetActive(false);
            spawning = true;
        }
    }

    private void refresh() {
        itemType = Random.Range(0, itemTypeCount - 1);
        transform.gameObject.SetActive(true);
    }
        // Update is called once per frame
    void Update()
    {
        if(spawning) {
            print("respwan");
            //refresh();
        }
    }
}

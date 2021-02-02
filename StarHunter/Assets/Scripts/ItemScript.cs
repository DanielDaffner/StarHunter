using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private const int itemTypeCount = 2;
    int itemType;
    bool isTaken;


    void Start()
    {
        refresh();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "OwnPlayer") return;
        CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
        if (!isTaken && !player.hasItem) {
            isTaken = true;
            player.hasItem = true;
            player.itemType = itemType;
            transform.gameObject.SetActive(false);
        }
    }

    public void refresh() {
        isTaken = false;
        itemType = Random.Range(0, itemTypeCount);
    }
        // Update is called once per frame
    void Update()
    {

    }
}

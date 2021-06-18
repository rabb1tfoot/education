using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public int itemID;
    public int count;
    public string pickUpSound;
    private bool OnTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTrigger = false;
    }

    private void Update()
    {
        if (OnTrigger && Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetItem(itemID, count);
            Destroy(this.gameObject);
        }
    }
}

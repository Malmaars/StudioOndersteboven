using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    bool inHand;
    private void OnMouseDown()
    {
        //add ticket to hand
        //if hand is full, don't pick up ticket

        if (!inHand)
        {
            Player player = FindObjectOfType<Player>();
            if (player.inventory.Count < 3)
            {
                player.inventory.Add(this.gameObject);
                player.UpdateInventory();
                inHand = true;
            }
        }

    }
}

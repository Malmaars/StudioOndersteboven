using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketDispenser : MonoBehaviour
{
    //I can cycle through available ticket with an array
    public GameObject ticket;
    public Transform ticketLocation;
    private void OnMouseDown()
    {
        Debug.Log("Clicky");
        //dispense ticket
        Instantiate(ticket, ticketLocation.position, ticketLocation.rotation);
    }
}

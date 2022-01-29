using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool horizontalInputCheck, verticalInputCheck, camLock;

    Vector3 newPos, oldPos;
    Quaternion newRot, oldRot;

    GameObject[] walkableTiles;

    public List<GameObject> inventory;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        //I can set this to any point to start the player wherever I want
        newPos = transform.position;
        newRot = transform.rotation;

        walkableTiles = GameObject.FindGameObjectsWithTag("Walkable");
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (camLock)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                //go out of cam lock
                newPos = oldPos;
                newRot = oldRot;
            }

            if(transform.position == oldPos && transform.rotation == oldRot && Input.GetAxisRaw("Vertical") == 0)
            {
                camLock = false;
            }
        }

        LerpToPosition();
        RotateToPosition();


    }

    void RotateToPosition()
    {
        //I check if left or right is pressed
        if (Input.GetAxisRaw("Horizontal") != 0 && !horizontalInputCheck && !verticalInputCheck && !camLock)
        {
            //Debug.Log("Q: VerticalInput: " + Input.GetAxisRaw("Vertical") + ", Horizontal Input: " + Input.GetAxisRaw("Horizontal") + ", VerticalInputCheck: " + verticalInputCheck + ", HorizontalInpuCheck: " + horizontalInputCheck);
            horizontalInputCheck = true;
            //if it is, I set a destination target to rotate to
            newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90 * Input.GetAxisRaw("Horizontal"), transform.rotation.eulerAngles.z);
        }

        if(newRot != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 40f);

            if(Vector3.Distance(transform.rotation.eulerAngles, newRot.eulerAngles) < 1f)
            {
                //Debug.Log(newRot.eulerAngles);
                transform.rotation = newRot;
            }
        }

        if (transform.rotation == newRot && Input.GetAxisRaw("Horizontal") == 0)
        {
            transform.rotation = Quaternion.Euler(newRot.eulerAngles.x, Mathf.Round(transform.rotation.eulerAngles.y), newRot.eulerAngles.z);
            horizontalInputCheck = false;
        }
    }

    void LerpToPosition()
    {
        //I check if up or down is pressed
        if (Input.GetAxisRaw("Vertical") != 0 && !verticalInputCheck && !horizontalInputCheck && !camLock)
        {
            if (!camLock)
            {
                Transform specialCamCheck = CheckSpecialCamPosition();
                if (specialCamCheck != null)
                {
                    camLock = true;
                    oldPos = transform.position;
                    oldRot = transform.rotation;
                    newPos = specialCamCheck.position;
                    newRot = specialCamCheck.rotation;
                    return;
                }
            }

            for (int i = 0; i < walkableTiles.Length; i++)
            {
                //Debug.Log(transform.position + transform.forward * Input.GetAxisRaw("Vertical"));
                if (transform.position + transform.forward * Input.GetAxisRaw("Vertical") == walkableTiles[i].transform.position)
                {
                    break;
                }

                if (i == walkableTiles.Length - 1)
                {
                    return;
                }
            }

            //Debug.Log("V: VerticalInput: " + Input.GetAxisRaw("Vertical") + ", Horizontal Input: " + Input.GetAxisRaw("Horizontal") + ", VerticalInputCheck: " + verticalInputCheck + ", HorizontalInpuCheck: " + horizontalInputCheck);
            verticalInputCheck = true;
            //if it is, I set a destination target forward or backward from where the player is facing
            newPos = transform.position + transform.forward * Input.GetAxisRaw("Vertical");
        }

        if (newPos != transform.position)
        {
            //we lerp to the destination
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 40f);

            //If we arrive where we want to go, we can set a new destination again (reset bool)
            if (Vector3.Distance(transform.position, newPos) < 0.01f)
            {
                transform.position = newPos;
            }
        }

        if (newPos == transform.position && Input.GetAxisRaw("Vertical") == 0)
        {
            verticalInputCheck = false;
        }
    }

    Transform CheckSpecialCamPosition()
    {

        Vector3 CheckPos = transform.position + transform.forward * Input.GetAxisRaw("Vertical");
        //if a special view is in that position, switch to the special view.
        GameObject[] specialviews = GameObject.FindGameObjectsWithTag("SpecialView");

        foreach(GameObject view in specialviews)
        {
            if(view.transform.position.x < CheckPos.x + 0.5f && view.transform.position.x > CheckPos.x - 0.5f &&
               view.transform.position.y < CheckPos.y + 0.5f && view.transform.position.y > CheckPos.y- 0.5f &&
               view.transform.position.z < CheckPos.z + 0.5f && view.transform.position.z > CheckPos.z - 0.5f)
            {
                //there is a special cam there. go to the special cam.
                return view.transform;
            }
        }

        return null;
    }

    public void UpdateInventory()
    {
        if(inventory.Count > 0)
        {
            //show them
            hand.transform.localPosition = new Vector3(0.35f, -0.2f, 0.5f);
            for(int i = 0; i < inventory.Count; i++)
            {
                //maak een waaier van tickets?

                //change this (hand.transform) to another transform later
                inventory[i].transform.position = hand.transform.GetChild(0).transform.position;
                inventory[i].transform.rotation = hand.transform.GetChild(0).transform.rotation;
            }
            //max 3 tickets
        }

        else
        {
            //put away the hand
            hand.transform.localPosition = new Vector3(0.35f, -0.5f, 0.5f);
        }
    }
}

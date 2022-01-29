using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    int lineNumber;
    public string[] myLine;
    public Vector3 simplifiedPosition;
    bool talking;

    public Ticket myTicket;
    // Start is called before the first frame update
    void Start()
    {
        //simplify my position to my grid position
        simplifiedPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        //simplify camera position to its grid position
        Vector3 simplifiedPlayer = new Vector3(Mathf.Round(Camera.main.transform.position.x), Mathf.Round(Camera.main.transform.position.y), Mathf.Round(Camera.main.transform.position.z));

        Debug.Log(simplifiedPosition);
        Debug.Log(simplifiedPlayer);

        if(simplifiedPlayer == simplifiedPosition)
        {
            if (talking == false)
            {
                lineNumber = 0;
                talking = true;
            }
            Talk();

            if (Input.GetMouseButtonDown(0))
            {
                lineNumber += 1;
            }
        }

        if(simplifiedPlayer != simplifiedPosition && talking)
        {
            talking = false;
            GameObject.Find("NPCDialogue").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    void Talk()
    {
        //Make a new textmeshcomponent for each character and move them independently 

        if (lineNumber >= myLine.Length)
        {
            GameObject.Find("NPCDialogue").GetComponent<TextMeshProUGUI>().text = "";
        }

        else
        {
            GameObject.Find("NPCDialogue").GetComponent<TextMeshProUGUI>().text = myLine[lineNumber];
        }
    }
}

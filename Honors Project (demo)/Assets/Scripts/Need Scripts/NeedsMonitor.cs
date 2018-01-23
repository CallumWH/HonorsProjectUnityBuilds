using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsMonitor : MonoBehaviour {

    //PUBLICS
    public List<NeedsBase> currentNeeds;

    //PRIVATES
    private int focusNeed;
    private int listSize;
    private bool fillingNeed;
    private bool fillNeedStateInit;
    private Vector3 oldPosition;
    private enum currentState { none, fillNeed };
    private string needTag;
    GameObject needNodeObject;

    currentState state;
    

	// Use this for initialization
	void Start () {
        listSize = currentNeeds.Count;
        this.GetComponentInChildren<NeedsUIScript>().addToList();
        oldPosition = gameObject.transform.position;

        //default state
        state = currentState.none;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case currentState.none:

                for (int i = 0; i < listSize; i++)
                {
                    //Poll the needs and check their states
                    if (currentNeeds[i].GetValue() < currentNeeds[i].GetThreshold())
                    {
                        state = currentState.fillNeed;
                        focusNeed = i;
                    }

                }

                gameObject.GetComponent<PlayerMovement>().MoveTo(oldPosition);
                break;

                
            case currentState.fillNeed:

                //do init
                if(!fillNeedStateInit)
                {
                    //get the tag for what fills the triggered need
                    needTag = currentNeeds[focusNeed].GetFulfillmentTag();

                    //find a node for the triggered need
                    needNodeObject = GameObject.FindGameObjectWithTag(needTag);

                    //INITIALIZE COMPLETE
                    fillNeedStateInit = true;
                }    

                //move to that node
                gameObject.GetComponent<PlayerMovement>().MoveTo(needNodeObject.transform.position);

                //if we are inside the node, restore hunger
                if(fillingNeed)
                {
                      
                    //start restoring the hunger
                    currentNeeds[focusNeed].drainRate = -10.0f;
                            
                    //once full, switch state back to normal
                    if (currentNeeds[focusNeed].GetValue() > 100.0f)
                    {
                        state = currentState.none;
                        currentNeeds[focusNeed].drainRate = 2.0f;
                        Debug.Log(needTag + "Fulfilled");

                        //CLEANUP

                        //Clear the tag of the need
                        needTag = string.Empty;

                        //Clear the target node that was used
                        needNodeObject = null;

                        //reset the init
                        fillNeedStateInit = false;
                    }
                }

                break;
        }

        //State independant code
        //output debug value
        /*
        for (int i = 0; i < listSize; i++)
        {
            Debug.Log(currentNeeds[i].GetFulfillmentTag() + currentNeeds[i].GetValue());
        }
        */
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == needTag)
        {
            fillingNeed = true;
        }
        else
        {
            fillingNeed = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == needTag)
        {
            fillingNeed = false;
        }
    }

    public int GetListSize() { return listSize; }
}

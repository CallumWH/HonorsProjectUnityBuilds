using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsBase : MonoBehaviour {

    //Define drain rates for the needs
    public string needName;
    public float drainRate; //This is Per Second
    public float threshhold; //Threshhold this value will trigger it's action.
    public string fulfillmentTag; //What fufils this need?

    //set initial values for the needs (Weights will go in here eventually)
    public int intWeight;

    private float currentValue; //Value for the need, this needs a better variable name
    private int currentWeight;
    private float deltaTime;

	// Use this for initialization
	void Start ()
    {
        //Mosty error catching
        //check if a name was inputed
        if(needName.Length.Equals(0))
        {
            Debug.LogError("name not defined for need");
            Debug.Break();
        }

        //Set the initial weighting value, also catches if it wasn't initialized
        if (intWeight != 0)
        {
            currentWeight = intWeight;
        }
        else
        {
            Debug.LogError("forgot to initialize weighting for " + needName);
            Debug.Break();
        }

        //Check if a node tag is defined
        if(fulfillmentTag.Length == 0)
        {
            Debug.LogError("No tag defined for need fufulment");
        }

        //Checks if the drain rate has a value that makes sense
        if(drainRate <= 0)
        {
            Debug.LogError("Non-sensicle value or uninitilized drain rate for " + needName);
            Debug.Break();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //get the delta time
        deltaTime = Time.deltaTime;
        currentValue -= (drainRate * deltaTime);
	}

    public float GetValue() { return currentValue; }

    public float GetThreshold() { return threshhold; }

    public string GetFulfillmentTag() { return fulfillmentTag; }

}

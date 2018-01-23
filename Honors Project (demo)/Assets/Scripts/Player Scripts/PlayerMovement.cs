using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo(Vector3 targetPosition)
    {
        //calculate the relative position of the players target local to the player
        Vector3 relativePosition = targetPosition - gameObject.transform.position;

        //normalise it
        relativePosition = relativePosition.normalized;

        //get rid of Y value
        relativePosition.y = 0.0f;

        //Move to the poition
        gameObject.transform.Translate((relativePosition * speed) * Time.deltaTime);
    }
}

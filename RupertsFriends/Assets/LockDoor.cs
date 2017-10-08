using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Start Door Event after some time

        // Sound from outside ( Steps and someone talking weirdly)
        // Shadow?

       // Teddy wants player to got to the door and lock it (two times)


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "teddy")
        {
            //Start the teddy lock animation and play sound
            // Play Door sound 
        }

    }
}

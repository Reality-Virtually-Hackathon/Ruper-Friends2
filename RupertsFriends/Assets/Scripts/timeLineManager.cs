using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLineManager : MonoBehaviour {

	public GameObject teddy;
	public Camera head;

	public bool hasHugged = false;

	// Use this for initialization
	void Start () {
	
		Invoke ("TriggerLightening",3);

	}

	void TriggerLightening(){
		//Trigger lightening and sound

		Invoke ("cry", 0.1f);

		if (!hasHugged) {
			StartCoroutine ("askForHugs");
		}

		Invoke ("TriggerLightening", Random.Range (4, 8));
	}

	IEnumerator askforHugs(){

		if ((teddy.transform.position - head.transform.position).magnitude < 0.2f) {
			//trigger hugs

			// trigger pickUp Event
		} else {
			yield return new WaitForSeconds (0.1f);
		}

		yield return false;

	}

	void goToDoor(){
		//wait for trigger collision
	}



	void cry(){

		//trigger crying events

	}


}

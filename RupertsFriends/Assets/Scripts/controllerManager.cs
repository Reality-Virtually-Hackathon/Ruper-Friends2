using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerManager : MonoBehaviour {

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	public GameObject teddyController, playerController, headset;
	public float safeDistance;
	public bool gripButtonUp = false;
	public bool gripButtonDown = false;
	public bool gripButtonPressed = false; 

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	public bool triggerButtonUp = false;
	public bool triggerButtonDown = false;
	public bool triggerButtonPressed = false; 


	public float threshVelocity;
	public Animator teddyAnim;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } } 

	void Start(){

		trackedObj = GetComponent<SteamVR_TrackedObject> ();

	}


	void Update(){
		if (controller == null) {
			Debug.Log ("Controller not initialised");
			return;
		}

		if (controller.velocity.magnitude > threshVelocity) {		//isAngry
			teddyAnim.SetBool ("isAngry", true);
		} else {
			teddyAnim.SetBool ("isAngry", false);
		}
			
		gripButtonDown = controller.GetPressDown (gripButton);
		gripButtonUp = controller.GetPressUp (gripButton);
		gripButtonPressed = controller.GetPressDown (gripButton);


		triggerButtonDown = controller.GetPressDown (triggerButton);
		triggerButtonUp = controller.GetPressUp (triggerButton);
		triggerButtonPressed = controller.GetPressDown (triggerButton);

		if (gripButtonDown) {
			Debug.Log ("Grip Button was just pressed");
		}
		if (gripButtonUp) {
			Debug.Log ("Grip Button was just released");
		}
		if (triggerButtonDown) {		// interact

			//teddyAnim.SetTrigger ("interact");
			Debug.Log ("Grip Button was just pressed");
		}
		if (triggerButtonUp) {
			Debug.Log ("Grip Button was just released");
		}

		teddyAnim.SetFloat ("distance", (teddyController.transform.position - headset.transform.position).magnitude);


	}


	void OnTriggerEnter(Collider obj){

		if (triggerButtonDown) {			// interact with the object only if trigger is held down and the teddy is in it's proximity
			teddyAnim.SetTrigger ("interact");
		}
	}

	void OnTriggerExit(){

		teddyAnim.settriger

	}
}


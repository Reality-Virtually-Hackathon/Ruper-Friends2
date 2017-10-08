using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controllerManager : MonoBehaviour {

	public Text debugger;

	public Rigidbody[] rbs;
	public GameObject teddy;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	public GameObject teddyController,  headset;

    public GameObject CameraPos;
    public GameObject ControlPos;
    public GameObject Spot;

    public float safeDistance;
	public bool gripButtonUp = false;
	public bool gripButtonDown = false;
	public bool gripButtonPressed = false; 

	private bool isRagdolled = false;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	public bool triggerButtonUp = false;
	public bool triggerButtonDown = false;
	public bool triggerButtonPressed = false;


    public bool activatedMonster = false;

	public float threshVelocity;
	public Animator teddyAnim;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } } 

	float AngerMeter = 0;


    public GameObject monster;
	void Start(){

		rbs = teddy.GetComponentsInChildren<Rigidbody> ();
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		debugger.text = "Initialized...";
	}


	void Update(){

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Idle_held")) {
			debugger.text = "Picked Up";
            activatedMonster = true;
           // AkSoundEngine.PostEvent("Missed", gameObject);
        }

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Hug")) {
			debugger.text = "Hugging";
      //      
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("RaisedHigh")) {
			debugger.text = "Raised High";
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Angry")) {
			debugger.text = "Angry";
		}


		if (controller == null) {
			Debug.Log ("Controller not initialised");
			return;
		}

		if (AngerMeter > 10) {
			debugger.text =  ("Teddy Turned on you");
			teddyAnim.SetTrigger ("reallyAngry");
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).tagHash.Equals ("ragdoll") && false) {		// inactive rn

			if (isRagdolled) {
				return;
			}
			teddyAnim.SetTrigger ("ragdoll");
			teddyAnim.enabled = false;

			debugger.text = "Ragdoll initiated";

			foreach (Rigidbody rb in rbs) {
				rb.isKinematic = false;
			}
			isRagdolled = true;
		}

		if (controller.velocity.magnitude > threshVelocity) {		//isAngry
			teddyAnim.SetBool ("isAngry", true);
			//AngerMeter += Time.deltaTime;
			debugger.text = "Angry state";
		} else {
			AngerMeter = 0;
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
			Debug.Log("Turn on falshLights");
            Spot.SetActive(true);
            teddyAnim.SetTrigger("interact");
		}
		if (triggerButtonUp) {
            Spot.SetActive(false);
            Debug.Log("Turn off falshLights");
			Debug.Log ("Grip Button was just released");
		}

		if (controller.transform.pos.y >= 0.2f) {
			teddyAnim.SetBool("isHeld",true);
            Debug.Log("idle" + controller.transform.pos.y);
		}

		teddyAnim.SetFloat ("distance", (CameraPos.transform.position - ControlPos.transform.position).sqrMagnitude);

		teddyAnim.SetFloat ("height", (ControlPos.transform.position.y));

	}



	void OnTriggerEnter(Collider obj){

		if (obj.gameObject.tag.Equals("doorKnob")) {			// interact with the object only if trigger is held down and the teddy is in it's proximity
			teddyAnim.SetTrigger ("interact");
			debugger.text = "interacting";
        //    AkSoundEngine.PostEvent("Close_Door", gameObject);

			//TODO: trigger excited animation/sounds

			//turn on monster's eyes

			//teddy asks to hold him tight, 
		}


		if (triggerButtonDown && obj.gameObject.tag.Equals ("monster")) {
            monster.SetActive(false);
		}

	}

}


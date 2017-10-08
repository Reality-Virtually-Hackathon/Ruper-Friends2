using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controllerManager : MonoBehaviour {

	public Text debugger;

	public Rigidbody[] rbs;
	public GameObject teddy;
    public GameObject Spot;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	public GameObject teddyController,  headset,otherController;

    public GameObject CameraPos;
    public GameObject ControllerPos;
	public float safeDistance;
	public bool gripButtonUp = false;
	public bool gripButtonDown = false;
	public bool gripButtonPressed = false; 

	private bool isRagdolled = false;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	public bool triggerButtonUp = false;
	public bool triggerButtonDown = false;
	public bool triggerButtonPressed = false; 


	public float threshVelocity;
	public Animator teddyAnim;

	private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device controller; // { get { return SteamVR_Controller.Input ((int)trackedObj.index); } } 

	float AngerMeter = 0;

	void Start(){

		rbs = teddy.GetComponentsInChildren<Rigidbody> ();
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		debugger.text = "Initialized...";
	}


	void Update(){


        controller = SteamVR_Controller.Input((int)trackedObj.index);

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Idle_held")) {
			debugger.text = "Picked Up";
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Hug")) {
			debugger.text = "Hugging";
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
			AngerMeter += Time.deltaTime;
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
		if (triggerButtonDown) {        // interact

            //teddyAnim.SetTrigger ("interact");
            Spot.SetActive(true);
			Debug.Log ("Grip Button was just pressed");
		}
		if (triggerButtonUp) {

            Spot.SetActive(false);
            Debug.Log ("Grip Button was just released");
		}

		teddyAnim.SetFloat ("distance", (CameraPos.transform.position - ControllerPos.transform.position).sqrMagnitude);

		teddyAnim.SetFloat ("height", (teddyController.transform.position.y));

        Debug.Log(("distance"+ (teddyController.transform.position - headset.transform.position).magnitude)); 


        if(controller.velocity.magnitude != 0)
        {

            teddyAnim.SetBool("isHeld",true);

        }
	}


    void OnTriggerEnter(Collider obj)
    {

        if (triggerButtonDown && obj.gameObject.tag.Equals("doorKnob"))
        {           // interact with the object only if trigger is held down and the teddy is in it's proximity
            teddyAnim.SetTrigger("interact");
            debugger.text = "interacting";

            //TODO: trigger excited animation/sounds

            //turn on monster's eyes

            //teddy asks to hold him tight, 
        }


        if (triggerButtonDown && obj.gameObject.tag.Equals("monster"))
        {
            //destroy monster
        }

    }
}


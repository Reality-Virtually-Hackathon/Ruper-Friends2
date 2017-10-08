using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationManager : MonoBehaviour {

	public GameObject Teddy;
	public Animator teddyAnim;
	public Animation tedAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Hug")) {

			tedAnim.Play ("hug");

 		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("RaisedHigh")) {
		
			tedAnim.Play ("wave");
			//TODO play sound
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Sad")) {

			tedAnim.Play ("sad");		
		
		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Idle_sitting")) {

			tedAnim.Play ("sittingIdle");
          //  AkSoundEngine.PostEvent("Cry_Scared", gameObject);

		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Idle_held")) {

			tedAnim.Play ("holdingIdle");		

		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Angry")) {

			tedAnim.Play ("angry");		

		}

		if (teddyAnim.GetCurrentAnimatorStateInfo (0).IsName ("Interact")) {

			tedAnim.Play ("interact");
         //   AkSoundEngine.

		}

		}
}


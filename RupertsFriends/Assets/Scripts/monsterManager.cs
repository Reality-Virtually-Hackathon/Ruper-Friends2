using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour {

    public controllerManager cm;
    public GameObject monster;
	// Use this for initialization
	void Start () {
        monster.SetActive(false);
        Invoke("activate", 10);
    }

    public void activate()
    {

        monster.SetActive(true);
        AkSoundEngine.PostEvent("Second_Creature", gameObject);

    }
	
    // Update is called once per frame
	void Update () {
       
	}
}

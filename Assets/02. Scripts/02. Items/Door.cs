using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    Animator doorAnim;
    void Start()
    {
        doorAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        //Door Control
        if (other.CompareTag("Player")) { 
            doorAnim.SetBool("character_nearby", true);
            //Debug.Log("On Trigger ENter");
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            doorAnim.SetBool("character_nearby", false);
           //Debug.Log("On Trigger Exit");
        }
    }
}

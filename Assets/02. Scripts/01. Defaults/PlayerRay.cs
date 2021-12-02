using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRay : MonoBehaviour { 
    RaycastHit hit;
    //Ray ray;

    [SerializeField]
    private const float MAX_DISTANCE = 15f;
    [SerializeField]
    public IRaycastable raycastObj { get; private set; }
    [SerializeField]
    public GameObject pickUpObj { get; private set; }
    public Transform pickupLocation { get; private set; }

    public static bool isObjectPickUp { get; private set; }

    private Vector3 pickupObjScale;
    // Start is called before the first frame update
    void Start(){
        //pickupLocation = this.transform.parent.GetChild(1);
        //pickupLocation = this.gameObject.transform;
        pickupLocation = new GameObject().transform;
        pickupLocation.SetParent(this.gameObject.transform.parent);
    }
    void OnRayChanged() {
        try { 
            if(hit.transform.gameObject.GetComponent<IRaycastable>() != raycastObj) {
                if (raycastObj != null) {
                    raycastObj.OnRayOut();
                }
                raycastObj = hit.transform.GetComponent<IRaycastable>();
                raycastObj.OnRayHit();
                Debug.Log("[Player Ray]: Ray Hits an Object: " + raycastObj.getGameObject().name);
            }
        }
        catch(NullReferenceException nullex) {
            if(raycastObj!= null) {
                raycastObj.OnRayOut();
                Debug.Log(nullex.Message);
            }
        }
    }

    // Update is called once per frame
    void Update(){
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAX_DISTANCE)) {

            if (hit.transform.gameObject.GetComponent<IRaycastable>() != null) {
                Debug.DrawRay(transform.position, transform.forward * MAX_DISTANCE, Color.red);
                OnRayChanged();
            } else {
                Debug.DrawRay(transform.position, transform.forward * MAX_DISTANCE, Color.blue);
                OnRayChanged();
            }

        } else {
            Debug.DrawRay(transform.position, transform.forward * MAX_DISTANCE, Color.black);
            OnRayChanged();
        }
        if (raycastObj != null)
            raycastObj.OnRayIng();

        if(raycastObj!= null && Input.GetButtonDown("Handle") && pickUpObj == null) {
            PickUpObj(raycastObj.getGameObject());
        } else if(pickUpObj != null && Input.GetButtonDown("Handle")) {
            PickDown();
        }else if(pickUpObj != null&& Input.GetButtonDown("Fire1")) {
            ShotObj();
        }
    }
    public void PickUpObj(GameObject gameobject) {
        Debug.Log("In Pickup");
        pickUpObj = gameobject;
        pickupObjScale = pickUpObj.transform.localScale;
        pickUpObj.transform.SetParent(pickupLocation.transform);
        pickUpObj.transform.position = (this.transform.parent.forward * 1.5f) + this.transform.parent.position;
        pickUpObj.transform.rotation = Quaternion.Euler(Vector3.zero);
        pickUpObj.transform.LookAt(this.transform.forward * 10f);
        pickUpObj.transform.rotation = Quaternion.Euler(Vector3.zero);
        pickUpObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pickUpObj.GetComponent<Rigidbody>().useGravity = false;
        pickUpObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
        isObjectPickUp = true;
    }
    public void PickDown() {
        isObjectPickUp = false;
        //pickUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickUpObj.GetComponent<Rigidbody>().useGravity = true;
        pickUpObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickupLocation.DetachChildren();
        pickUpObj.transform.localScale = pickupObjScale;
        pickUpObj = null;
    }
    public void ShotObj() {
        isObjectPickUp = false;
        //pickUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickUpObj.GetComponent<Rigidbody>().useGravity = true;
        pickUpObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickupLocation.DetachChildren();
        pickUpObj.transform.localScale = pickupObjScale;
        pickUpObj.GetComponent<Rigidbody>().AddForce(this.transform.parent.forward * 1000f);
        pickUpObj = null;
    }
    
}


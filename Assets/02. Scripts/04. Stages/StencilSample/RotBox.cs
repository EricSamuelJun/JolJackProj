using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up * 0.05f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {
    public Material Green;
    public Material Blue;
    public Material Sky;
    public Material Red;
    public Material Trans;
    public struct Mats {
        public Material Green { get; internal set; }
        public Material Blue { get; internal set; }
        public Material Sky { get; internal set; }
        public Material Red { get; internal set; }
        public Material Trans { get; internal set; }
    }
    public static Mats mats;
    // Start is called before the first frame update
    private void OnEnable() {
        mats.Green = Green;
        mats.Blue = Blue;
        mats.Sky = Sky;
        mats.Red = Red;
        mats.Trans = Trans;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHUD : MonoBehaviour
{
    float dtime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dtime += (Time.unscaledDeltaTime - dtime) * 0.1f;
    }
    private void OnGUI() {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h* 2/ 100);
        float msec = dtime * 1000f;
        float fps = 1.0f / dtime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text);
    }
}

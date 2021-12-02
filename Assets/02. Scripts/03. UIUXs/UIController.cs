using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Vector2 prevMousePos;
    private Vector2 movement;
    private Vector2[] uiPos;

    public GameObject colorUI;
    public GameObject player;
    public PlayerInteraction playerInteraction;
    public mySimpleFirstPersonController mySimpleFirstPersonController;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInteraction = player.transform.GetChild(0).GetComponent<PlayerInteraction>();
        mySimpleFirstPersonController = player.GetComponent<mySimpleFirstPersonController>();

        uiPos = new Vector2[5];
        uiPos[0].x = 0;
        uiPos[0].y = 1;

        for (int i = 1; i < uiPos.Length; i++)
        {
            uiPos[i].x = Mathf.Cos((Mathf.PI * (90 + i * 72)) / 180);
            uiPos[i].y = Mathf.Sin((Mathf.PI * (90 + i * 72)) / 180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            prevMousePos = Input.mousePosition;
        }
        //Debug.Log(Input.GetAxis("Mouse X").ToString());
        if (Input.GetKey(KeyCode.LeftAlt))
        {
           // Debug.Log("UI 보임");
            colorUI.SetActive(true);
            mySimpleFirstPersonController.enabled = false;
            Time.timeScale = 0.1f;
            /*
            movement.x = Input.GetAxis("Mouse X");
            movement.y = Input.GetAxis("Mouse Y");
            movement = movement.normalized;
            Debug.Log(vec.ToString());
            */
            Vector2 vec = (Vector2)(Input.mousePosition) - prevMousePos;
            vec = vec.normalized;




            // player 오브젝트에 있는 스크립트에서 ability 변수 변경
            if (vec.x == 0 && vec.y == 0)
            {
                
            }
            else if ((vec.x <= uiPos[0].x || vec.x > uiPos[1].x) && (vec.y <= uiPos[0].y || vec.y > uiPos[1].y))
            {
                playerInteraction.ability = Ability.magnetism;
            }
            else if ((vec.x >= uiPos[1].x || vec.x < uiPos[2].x) && (vec.y <= uiPos[1].y || vec.y > uiPos[2].y))
            {
                playerInteraction.ability = Ability.gravity;
            }
            else if ((vec.x >= uiPos[2].x || vec.x < uiPos[3].x) && (vec.y >= uiPos[2].y || vec.y < uiPos[3].y))
            {
                playerInteraction.ability = Ability.gravity;
            }
            else if ((vec.x >= uiPos[3].x || vec.x < uiPos[4].x) && (vec.y >= uiPos[3].y || vec.y < uiPos[4].y))
            {
                playerInteraction.ability = Ability.gravity;
            }
            else
            {
                playerInteraction.ability = Ability.gravity;
            }

            Debug.Log(playerInteraction.ability.ToString());
        }
        else
        {
            //Debug.Log("UI 안보임");
            colorUI.SetActive(false);
            mySimpleFirstPersonController.enabled = true;
            Time.timeScale = 1.0f;
        }
    }

    public IEnumerator MousePosFunc()
    {
        yield return new WaitForSeconds(0.2f);
        prevMousePos = Input.mousePosition;
    }
}



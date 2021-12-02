using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private RaycastHit hit;         // 충돌 물체
    public Ability ability;         // 플레이어 현재 능력
    public bool[] canUseAbility;    // 능력 사용 가능 여부

    // Start is called before the first frame update
    void Start()
    {
        ability = Ability.nothing;
        canUseAbility = new bool[5];
        for (int i = 0; i < canUseAbility.Length; i++) 
            canUseAbility[i] = true;
    }

    // Update is called once per frame
    void Update()
    {
        //2021.05.17 전성욱 물건 들고 있을땐 색 못쏘게 설정
        if (!PlayerRay.isObjectPickUp) {
            // 능력 선택
            if (Input.GetKeyDown(KeyCode.Alpha1) && canUseAbility[0])
            {
                ability = Ability.magnetism;
                Debug.Log("현재 능력: Magnetism");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseAbility[1])
            {
                ability = Ability.gravity;
                Debug.Log("현재 능력: Gravity");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseAbility[2])
            {
                ability = Ability.acceleration;
                Debug.Log("현재 능력: Acceleration");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && canUseAbility[3])
            {
                ability = Ability.invisible;
                Debug.Log("현재 능력: Invisible");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && canUseAbility[4])
            {
                ability = Ability.elasticity;
                Debug.Log("현재 능력: Elasticity");
            }

            // 속성 부여/뺏기
            if (Input.GetMouseButtonDown(0))
            {
                Physics.Raycast(transform.position, transform.forward, out hit);

               try
               {
                    GameObject hittedObject = hit.transform.gameObject;

                    // Colorable 큐브 맞출시
                    if (hittedObject.GetComponent<IRaycastable>() != null && hittedObject.GetComponent<ColorableObject>() != null)
                    {
                        Icolor preveiousColorMode;  // 물체에 이미 적용되있는 상태변수
                        ColorableObject colorableObject = hittedObject.GetComponent<ColorableObject>();

                        preveiousColorMode = colorableObject.icolor;

                        // 현재 큐브의 속성이 없거나 속성이 부여된 큐브에 다른 속성을 부여하는 경우
                        if (colorableObject.icolor.abilityState == Ability.nothing || ability != colorableObject.icolor.abilityState)
                        {
                            preveiousColorMode.ColorOff();

                            switch (ability)
                            {
                                case Ability.magnetism:
                                    colorableObject.icolor = colorableObject.colorMagnetism;
                                    break;

                                case Ability.gravity:
                                    colorableObject.icolor = colorableObject.colorGravity;
                                    break;

                                case Ability.acceleration:
                                    colorableObject.icolor = colorableObject.colorAcceleration;
                                    break;

                                case Ability.invisible:
                                    colorableObject.icolor = colorableObject.colorInvisible;
                                    break;

                                case Ability.elasticity:
                                    colorableObject.icolor = colorableObject.colorElasticity;
                                    break;

                                default:
                                    colorableObject.icolor = colorableObject.colorNothing;
                                    break;
                            }
                            colorableObject.icolor.abilityState = ability;
                            colorableObject.icolor.ColorOn();
                        }
                        else
                        {
                            colorableObject.icolor.ColorOff();
                            colorableObject.icolor.abilityState = Ability.nothing;
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Debug.Log(e.ToString());
                }



            }
        }
    }
}

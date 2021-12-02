using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private RaycastHit hit;         // �浹 ��ü
    public Ability ability;         // �÷��̾� ���� �ɷ�
    public bool[] canUseAbility;    // �ɷ� ��� ���� ����

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
        //2021.05.17 ������ ���� ��� ������ �� ����� ����
        if (!PlayerRay.isObjectPickUp) {
            // �ɷ� ����
            if (Input.GetKeyDown(KeyCode.Alpha1) && canUseAbility[0])
            {
                ability = Ability.magnetism;
                Debug.Log("���� �ɷ�: Magnetism");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseAbility[1])
            {
                ability = Ability.gravity;
                Debug.Log("���� �ɷ�: Gravity");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseAbility[2])
            {
                ability = Ability.acceleration;
                Debug.Log("���� �ɷ�: Acceleration");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && canUseAbility[3])
            {
                ability = Ability.invisible;
                Debug.Log("���� �ɷ�: Invisible");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && canUseAbility[4])
            {
                ability = Ability.elasticity;
                Debug.Log("���� �ɷ�: Elasticity");
            }

            // �Ӽ� �ο�/����
            if (Input.GetMouseButtonDown(0))
            {
                Physics.Raycast(transform.position, transform.forward, out hit);

               try
               {
                    GameObject hittedObject = hit.transform.gameObject;

                    // Colorable ť�� �����
                    if (hittedObject.GetComponent<IRaycastable>() != null && hittedObject.GetComponent<ColorableObject>() != null)
                    {
                        Icolor preveiousColorMode;  // ��ü�� �̹� ������ִ� ���º���
                        ColorableObject colorableObject = hittedObject.GetComponent<ColorableObject>();

                        preveiousColorMode = colorableObject.icolor;

                        // ���� ť���� �Ӽ��� ���ų� �Ӽ��� �ο��� ť�꿡 �ٸ� �Ӽ��� �ο��ϴ� ���
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

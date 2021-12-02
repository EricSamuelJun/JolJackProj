using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    nothing         = 0,   // ���Ӽ�
    magnetism       = 1,   // �ڼ�
    gravity         = 2,   // ���߷�
    acceleration    = 3,   // ����
    invisible       = 4,   // ����
    elasticity      = 5,   // ź��
}

public interface IRaycastable {

    public void OnRayHit();
    public void OnRayOut();

    public void OnRayIng();
    public GameObject getGameObject();
}
public interface ICarryable {
    /// <summary>
    /// ������ �� �� ȣ��
    /// </summary>
    public void OnLiftUp();
    /// <summary>
    /// ������ ��� �ִ� ���� ȣ��
    /// </summary>
    public void OnLifting();
    /// <summary>
    /// ���� ���������� ȣ��
    /// </summary>
    /// 
    public void OnLiftDown();
}

public interface IColorable {

}


[RequireComponent(typeof(Outline))]
public class SelectableObject : MonoBehaviour, IRaycastable{
    #region �����̺� ����
    private bool isColorable = false;
    private Outline myoutline;
    #endregion


    

    private void Awake() {
        myoutline = this.gameObject.GetComponent<Outline>();
    }
    // Start is called before the first frame update
    void Start(){
        myoutline.enabled = false;
    }

    // Update is called once per frame
    void Update(){
        
    }
    
    void IRaycastable.OnRayHit() {
        myoutline.enabled = true;
    }
    void IRaycastable.OnRayOut() {
        myoutline.enabled = false;
    }
    void IRaycastable.OnRayIng() {

    }
    public GameObject getGameObject() {
        return this.gameObject;
    }

}

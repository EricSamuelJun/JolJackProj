using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    nothing         = 0,   // 무속성
    magnetism       = 1,   // 자성
    gravity         = 2,   // 역중력
    acceleration    = 3,   // 가속
    invisible       = 4,   // 투명
    elasticity      = 5,   // 탄성
}

public interface IRaycastable {

    public void OnRayHit();
    public void OnRayOut();

    public void OnRayIng();
    public GameObject getGameObject();
}
public interface ICarryable {
    /// <summary>
    /// 물건을 들 때 호출
    /// </summary>
    public void OnLiftUp();
    /// <summary>
    /// 물건을 들고 있는 동안 호출
    /// </summary>
    public void OnLifting();
    /// <summary>
    /// 물건 내려놓을때 호출
    /// </summary>
    /// 
    public void OnLiftDown();
}

public interface IColorable {

}


[RequireComponent(typeof(Outline))]
public class SelectableObject : MonoBehaviour, IRaycastable{
    #region 프라이빗 변수
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

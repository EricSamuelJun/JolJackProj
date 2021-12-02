using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public abstract class Icolor
{
    public Ability abilityState = Ability.nothing;
    public GameObject gameObject;
    public MeshRenderer meshRenderer;
    public Rigidbody rigidbody;

    public bool gravityOn = true;
    public bool magneticOn = false;
    public float gravity = 9.81f;
    public float magneticForce = 1000.0f;


    //21.05.17 전성욱 마테리얼 추가 제거
    public Material myColoredMaterial;
    public List<Material> materials;
    public abstract void setMaterials();
    public abstract void ColorOn();
    public abstract void Coloring();
    public abstract void ColorOff();
}

public class ColorNothing : Icolor {
    public override void setMaterials() { }
    public override void ColorOn() { }
    public override void Coloring() { }
    public override void ColorOff() { }
}

public class ColorMagnetism : Icolor {
    public override void setMaterials() {
        materials = meshRenderer.sharedMaterials.ToList();
        myColoredMaterial = MaterialManager.mats.Green;
    }
    public override void ColorOn()
    {
        magneticOn = true;
        //meshRenderer.material.color = Color.green;
        materials.Add(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }
    public override void Coloring()
    {

    }
    public override void ColorOff()
    {
        magneticOn = false;
        rigidbody.useGravity = true;
        //meshRenderer.material.color = Color.white;
        materials.Remove(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }
}

public class ColorGravity : Icolor {
    public override void setMaterials() {
        materials = meshRenderer.sharedMaterials.ToList();
        myColoredMaterial = MaterialManager.mats.Sky;
    }
    public override void ColorOn()
    {
        gravityOn = false;
        rigidbody.useGravity = false;
        //meshRenderer.material.color = Color.blue;
        materials.Add(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }
    public override void Coloring()
    {
        if (gravityOn == false)
        {
            rigidbody.AddForce(new Vector3(0, gravity * 40.0f, 0) * Time.deltaTime, ForceMode.Force);
        }
    }
    public override void ColorOff()
    {
        gravityOn = true;
        rigidbody.useGravity = true;
        //meshRenderer.material.color = Color.white;
        materials.Remove(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }
}

public class ColorAcceleration : Icolor {
    float tempMass;
    public override void setMaterials() {
        materials = meshRenderer.sharedMaterials.ToList();
        myColoredMaterial = MaterialManager.mats.Red;
    }
    public override void ColorOn()
    {
        //meshRenderer.material.color = Color.red;
        materials.Add(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        tempMass = rigidbody.mass;
        rigidbody.mass = 0.7f * tempMass;
        
    }
    public override void Coloring()
    {
        //가속도 계속 줌
        if(rigidbody.velocity.magnitude > 1f) {
            rigidbody.AddForce(new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,rigidbody.velocity.z) * 100f * Time.deltaTime);
            
        }
    }
    public override void ColorOff()
    {
        //meshRenderer.material.color = Color.white;
        materials.Remove(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rigidbody.mass = tempMass;

    }
}
public class ColorInvisible : Icolor {
    public GameObject mObj;
    public override void setMaterials() {
        materials = meshRenderer.sharedMaterials.ToList();
        myColoredMaterial = MaterialManager.mats.Trans;
    }
    public override void ColorOn()
    {
        //meshRenderer.enabled = false;
        materials.Add(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
        mObj.layer = 9;
    }
    public override void Coloring() { }
    public override void ColorOff() {
        //meshRenderer.material.color = Color.white;
        materials.Remove(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
        mObj.layer = 8;
    }
}

public class ColorElasticity : Icolor {
    public override void setMaterials() {
        materials = meshRenderer.sharedMaterials.ToList();
        myColoredMaterial = MaterialManager.mats.Blue;
    }
    public BoxCollider boxCollider;
    //2021.05.17 전성욱 피직스 머티리얼 가져옴
    /// <summary>
    /// 탄성 상태의 피직스 머티리얼 입니다.
    /// </summary>
    public PhysicMaterial bouncyPhyMat;
    /// <summary>
    /// 일반 상태의 기본 피직스 머티리얼 입니다.
    /// </summary>
    public PhysicMaterial normalPhyMat;
    public override void ColorOn()
    {
        boxCollider.material= this.bouncyPhyMat;
        //meshRenderer.material.color = Color.cyan;
        materials.Add(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }

    public override void Coloring() { }

    public override void ColorOff()
    {

        boxCollider.material = this.normalPhyMat;
        //meshRenderer.material.color = Color.white;
        materials.Remove(myColoredMaterial);
        meshRenderer.materials = materials.ToArray();
    }


}

public class ColorableObject : MonoBehaviour, IColorable
{
    public Icolor icolor;

    public ColorNothing colorNothing;
    public ColorMagnetism colorMagnetism;
    public ColorGravity colorGravity;
    public ColorAcceleration colorAcceleration;
    public ColorInvisible colorInvisible;
    public ColorElasticity colorElasticity;
    [SerializeField]
    PhysicMaterial bouncy;
    [SerializeField]
    PhysicMaterial normal;

    [SerializeField]
    public MaterialManager.Mats mats;

    public SphereCollider spCol;

    private void Awake()
    {
        colorNothing = new ColorNothing();
        colorMagnetism = new ColorMagnetism();
        colorGravity = new ColorGravity();
        colorAcceleration = new ColorAcceleration();
        colorInvisible = new ColorInvisible();
        colorElasticity = new ColorElasticity();

        icolor = colorNothing;
        icolor.rigidbody = GetComponent<Rigidbody>();
        icolor.meshRenderer = GetComponent<MeshRenderer>();

        colorElasticity.boxCollider = GetComponent<BoxCollider>();

        colorMagnetism.rigidbody = GetComponent<Rigidbody>();
        colorGravity.rigidbody = GetComponent<Rigidbody>();
        colorAcceleration.rigidbody = GetComponent<Rigidbody>(); 
        colorInvisible.rigidbody = GetComponent<Rigidbody>();
        colorElasticity.rigidbody = GetComponent<Rigidbody>();

        colorMagnetism.meshRenderer = GetComponent<MeshRenderer>();
        colorGravity.meshRenderer = GetComponent<MeshRenderer>();
        colorAcceleration.meshRenderer = GetComponent<MeshRenderer>();
        colorInvisible.meshRenderer = GetComponent<MeshRenderer>();
        colorElasticity.meshRenderer = GetComponent<MeshRenderer>();

        colorInvisible.mObj = this.gameObject;
        
    }
    void Start() {
        colorElasticity.bouncyPhyMat = bouncy;
        colorElasticity.normalPhyMat = normal;

        colorMagnetism.setMaterials();
        colorGravity.setMaterials();
        colorAcceleration.setMaterials();
        colorInvisible.setMaterials();
        colorElasticity.setMaterials();

    }
    void OnEnable() {
        if(spCol == null) {
            spCol = this.gameObject.AddComponent<SphereCollider>();
            spCol.isTrigger = true;
            spCol.radius = 10;
        } else {
            spCol.enabled = true;
        }
    }
    void OnDisable() {
        spCol.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            icolor.Coloring();
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // 자성 처리
        if (icolor.magneticOn == true && other.gameObject.GetComponent<IMagnetic>() != null)
        {
            Debug.Log("자성 발동");
            GetComponent<Rigidbody>().useGravity = false;
            transform.LookAt(other.transform);
            Vector3 towardObject = other.gameObject.transform.position - this.gameObject.transform.position;
            GetComponent<Rigidbody>().AddForce(towardObject.normalized * icolor.magneticForce * Time.deltaTime, ForceMode.Force);
            //GetComponent<Rigidbody>().transform.Translate(towardObject * 3f * Time.deltaTime);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(icolor == colorMagnetism)
            GetComponent<Rigidbody>().useGravity = true;
    }
}







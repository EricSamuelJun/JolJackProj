using UnityEngine;

//simple first person controller using rigidbody, by Damián González, specially for portals asset.
public class mySimpleFirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    Transform cam;
    public float walkSpeed = 5f;
    public float runSpeed  = 15f;
    public Vector2 mouseSensitivity = new Vector2(1f, 1f);
    float rotX = 0; //start looking forward
    public float maxVelY = 10f;
    public float jumpImpulse = 10f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void FixedUpdate() {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 forwardNotTilted = new Vector3(transform.forward.x, 0, transform.forward.z);

        rb.velocity = (
            forwardNotTilted * speed * Input.GetAxis("Vertical")    //move forward
            +
            transform.right * speed * Input.GetAxis("Horizontal")   //slide to sides
            +
            new Vector3(0, rb.velocity.y , 0)                       //allow jumping & falling
        );


        //look up and down
        rotX += Input.GetAxis("Mouse Y") * mouseSensitivity.y * -1;
        rotX = Mathf.Clamp(rotX, -60f, 60f); //clamp look 
        cam.localRotation = Quaternion.Euler(rotX, 0, 0);

        
        //player tilted? try to make him stand still
        rb.MoveRotation(Quaternion.Lerp(
            transform.rotation * Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity.x, 0),
            Quaternion.Euler(0, transform.eulerAngles.y, 0),
            .1f
        ));

    }

    private void Update() {
        if (Input.GetButtonDown("Jump") && Physics.CheckSphere(transform.position - new Vector3(0, 1.5f, 0), .5f))
            rb.AddForce(0, jumpImpulse, 0, ForceMode.Impulse);
    }

}

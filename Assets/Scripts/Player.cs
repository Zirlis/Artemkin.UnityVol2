using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player inst;

    [Header("Move")]
    [SerializeField] public float speed = 16f;
    [SerializeField] public float rotSpeed = 120f;
    [SerializeField] private float modBackSpeed = 0.75f;
    [SerializeField] public float forceOfJump = 200f;
    [SerializeField] private float jumpCoolDown = 1f;
    [SerializeField] public float forceOfDown;
    [SerializeField] public float nullForceOfDown = 0f;
    [SerializeField] public float maxForceOfDown = 5f;
    [SerializeField] public float verticalSpeedMod = 1.2f;
    public float nullVerticalSpeedMod = 0f;
    public float modSpeed;
    private bool moved;

    private float eulerY;
    private float eulerX;

    [Header("Other")]  
    [SerializeField] Transform Camera;
    Rigidbody body;

    public bool canJump = false;    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = gameObject.GetComponent<Rigidbody>();
        modSpeed = verticalSpeedMod;
        forceOfDown = nullForceOfDown;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(transform.forward * speed, ForceMode.Impulse);
            body.AddForce(transform.up * modSpeed * speed, ForceMode.Impulse);
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(transform.forward * -speed * modBackSpeed, ForceMode.Impulse);
            body.AddForce(transform.up * modSpeed * speed, ForceMode.Impulse);
            moved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(transform.right * -speed * modBackSpeed, ForceMode.Impulse);
            body.AddForce(transform.up * modSpeed * speed, ForceMode.Impulse);
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * speed * modBackSpeed, ForceMode.Impulse);
            body.AddForce(transform.up * modSpeed * speed, ForceMode.Impulse);
            moved = true;
        }


        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            body.AddForce(Vector3.up * forceOfJump, ForceMode.Impulse);                   
        }

        body.AddForce(Vector3.up * -forceOfDown, ForceMode.Impulse);        
    }

    void Update()
    {
        float X = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        float Y = -Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        eulerX = (Camera.rotation.eulerAngles.x + Y) % 360;
        eulerY = (transform.rotation.eulerAngles.y + X) % 360;

        transform.rotation = Quaternion.Euler(0, eulerY, 0);
        if (eulerX >= 80 && eulerX <= 90)
            eulerX = 80;
        if (eulerX <= 270 && eulerX >= 250)
            eulerX = 270;

        Camera.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        
        if(!moved && canJump)
        {
            body.useGravity = false;
        }
        else
        {
            body.useGravity = true;
        }
        moved = false;
    }
}

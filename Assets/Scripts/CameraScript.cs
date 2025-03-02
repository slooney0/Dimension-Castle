using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject camera3d;
    [SerializeField] GameObject camera2d;

    public static bool rotation = true;

    private float _sensitivity = 360f;
    private Vector2 _mouseInput;
    private float _pitch;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (LeverScript.leverActive == false)
        {
            camera2d.SetActive(true);
            camera3d.SetActive(false);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -12f), new Vector3(player.transform.position.x, player.transform.position.y, -12f), 0.5f);
            if (rotation == false)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rotation = true;
            }
        }
        else {
            camera2d.SetActive(false);
            camera3d.SetActive(true);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z),new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), 0.1f);
            _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            transform.Rotate(Vector3.up, _mouseInput.x * _sensitivity * Time.deltaTime);
            _pitch -= _mouseInput.y * _sensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -80f, 80f);
            transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);
        }
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }
}

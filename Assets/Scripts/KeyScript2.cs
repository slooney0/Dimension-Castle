using UnityEngine;
using UnityEngine.InputSystem;

public class KeyScript2 : MonoBehaviour
{
    [SerializeField] GameObject key;
    public static bool keyActive;
    private float rotationSpeed = 0.1f;
    private float moveSpeed = 0.001f;
    private float startHeight;
    private bool moveUp;

    private void Start()
    {
        keyActive = false;
        Debug.Log(transform.rotation.w);
        startHeight = transform.position.y;
        moveUp = true;
    }

    private void Update()
    {
        if (keyActive == false)
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0f, transform.localEulerAngles.y + rotationSpeed, transform.localEulerAngles.z), 1f);

            if (moveUp)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z), 1f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z), 1f);
            }

            if (transform.position.y > (startHeight + 0.1f))
            {
                moveUp = false;
            }
            else if (transform.position.y < (startHeight - 0.1f))
            {
                moveUp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            keyActive = true;
            key.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    private GameObject player;

    [SerializeField]
    private float mouseXLimit = 88f;
    
    private float mouseX;
    private float mouseY;

    private float rotationX;
    private float rotationY;

    private float mouseSensitivityX = 100f;
    private float mouseSensitivityY = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        
        mouseX = mouseSensitivityX * Input.GetAxisRaw("Mouse X") * Time.deltaTime;
        mouseY = mouseSensitivityY * Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

        rotationX += mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -mouseXLimit, mouseXLimit);

        transform.rotation = Quaternion.Euler(-rotationX, rotationY, 0);
        player.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        
        //Handle Aiming
        Vector3 middleScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cam.nearClipPlane));
        
        RaycastHit hit;
        if (Physics.Raycast(middleScreen, transform.forward, out hit, 10f))
        {
            Debug.Log("Hit " + hit.collider.name);
        };
    }
}
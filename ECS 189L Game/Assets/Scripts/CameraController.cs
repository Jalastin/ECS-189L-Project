using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _mousePosition;
    public Vector3 MousePosition
    {
        get => _mousePosition;
        set => _mousePosition = value;
    }
    private Camera managedCamera;
    [SerializeField] private GameObject target;
    [SerializeField] private float Speed;
    // Boolean flag to keep track of whether the ball is out of the screen.
    private bool isOutOfScreen = false;
    // Initialize the camera and line-renderer.
    private void Awake()
    {
        this.managedCamera = this.gameObject.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        // The camera is set to the target's position (except the z-value).
        var pearlObject = GameObject.Find("Pearl(Clone)");
        var targetPosition = this.target.transform.position;
        var cameraPosition = this.managedCamera.transform.position;
        Vector3 cameraTarget = new Vector3(targetPosition.x, targetPosition.y + 10, cameraPosition.z);

        // Focus camera on pearl if it moves out of the screen. 
        if (pearlObject)
        {
            var pearlPosition = pearlObject.transform.position;
            var viewPos = this.managedCamera.WorldToViewportPoint(pearlPosition);
            // Check if the pearl is out of the screen. 
            if (!(viewPos.x > 0 && viewPos.x < 1 && viewPos.y < 1 && viewPos.y > 0 && viewPos.z > 0))
            {
                // Set boolean flag.
                this.isOutOfScreen = true;
            }

            if (this.isOutOfScreen)
            {
                // Focus camera on the pearl if it is out of screen.
                Vector3 cameraTargetPearl = new Vector3(pearlPosition.x, pearlPosition.y, cameraPosition.z);
                var newCameraPosition = Vector3.MoveTowards(cameraPosition, cameraTargetPearl, Time.deltaTime * this.Speed);
                this.managedCamera.transform.position = newCameraPosition;
            }
        }
        else
        {
            // Reset boolean flag if pearl is not moving out of the screen.
            this.isOutOfScreen = false;
            // Focus on player if pearl is not traveling out of the screen.
            cameraPosition = Vector3.MoveTowards(cameraPosition,
            cameraTarget, Time.deltaTime * this.Speed);
            this.managedCamera.transform.position = cameraPosition;
        }
        // Update MousePosition to wherever the mouse currently is.
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}


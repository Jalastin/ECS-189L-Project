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
        var targetPosition = this.target.transform.position;
        var cameraPosition = this.managedCamera.transform.position;
        Vector3 cameraTarget = new Vector3(targetPosition.x, targetPosition.y+10, cameraPosition.z);
        // Only move the camera if it is not already on the target.
        if (cameraPosition != cameraTarget)
        {
            // Target is moving, but within leash; move camera at fractional target's speed.
            cameraPosition = Vector3.MoveTowards(cameraPosition,
                cameraTarget, Time.deltaTime * this.Speed);
        }

        this.managedCamera.transform.position = cameraPosition;

        // Update MousePosition to wherever the mouse currently is.
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}


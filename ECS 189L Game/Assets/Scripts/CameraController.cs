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
    // Start is called before the first frame update
    void Start()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Update MousePosition to wherever the mouse currently is.
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

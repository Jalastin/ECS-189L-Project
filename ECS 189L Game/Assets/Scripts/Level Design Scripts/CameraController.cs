using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign {
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
        void LateUpdate()
        {
            // The camera is set to the target's position (except the z-value).
            var targetPosition = this.target.transform.position;
            this.managedCamera.transform.position =
                new Vector3(targetPosition.x, targetPosition.y, this.managedCamera.transform.position.z);  
            // Update MousePosition to wherever the mouse currently is.
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}

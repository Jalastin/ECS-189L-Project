using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour {
    public float _Speed;

    void Update()
    {
        transform.Rotate(0, 0, _Speed * Time.deltaTime);
    }
}

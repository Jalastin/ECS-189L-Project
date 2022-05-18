using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpriteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10f);
        this.GetComponent<WindSpriteMotion>().Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

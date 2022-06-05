using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Factory class that all factories must implement.
public class Factory : MonoBehaviour
{
    GameObject Build(Spec newSpec)
    {
        return null;
    }
}

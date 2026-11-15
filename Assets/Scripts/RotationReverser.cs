using System;
using UnityEngine;

public class RotationReverser : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}

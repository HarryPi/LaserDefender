using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);
    }
}

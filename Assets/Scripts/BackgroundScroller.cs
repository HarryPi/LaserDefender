using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField]
    private float backgroundScrollSpeed = 0.5f;

    private Vector2 _offset;
    private Material _myMaterial;

    void Start() {
        _myMaterial = GetComponent<Renderer>().material;
        _offset = new Vector2(0f, backgroundScrollSpeed);
    }

    void Update() {
        _myMaterial.mainTextureOffset += _offset * Time.deltaTime;
    }
}
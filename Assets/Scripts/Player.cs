using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMin;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float padding = 1f;
    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1)).y;

    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float xPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float yPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(xPos, yPos);
    }
}
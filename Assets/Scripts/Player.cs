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

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private float projectileFirePeriod = 0.1f;

    private Coroutine fireCoroutine;
    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Fire();
    }

    IEnumerator FireContentiously() {
        while (true) {
            GameObject laserGameObject = Instantiate(laser, transform.position, Quaternion.identity);
            laserGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            fireCoroutine = StartCoroutine(FireContentiously());
        }

        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(fireCoroutine);
        }

    }

    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float xPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float yPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(xPos, yPos);
    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1)).y - padding;

    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Stats")]
    [SerializeField]
    private int health = 200;

    [Header("Player Movement")]
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float padding = 1f;

    [Header("Projectile")]
    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private float projectileFirePeriod = 0.1f;

    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

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

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) {
            health -= damageDealer.Damage;

            if (health <= 0) {
                Destroy(gameObject);
            }
        }
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

        float xPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        float yPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

        transform.position = new Vector2(xPos, yPos);
    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;

        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).x + padding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0)).x - padding;

        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).y + padding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1)).y - padding;

    }

}
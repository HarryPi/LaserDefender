using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private float shotcounter;

    [SerializeField]
    private float minTimeBetweenShots = 0.2f;

    [SerializeField]
    private float maxTimeBetweenShots = 3f;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float projectileSpeed = 5f;

    public void Start() {
        shotcounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update() {
        CountDownAndShoot();
    }

    private void CountDownAndShoot() {
        shotcounter -= Time.deltaTime;
        if (shotcounter <= 0f) {
            Fire();
            shotcounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire() {
        GameObject newLaser = Instantiate(laser, gameObject.transform.position, Quaternion.identity);
        newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageComponent = other.GetComponent<DamageDealer>();

        if (damageComponent != null) {
            health -= damageComponent.Damage;
            damageComponent.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
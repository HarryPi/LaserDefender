using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField]
    private WaveConfig waveConfig;
    
    private List<Transform> _wayPoints = new List<Transform>();

    [SerializeField]
    private float moveSpeed = 2f;

    private int _wayPointIndex = 0;

    private void Start() {
        _wayPoints = waveConfig.GetWayPoints().ToList();
        transform.position = _wayPoints[_wayPointIndex].transform.position;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (_wayPointIndex <= _wayPoints.Count - 1) {
            Vector3 targetPosition = _wayPoints[_wayPointIndex].transform.position;
            float movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition) {
                _wayPointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
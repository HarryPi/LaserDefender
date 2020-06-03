using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    private WaveConfig _waveConfig;
    private List<Transform> _wayPoints = new List<Transform>();
    private int _wayPointIndex = 0;

    public void Start() {
        _wayPoints = _waveConfig.GetWayPoints().ToList();
        transform.position = _wayPoints[_wayPointIndex].transform.position;
    }

    public void Update() {
        Move();
    }

    public WaveConfig WaveConfig {
        set => _waveConfig = value;
    }

    private void Move() {
        if (_wayPointIndex <= _wayPoints.Count - 1) {
            Vector3 targetPosition = _wayPoints[_wayPointIndex].transform.position;
            float movementThisFrame = _waveConfig.MoveSpeed * Time.deltaTime;
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
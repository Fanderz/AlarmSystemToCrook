using UnityEngine;

public class Robber : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform[] _points;

    private Transform _target;
    private int _pointIndex = 0;
    private bool _reachedLastPoint = false;

    private void Start()
    {
        _target = _points[0];
    }

    private void Update()
    {
        if (transform.position == _target.position)
        {
            SetTarget();
            Rotate();
        }
        
        Move();
    }

    private void Move() =>
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);

    private void Rotate() =>
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position, Vector3.up);

    private void SetTarget()
    {
        if (_pointIndex < _points.Length - 1 && _reachedLastPoint == false)
        {
            _pointIndex++;
        }
        else if (_pointIndex == 0)
        {
            _reachedLastPoint = false;
        }
        else
        {
            _reachedLastPoint = true;
            _pointIndex--;
        }

        _target = _points[_pointIndex];
    }
}

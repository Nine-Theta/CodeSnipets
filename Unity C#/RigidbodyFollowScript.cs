using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyFollowScript : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private ForceMode _forceMode;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _followDeadzone = 1.0f, _speedMultiplier = 1.0f;

    [SerializeField]
    private bool _lookAtTarget = false;
    [SerializeField]
    private Transform _altLookTarget = null;

    private Vector3 _targetVec, _relativeOffset, _deltaVec;

    private Rigidbody _rigidbody;

    public bool LookAtTarget
    {
        get { return _lookAtTarget; }
        set { _lookAtTarget = value; }
    }

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _relativeOffset = _target.right * _offset.x;
        _relativeOffset += _target.up * _offset.y;
        _relativeOffset += _target.forward * _offset.z;
        _targetVec = _target.position + _relativeOffset;


        if ((_targetVec - transform.position).sqrMagnitude > _followDeadzone)
        {
            _deltaVec = _targetVec - transform.position;
            _rigidbody.AddForce(_deltaVec.normalized * _speedMultiplier, _forceMode);
        }

        if (_lookAtTarget)
        {
            if(_altLookTarget == null)
                transform.LookAt(_target.position);
            else
                transform.LookAt(_altLookTarget);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void SetLookTarget(Transform newLookAtTarget)
    {
        _altLookTarget = newLookAtTarget;
    }
}

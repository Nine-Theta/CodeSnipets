using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowScript : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Vector3 _rotationOffset;

    [SerializeField]
    private bool _lookAtTarget = false;
    [SerializeField]
    private Transform _altLookTarget = null;

    public bool LookAtTarget
    {
        get { return _lookAtTarget; }
        set { _lookAtTarget = value; }
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
        transform.position += transform.right * _offset.x;
        transform.position += transform.up * _offset.y;
        transform.position += transform.forward * _offset.z;

        if(_lookAtTarget)
        {
            if (_altLookTarget == null)
                transform.LookAt(_target.position);
            else
                transform.LookAt(_altLookTarget);
        }

        transform.rotation *= Quaternion.Euler(_rotationOffset);
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

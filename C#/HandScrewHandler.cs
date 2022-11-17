using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interhaptics.Modules.Interaction_Builder.Core;
using Interhaptics.InteractionsEngine.Shared.Types;
using Interhaptics.InteractionsEngine;
using Interhaptics.ObjectSnapper.core;
using Interhaptics.HandTracking;
public class HandScrewHandler : MonoBehaviour
{

    [SerializeField]
    private Quaternion _startRot;

    private TrackedHand _rightHand;
    private TrackedHand _leftHand;

    [SerializeField]
    private float _rotationScalar = 1.0f;

    [SerializeField]
    private bool _invertedRotation = false;

    [SerializeField]
    private int _rotationMin = 0;
    [SerializeField]
    private int _rotationMax = 360;

    private float _oldAngle;
    private Vector3 _startRight; //the right dir of the hand that's interacting, when it starts to interact.

    private bool _isLeft;
    private bool _startRecording = false;

    private void Start()
    {
        _rightHand = SceneFlowHandler.Instance.RightHand;
        _leftHand = SceneFlowHandler.Instance.LeftHand;

        if (_rightHand == null || _leftHand == null) Debug.LogWarning(this + " is missing one or both hand references. The script won't work without atleast one of them. (though both is preferred)");
    }

    public void StartScrew(InteractionObject interactionObject)
    {
        gameObject.transform.localRotation.ToAngleAxis(out _oldAngle, out Vector3 n);

        if (interactionObject.InteractWith == BodyPartInteractionStrategy.RightHand)
        {
            _startRight = _rightHand.transform.right;
            _isLeft = false;
        }
        else
        {
            _startRight = _leftHand.transform.right;
            _isLeft = true;
        }

        _startRecording = true;
    }

    private void Update()
    {
        if (!_startRecording)
            return;

        float deltaAngle = 0;

        if (_isLeft)
        {
            deltaAngle = AngleOffAroundAxis(_startRight, _leftHand.transform.right, _leftHand.transform.forward, true);
        }
        else
        {
            deltaAngle = AngleOffAroundAxis(_startRight, _rightHand.transform.right, _rightHand.transform.forward, false);
        }

        float newAngle = _oldAngle + (deltaAngle * _rotationScalar);

        if (newAngle > _rotationMax) newAngle = _rotationMax;
        if (newAngle < _rotationMin) newAngle = _rotationMin;

        //to ensure the ability to continously rotate.
        if (newAngle > 360)
            newAngle -= 360;
        else if (newAngle < 0)
            newAngle += 360;

        if (_invertedRotation)
            gameObject.transform.localRotation = Quaternion.AngleAxis(newAngle, Vector3.down);
        else
            gameObject.transform.localRotation = Quaternion.AngleAxis(newAngle, Vector3.up);
    }

    public void EndScrew()
    {
        _startRecording = false;
    }

    //yoinked from https://forum.unity.com/threads/is-vector3-signedangle-working-as-intended.694105/#post-4642798
    //Many thanks to lordofduct

    /// <summary>
    /// Find some projected angle measure off some forward around some axis.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="forward"></param>
    /// <param name="axis"></param>
    /// <returns>Angle in degrees</returns>
    private float AngleOffAroundAxis(Vector3 from, Vector3 to, Vector3 axis, bool clockwise = false)
    {
        Vector3 right;
        if (clockwise)
        {
            right = Vector3.Cross(from, axis);
            from = Vector3.Cross(axis, right);
        }
        else
        {
            right = Vector3.Cross(axis, from);
            from = Vector3.Cross(right, axis);
        }
        return Mathf.Atan2(Vector3.Dot(to, right), Vector3.Dot(to, from)) * Mathf.Rad2Deg;
    }
}

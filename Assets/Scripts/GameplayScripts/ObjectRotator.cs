using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float rotSpeed = 0f;
    [SerializeField] private RotationAxis rotAxis = RotationAxis.X;
    #endregion

    #region Delegates
    private delegate void RotationMechanism();
    private RotationMechanism rotationMechanism;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        EnableRotation(true);
    }

    private void Update()
    {
        if (rotationMechanism != null)
        {
            rotationMechanism();
        }
        return;
    }
    #endregion

    #region Private Functions
    private void RotationX()
    {
        transform.Rotate(Vector3.right * rotSpeed);
    }

    private void RotationY()
    {
        transform.Rotate(Vector3.up * rotSpeed);
    }

    private void RotationZ()
    {
        transform.Rotate(Vector3.forward * rotSpeed);
    }
    #endregion

    #region Public Functions
    public void EnableRotation(bool value)
    {
        if (!value && rotationMechanism != null)
        {
            switch (rotAxis)
            {
                case RotationAxis.X:
                    rotationMechanism = null;
                    break;
                case RotationAxis.Y:
                    rotationMechanism = null;
                    break;
                case RotationAxis.Z:
                    rotationMechanism = null;
                    break;
            }
        }
        else if (value && rotationMechanism == null)
        {
            switch (rotAxis)
            {
                case RotationAxis.X:
                    rotationMechanism += RotationX;
                    break;
                case RotationAxis.Y:
                    rotationMechanism += RotationY;
                    break;
                case RotationAxis.Z:
                    rotationMechanism += RotationZ;
                    break;
            }
        }
    }
    #endregion
}

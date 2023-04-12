using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    const float k_MouseSensitivityMultiplier = 0.01f;
    /// <summary>
    /// Rotation speed when using the mouse.
    /// </summary>
    public float m_LookSpeedMouse = 4.0f;
    public float m_SmoothMultiplier = 5f;

    private static string kMouseX = "Mouse X";
    private static string kMouseY = "Mouse Y";

    private static string kSpeedAxis = "Speed Axis";

    float inputRotateAxisX, inputRotateAxisY;
    float inputChangeSpeed;
    float inputVertical, inputHorizontal, inputYAxis;
    bool leftShiftBoost, leftShift, fire1;

    private Quaternion _desiredRot;

    void UpdateInputs()
    {
        inputRotateAxisX = 0.0f;
        inputRotateAxisY = 0.0f;
        leftShiftBoost = false;
        fire1 = false;

        inputRotateAxisX = Input.GetAxis(kMouseX) * m_LookSpeedMouse;
        inputRotateAxisY = Input.GetAxis(kMouseY) * m_LookSpeedMouse;
    }

    // Start is called before the first frame update
    void Start()
    {
        _desiredRot = transform.localRotation;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        bool moved = inputRotateAxisX != 0.0f || inputRotateAxisY != 0.0f || inputVertical != 0.0f || inputHorizontal != 0.0f || inputYAxis != 0.0f; 
        if (moved)
        {
            float rotationX = _desiredRot.eulerAngles.x;
            float newRotationY = _desiredRot.eulerAngles.y + inputRotateAxisX;

            // Weird clamping code due to weird Euler angle mapping...
            float newRotationX = (rotationX - inputRotateAxisY);
            if (rotationX <= 90.0f && newRotationX >= 0.0f)
                newRotationX = Mathf.Clamp(newRotationX, 0.0f, 90.0f);
            if (rotationX >= 270.0f)
                newRotationX = Mathf.Clamp(newRotationX, 270.0f, 360.0f);

            _desiredRot = Quaternion.Euler(newRotationX, newRotationY, _desiredRot.eulerAngles.z);
        }
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _desiredRot, Time.deltaTime * m_SmoothMultiplier);
    }
}

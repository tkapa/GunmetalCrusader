using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_RotationHandler : MonoBehaviour {

    [Tooltip("The min and max thresholds for turning.")]
    [SerializeField]
    private Vector2 CameraTurnThreshold = new Vector2(45.0f, 60.0f);

    [Tooltip("The min and max turn speeds.")]
    [SerializeField]
    private Vector2 CameraTurnSpeed = new Vector2(5.0f, 30.0f);

    private bool CalledTurnSound;

    void Update()
    {
        if (Camera.main && !Player.p.Locked)
        {
            Vector3 tempRotation = this.transform.eulerAngles;
            float camYaw = Camera.main.transform.localEulerAngles.y;

            // Calculate the Rotation Speed Alpha
            float rotationSpeedAlpha = 0;

            // Figure the Extremities
            if (camYaw < 180)
            {
                rotationSpeedAlpha = Mathf.Clamp((camYaw - CameraTurnThreshold.x) / (CameraTurnThreshold.y - CameraTurnThreshold.x), 0, 1);
            }
            else
            {
                camYaw = 360 - camYaw;

                rotationSpeedAlpha = -Mathf.Clamp((camYaw - CameraTurnThreshold.x) / (CameraTurnThreshold.y - CameraTurnThreshold.x), 0, 1);
            }

            float finalSpeed = Mathf.Lerp(CameraTurnSpeed.x, CameraTurnSpeed.y, Mathf.Abs(rotationSpeedAlpha));

            if (rotationSpeedAlpha == 0) { finalSpeed = 0;  JacksSoundManager.Instance.StoppedTurning(); CalledTurnSound = false; }

            else
            {
                if (!CalledTurnSound)
                {
                    JacksSoundManager.Instance.MechTurning();
                    CalledTurnSound = true;
                }
            }

            tempRotation.y += (Mathf.Sign(rotationSpeedAlpha) * finalSpeed) * Time.deltaTime;

            this.transform.eulerAngles = tempRotation;
        }
    }
}

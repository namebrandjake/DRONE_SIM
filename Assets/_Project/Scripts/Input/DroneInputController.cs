using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneSim.Input
{
    /// <summary>
    /// Device-agnostic source of flight input. Flight/camera/mission code should depend only on this
    /// class' public members, never on Gamepad/Keyboard/Joystick APIs directly, so the same code works
    /// regardless of which physical device is active.
    /// </summary>
    /// <remarks>
    /// Axis convention: Throttle, Yaw, Pitch and Roll all range -1..1 for every bound device.
    /// Throttle: +1 = climb, -1 = descend. Yaw: +1 = yaw right. Pitch: +1 = pitch up. Roll: +1 = roll right.
    /// The Joystick control scheme only binds Pitch/Roll (via the generic stick) and Reset (via the
    /// generic trigger) — Unity's built-in Joystick layout exposes just one 2-axis stick and one button,
    /// so Throttle/Yaw and ToggleFlightMode have no generic Joystick binding yet. Supporting a real FPV
    /// radio's extra axes (commonly Z/Rz) needs a custom HID layout authored once real hardware is
    /// available to inspect via Window > Analysis > Input Debugger.
    /// </remarks>
    public class DroneInputController : MonoBehaviour
    {
        DroneControls controls;

        public float Throttle { get; private set; }
        public float Yaw { get; private set; }
        public float Pitch { get; private set; }
        public float Roll { get; private set; }

        public event Action ResetRequested;
        public event Action FlightModeToggleRequested;

        void Awake()
        {
            controls = new DroneControls();
        }

        void OnEnable()
        {
            controls.Flight.Enable();
            controls.Flight.Reset.performed += OnResetPerformed;
            controls.Flight.ToggleFlightMode.performed += OnToggleFlightModePerformed;
        }

        void OnDisable()
        {
            controls.Flight.Reset.performed -= OnResetPerformed;
            controls.Flight.ToggleFlightMode.performed -= OnToggleFlightModePerformed;
            controls.Flight.Disable();
        }

        void OnDestroy()
        {
            controls.Dispose();
        }

        void Update()
        {
            Refresh();
        }

        /// <summary>
        /// Re-reads all axis values immediately. Called every frame from Update(); exposed publicly so
        /// editor tooling/tests can force a read without waiting for a MonoBehaviour tick.
        /// </summary>
        public void Refresh()
        {
            Throttle = controls.Flight.Throttle.ReadValue<float>();
            Yaw = controls.Flight.Yaw.ReadValue<float>();
            Pitch = controls.Flight.Pitch.ReadValue<float>();
            Roll = controls.Flight.Roll.ReadValue<float>();
        }

        void OnResetPerformed(InputAction.CallbackContext context) => ResetRequested?.Invoke();

        void OnToggleFlightModePerformed(InputAction.CallbackContext context) => FlightModeToggleRequested?.Invoke();
    }
}

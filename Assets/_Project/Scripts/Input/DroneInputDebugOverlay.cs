using UnityEngine;

namespace DroneSim.Input
{
    /// <summary>
    /// Temporary dev-only OnGUI overlay for eyeballing live input values while validating device
    /// support (gamepad/keyboard/joystick). Not the real HUD/OSD — that's built in step 6.
    /// </summary>
    [RequireComponent(typeof(DroneInputController))]
    public class DroneInputDebugOverlay : MonoBehaviour
    {
        DroneInputController input;
        string lastEvent = "(none)";

        void Awake()
        {
            input = GetComponent<DroneInputController>();
        }

        void OnEnable()
        {
            input.ResetRequested += OnResetRequested;
            input.FlightModeToggleRequested += OnFlightModeToggleRequested;
        }

        void OnDisable()
        {
            input.ResetRequested -= OnResetRequested;
            input.FlightModeToggleRequested -= OnFlightModeToggleRequested;
        }

        void OnResetRequested() => lastEvent = $"Reset @ {Time.time:F1}s";

        void OnFlightModeToggleRequested() => lastEvent = $"ToggleFlightMode @ {Time.time:F1}s";

        void OnGUI()
        {
            GUI.Box(new Rect(10, 10, 260, 130), "DroneInputController (debug)");
            GUI.Label(new Rect(20, 35, 240, 20), $"Throttle: {input.Throttle:F2}");
            GUI.Label(new Rect(20, 55, 240, 20), $"Yaw:      {input.Yaw:F2}");
            GUI.Label(new Rect(20, 75, 240, 20), $"Pitch:    {input.Pitch:F2}");
            GUI.Label(new Rect(20, 95, 240, 20), $"Roll:     {input.Roll:F2}");
            GUI.Label(new Rect(20, 115, 240, 20), $"Last event: {lastEvent}");
        }
    }
}

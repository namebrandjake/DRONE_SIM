# Step 2 — Input System & Device Abstraction

## Context

This is step 2 of the "First Flight" vertical slice for DRONE_SIM. Step 1 created the Unity project (URP, Input System package installed, folder structure in place, empty `Sandbox.unity` scene). This step builds the input layer that later steps (flight physics in step 3) will consume. No drone or flight code exists yet — this step only needs to produce input values, not move anything.

A key project requirement (see README.md "Controller & Hardware Support") is that real FPV transmitters (RadioMaster, TBS, FrSky, etc.) must work via USB joystick/HID mode, in addition to standard gamepad and keyboard/mouse.

## Goal

Create an Input System action map for drone flight (throttle, yaw, pitch, roll, plus mode-switch/reset actions), and an abstraction layer so flight code never talks to raw devices directly — it reads normalized values from a `DroneInputController` (or similar) regardless of whether the source is a gamepad, keyboard, or FPV radio in joystick mode.

## Tasks

1. Create an Input Actions asset at `Assets/_Project/Settings/DroneControls.inputactions` with an action map (e.g. `Flight`) containing:
   - `Throttle` (1D axis, -1 to 1 or 0 to 1 — decide and document which, since this affects step 3's motor mixer)
   - `Yaw` (1D axis, -1 to 1)
   - `Pitch` (1D axis, -1 to 1)
   - `Roll` (1D axis, -1 to 1)
   - `Reset` (button — resets drone to pad)
   - `ToggleFlightMode` (button — placeholder for future acro/self-level toggle, not required to do anything yet)
2. Add control schemes/bindings for:
   - Gamepad (both sticks mapped to the four flight axes in a standard FPV layout — left stick = throttle/yaw, right stick = pitch/roll, "Mode 2" convention)
   - Keyboard (WASD + additional keys for yaw, as a fallback/dev-testing scheme)
   - Generic Joystick/HID (so a RadioMaster/TBS/FrSky radio in USB joystick mode is recognized and its axes are mappable to Throttle/Yaw/Pitch/Roll)
3. Implement a `DroneInputController` script in `Assets/_Project/Scripts/Input/` that:
   - Reads the generated C# Input Actions class.
   - Exposes normalized `float Throttle, Yaw, Pitch, Roll` and `bool ResetPressed` properties/events.
   - Is device-agnostic to any consumer — nothing downstream should reference `Gamepad.current` or similar directly.
4. Add a small dev-only debug overlay (on-screen text or console log, temporary) showing live input values, to verify all three device types produce correct, correctly-signed values.
5. Document the chosen axis conventions (range, sign, which stick/key maps to what) in a short comment block at the top of `DroneInputController` or in `steps/02_input_system.md`'s notes — step 3 depends on this being unambiguous.

## Acceptance Criteria

- `DroneControls.inputactions` exists with the `Flight` action map and all listed actions/bindings.
- Gamepad, keyboard, and a generic joystick/HID device (test with any available joystick/radio in joystick mode, or Unity's joystick simulation if hardware isn't available yet) all drive the same `DroneInputController` values.
- Debug overlay confirms correct sign/range for all four flight axes on at least two of the three input methods (ideally all three).
- No downstream code exists yet to consume this (that's step 3), but the controller is ready to be read from another script via a public API.

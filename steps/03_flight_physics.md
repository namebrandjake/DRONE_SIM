# Step 3 — Core Flight Physics (Motor Mixer)

## Context

This is step 3 of the "First Flight" vertical slice for DRONE_SIM. Step 1 set up the Unity project; step 2 produced a `DroneInputController` exposing normalized `Throttle/Yaw/Pitch/Roll` values regardless of input device. This step builds the actual flight physics — the most important system in the whole project, since the README's #1 core pillar is "Authentic Flight Feel: physics-based flight model (not arcade lerp-to-target)."

No environment or camera exists yet — this step can be validated in a blank test scene with a placeholder drone body (a capsule or simple prefab is fine) and a flat ground plane.

## Goal

Implement a rigidbody-based quadcopter flight controller for a single **consumer-class drone**, using a proper motor-mixer model (four virtual motors, thrust + torque, not a single force-and-lerp hack), tunable via a ScriptableObject config.

## Tasks

1. Create a `DroneConfig` ScriptableObject (in `Assets/_Project/Scripts/Flight/` with instances in `Assets/_Project/ScriptableObjects/DroneConfigs/`) exposing tunable parameters: mass, max thrust per motor, motor response/lag, drag coefficients (linear + angular), max tilt angle, battery capacity and discharge curve, and self-level vs. acro flight mode flag.
2. Create the first `DroneConfig` asset for the consumer-class drone described in the README (GPS-assisted/self-leveling, beginner-friendly).
3. Implement a `DroneFlightController` (or `QuadcopterPhysics`) MonoBehaviour in `Assets/_Project/Scripts/Flight/` that:
   - Requires a `Rigidbody` on the drone GameObject.
   - Models four motors in an "X" configuration; computes each motor's thrust from `Throttle/Yaw/Pitch/Roll` inputs via a standard quad motor-mixing formula.
   - Applies thrust as upward forces at each motor's local position (`Rigidbody.AddForceAtPosition`) so pitch/roll/yaw emerge from differential thrust, not from directly rotating the transform.
   - Applies drag and gravity realistically (don't disable Unity's built-in gravity unless intentionally replacing it).
   - Implements self-leveling behavior for this drone's flight mode (auto-return to level attitude when pitch/roll input is neutral), matching the "GPS-assisted" consumer drone class from the README.
   - Reads its tunable parameters from the assigned `DroneConfig` asset — no magic numbers hardcoded in the controller.
   - Consumes input from the `DroneInputController` built in step 2.
4. Implement a simple battery drain model: battery percentage decreases over time as a function of throttle usage, using the discharge curve from `DroneConfig`. No low-battery behavior/failsafe needed yet beyond exposing the current percentage (HUD in step 6 will display it).
5. Build a temporary test scene (or reuse `Sandbox.unity` with placeholder geometry) with a flat ground plane and the drone prefab, to manually test: hover stability, response to each of the four inputs independently, and that self-leveling actually returns the drone to level attitude.
6. Save the drone as a prefab at `Assets/_Project/Prefabs/Drones/ConsumerDrone.prefab`.

## Acceptance Criteria

- Drone hovers stably at a constant altitude when throttle input holds it at equilibrium — no oscillation or drift beyond minor, physically-plausible settling.
- Pitch, roll, and yaw each respond correctly and independently to their respective inputs, with attitude driven by differential motor thrust, not by directly setting `transform.rotation`.
- Releasing pitch/roll input returns the drone toward level attitude (self-leveling working).
- All flight-relevant numbers (mass, thrust, drag, max tilt, battery curve) live in the `DroneConfig` asset, not hardcoded — changing the asset visibly changes flight behavior without touching code.
- Battery percentage decreases during flight and is readable from another script (for step 6's HUD).
- No camera or environment art is required to pass this step — a grey box scene is sufficient.

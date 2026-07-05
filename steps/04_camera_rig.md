# Step 4 — FPV Camera Rig

## Context

This is step 4 of the "First Flight" vertical slice for DRONE_SIM. Step 3 produced a `DroneFlightController` on a `ConsumerDrone.prefab` that flies correctly using rigidbody physics. Up to now testing has likely used the Scene view or a generic follow camera. This step gives the player an actual first-person view from the drone, which is central to the "FPV" in FPV drone simulator.

## Goal

Build an FPV camera rig attached to the drone that renders what the pilot would see, with a configurable field of view and camera tilt angle, as described in the README's Camera System notes. This is a body-mounted FPV cam (not the stabilized gimbal cam — that's for the photography slice, not this one, though the code should be structured so a gimbal cam can be added later without a rewrite).

## Tasks

1. Create an `FpvCameraRig` prefab/script in `Assets/_Project/Scripts/Camera/` that:
   - Parents a `Camera` (or Cinemachine virtual camera, using the Cinemachine package installed in step 1) to the drone body at a configurable local offset/angle representing the FPV camera mount point.
   - Exposes a configurable **FOV** (field of view) and **camera tilt angle** (the fixed forward/down tilt typical of FPV cams) as inspector fields.
   - Rotates/moves rigidly with the drone body (this is the "body cam," not stabilized) — validate that fast rotations from step 3's flight controller translate directly to camera movement.
2. Attach the `FpvCameraRig` to `ConsumerDrone.prefab` from step 3.
3. Add a basic "chase cam" or "orbit cam" as a secondary, togglable camera (not the default) for debugging/dev use — useful when testing flight physics visually, switchable via a temporary debug key binding.
4. Wire camera switching (FPV vs. chase) through a simple `CameraModeController` so it's not hardcoded to one camera being active.
5. Sanity-check comfort: confirm FOV and tilt defaults are close to real FPV drone conventions (roughly 130-150° FOV is typical for actual FPV cams) and leave this tunable rather than fixed, since it will need play-testing.

## Acceptance Criteria

- Player can fly the drone (using step 2/3's input and physics) entirely from the FPV camera's point of view.
- FOV and tilt angle are inspector-exposed and changing them visibly changes the rendered view without code changes.
- A secondary debug/chase camera exists and can be toggled at runtime.
- Camera rig is structured (e.g., via an interface or clearly separated component) so that a future stabilized gimbal camera variant can be added later without restructuring the drone prefab.

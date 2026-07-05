# "First Flight" Vertical Slice — Step Prompts

This folder contains a sequence of self-contained prompts for building vertical slice **#1: "First Flight" — Take-off/Landing Trainer** (see [../VERTICAL_SLICE.md](../VERTICAL_SLICE.md) and [../README.md](../README.md)).

Each file is a standalone prompt that can be handed to an agent (or worked through manually) one at a time, in order. Later steps assume earlier steps are complete, but each prompt restates enough context to be picked up cold.

## Slice Recap

One consumer-class drone, one backyard/suburban sandbox environment. Loop: spawn on pad → take off → hover → fly to 2-3 marked landing zones → land with a precision score. Goal is to validate that the physics-based flight model feels good and that the core input/camera/HUD/scoring loop works end-to-end, before investing in any other mode or environment.

## Step Sequence

1. [01_project_setup.md](01_project_setup.md) — Unity project, render pipeline, packages, folder structure.
2. [02_input_system.md](02_input_system.md) — Input System action maps and device abstraction (gamepad, keyboard, real FPV radio).
3. [03_flight_physics.md](03_flight_physics.md) — Rigidbody-based motor-mixer flight controller for the consumer drone.
4. [04_camera_rig.md](04_camera_rig.md) — FPV camera rig attached to the drone.
5. [05_environment_greybox.md](05_environment_greybox.md) — Backyard/suburban sandbox greybox with takeoff pad and landing zones.
6. [06_hud_osd.md](06_hud_osd.md) — On-screen telemetry display (battery, altitude, speed, artificial horizon).
7. [07_mission_scoring.md](07_mission_scoring.md) — Mission definition data and landing-precision scoring system.
8. [08_playtest_tuning.md](08_playtest_tuning.md) — Integration pass, flight-feel tuning, bug fixing, slice sign-off.

## Definition of Done for the Slice

- Player can take off from a pad, hover stably, fly to each of 2-3 marked landing zones, and land.
- Flight is driven by real rigidbody physics (motor thrust/torque mixing), not scripted lerps.
- Input works via gamepad/keyboard and via a real FPV radio in USB joystick/HID mode.
- HUD shows battery %, altitude, speed, and an artificial horizon.
- Each landing is scored on precision (distance from target center, softness of touchdown) and the score is displayed to the player.
- The loop is replayable without restarting the app (reset/retry flow).

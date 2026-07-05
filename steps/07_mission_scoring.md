# Step 7 — Mission Definition & Landing Scoring

## Context

This is step 7 of the "First Flight" vertical slice for DRONE_SIM. By this point the environment (step 5) has a takeoff pad and 2-3 `LandingZone` components, the drone flies and tracks battery (step 3), the camera and HUD are working (steps 4/6). What's still missing is the actual "training mode" structure: a defined mission (take off, visit zones, land) and a scoring system that grades landing precision, matching the README's data-driven Mission/Objective System and Scoring/Grading System.

## Goal

Implement a data-driven mission definition for the take-off/landing training mode, and a scoring system that grades each landing attempt on precision (distance from target center) and softness (touchdown velocity), displaying results to the player.

## Tasks

1. Create a `MissionDefinition` ScriptableObject (in `Assets/_Project/Scripts/Missions/`, instances in `Assets/_Project/ScriptableObjects/MissionDefinitions/`) that references an ordered or unordered list of `LandingZone`s (from step 5) to be visited/landed on, plus basic metadata (mission name, description).
2. Create a `FirstFlightTraining` `MissionDefinition` asset referencing the 2-3 landing zones built in step 5.
3. Implement a `MissionController` (`Assets/_Project/Scripts/Missions/`) that:
   - Loads the active `MissionDefinition` at scene start.
   - Tracks drone proximity to each `LandingZone` and detects a "landing attempt" (drone touches down within/near a zone — define a reasonable detection radius and a low-velocity threshold to count as "landed" vs. "flew over").
   - On each landing attempt, computes a **precision score** (e.g., based on horizontal distance from the zone's target center relative to its radius) and a **softness score** (based on vertical touchdown speed at contact) and combines them into a per-zone score.
   - Tracks which zones have been successfully landed on and whether the mission (all zones visited) is complete.
4. Implement a simple results/feedback UI (can extend the HUD Canvas from step 6, or a separate results panel) that displays the score immediately after each landing attempt (e.g., "Zone B: 82/100 — Precision: Good, Softness: Excellent") and an overall mission-complete summary once all zones are done.
5. Implement a **reset/retry flow**: a bound input (reuse the `Reset` action from step 2) that returns the drone to the takeoff pad and clears/restarts the current mission attempt without requiring an app restart.
6. Keep scoring formulas and thresholds as tunable fields on the `MissionController` or `MissionDefinition` (not hardcoded magic numbers), so difficulty/grading can be balanced without code changes.

## Acceptance Criteria

- A `MissionDefinition` asset exists referencing the sandbox's landing zones, and a `MissionController` drives the mission loop using it — no zone/mission logic is hardcoded to specific scene objects by name.
- Landing at each zone produces a visible precision + softness score immediately after touchdown.
- Completing all zones in a mission shows a mission-complete summary.
- Pressing the reset input at any time returns the drone to the takeoff pad and restarts the mission attempt cleanly, without needing to stop/restart Play mode.
- Scoring thresholds/weights are exposed as tunable data, not hardcoded constants buried in logic.

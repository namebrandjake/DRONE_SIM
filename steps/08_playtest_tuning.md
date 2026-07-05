# Step 8 — Integration Pass, Tuning & Slice Sign-off

## Context

This is the final step of the "First Flight" vertical slice for DRONE_SIM. Steps 1-7 have independently produced: a configured Unity project, cross-device input, a physics-based flight controller, an FPV camera rig, a greybox sandbox environment with landing zones, an HUD/OSD, and a mission/scoring system. Individually each system may work, but they haven't been evaluated together as one continuous play experience. This step is about integration, playtesting, and tuning — not new features.

## Goal

Take the full "First Flight" loop (spawn → take off → hover → fly to each landing zone → land → get scored → reset/retry) from "technically works" to "feels good," and confirm it meets the slice's Definition of Done in [00_overview.md](00_overview.md).

## Tasks

1. Play through the full loop repeatedly across all input methods built in step 2 (gamepad, keyboard, and a real FPV radio in joystick mode if hardware is available) and note any input-specific issues (dead zones, inverted axes, sensitivity mismatches).
2. Tune `DroneConfig` (step 3) flight parameters — thrust, drag, self-level responsiveness, max tilt — based on actual play feel, not just "it doesn't crash." Aim for stable, predictable hover and smooth, controllable transitions to each landing zone.
3. Tune camera FOV/tilt (step 4) based on how flying actually feels in FPV view — adjust if the player has trouble judging distance/altitude to landing zones.
4. Tune landing zone radii and scoring thresholds/weights (steps 5 and 7) so that: the easy zone is genuinely easy, the hard zone is genuinely hard but achievable, and scores meaningfully differentiate a good landing from a sloppy one.
5. Fix integration bugs surfaced by end-to-end play — e.g., HUD not updating after reset, mission state not clearing correctly, camera clipping through geometry, collisions behaving oddly against greybox meshes.
6. Verify performance (stable frame rate) in the sandbox scene with all systems active together.
7. Run through the full Definition of Done checklist in [00_overview.md](00_overview.md) and confirm each item explicitly; note and fix any gaps.
8. Capture a short recording or set of screenshots of a full successful playthrough (take off → land on all zones → mission complete) as evidence of the completed slice, for use in future pitches/reviews.

## Acceptance Criteria

- A single continuous playthrough — take off, visit and land on every zone, see scores, see mission complete, reset, and do it again — works without manual intervention, restarts, or console errors.
- Flight feel is judged "good" by at least one real playtest pass (not just "functions"), with tuning changes reflected in the `DroneConfig` and camera settings.
- All items in the [00_overview.md](00_overview.md) Definition of Done are satisfied.
- Known issues that aren't fixed are explicitly listed (not silently dropped) so they can be triaged before moving to the next vertical slice or mode.

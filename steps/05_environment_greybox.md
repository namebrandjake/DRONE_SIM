# Step 5 — Sandbox Environment Greybox

## Context

This is step 5 of the "First Flight" vertical slice for DRONE_SIM. Steps 1-4 produced a Unity project with a flyable, physics-driven drone (`ConsumerDrone.prefab`) with a working FPV camera. All testing so far has happened in a flat, placeholder test area. This step builds the actual environment described in the slice: a backyard/suburban sandbox with a takeoff pad and 2-3 marked landing zones.

This is a greybox/blockout pass — final art, textures, and set dressing are explicitly out of scope for the vertical slice. Use ProBuilder (installed in step 1) for fast blockout geometry.

## Goal

Build the `Sandbox.unity` scene into a small, bounded backyard/suburban area containing one takeoff pad and 2-3 distinct landing zones at varying difficulty (flat ground, small rooftop, tighter/elevated pad), matching the README's environment list ("Suburban/backyard sandbox (tutorial area)").

## Tasks

1. In `Assets/_Project/Scenes/Sandbox/Sandbox.unity`, block out a small bounded area (suggest roughly 50m x 50m) representing a backyard/suburban setting: ground plane, a simple house/shed shape or two for visual reference and as a light obstacle, a fence or boundary marker so the player knows the play area limits.
2. Place one **takeoff pad** (visually distinct, e.g., a marked circle) as the drone's spawn point.
3. Place **2-3 landing zones** at increasing difficulty, e.g.:
   - Zone A: flat open ground, generous radius (easy).
   - Zone B: small elevated platform/rooftop (medium — requires precise altitude control).
   - Zone C: smaller/tighter pad, possibly partially obstructed (hard).
4. Each landing zone should have a clear visual marker (color-coded circle/decal) and a defined "target center" + acceptable radius, exposed via a simple `LandingZone` component (position + radius + difficulty label) — this will be consumed by step 7's scoring system, so keep the component's public API simple and stable.
5. Add basic environment collision (colliders on ground, house shapes, fences, platform edges) so the drone in step 3 can actually collide with/land on these surfaces realistically.
6. Set dressing should stay minimal/greybox (ProBuilder primitives, basic materials) — do not invest in final art for this slice.
7. Verify the drone prefab spawns correctly on the takeoff pad and that all landing zones are reachable within reasonable flight time/battery (per step 3's battery model).

## Acceptance Criteria

- `Sandbox.unity` contains a bounded backyard/suburban greybox environment with a clearly marked takeoff pad and 2-3 clearly marked, difficulty-varied landing zones.
- Each landing zone exposes a `LandingZone` component with target center, acceptable radius, and difficulty label, ready to be read by the scoring system in step 7.
- Drone spawns on the takeoff pad at scene start and can physically reach and land on/at every zone.
- Colliders are correctly placed so the drone interacts physically with ground, structures, and platforms (no falling through geometry, no invisible walls blocking intended flight paths).
- No final art/textures required — greybox quality is acceptable for this step.

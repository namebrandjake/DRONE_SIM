# Step 6 — HUD / OSD Telemetry

## Context

This is step 6 of the "First Flight" vertical slice for DRONE_SIM. At this point: the drone flies with real physics (step 3), is controllable via gamepad/keyboard/FPV radio (step 2), is viewed through an FPV camera (step 4), and has a greybox environment with a takeoff pad and landing zones to fly to (step 5). The player currently has no in-game feedback about battery, altitude, speed, or attitude. This step adds the on-screen display (OSD) described in the README's UI/HUD system, mimicking real FPV goggle telemetry.

## Goal

Build a screen-space HUD overlay, rendered on top of the FPV camera view, showing the core telemetry a real FPV pilot would see: battery %, altitude, speed, and an artificial horizon. GPS lock/signal-strength indicators are mentioned in the README but are stretch/cosmetic for this slice since there's no real GPS/signal simulation yet — a static or placeholder indicator is acceptable.

## Tasks

1. Build a UI Canvas (screen space - overlay, or screen space - camera targeting the FPV camera from step 4) at `Assets/_Project/Prefabs/UI/` containing:
   - **Battery %** readout, reading live from `DroneFlightController`'s battery value (step 3).
   - **Altitude** readout (height above ground/start point — raycast down or use world Y relative to takeoff pad).
   - **Speed** readout (derived from the drone rigidbody's velocity magnitude).
   - **Artificial horizon** — a simple pitch/roll ladder or horizon line widget that visually reflects the drone's current attitude (can be a basic rotating/translating UI element, doesn't need to be broadcast-quality).
2. Implement a `HudController` script in `Assets/_Project/Scripts/UI/` that pulls live values each frame from the `DroneFlightController` (and rigidbody) and updates the UI elements — keep this read-only/one-way (HUD never writes back into flight state).
3. Style the HUD to visually read as an FPV goggle OSD (monospace/digital-style font, minimal color palette, corner-anchored elements) rather than a generic game UI — this doesn't need final art polish, but should be recognizably "FPV OSD" in layout intent.
4. Add a placeholder/static GPS lock and signal-strength indicator (can be hardcoded to "locked"/full bars for this slice) so the layout matches the README's described OSD and can be wired to real data later without a layout change.
5. Confirm the HUD renders correctly at the target resolution(s) and doesn't obstruct critical FPV view (avoid covering the center of the frame).

## Acceptance Criteria

- While flying in FPV camera view, the player can see live battery %, altitude, speed, and an artificial horizon that responds to the drone's actual attitude/motion in real time.
- HUD values are driven by live data from the flight controller/rigidbody, not placeholder/static numbers (except the explicitly-allowed GPS/signal placeholder).
- HUD is legible and doesn't obscure the flight path/landing zones in normal play.
- No changes required to step 3's flight controller other than exposing read-only accessors if not already public.

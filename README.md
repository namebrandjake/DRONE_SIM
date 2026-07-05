# DRONE_SIM

A realistic FPV (First-Person View) drone flight simulator built in **Unity**. Fly, train, and film — from backyard freestyle to commercial inspection contracts — without risking real hardware.

## Background

When I was a child, I loved drones and all things that fly. I wanted to be a bird when I was 7. Eventually I would realize I identify with the drone community — I am an Apache helicopter.

## Objective

Build a drone simulator that gives the user a genuine FPV drone experience. The simulator lets players fly virtually in a videogame-like environment, practice flight skills risk-free, and train before ever touching real hardware. It should feel equally at home as:

- A **freestyle/FPV arcade experience** for fun and skill-building.
- A **training tool** for real-world flight proficiency (manual mode, acro, GPS-assisted).
- A **commercial simulation** for photography, marketing, and infrastructure inspection work.

---

## Game Modes

| Mode | Description |
|---|---|
| **Free Flight / Sandbox** | Open-world flying with no objectives — pure practice and exploration. |
| **Take-off / Landing Training** | Guided drills focused on stable takeoff, hover, and precision landing. |
| **Precision Landing Challenges** | Land on constrained or moving targets (rooftops, helipads, boats, moving vehicles). |
| **City Flight / Urban Navigation** | Navigate dense urban environments, gaps between buildings, traffic, and no-fly zones. |
| **Commercial Photography Simulator** | Fly a scripted or freeform shoot to capture marketing photos/video of a virtual local business. Scored on framing, stability, and coverage. |
| **Industrial Inspection Simulator** | Fly close-proximity inspection routes on infrastructure (cell towers, solar farms, bridges, rooftops, pipelines) to identify flagged defects before repair/maintenance approval. |
| **Race / Freestyle Mode** | FPV gate racing and freestyle tricks on custom or community-built tracks. |
| **Career / Contracts Mode** | Progression system where completing photography/inspection/training jobs unlocks new drones, locations, and equipment. |

## Core Pillars

1. **Authentic Flight Feel** — physics-based flight model (not arcade lerp-to-target), tunable per drone class.
2. **Real Hardware Fidelity** — drone roster and specs modeled after real, commercially available drones.
3. **Purposeful Play** — every mode ties back to either fun, training value, or commercial simulation utility.
4. **Progression & Risk-Free Practice** — mistakes cost nothing but in-sim "reputation"/currency, encouraging experimentation.

---

## Drone Roster (Target)

Modeled after real commercial and consumer drone classes (names generalized/genericized to avoid trademark issues in-game, but recognizable):

- **Consumer Camera Drones** — DJI Mini/Air/Mavic-class: GPS-assisted, obstacle avoidance, gimbal-stabilized camera, beginner-friendly flight modes.
- **Prosumer/Enterprise Inspection Drones** — DJI Enterprise/Matrice-class and Skydio-class (US-based): thermal/zoom camera payloads, autonomous waypoint/obstacle-avoidance flight, used for infrastructure inspection missions.
- **FPV Freestyle/Racing Drones** — fully manual acro flight, high-speed, no assisted modes, used for racing and freestyle content.
- **US-Based Commercial/Defense-Adjacent Drones** — e.g., Skydio, BRINC, Anzu Robotics-class options, representing the modern "American-made" commercial drone segment (for players/customers who want non-DJI options in-sim, mirroring real market dynamics like NDAA compliance concerns).

Each drone should expose tunable parameters: mass, thrust curve, drag, max tilt angle, battery capacity/discharge curve, camera FOV/gimbal limits, GPS vs. manual flight modes, and obstacle-avoidance sensor simulation.

## Environments

- Suburban/backyard sandbox (tutorial area)
- Dense downtown city (skyscrapers, streets, traffic, tight gaps)
- Local business exteriors (storefronts, parking lots — for the photography mode)
- Industrial/infrastructure sites (cell tower, solar farm, bridge, rooftop HVAC, pipeline corridor)
- Open terrain / mountains (long-range and racing)
- Indoor/warehouse (precision flight, GPS-denied practice)

---

## Technical Architecture (Unity)

### Engine & Version
- **Unity LTS** (2022 LTS or newer, evaluate Unity 6 LTS at project start).
- **Render Pipeline:** URP (Universal Render Pipeline) for scalable performance across desktop/VR; HDRP evaluated only if photorealism becomes a hard requirement and target hardware supports it.
- **Input System:** Unity's new Input System package — required to support FPV transmitter-style USB/Bluetooth controllers (e.g., real FPV radios like TBS/RadioMaster via USB joystick mode), gamepads, and keyboard/mouse fallback.

### Core Systems

| System | Notes |
|---|---|
| **Flight Physics** | Custom rigidbody-based quadcopter physics (thrust per motor, torque/yaw from motor differential, drag, simulated battery sag). Avoid relying on Unity's default `Rigidbody` drag alone — implement a proper motor-mixer model (standard quad "X" or "+" mixing). |
| **Input/Controller Layer** | Abstracted input mapping so throttle/yaw/pitch/roll can come from gamepad, keyboard, or a real FPV radio in joystick mode. Support rate/acro mode and self-leveling/GPS-assist mode per drone class. |
| **Camera System** | FPV camera rig with configurable FOV, camera tilt angle, simulated latency/motion blur toggle, plus a "gimbal cam" mode for photography/inspection drones (stabilized, independent of body attitude). |
| **World/Level Streaming** | Modular scene loading (Addressables) so city, business, and industrial environments load independently for fast iteration and smaller build sizes. |
| **Mission/Objective System** | Data-driven mission definitions (ScriptableObjects) for take-off/landing drills, inspection checklists, and photography shot lists — decouples content design from code. |
| **Scoring/Grading** | Per-mode scoring: landing precision (distance from target, softness of touchdown), photography (framing/exposure/coverage checklist), inspection (defects found vs. missed, battery/time efficiency). |
| **Damage/Failure Model** | Optional realism layer: collisions cause visual damage, motor/prop failure, or forced landing — toggleable for training vs. arcade play. |
| **Weather/Wind System** | Wind vector + gust simulation affecting flight physics; time-of-day and weather presets impacting visibility and difficulty. |
| **UI/HUD** | OSD (on-screen display) mimicking real FPV goggles telemetry: battery %, signal strength, altitude, speed, GPS lock, artificial horizon. |
| **Progression/Save System** | Player profile tracking unlocked drones, completed contracts, best scores, in-sim currency/reputation. |
| **Audio** | Motor pitch tied to throttle/RPM, wind noise scaling with speed, environment ambience, collision/impact SFX. |

### Suggested Project Structure

```
Assets/
  _Project/
    Art/
    Audio/
    Prefabs/
      Drones/
      Environments/
      UI/
    Scenes/
      Sandbox/
      City/
      Commercial/
      Industrial/
      Racing/
    Scripts/
      Flight/          # motor mixer, rigidbody physics, drone controller
      Input/           # input system bindings, device abstraction
      Camera/          # FPV cam rig, gimbal cam
      Missions/         # ScriptableObject-driven objectives & scoring
      UI/               # HUD/OSD, menus
      Progression/      # save data, unlocks, currency
      Environment/      # wind/weather, streaming
    ScriptableObjects/
      DroneConfigs/
      MissionDefinitions/
    Settings/            # URP assets, input action maps
  Plugins/
Packages/
  manifest.json
ProjectSettings/
```

### Key Unity Packages/Tools to Evaluate
- **Input System** (`com.unity.inputsystem`) — controller/radio support.
- **Cinemachine** — camera rigs, especially for cinematic replay/spectator cam.
- **Addressables** — environment streaming, DLC-style content drops (new drones/maps).
- **Timeline** — scripted mission intros/replays.
- **Unity Recorder** — capturing in-sim "marketing photos" output for the photography mode.
- **ProBuilder** — greyboxing environments quickly during design iteration.
- **Terrain Tools** — open terrain/mountain environments.
- **XR Plugin Management** — future VR/FPV-goggle support (stretch goal).

### Multiplayer (Stretch Goal)
- Netcode for GameObjects (or Photon Fusion) for racing mode and shared inspection/training sessions.
- Leaderboards for racing and precision-landing challenges.

---

## Controller & Hardware Support

- Standard gamepad (Xbox/PlayStation) — default input.
- Keyboard/mouse fallback.
- **Real FPV transmitters** (RadioMaster, TBS, FrSky, etc.) via USB joystick/HID mode, and via SBUS-to-USB adapters — critical for the "training tool" objective since it lets pilots practice with the same sticks they fly with in real life.
- VR/FPV-goggle output support (stretch goal) for full immersion.

---

## Target Platforms

- **Primary:** Windows (Steam) — best controller/HID compatibility for FPV radios.
- **Secondary:** macOS.
- **Stretch:** VR headset support (SteamVR/OpenXR), mobile companion app for casual/photography modes.

---

## Monetization / Distribution (Consideration)

- Premium one-time purchase base app (sandbox + core modes + a handful of drones/maps).
- Optional DLC map packs and licensed-style drone packs (Addressables-based content drops).
- No pay-to-win mechanics in training-oriented modes — commercial/training credibility depends on it.

---

## Development Roadmap

### Phase 0 — Pre-production
- Finalize Unity version, render pipeline (URP), and input strategy.
- Prototype core flight physics (motor mixer + rigidbody) in a blank scene.
- Validate real FPV radio input via Input System.

### Phase 1 — Vertical Slice
- One drone (consumer class), one environment (suburban sandbox).
- Take-off/landing training mode fully playable.
- Basic HUD/OSD.

### Phase 2 — Content Expansion
- Add city environment + urban navigation mode.
- Add FPV racing drone class + freestyle/race mode.
- Add scoring/grading system.

### Phase 3 — Commercial Simulation Modes
- Commercial photography mode (local business environment + shot-list scoring).
- Industrial inspection mode (infrastructure environment + defect-detection scoring).
- Progression/contracts system tying modes together.

### Phase 4 — Polish & Platform Expansion
- Weather/wind system, damage model, audio pass.
- Additional drone roster (enterprise/US-based commercial drones).
- Steam release; evaluate VR support.

---

## Open Questions / Design Decisions Needed

- How "sim" vs. "arcade" should default settings be — and should difficulty presets bridge the two?
- Should real drone brand names/likenesses be used, or should the roster stay genericized to avoid licensing issues?
- Is multiplayer racing a launch feature or a post-launch addition?
- Target minimum spec hardware — does URP fidelity need to scale down for lower-end PCs?

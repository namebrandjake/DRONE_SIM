# Step 1 — Project Setup

## Context

This is the first step of the "First Flight" vertical slice for DRONE_SIM, a Unity FPV drone simulator (see repo README.md). No Unity project exists yet in this repo — only a `.gitignore`, `LICENSE`, and design docs. This step creates the actual Unity project and its baseline structure so later steps (flight physics, input, camera, environment, HUD, scoring) have somewhere to live.

## Goal

Stand up a clean, empty Unity project configured with the right render pipeline and packages, and lay down the folder structure described in the README so subsequent steps drop code/assets into predictable locations.

## Tasks

1. Create a new Unity project using the latest Unity LTS release (2022 LTS or newer; check whether Unity 6 LTS is available and prefer it if stable).
2. Configure the project to use **URP (Universal Render Pipeline)**.
3. Install and verify the following packages via Package Manager:
   - Input System (`com.unity.inputsystem`)
   - Cinemachine
   - Addressables
   - ProBuilder
4. Create the folder structure under `Assets/_Project/` as specified in the README:
   ```
   Assets/_Project/
     Art/
     Audio/
     Prefabs/Drones/
     Prefabs/Environments/
     Prefabs/UI/
     Scenes/Sandbox/
     Scripts/Flight/
     Scripts/Input/
     Scripts/Camera/
     Scripts/Missions/
     Scripts/UI/
     Scripts/Progression/
     Scripts/Environment/
     ScriptableObjects/DroneConfigs/
     ScriptableObjects/MissionDefinitions/
     Settings/
   ```
5. Create a single empty scene at `Assets/_Project/Scenes/Sandbox/Sandbox.unity` and set it as the project's default startup scene.
6. Verify the project opens, builds, and runs (empty scene, no errors/warnings in the console) on the target dev platform (Windows).
7. Commit the Unity project to the repo, confirming the existing `.gitignore` correctly excludes `Library/`, `Temp/`, `Obj/`, `Build/`, `Logs/`, `UserSettings/`.

## Acceptance Criteria

- Project opens in Unity Editor with URP active and no console errors.
- Input System, Cinemachine, Addressables, and ProBuilder packages are installed and show no errors.
- Folder structure matches the layout above.
- `Sandbox.unity` scene exists, is empty, and is set as the startup scene.
- A build (development build) completes successfully with no scenes-in-build errors.
- `git status` shows only source-relevant files staged (no `Library/`, `Temp/`, or other generated folders).

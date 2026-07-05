# Vertical Slice Candidates

Options for a first vertical slice of DRONE_SIM, ordered from safest/smallest to most ambitious. A vertical slice should prove the core flight feel plus one full mode loop end-to-end, not spread thin across content.

## 1. "First Flight" — Take-off/Landing Trainer

- **Drone:** One consumer class drone.
- **Environment:** Backyard/suburban sandbox.
- **Loop:** Spawn on pad → take off → hover → fly to 2-3 marked landing zones → land with a precision score.
- **Proves:** Motor-mixer flight physics, HUD/OSD, basic scoring, input abstraction (gamepad + real FPV radio).
- **Risk:** Lowest — smallest environment, no AI, no mission scripting complexity. Best pick to validate flight *feel* before committing to anything else.

## 2. "The Client Job" — Commercial Photography Contract

- **Drone:** Consumer/gimbal drone.
- **Environment:** Single storefront + parking lot.
- **Loop:** Accept a contract → fly a shot list (3-4 required angles) → gimbal-stabilized camera capture → get graded on framing/coverage → payout/reputation.
- **Proves:** Mission/objective system (ScriptableObjects), gimbal camera mode, scoring beyond "did you land," and the progression hook (contracts → currency) that differentiates this from a generic flight sim.
- **Risk:** More ambitious than #1 but demonstrates the "purposeful play" pillar central to the pitch.

## 3. "One Block" — Urban Navigation

- **Drone:** FPV/racing class, manual acro control only.
- **Environment:** A single dense city block (not the whole city).
- **Loop:** Fly a gated path between buildings within a time limit.
- **Proves:** Tighter flight tuning (acro mode), collision/damage toggle, denser greybox environment performance.
- **Risk:** Good if the racing/freestyle pillar is the priority, but city geometry is more art/level-design cost than #1 or #2.

## 4. "Rooftop Inspection" — Industrial Slice

- **Drone:** Enterprise class.
- **Environment:** One small structure (single cell tower or rooftop HVAC unit).
- **Loop:** Fly a waypoint-guided inspection path, spot 2-3 flagged defects, file a report.
- **Proves:** Waypoint/semi-autonomous flight mode and the inspection scoring model.
- **Risk:** Least "fun" to build first since it leans on UI/checklist logic more than flight feel.

## Recommendation

Start with **#1 ("First Flight")** to validate whether the physics-based flight model actually feels good — that's the pillar everything else depends on. Then fold in **#2 ("The Client Job")** once flight feel is solid, since it exercises the mission/scoring/progression systems that get reused across every other mode.

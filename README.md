# RTS Game (Unity 6)

A sci-fi **real-time strategy (RTS)** game prototype built with **Unity 6** and the **Universal Render Pipeline (URP)**. It implements the core systems of a classic RTS: an RTS camera, unit selection, NavMesh movement with formations, and an event-driven architecture that later features (economy, buildings, combat) plug into.

> 🚧 **Work in progress.** Camera, selection and movement are playable. Buildings, resource gathering and combat have their models, animations and UI prefabs in place and are being wired up.

---

## Features

### Implemented
- **RTS camera** (Cinemachine 3)
  - Pan with arrow keys or by moving the mouse to the screen edge
  - Smooth zoom and orbit-style rotation
  - All speeds and limits set in the Inspector through a `CameraConfig` class
- **Unit selection**
  - Click to select a unit, or **drag a selection box** to select several
  - Hold **Shift** to add to the current selection
  - Selected units are highlighted with a URP **decal projector**
- **Movement and formations**
  - Right-click the ground to move selected units with a **NavMeshAgent**
  - Units spread out in **concentric rings** around the target point, spaced by agent radius, so they don't all fight for the same spot
- **Event bus architecture**
  - A generic, type-safe `Bus<T>` for publishing and subscribing to game events (`UnitSpawnEvent`, `UnitSelectedEvent`, `UnitDeselectedEvent`)
  - Units and the player controller never reference each other directly
- **Character animation**
  - Animator controllers for walk, run, idle, mining, shooting and throwing
  - `HoldGunIK`: **inverse kinematics** that keeps both hands (and elbows) attached to the weapon
- **Custom Shader Graph shaders:** toon shading, water, scrolling UVs, screen-space stripes, and a "ghost" shader for previewing building placement

### Planned (assets already in the project)
| Area | Content |
|---|---|
| Units | Worker, Rifleman, Grenadier, Air Transport (dropship) |
| Buildings | Command Post, Barracks, Infantry School, Supply Hut, Airport |
| Resources | Minerals, Gas, Poison Gas |
| UI | Action buttons, build queue, control groups, progress bars, armor/damage icons |

---

## Controls

| Action | Input |
|---|---|
| Select unit | Left click |
| Box select | Left click + drag |
| Add to selection | Shift + click / drag |
| Move selected units | Right click on the ground |
| Pan camera | Arrow keys, or mouse at the screen edge |
| Rotate camera | Page Up / Page Down |
| Zoom in | Hold End |

---

## Architecture

```
Assets/Scripts/
├── EventBus/
│   ├── IEvent.cs              # marker interface for events
│   └── Bus.cs                 # generic static event bus: Bus<T>.Raise / Bus<T>.OnEvent
├── Events/                    # UnitSpawnEvent, UnitSelectedEvent, UnitDeselectedEvent
├── Player/
│   ├── PlayerInput.cs         # camera, selection box, click selection, move commands
│   └── CameraConfig.cs        # Inspector-tunable camera settings
└── Units/
    ├── ISelectable.cs         # Select() / Deselect()
    ├── IMoveable.cs           # MoveTo(Vector3)
    ├── AbstractUnit.cs        # base unit: NavMeshAgent + selection decal + events
    ├── Worker.cs
    └── HoldGunIK.cs           # weapon-holding IK
```

**How the pieces fit together:**
1. When a unit spawns, `AbstractUnit` raises a `UnitSpawnEvent`. `PlayerInput` listens and keeps a list of living units, so it never has to search the scene.
2. Selection works through the `ISelectable` interface. Selecting a unit raises `UnitSelectedEvent`, which any system (for example the UI) can react to.
3. Move commands go through `IMoveable`, so any new unit type that inherits from `AbstractUnit` can be selected and moved without extra code.

---

## Tech stack
- **Unity 6** (6000.4.9f1), **C#**
- **Universal Render Pipeline** + Shader Graph
- **Cinemachine 3**: camera
- **Input System**: mouse and keyboard input
- **AI Navigation**: NavMesh pathfinding
- **Behavior** package: planned for unit AI

---

## Getting started
1. Install **Unity 6000.4.9f1** (or a newer Unity 6 version) through Unity Hub.
2. Clone the repository:
   ```bash
   git clone https://github.com/behradbina/rts_game.git
   ```
3. In Unity Hub, click **Add → Add project from disk** and select the folder.
4. Open `Assets/Scenes/Game.unity` and press **Play**.

---

## Roadmap
- [x] RTS camera (pan, zoom, rotate)
- [x] Click and box selection
- [x] NavMesh movement with ring formations
- [ ] Worker resource gathering (minerals, gas)
- [ ] Building placement with ghost preview
- [ ] Unit production queues
- [ ] Combat: riflemen, grenadiers, health and damage
- [ ] Control groups and in-game UI
- [ ] Enemy AI with Unity Behavior graphs

---

## Acknowledgements
Built while following the **GameDev.tv** Unity RTS course. Third-party models and textures (in `Assets/SciFi Pack` and `Assets/Units`) belong to their original authors and are covered by their own licenses.

## License
The source code is released under the MIT License. See [LICENSE](LICENSE).

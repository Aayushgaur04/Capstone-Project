# ⚔️ Capstone 2D Platformer Game

A pixel-art sci-fi platformer created in Unity. Includes puzzle-solving, enemy encounters, a boss battle, and cinematics. This was built as a capstone project showcasing core game design, programming, and polish.

---

## 🚀 Features

### 🎮 Core Gameplay
- 2D side-scrolling platformer
- Platforming + puzzle mechanics integrated into each level
- Combat system with shooting and enemy AI
- Health and collectible system

### 🧠 Puzzle Mechanics
- Activatable platforms
- Doors triggered by solving puzzles
- Progression logic across levels

### 👾 Enemies
- Chaser enemies that follow and damage the player
- Shooter enemies with directional detection
- Boss enemy with 3 unique attack patterns:
  - Laser beam (with telegraph)
  - Invulnerable sword rain
  - Tackle dash

### ⚔️ Boss Battle
- Custom health bar UI for the boss
- Cinematic intro animation
- Defeat condition triggers game completion screen

### 📊 UI & Persistence
- Persistent Player UI (health bar, collectibles) across levels
- Pause and Game Over menus with functional buttons
- GameManager handles player health and collectible data using `DontDestroyOnLoad`

### 🎥 Cinematics
- Intro cutscene played once at the game start, skippable with `Escape`

### 🔊 Audio
- Background music for levels and boss
- UI click sound support (can be extended for more SFX)

---

## 🛠️ Controls

| Action       | Key         |
|--------------|-------------|
| Move         | A / D       |
| Jump         | Space       |
| Shoot        | Left Mouse  |
| Pause        | Escape      |
| Skip Cutscene| Escape      |

---

## 🗺️ Scene Flow

1. **Cutscene Scene** → Skippable cinematic
2. **Main Menu Scene** → Start game or quit
3. **Hub Scene (Level 0)** → First playable level
4. **Level 1-4** → Puzzle + Combat levels
5. **Boss Scene** → Final boss battle
6. **Game Completion Scene** → Triggered after boss defeat

---

## 🔧 How to Run

1. Clone the repository
2. Open the project in Unity (tested in Unity 2022+)
3. Press Play from the `Cutscene` or `MainMenu` scene

---

## 📁 Project Structure

Assets/
│
├── Animations/         # All animation controllers & clips  
├── Audio/              # Background music and SFX  
├── Cinematics/         # Cutscene video and controller  
├── Enemies/            # Enemy prefabs and scripts  
├── Player/             # Player movement, health, shooting  
├── Scripts/            # Managers and core logic  
├── UI/                 # Canvas elements and UI managers  
└── Scenes/             # All Unity scenes  

## 🧑‍💻 Developed By

Group 13
| Name                | Registration Number |
|---------------------|---------------------|
| Aayush Gaur         | 21CG10048           |
| Ashish Noel Moses   | 21BCG10048          |
| Aaryan Manghnani    | 21BCG10111          |
| Vaibhav Goswami     | 21BCG10132          |

---

## 📝 License

This project is for educational and portfolio purposes only.

---

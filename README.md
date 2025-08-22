# Match-3 & Tower Defense Hybrid

A mobile game prototype combining the **resource-gathering puzzle mechanics of Match-3** with the **strategic combat of Tower Defense**.  
Players match tiles to earn coins, which are then used to build towers to defend their base against waves of enemies.  

This project was built with **Unity 2021 LTS** using the **2D URP (Universal Render Pipeline)** template.

---

## 🎮 How to Play / Controls
- **Platform:** Designed for **mobile touch input**, but also works with a **mouse** in the Unity Editor.  
- **Matching Tiles:**  
  - Click and drag (or click two adjacent tiles) to swap them.  
  - If the swap creates a line of 3+ matching tiles (horizontal/vertical), they are cleared, and you earn coins.  
- **Placing Towers:**  
  - Use coins to build towers.  
  - Click an empty **Tower Slot** on the battlefield to place a tower.  
  - Towers automatically attack enemies within range.

---

## ✨ Features Implemented

### Puzzle System (Match-3)
- 9x6 tile grid generated at game start.  
- Multiple tile types; initial board ensures **no pre-matches**.  
- Valid swaps only — if no match is created, tiles revert.  
- Matches clear tiles, drop new ones from above, and trigger **chain reactions**.  
- Matches reward coins for tower building.  

### Tower Defense System
- Predefined **enemy path** via waypoints.  
- One enemy type spawns and moves toward the base.  
- One tower type available, placed on **predefined slots**.  
- Towers automatically find the nearest enemy and fire projectiles.  

### Core Gameplay Loop
- **Resource Connection:** Coins from Match-3 are the only tower-building resource.  
- **UI / HUD:** Displays coins, base health, and wave number.  
- **Game Flow:**  
  - Main Menu → Gameplay Scene  
  - Survive waves of enemies.  
  - Game Over when base health = 0.  
  - Victory when all waves are cleared.  

---

## ⚡ Optimizations and Design Choices
- **Mobile UI Scaling:**  
  Used *Canvas Scaler* set to `Scale With Screen Size` for responsive UI across devices.  

- **Efficient Target Acquisition:**  
  Instead of checking every frame, towers use `InvokeRepeating` (twice per second) to find targets, reducing CPU load.  

- **Object Pooling (Future Consideration):**  
  For larger-scale projects, object pooling for projectiles/enemies would replace frequent instantiation/destruction to minimize garbage collection and improve performance.  

---

## 🛠️ Tech Stack
- **Engine:** Unity 2021 LTS  
- **Rendering:** 2D URP (Universal Render Pipeline)  
- **Language:** C#  
- **Platform:** Mobile-first, playable in Editor with mouse  

---

## 🚀 Future Improvements
- Multiple enemy and tower types  
- Object pooling system  
- Power-ups and special tiles in Match-3  
- Procedural wave generation  
- Expanded UI/UX for better player feedback  

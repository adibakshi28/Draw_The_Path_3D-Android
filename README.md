# Draw The Path 3D

Draw The Path 3D is a strategic puzzle game where players must draw paths to navigate a robot through a series of challenging levels filled with obstacles and traps. Developed using the Unity engine, this repository contains all the necessary files and resources to build, run, and modify the game.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Gameplay](#gameplay)
- [Controls](#controls)
- [Scripts Overview](#scripts-overview)
  - [GameController.cs](#gamecontrollercs)
  - [LevelController.cs](#levelcontrollercs)
  - [PathDrawer.cs](#pathdrawercs)
  - [ObstacleController.cs](#obstaclecontrollercs)
  - [UIController.cs](#uicontrollercs)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Introduction

Draw The Path 3D is an engaging puzzle game where players must draw paths to guide a robot safely through various obstacles. The game is designed with Unity, providing a smooth and immersive experience for Android users.

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/DP1.png" alt="Game Screenshot 1" style="width: 32%;">
  <img src="Game%20Screenshot/DP2.png" alt="Game Screenshot 2" style="width: 32%;">
  <img src="Game%20Screenshot/DP3.png" alt="Game Screenshot 3" style="width: 32%;">
</div>

## Features

- **Strategic Path Drawing:** Draw precise paths to navigate through obstacles.
- **Challenging Levels:** Increasing difficulty with each level.
- **High-Quality Graphics:** Stunning 3D graphics and animations.
- **Immersive Sound Effects:** High-quality sound effects to enhance the gaming experience.
- **Real-time Feedback:** Instant feedback on player performance.
- **Cross-Platform:** Built with Unity for easy portability.

## Installation

To set up and run the project locally, follow these steps:

### Prerequisites

- Unity Hub installed.
- Unity Editor (v5.4 recommended).
- Android SDK configured.

### Steps

1. Clone the repository:

    ```sh
    git clone https://github.com/adibakshi28/Draw_The_Path_3D-Android.git
    ```

2. Open the project in Unity:
    - Open Unity Hub.
    - Click on "Add" and select the cloned project directory.
    - Open the project.

3. Configure Build Settings for Android:
    - Navigate to `File > Build Settings`.
    - Select Android and click on `Switch Platform`.
    - Adjust player settings, including package name and version.

4. Build the Project:
    - Connect your Android device or set up an emulator.
    - Click on `Build and Run` to generate the APK and install it on the device.

## Usage

After building the project, install the APK on your Android device. Launch the game and follow the on-screen instructions to start playing. Draw paths to navigate through levels and avoid obstacles.

## Project Structure

- **Assets:** Contains all game assets, including:
    - **Scenes:** Different levels and menus.
    - **Scripts:** C# scripts for game logic.
    - **Prefabs:** Pre-configured game objects.
    - **Animations:** Animation controllers and clips.
    - **Audio:** Sound effects and music files.
    - **UI:** User interface elements.
- **Packages:** Unity packages used in the project.
- **ProjectSettings:** Project settings including input, tags, layers, and build settings.
- **.gitignore:** Specifies files and directories to be ignored by Git.
- **LICENSE:** The license under which the project is distributed.
- **README.md:** This readme file.

## Gameplay

Players must strategically draw paths to guide a robot through a series of challenging levels filled with obstacles and traps. The game includes:

- **Levels:** Each level offers different challenges and obstacles to overcome.
- **Objectives:** Draw paths to safely navigate the robot to the goal.
- **Scoring:** Points are awarded based on performance and efficiency.
- **Feedback:** Real-time feedback to help players improve.

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/DP4.png" alt="Game Screenshot 4" style="width: 32%;">
  <img src="Game%20Screenshot/DP5.png" alt="Game Screenshot 5" style="width: 32%;">
  <img src="Game%20Screenshot/DP6.png" alt="Game Screenshot 6" style="width: 32%;">
</div>

## Controls

- **Draw Paths:** Use your finger to draw paths on the screen. The robot will follow the path you draw.
- **Navigate Obstacles:** Plan your path to avoid obstacles and traps.
- **Complete Levels:** Guide the robot to the goal to complete each level.

## Scripts Overview

The `Assets/Scripts` directory contains essential C# scripts that drive the game's functionality. Here's a detailed overview:

### GameController.cs

Manages the overall game state, including game flow, starting and ending sessions, tracking player progress, and updating the UI with scores and other information.

### LevelController.cs

Handles the setup and control of individual game levels. It initializes level-specific elements, tracks progress within levels, and manages transitions between levels.

### PathDrawer.cs

Manages the drawing of paths that the robot will follow. Handles input from the player and translates it into navigable paths for the robot.

**Key Functions:**
- `StartPath()`: Initializes the path drawing process.
- `UpdatePath()`: Updates the path based on player input.
- `EndPath()`: Finalizes the path for the robot to follow.

### ObstacleController.cs

Controls the behavior of obstacles within the game, including movement, interaction with the player, and triggering game events.

**Key Functions:**
- `ActivateObstacle()`: Activates obstacle behavior.
- `DeactivateObstacle()`: Deactivates obstacle behavior.
- `CheckCollision()`: Checks for collisions with the player.

### UIController.cs

Manages the user interface, handling interactions with menus, buttons, and other UI elements.

**Key Functions:**
- `UpdateScore()`: Updates the score display.
- `ShowLevelComplete()`: Displays the level complete screen.
- `ShowGameOver()`: Displays the game over screen.

## Screenshots

Here are some screenshots of the game:

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/DP7.png" alt="Game Screenshot 7" style="width: 32%;">
  <img src="Game%20Screenshot/DP8.png" alt="Game Screenshot 8" style="width: 32%;">
  <img src="Game%20Screenshot/DP1.png" alt="Game Screenshot 1" style="width: 32%;">
</div>

## Contributing

Contributions are welcome and greatly appreciated. To contribute:

1. Fork the repository:
    - Click the "Fork" button at the top right of the repository page.

2. Create a feature branch:

    ```sh
    git checkout -b feature/AmazingFeature
    ```

3. Commit your changes:

    ```sh
    git commit -m 'Add some AmazingFeature'
    ```

4. Push to the branch:

    ```sh
    git push origin feature/AmazingFeature
    ```

5. Open a pull request:
    - Navigate to your forked repository.
    - Click on the "Pull Request" button and submit your changes for review.

## License

This project is licensed under the MIT License. See the LICENSE file for more information.

## Contact

For any inquiries or support, feel free to contact:

[Adibakshi28 - GitHub Profile](https://github.com/adibakshi28)

Project Link: [Draw The Path 3D-Android](https://github.com/adibakshi28/Draw_The_Path_3D-Android)

<table>
  <tr>
    <td align="left" width="50%">
      <img width="100%" alt="gif1" src="https://github.com/AlbertNugroho/Project-PixelGameJam/blob/main/prev1.gif">
    </td>
    <td align="right" width="50%">
      <img width="100%" alt="gif2" src="https://github.com/AlbertNugroho/Project-PixelGameJam/blob/main/prev2.gif">
    </td>
  </tr>
</table>

<p align="center">
  <img width="100%" alt="gif3" src="https://github.com/AlbertNugroho/Project-PixelGameJam/blob/main/prev3.gif">
</p>

## 📜Scripts and Features

the many stuff in the game like shooting, inventory system, enemies, Boss, health, Ammo, Crafting, Gun Attachments and so much more is thanks to tons of scripting has been implemented to the game

| Script              | Description                                                                              |
| ------------------- | ---------------------------------------------------------------------------------------- |
| `BossAI.cs`         | Responsible for the Boss's AI like grabing attack, change modes, shooting, etc           |
| `SpawnBoss.cs`      | Responsible for the Boss starting animation and triggering the bossfight                 |
| `PlayerMovement.cs` | Responsible for all the Movement,attacking,dashing and using skills                      |
| `Inventory.cs`      | Responsible for all the inventory stuff in game like drag and drop, equiping attachments |
| `Health.cs`         | Responsible for all the Health system in the game                                        |
| `EnemyAI.cs`        | Responsible for the enemy's AI like attacking, and following the player                  |
| `AmmoWorks.cs`      | Responsible for Ammo UI and General Ammo Usage                                           |
| `etc`               |                                                                                          |

<br>

## 🔴About

"Necrodemios" is a roguelike game made for Pixel Game Jam 2025, developed in 10 days. You play as NecroDeimos, forging weapons from fallen enemies to battle your way to the boss.
<br>

## 🕹️Play Game

<a href="https://skyalert.itch.io/necrodemios">Play Now</a>
<br>

## 👤Developer & Contributions

- Albert Nugroho (Game Programmer & Player, Guns & Attachment, Boss, Enemy Sprite Animator)
  <br>

## 📂Files description

```
├── Project-PixelGameJam              # Contain everything needed for Necrodemios to work.
   ├── Assets                         # Contains every assets that have been worked with unity to create the game like the scripts and the art.
      ├── Animation                   # Contains every animation clip and animator controller that played when the game start.
      ├── Materials                   # Contains all the material for the game.
      ├── Sounds                      # Contains every sound used for the game like music and sound effects.
      ├── Scripts                     # Contains all scripts needed to make the gane work like PlayerMovement scripts.
      ├── Sprites                     # Contains every sprites used in the game.
      ├── Shaders                     # Contains every shaders used in the game.
      ├── Prefab                      # Contains every Prefab, reusable game object that you can instantiate (create copies of) in your game scene.
      ├── Scenes                      # Contains all scenes that exist in the game for it to interconnected with each other like MainMenu and Game.
   ├── Packages                       # Contains game packages that responsible for managing external libraries and packages used in your project.
      ├── Manifest.json               # Contains the lists of all the packages that your project depends on and their versions.
      ├── Packages-lock.json          # Contains packages that ensuring your project always uses the same versions of all dependencies and their sub-dependencies.
   ├── Project Settings               # Contains the configuration of your game to control the quality settings, icon, or even the cursor settings
├── README.md                         # The description of Necrodemios file from About til the developers and the contribution for this game.
```

<br>

## 🕹️Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding | Function          |
| ----------- | ----------------- |
| W,A,S,D     | Standard movement |
| Esc         | Pause             |
| Tab         | Open Inventory    |
| E           | Interact          |
| Shift       | Dash              |
| Left Click  | Attack            |

<br>

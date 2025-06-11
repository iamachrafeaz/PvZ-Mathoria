# PvZ-Mathoria 🌱🧮

An educational tower defense game that combines the beloved Plants vs Zombies gameplay with mathematical learning objectives. Defend your garden while solving math problems and strengthening your numerical skills!

## 🎮 About the Game

PvZ-Mathoria is an innovative educational game that merges the strategic tower defense mechanics of Plants vs Zombies with interactive mathematics challenges. Players must solve mathematical problems to unlock plants, upgrade defenses, and progress through increasingly challenging levels.

## ✨ Features

- **Educational Gameplay**: Solve math problems to earn sun points and unlock new plants
- **Progressive Difficulty**: Mathematical challenges scale with professor configuration
- **Classic PvZ Mechanics**: Familiar plant-based tower defense gameplay
- **Math Topic**: Subtraction 
- **Achievement System**: Track progress and celebrate mathematical milestones
- **Kid-Friendly Interface**: Colorful, engaging visuals designed for young learners

## 🎯 Learning Objectives

- **Arithmetic Skills**: 
- **Problem Solving**: Critical thinking through strategic gameplay
- **Quick Mental Math**: Timed challenges to improve calculation speed
- **Mathematical Confidence**: Building positive associations with math learning

## 🚀 Getting Started

### Prerequisites

- Unity 2021.3 or higher
- Android SDK and NDK
- Firebase account
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/iamachrafeaz/PvZ-Mathoria.git
   cd PvZ-Mathoria
   ```

2. **Switch to Android Platform**:
   - Open the project in Unity
   - Go to File → Build Settings
   - Select Android and click "Switch Platform"

3. **Configure Firebase**:
   - Insert your Firebase JSON configuration file in the `assets` folder
   - Install Firebase Database SDK from: https://firebase.google.com/docs/firestore/client/libraries

4. **Setup Test Configuration and Student Data**:
   - Create basic test configuration and student data using the web platform
   - Clone and setup the web platform: https://github.com/mohamediliasskaddar/TARL-WEB-.git

5. **Build and Run**:
   - Connect your Android device or setup an emulator
   - Build and deploy to your Android device

## 🎮 How to Play

1. **Start a Level**: Choose your difficulty and math topic focus
2. **Solve Math Problems**: Answer correctly to earn sun points
3. **Plant Your Defense**: Use sun points to place plants strategically
4. **Defend Your Garden**: Prevent zombies from reaching your house
5. **Progress**: Complete levels to unlock new plants and challenges

### Controls

- **Mouse**: Click to interact with UI elements and place plants
- **Keyboard**: Type answers to mathematical problems
- **Space**: Pause/unpause the game
- **ESC**: Return to main menu

## 🌱 Plant Types

Each plant requires solving specific types of math problems to unlock:

- **Peashooter**: Basic arithmetic (addition/subtraction)
- **Sunflower**: Number patterns and sequences
- **Cherry Bomb**: Multiplication tables
- **Wall-nut**: Geometry and shapes
- **Chomper**: Advanced problem solving

## 🧟 Zombie Challenges

Different zombies present unique mathematical obstacles:

- **Basic Zombie**: Simple arithmetic problems
- **Conehead Zombie**: Multi-step problems
- **Buckethead Zombie**: Complex equations
- **Football Zombie**: Time-pressured challenges

## 🏆 Scoring System

- **Correct Answers**: +10 points per correct math problem
- **Speed Bonus**: Additional points for quick responses
- **Level Completion**: Bonus points for completing levels
- **Streak Multiplier**: Consecutive correct answers increase score

## 🛠️ Development

### Project Structure

```
PvZ-Mathoria/
├── Assets/
│   ├── Scripts/        # C# game scripts
│   ├── Scenes/         # Unity scenes
│   ├── Prefabs/        # Game prefabs
│   ├── Sprites/        # Game graphics and UI
│   ├── Audio/          # Sound effects and music
│   ├── Fonts/          # Text fonts
│   └── firebase-config.json  # Firebase configuration
├── ProjectSettings/    # Unity project settings
├── Packages/          # Unity package dependencies
└── README.md
```

### Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Setup

1. Clone the repository
2. Open the project in Unity Hub
3. Ensure you have the Android Build Support module installed
4. Set up Firebase:
   - Create a Firebase project at https://console.firebase.google.com/
   - Download the `google-services.json` file
   - Place it in the `Assets/` folder
5. Install Firebase SDK for Unity
6. Configure the web platform for data management:
   ```bash
   git clone https://github.com/mohamediliasskaddar/TARL-WEB-.git
   ```
7. Build and test on Android device

## 📚 Educational Standards

This game aligns with common core mathematics standards for:
- **Grades K-2**: Basic number operations and counting
- **Grades 3-5**: Multiplication, division, and fractions
- **Grades 6-8**: Algebra, geometry, and problem solving

## 🎨 Credits

- **Original PvZ Concept**: Inspired by PopCap Games' Plants vs Zombies
- **Educational Framework**: Developed with input from mathematics educators
- **Graphics**: [Credit your artists/sources]
- **Audio**: [Credit your sound designers/sources]

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Acknowledgments

- Plants vs Zombies by PopCap Games for the original gameplay inspiration
- Educational consultants who helped design the mathematical components
- Beta testers and the educational gaming community
- Contributors and supporters of this open-source project

## 📞 Support

- **Issues**: Report bugs and feature requests via [GitHub Issues](https://github.com/iamachrafeaz/PvZ-Mathoria/issues)
- **Discussions**: Join our community discussions for gameplay tips and educational insights
- **Contact**: Reach out to the development team for collaboration opportunities

## 🔄 Version History

- **v1.0.0**: Initial release with basic math operations and core gameplay
- **v0.9.0**: Beta version with user testing feedback incorporated
- **v0.5.0**: Alpha version with core mechanics implemented

---

**Made with ❤️ for educators, students, and lifelong learners**
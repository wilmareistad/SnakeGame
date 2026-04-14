# Snake

A classic Snake game running in the console, built in C#.

## How to play

Use the arrow keys to steer the snake. Eat the red food (✱) to grow and earn points. Don't hit the walls or yourself!

| Key | Action |
|-----|--------|
| ← → | Move left / right |
| ↑ ↓ | Move up / down |

## Rules

- The snake grows longer every time it eats food
- Hitting a wall = Game Over
- Hitting yourself = Game Over (LOSER!)
- The game gets faster the more you eat

## Scoring

1 point per food eaten. Your score is shown below the board and displayed at Game Over.

## Play again

After Game Over you'll be asked `Want to play again? Y/n`. Press `Y`, `Space` or `Enter` to restart, anything else to quit.

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download)

## Installation

```bash
git clone https://github.com/ditt-namn/snake.git
cd snake
dotnet run
```

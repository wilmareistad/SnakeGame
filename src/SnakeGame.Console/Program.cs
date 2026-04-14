Console.CursorVisible = false;

var width = 30;
var height = 15;
Random rand = new Random();

while (true)
{
    var movex = 1;
    var movey = 0;
    int score = 0;
    int speed = 180;

    List<(int x, int y)> snake = new List<(int x, int y)>
    {
        (10, 10),
        (9, 10),
        (8, 10)
    };

    // Random food position
    (int x, int y) foodPosition = (rand.Next(width), rand.Next(height));

    // Game loop
    while (true)
    {
        // Check if a key has been pressed
        // If so, read which key was pressed without displaying it on the console
        // Then update the snake's direction if it is not already moving in that or the opposite direction. 
        // It is not possible to move 180 degrees
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow && movey == 0) { movex = 0; movey = -1; }
            if (key == ConsoleKey.DownArrow && movey == 0) { movex = 0; movey = 1; }
            if (key == ConsoleKey.LeftArrow && movex == 0) { movex = -1; movey = 0; }
            if (key == ConsoleKey.RightArrow && movex == 0) { movex = 1; movey = 0; }
        }
        
        // Update snake position
        var head = snake[0];
        var newHead = (x: head.x + movex, y: head.y + movey);
        
        if (newHead == foodPosition)
        {
            // Grow snake
            snake.Insert(0, newHead);
            score++;
            speed = Math.Max(30, speed - 10); // faster, but never under 30ms
            do
            {
                foodPosition = (rand.Next(width), rand.Next(height));
            } while (snake.Contains(foodPosition)); // avoid spawning food on the snake
        }
        else
        {
            // Normal move: insert head and remove tail
            snake.Insert(0, newHead);
            snake.RemoveAt(snake.Count - 1);
        }

        // Draw
        Console.SetCursorPosition(0, 0); // reset cursor
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (snake[0] == (x, y))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("●");
                }
                else if (snake.Contains((x, y)))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("●");
                }
                else if ((x, y) == foodPosition)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("✱");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("□"); //
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        
        Console.SetCursorPosition(0, height);
        Console.WriteLine($"Poäng: {score}   ");
        
        // Game over if the snake hits the wall
        if (newHead.x < 0 || newHead.x >= width || newHead.y < 0 || newHead.y >= height)
        {
            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine($"Game Over! Score: {score}");
            break;
        }
        
        // Game over if the snake hits itself
        if (snake.Skip(1).Contains(newHead))
        {
            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine($"Game Over! You hit yourself LOSER! Score: {score}"); 
            break;
        }
        int sleepTime = movey != 0 ? speed * 2 : speed; // dubbel time for up and down
        Thread.Sleep(sleepTime); // Speed
        
    }

    // Ask if the player wants to play again
    Console.SetCursorPosition(0, height + 2);
    Console.Write("Want to play again? Y/n: ");
    var answer = Console.ReadLine();
    if (answer?.ToLower() != "y" && answer != " " && answer != "")
        break; // stop game if player dont answer yes och space

    Console.Clear();
}

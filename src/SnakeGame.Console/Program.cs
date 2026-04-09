Console.CursorVisible = false;

var width = 20;
var height = 20;
var movex = 1;
var movey = 0;

List<(int x, int y)> snake = new List<(int x, int y)>
{
    (10, 10),
    (9, 10),
    (8, 10)
};

// Random food position
Random rand = new Random();
(int x, int y) foodPosition = (rand.Next(width), rand.Next(height));

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
    Console.Clear();
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (snake.Contains((x, y)))
                Console.Write("0");
            else if ((x, y) == foodPosition)
                Console.Write("X");
            else
                Console.Write(".");
        }
        Console.WriteLine();
    }
    
    // Game over if the snake hits the wall
    if (newHead.x < 0 || newHead.x >= width || newHead.y < 0 || newHead.y >= height)
    {
        Console.SetCursorPosition(0, height + 1);
        Console.WriteLine("Game Over!");
        break;
    }
    
    // Game over if the snake hits itself
    if (snake.Skip(1).Contains(newHead))
    {
        Console.SetCursorPosition(0, height + 1);
        Console.WriteLine("Game Over! You hit yourself BITCH");
        break;
    }

    Thread.Sleep(200); // Speed
    
}
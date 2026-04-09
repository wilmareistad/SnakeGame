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
            
    // Move Body
    snake.Insert(0, newHead);
    snake.RemoveAt(snake.Count - 1);

    // Draw
    Console.Clear();
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (snake.Contains((x, y)))
                Console.Write("O");
            else
                Console.Write(".");
        }
        Console.WriteLine();
    }

    Thread.Sleep(200); // Speed
    
}
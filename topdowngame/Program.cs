using Raylib_cs;
using System.Numerics;

int windowWidth = 1000;
int windowHeight = 750;
bool levelComplete = false;
bool changeLevel = false;


Raylib.InitWindow(windowWidth, windowHeight, "HÖR DU MIG???");
Raylib.SetTargetFPS(60);

Player player = new Player();
Color playerColor = new Color(140, 140, 140, 255);
Color bgColor = new Color(70, 70, 70, 255);

Vector2 goalPos = new Vector2(0, 0);
Vector2 spawnPos = new Vector2(0, 0);
float goalSize = 25;

int currentLevel = 0;
List<int[][]> levels = new List<int[][]>();

//Level design, every number = 50 pixels. 0 = air, 1 = goal, 2 = block, 3 = danger block don't touch :(((( 
int[][] level1 = {new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 2, 2, 2},
                  new int[] { 0, 4, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 1, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2},
                  new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}};
levels.Add(level1);

int[][] level2 = {new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 2, 0, 2, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
levels.Add(level2);



player.previousPos = player.position - player.moveSpeed;

void CheckCollisions(Rectangle rect, Rectangle obstacle, string type)
{
    if (Raylib.CheckCollisionRecs(rect, obstacle))
    {
        if (type == "wall")
        {
            Console.WriteLine(obstacle.x + " " + player.position.X);
            player.position = player.previousPos;
        }
        else if (type == "danger")
        {
            Rectangle overlap = Raylib.GetCollisionRec(rect, obstacle);
            Raylib.DrawRectangleRec(overlap, Color.BLUE);
            Vector2 overlapSize = new Vector2(overlap.width, overlap.height);

            Console.WriteLine(overlapSize.X * overlapSize.Y * 0.001);
            player.hp -= overlapSize.X * overlapSize.Y * 0.001f;
        }
    }
}


void DrawLevel(int[][] level, Rectangle player)
{
    for (int row = 0; row <= level.Count() - 1; row++)
    {
        for (int column = 0; column <= level[row].Count() - 1; column++)
        {
            //If the current row and collumn is 1 then spawn a goal at those coordinates
            if (level[row][column] == 1)
            {
                goalPos.X = column * 50 + 25;
                goalPos.Y = row * 50 + 25;

                Raylib.DrawCircle((int)goalPos.X, (int)goalPos.Y, goalSize, Color.GREEN);
            }
            //If the current row and column is a 2 then spawn a block at those coordinates
            else if (level[row][column] == 2)
            {
                Rectangle block = new Rectangle(column * 50, row * 50, 50, 50);
                Raylib.DrawRectangleRec(block, Color.BROWN);
                CheckCollisions(player, block, "wall");
            }

            else if (level[row][column] == 3)
            {
                Rectangle danger = new Rectangle(column * 50, row * 50, 50, 50);
                Raylib.DrawRectangleRec(danger, playerColor);
                CheckCollisions(player, danger, "danger");
            }

            else if (level[row][column] == 4){
                spawnPos.X = column * 50;
                spawnPos.Y = row * 50;
            }
        }
    }
}


bool levelTransition = false;

//Game loop
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(bgColor);


    //Entities
    Rectangle playerRect = new Rectangle(player.position.X, player.position.Y, 50, 50);


    //Draw level
    DrawLevel(levels[currentLevel], playerRect);


    Console.WriteLine(player.position + " " + player.previousPos);

    //Movement
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        player.position.X -= player.moveSpeed.X;
        player.previousPos.X = player.position.X + player.moveSpeed.X;

        if (player.position.X < 0)
        {
            player.position.X = 0;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        player.position.X += player.moveSpeed.X;
        player.previousPos.X = player.position.X - player.moveSpeed.X;

        if (player.position.X > windowWidth - 50)
        {
            player.position.X = windowWidth - 50;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        player.position.Y -= player.moveSpeed.Y;
        player.previousPos.Y = player.position.Y + player.moveSpeed.Y;

        if (player.position.Y < 0)
        {
            player.position.Y = 0;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        player.position.Y += player.moveSpeed.Y;
        player.previousPos.Y = player.position.Y - player.moveSpeed.Y;

        if (player.position.Y > windowHeight - 50)
        {
            player.position.Y = windowHeight - 50;
        }
    }


    Raylib.DrawRectangleRec(playerRect, playerColor);
    Raylib.DrawText("Hp: " + (int)player.hp, 10, 10, 24, Color.BLACK);
    Raylib.DrawCircle((int)goalPos.X, (int)goalPos.Y, goalSize, Color.GREEN);

    //Check if player touches goal
    if (Raylib.CheckCollisionCircleRec(goalPos, 25, playerRect))
    {
        levelComplete = true;
    }


    if (levelComplete)
    {
        goalSize *= 1.1f;
    }
    
    if (goalSize >= windowWidth * 2){
            levelTransition = true;
            levelComplete = false;  
            changeLevel = true;
        }

    if (changeLevel == true){
        currentLevel += 1;
        player.position = spawnPos;
        changeLevel = false;  
    }

    if (levelTransition){
        goalSize *= 0.9f;

        if (goalSize <= 25){
            goalSize = 25;
            levelTransition = false;
        }
    }

    Raylib.EndDrawing();
}


public class Player
{
    public float hp = 100;
    public Vector2 position = new Vector2(50, 300);
    public Vector2 previousPos = new Vector2(0, 0);
    public Vector2 moveSpeed = new Vector2(5, 5);
}

using System;
using Raylib_cs;
using TechDev.Log;
using TechDev.IO;

namespace NovelEngine
{
    class Renderer
    {   
        public enum cScreen{
            Intro,
            Menu,
            Settings,
            Credits,
            Load,
            Game
        }

        Texture2D background;

        public static cScreen currentScreen; // Current Scene

        static void Main()
        {
            Renderer renderer = new Renderer();
            renderer.Render();
        }

        public void Render()
        {
            Raylib.InitWindow(1280, 720, "Visual Novel Engine"); // Create window

            // Load textures
            background = Raylib.LoadTexture(TechDev.IO.Loading.GetCurrentDir() + "\\resources\\img\\bg\\blank.png");

            currentScreen = cScreen.Intro;

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                // Write raylib renderer events past this point
                Raylib.DrawTexture(background,0,0,Color.WHITE); // Do not remove under any circumstances or this project is gonna commit die

                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(background);
            Raylib.CloseWindow();
        }

        public bool InGame()
        {
            // Check if current scene is the game scene
            if (currentScreen == cScreen.Game)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetSprite(string character, string state)
        {
            // Check if current scene is the game scene, if not, print an error message in the console
            switch (InGame())
            {
                case true:
                    Log.LogMessage("Loading provided image: resources/img/char/" + character + " " + state + ".png");
                    break;
                case false:
                    Log.LogError("Cannot add character images outside of the game scene.");
                    break;
            }
        }

        public void SetBackground(string background)
        {
            // Check if current scene is the game scene, if not, print an error message in the console
            switch (InGame())
            {
                case true:
                    Log.LogMessage("Loading provided image: resources/img/bg/" + background + ".png");

                    if(TechDev.IO.DirectoryAndFileChecker.fileExists(TechDev.IO.Loading.GetCurrentDir() + "\\resources\\img\\", background + ".png"))
                    {
                        
                    }
                    else
                    {
                        Log.LogError("File '" + TechDev.IO.Loading.GetCurrentDir() + "\\resources\\img\\" + "' does not exist. Perhaps the file is not a PNG?");
                    }

                    break;
                case false:
                    Log.LogError("Cannot add scenery images outside of the game scene.");
                    break;
            }
        }
    }
}

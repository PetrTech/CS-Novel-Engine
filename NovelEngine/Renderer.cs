using System;
using System.Numerics;
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

        public cScreen currentScreen = cScreen.Intro; // Current Scene

        static void Main()
        {
            Renderer renderer = new Renderer();
            renderer.Render();
        }

        public void Render() // Rendering goes here (WOW HOW UNEXPECTED)
        {
            int ww = 1920;
            int wh = 1080;
            Raylib.InitWindow(ww, wh, "Visual Novel Engine"); // Create window

            // Load textures

            // Load blank background image
            Image bgimg = Raylib.LoadImage("resources/img/bg/blank.png");
            background = Raylib.LoadTextureFromImage(bgimg);
            Raylib.UnloadImage(bgimg);

            // Load logo image
            Image limg = Raylib.LoadImage("resources/img/logo.png");
            Texture2D logoimg = Raylib.LoadTextureFromImage(limg);
            Raylib.UnloadImage(limg);

            Raylib.SetTargetFPS(30);

            Raylib.ToggleFullscreen();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                // Write raylib renderer events past this point
                Raylib.DrawTexture(background, 0, 0, Color.WHITE); // Do not remove under any circumstances or this project is gonna commit die

                switch (currentScreen)
                {
                    case cScreen.Intro: // If current scene is the intro scene, draw the logo.
                        Raylib.DrawTexture(logoimg, ww/2 - logoimg.width/2, wh/2 - logoimg.height/2, Raylib.Fade(Color.WHITE,));
                        break;
                }

                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(background);
            Raylib.UnloadTexture(logoimg);
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

        public void SetBackground(string background, bool fade)
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

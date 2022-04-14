using System;
using System.Numerics;
using System.Text;
using System.Collections;
using Raylib_cs;
using TechDev.Log;
using TechDev.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NovelEngine
{
    class Renderer
    {
        public enum cScreen {
            Intro,
            Menu,
            Settings,
            Credits,
            Load,
            Game
        }

        Texture2D background;

        Writing wr = new TechDev.IO.Writing();

        public cScreen currentScreen = cScreen.Intro; // Current Scene

        int timeOnSplash;

        // JSON values
        string splashmusic;
        bool splashplaymusic;
        float splashspeed;
        bool usesplash;
        List<int> splashcolor;

        string menubackground;
        bool menuplaymusic;
        string menumusicasset;

        static void Main()
        {
            Renderer renderer = new Renderer();
            renderer.LoadConfigs();
            renderer.Render();
        }

        public void LoadConfigs()
        {
            try
            {
                string json;
                json = Loading.LoadFile(Loading.GetCurrentDir() + "\\resources\\cfg", "splashcfg.json");
                var GetSplashConfig = JsonConvert.DeserializeObject<SplashConfig>(json);

                splashmusic = GetSplashConfig.MusicAsset;
                splashplaymusic = GetSplashConfig.PlayMusic;
                splashspeed = GetSplashConfig.Speed;
                splashcolor = GetSplashConfig.BackgroundColor;
                usesplash = GetSplashConfig.UseSplash;

                json = Loading.LoadFile(Loading.GetCurrentDir() + "\\resources\\cfg", "menucfg.json");
                var GetMenuConfig = JsonConvert.DeserializeObject<MenuConfig>(json);

                menubackground = GetMenuConfig.BackgroundAsset;
                menuplaymusic = GetMenuConfig.PlayMusic;
                menumusicasset = GetMenuConfig.MusicAsset;
            }
            catch (Exception e)
            {
                Log.LogError(e.ToString());
            }
        }

        public void Render() // Rendering goes here (WOW HOW UNEXPECTED)
        {
            wr.DestroyFile(TechDev.IO.Loading.GetCurrentDir(), "traceback.txt");

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
                Raylib.ClearBackground(new Color(splashcolor[0], splashcolor[1], splashcolor[2], splashcolor[3]));

                // Write raylib renderer events past this point
                switch (currentScreen)
                {
                    case cScreen.Intro: // If current scene is the intro scene, draw the logo.
                        if (usesplash)
                        {
                            timeOnSplash++;
                            Raylib.DrawTexture(logoimg, ww / 2 - logoimg.width / 2, wh / 2 - logoimg.height / 2, Raylib.Fade(Color.WHITE, (float)timeOnSplash / 15));

                            if (timeOnSplash > 140)
                            {
                                currentScreen = cScreen.Menu;
                            }
                        }

                        break;

                    case cScreen.Menu:

                        break;

                    case cScreen.Game:
                        Raylib.DrawTexture(background, 0, 0, Color.WHITE); // Do not remove under any circumstances or this project is gonna commit die
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

                    if (TechDev.IO.DirectoryAndFileChecker.fileExists(TechDev.IO.Loading.GetCurrentDir() + "\\resources\\img\\", background + ".png"))
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

    // Used for loading configuration (development config, not player config)
    class SplashConfig {
        public List<int> BackgroundColor { set; get; }
        public float Speed { set; get; }
        public bool PlayMusic { set; get; }
        public string MusicAsset { set; get; }
        public bool UseSplash { set; get; }
    }

    // Used for loading configuration (development config, not player config)
    class MenuConfig
    {
        public string BackgroundAsset { set; get; }
        public bool PlayMusic { set; get; }
        public string MusicAsset { set; get; }
    }
}

using System.Collections.Generic;
using KeyShine.ColorSchemes;
using KeyShine.Layout;
using KeyShine.Layout.LayoutProviders.Windows;
using KeyShine.LEDControllers;
using KeyShine.LEDControllers.Logitech;
using KeyShine.LEDControllers.Corsair;
using KeyShine.SceneManagers;
using UnityEngine;

namespace KeyShine
{
    /// <summary>
    ///     The main class, managing the keyboard appearance for every kind of scene KSP
    ///     uses.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class KeyShine : MonoBehaviour
    {
        public static KeyShine Instance;

        public ILayoutProvider LayoutProvider { get; private set; }

        private readonly List<ILEDController> _ledControllers = new List<ILEDController>();

        private readonly SceneManager _flightSceneManager = new FlightSceneManager();

        private readonly SceneManager _vabSceneManager = new VABSceneManager();

        /// <summary>
        ///     Called by unity during the launch of KSP. It will only be run once. So make sure it doesn't
        ///     crash.
        /// </summary>
        private void Start()
        {
            Instance = this;
            _ledControllers.Add(new LogitechLEDController());
            _ledControllers.Add(new CorsairLEDController());
            LayoutProvider = new WindowsLayoutProvider();
            DontDestroyOnLoad(this);
        }

        /// <summary>
        ///     Called by unity on every physics frame.
        /// </summary>
        /// 

        public static void extPrint(string input)
        {
            print(input);
        }


        private void Update()
        {
            ColorScheme scheme;
            if (HighLogic.LoadedScene == GameScenes.FLIGHT)
            {
                print($"ASL: {FlightGlobals.fetch.activeVessel.altitude} || AGL: {FlightGlobals.fetch.activeVessel.terrainAltitude}");
            }
            
            if (AnimationManager.Instance.animationRunning())
            {
                scheme = AnimationManager.Instance.getFrame();
            }
            else
            {
                switch (HighLogic.LoadedScene)
                {
                    case GameScenes.FLIGHT:
                        scheme = _flightSceneManager.getScheme();
                        break;
                    case GameScenes.EDITOR:
                        scheme = _vabSceneManager.getScheme();
                        break;
                    default:
                        scheme = new LogoScheme();
                        break;
                }
            }

            _ledControllers.ForEach(controller => controller.Send(scheme));
        }
    }
}
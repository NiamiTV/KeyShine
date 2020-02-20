using System.Collections.Generic;
using KSP_LogiRGB.ColorSchemes;
using KSP_LogiRGB.Layout;
using KSP_LogiRGB.Layout.LayoutProviders.Windows;
using KSP_LogiRGB.LEDControllers;
using KSP_LogiRGB.LEDControllers.Logitech;
using KSP_LogiRGB.SceneManagers;
using UnityEngine;

namespace KSP_LogiRGB
{
    /// <summary>
    ///     The main class, managing the keyboard appearance for every kind of scene KSP
    ///     uses.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class KSPLogitechRGBPlugin : MonoBehaviour
    {
        public static KSPLogitechRGBPlugin Instance;

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
            LayoutProvider = new WindowsLayoutProvider();
            DontDestroyOnLoad(this);
        }

        /// <summary>
        ///     Called by unity on every physics frame.
        /// </summary>
        /// 

      


        private void Update()
        {
            ColorScheme scheme;
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
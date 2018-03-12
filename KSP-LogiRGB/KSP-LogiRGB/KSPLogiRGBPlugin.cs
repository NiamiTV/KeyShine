﻿using System.Collections.Generic;
using KSP_LogiRGB.ColorSchemes;
using KSP_LogiRGB.LEDControllers;
using KSP_LogiRGB.LEDControllers.Logitech;
using KSP_LogiRGB.Logitech;
using KSP_LogiRGB.SceneManagers;
using UnityEngine;

namespace KSP_LogiRGB
{
    /// <summary>
    ///     The main class, managing the keyboard appearance for every kind of scene KSP
    ///     uses.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KSPChromaPlugin : MonoBehaviour
    {
        public static KSPChromaPlugin fetch;
        private readonly List<ILEDController> dataDrains = new List<ILEDController>();

        /// <summary>
        ///     The UDP network socket to send keyboard appearance orders to the server.
        /// </summary>
        private readonly SceneManager flightSceneManager = new FlightSceneManager();

        private readonly SceneManager vabSceneManager = new VABSceneManager();

        /// <summary>
        ///     Called by unity during the launch of this addon.
        /// </summary>
        private void Awake()
        {
            fetch = this;
            dataDrains.Add(new LogitechLEDController());
        }

        /// <summary>
        ///     Called by unity on every physics frame.
        /// </summary>
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
                        scheme = flightSceneManager.getScheme();
                        break;
                    case GameScenes.EDITOR:
                        scheme = vabSceneManager.getScheme();
                        break;
                    default:
                        scheme = new LogoScheme();
                        break;
                }
            }

            dataDrains.ForEach(drain => drain.Send(scheme));
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using VH.Engine.Display;
using VH.Engine.Levels;
using VH.Game;
using VH.Engine.Game;
using System.Windows.Forms;
using Sparc.Game;


// SPace Adventure Roguelike Crawler

namespace VH2 {

    class SPARC {

        [STAThread]
        static void Main(string[] args) {
            GameController.Instance = new SparcGameController();
            try {
                GameController.Instance.Play();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace);
                string filename = Application.StartupPath + "\\Data\\_error.log";
                System.IO.File.WriteAllText(filename, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}

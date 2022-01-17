using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.API
{
    public class Facade
    {
        public static GameSession gmSession { get; private set; }
        public static GameEnv gmEnv { get; private set; }

        public static void NewGame()
        {
            gmSession = new GameSession();
        }

        public static void NewEnv()
        {
            gmEnv = new GameEnv();
        }
    }
}

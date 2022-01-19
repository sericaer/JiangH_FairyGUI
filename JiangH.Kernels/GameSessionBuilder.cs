using JiangH.API;
using JiangH.Kernels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH.Kernels
{
    public class GameSessionBuilder : IGameSessionBuilder
    {
        public static GameSessionBuilder inst
        {
            get
            {
                if(_inst == null)
                {
                    _inst = new GameSessionBuilder();
                }

                return _inst;
            }
        }

        private static GameSessionBuilder _inst;

        public GameSession build()
        {
            GameSession gameSession = new GameSession();
            gameSession.player = new Person(gameSession);
            gameSession.date = new Date();
            gameSession.relationManager = new RelationManager();

            for (int i = 0; i < 3; i++)
            {
                gameSession.player.AddEstate(new Estate() { name = $"{i}_Estate" });
            }

            return gameSession;
        }
    }
}

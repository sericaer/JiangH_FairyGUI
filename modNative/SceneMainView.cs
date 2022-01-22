using JiangH.API;
using System;
using System.ComponentModel;
using ReactiveMarbles.PropertyChanged;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using PropertyChanged;

namespace modNative
{
    [UISceneBind("MainScene")]
    public class MainSceneView : UIView
    {
        public string playerName => Facade.gmSession.player.name;
        public string date => $"{year}-{month}-{day}";

        public bool isSpeed1 { get => speed == 1; set { if (value) speed = 1; } }

        public bool isSpeed2 { get => speed == 2; set { if (value) speed = 2; } }
        public bool isSpeed3 { get => speed == 3; set { if (value) speed = 3; } }
        public bool isSpeedPause { get => speed == -1; set { if (value) speed = -1; } }

        //public ICommand playerButtonCmd => _playerButtonCmd;

        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public int speed { get; set; }

        public int money { get; set; }
        public string moneyDetailIncome { get; set; }

        //private ICommand _playerButtonCmd = new PersonDetailWindowCmd(() => Facade.gmSession.player);

        public MainSceneView()
        {
            BindOneWay(Facade.gmSession, x => x.date.day, this, t=> t.day);
            BindOneWay(Facade.gmSession, x => x.date.year, this, t => t.year);
            BindOneWay(Facade.gmSession, x => x.date.month, this, t => t.month);
            BindOneWay(Facade.gmSession, x => x.player.money.count, this, t => t.money);
            BindOneWay(Facade.gmSession, x => x.player.money.detailIncome, this, t => t.moneyDetailIncome);

            BindTwoWay(Facade.gmEnv, x => x.DayIncSpeed, this, x => x.speed);
            //Facade.gmEnv.BindTwoWay(env => env.DayIncSpeed, this, x => x.speed);

            //Bind.OneWay.from(Facade.gmSession, x => x.player.name).to(this, x => x.playerName);


            //isSpeed1 = true;
        }

        public NewWindowCmd OnClickPlayerButton()
        {
            return new NewWindowCmd() {
                name = "PersonDetailWindow",
                param = Facade.gmSession.player
            };
        }

    }
}

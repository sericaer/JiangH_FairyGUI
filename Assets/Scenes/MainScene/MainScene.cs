using FairyGUI;
using FairyGUI.DataBind;
using JiangH.API;
using JiangH.Kernels.Entities;
using System.Collections;
using System.Linq;

using UnityEngine;

public class MainScene : MonoBehaviour
{
    private GObject gObject;

    private int count = 0;

    void Start()
    {
        BindContext.DoCmd = (context, obj) =>
        {
            switch(obj)
            {
                case NewWindowCmd cmd:
                    UIGenerator.GenWin(cmd.name, cmd.param);
                    break;
            }
        };

        gObject = UIGenerator.GenScene(nameof(MainScene));
    }

    void Update()
    {
        if(count % 1000 == 0)
        {
            //Facade.gmSession.player.AddEstate(new Estate() { name = $"{count}_ESTATE" });
        }

        if (count % 1500 == 0)
        {
            //Facade.gmSession.player.RemoveEstate(Facade.gmSession.player.estates.First());
        }

        count++;
    }
}


public class Test :GButton
{
    public override void ConstructFromResource()
    {
        base.ConstructFromResource();
    }

}
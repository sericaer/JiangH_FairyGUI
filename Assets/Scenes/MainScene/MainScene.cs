using FairyGUI;
using FairyGUI.DataBind;
using JiangH.API;
using System.Collections;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private GObject gObject;

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
        
    }

    void OnDestroy()
    {
        UIGenerator.Destroy(gObject);
    }
}

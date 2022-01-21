using FairyGUI;
using FairyGUI.DataBind;
using JiangH.API;
using JiangH.Kernels.Mods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

internal class UIGenerator
{
    private static Dictionary<string, UIGroups> dict;
    private static Dictionary<GObject, BindContext> dictBindContext;

    internal static void Init(IEnumerable<Mod> mods)
    {
        UIConfig.tooltipsWin = "ui://ah3qizbwp8bf11";

        dict = new Dictionary<string, UIGroups>();
        dictBindContext = new Dictionary<GObject, BindContext>();

        foreach (var mod in mods.Where(x => x.uiBytes != null))
        {
            var package = UIPackage.AddPackage(mod.uiBytes, mod.name, loadResource);

            dict.Add(mod.name, new UIGroups(package, mod.uiLogicDict));
        }
    }

    internal static void GenWin(string name, object param)
    {
        var def = dict["native"].GetUIDef(name);

        var dataSource = Activator.CreateInstance(def.uiLogic) as UIView;
        dataSource.Init(param);

        var gObject = def.uiPackage.CreateObject(def.uiItemName).asCom;
        var context = gObject.BindDataSource(dataSource);

        gObject.MakeFullScreen();

        Window win = new Window();
        win.contentPane = gObject;
        win.modal = true;
        win.Show();

        win.onRemovedFromStage.Add(() =>
        {
            context.Dispose();
        });
    }

    internal static GObject GenScene(string name)
    {
        var def = dict["native"].GetUIDef(name);

        var gObject = def.uiPackage.CreateObject(def.uiItemName).asCom;
        var dataSource = Activator.CreateInstance(def.uiLogic) as INotifyPropertyChanged;
        var context = gObject.BindDataSource(dataSource);

        dictBindContext.Add(gObject, context);

        GRoot.inst.AddChild(gObject);

        return gObject;
    }

    internal static void Destroy(GObject gObject)
    {
        dictBindContext[gObject].Dispose();
        dictBindContext.Remove(gObject);
    }

    private static object loadResource(string name, string extension, Type type, out DestroyMethod destroyMethod)
    {
        throw new NotImplementedException();
    }

    internal class UIDef
    {
        public UIPackage uiPackage;
        public string uiItemName;
        public Type uiLogic;
    }

    internal class UIGroups
    {
        private Dictionary<string, UIDef> defDict;

        public UIGroups(UIPackage package, Dictionary<string, Type> uiLogicDict)
        {
            defDict = new Dictionary<string, UIDef>();

            foreach (var item in package.GetItems())
            {
                if (!item.exported)
                {
                    continue;
                }

                if(!uiLogicDict.ContainsKey(item.name))
                {
                    continue;
                }

                defDict.Add(item.name, new UIDef()
                {
                    uiPackage = package,
                    uiItemName = item.name,
                    uiLogic = uiLogicDict[item.name]
                });
            }
        }

        internal UIDef GetUIDef(string name)
        {
            return defDict[name];
        }
    }
}


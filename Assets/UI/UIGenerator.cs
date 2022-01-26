using FairyGUI;
using FairyGUI.DataBind;
using FairyGUI.DataBind.BindCustomDatas;
using JiangH.API;
using JiangH.Kernels.Mods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UI.Extends;

internal class UIGenerator
{
    protected static Dictionary<string, Type> dictUIExt = new Dictionary<string, Type>()
    {
        //{ "ui://ah3qizbwroht1b", typeof(PersonEngineSpendSlider)}
    };

    private static Dictionary<string, UIGroups> dictUIGroups;

    internal static void Init(IEnumerable<Mod> mods)
    {
        dictUIGroups = new Dictionary<string, UIGroups>();

        foreach (var mod in mods.Where(x => x.uiBytes != null))
        {
            var package = UIPackage.AddPackage(mod.uiBytes, mod.name, loadResource);

            dictUIGroups.Add(mod.name, new UIGroups(package, mod.uiLogicDict));
        }

        UIConfig.tooltipsWin = "ui://ah3qizbwp8bf11";

        foreach(var key in dictUIExt.Keys)
        {
            var extendType = dictUIExt[key];

            UIObjectFactory.SetPackageItemExtension(key, extendType);

            var methodInitBindCustomData = extendType.GetMethod("InitBindCustomData");
            methodInitBindCustomData?.Invoke(null, null);
        }
    }

    internal static void GenWin(string name, object param)
    {
        var def = dictUIGroups["native"].GetUIDef(name);

        var dataSource = Activator.CreateInstance(def.uiLogic, new object[] { param }) as UIView;

        var gObject = def.uiPackage.CreateObject(def.uiItemName).asCom;
        gObject.BindDataSource(dataSource);

        gObject.MakeFullScreen();

        Window win = new Window();
        win.contentPane = gObject;
        win.modal = true;
        win.Show();
    }

    internal static GObject GenScene(string name)
    {
        var def = dictUIGroups["native"].GetUIDef(name);

        var gObject = def.uiPackage.CreateObject(def.uiItemName).asCom;
        var dataSource = Activator.CreateInstance(def.uiLogic) as INotifyPropertyChanged;
        gObject.BindDataSource(dataSource);

        GRoot.inst.AddChild(gObject);

        return gObject;
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


using JiangH.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JiangH.Kernels.Mods
{
    public class Mod
    {
        public string name { get; }

        public byte[] uiBytes;

        public Dictionary<string, Type> uiLogicDict;

        public Mod(string path)
        {
            name = Path.GetFileName(path);
            uiLogicDict = new Dictionary<string, Type>();

            uiBytes = File.ReadAllBytes(Path.Combine(path, $"{name}_fui.bytes"));

            var dllBytes = File.ReadAllBytes(Path.Combine(path, $"{name}.dll"));
            var assembly = Assembly.Load(dllBytes);
            var viewTypes = assembly.GetTypes().Where(x => typeof(UIView).IsAssignableFrom(x));
            foreach (var type in viewTypes)
            {
                var attrib = type.GetCustomAttribute<UISceneBind>();
                if (attrib == null)
                {
                    continue;
                }

                uiLogicDict.Add(attrib.label, type);
            }
        }
    }
}
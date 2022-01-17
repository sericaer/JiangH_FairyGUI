using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JiangH.Kernels.Mods
{
    public class ModManager
    {
        public static ModManager inst { get; private set; }

        public static void Init(string modPath, string[] modNames)
        {
            inst = new ModManager(modPath, modNames);
        }

        public Mod native { get; }

        public IEnumerable<Mod> mods;


        private ModManager(string modPath, string[] modNames)
        {
            mods = modNames.Select(name => new Mod(Path.Combine(modPath, name))).ToArray();

            native = mods.Single(x => x.name == "native");
        }
    }
}
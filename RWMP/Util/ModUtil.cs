using System.Linq;
using Verse;

namespace RWMP.Util
{
    public class ModUtil
    {
        public static string Name => "RWMP";

        public static ModMetaData MetaData =>
            ModLister.AllInstalledMods.FirstOrDefault(metaData => metaData.Name == Name);

        public static bool Active
        {
            get { return MetaData.Active; }
            set { MetaData.Active = value; }
        }

        public static void Disable()
        {
            Active = false;
        }
    }
}
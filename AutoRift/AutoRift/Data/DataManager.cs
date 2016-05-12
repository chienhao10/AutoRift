using System.IO;
using EloBuddy.Sandbox;

namespace AutoRift.Data
{
    public static class DataManager
    {
        public static readonly string DataPath = Path.Combine(SandboxConfig.DataDirectory, "AutoRift");

        public static void Init()
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            BuildManager.Load();
        }
    }
}
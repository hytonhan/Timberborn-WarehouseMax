using HarmonyLib;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;

namespace Hytone.Timberborn.Plugins.WarehouseMax
{
    [HarmonyPatch]
    public class BetterWarehousesPlugin : IModEntrypoint
    {
        public void Entry(IMod mod, IConsoleWriter consoleWriter)
        {
            var harmony = new Harmony("hytone.plugins.warehousemax");
            harmony.PatchAll();

            consoleWriter.LogInfo("WarehouseMax is loaded.");
        }
    }
}

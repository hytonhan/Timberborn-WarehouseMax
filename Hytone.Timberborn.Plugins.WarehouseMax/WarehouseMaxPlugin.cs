using HarmonyLib;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;

namespace Hytone.Timberborn.Plugins.WarehouseMax
{
    /// <summary>
    /// Basically this is used to get patches to work
    /// </summary>
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

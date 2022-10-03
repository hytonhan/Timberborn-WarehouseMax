using HarmonyLib;
using Timberborn.InventorySystem;
using Timberborn.StockKeeping;

namespace Hytone.Timberborn.Plugins.WarehouseMax.Patches
{
    /// <summary>
    /// Patch the method that handles desired logic
    /// </summary>
    [HarmonyPatch(typeof(ToggleableGoodDisallower), nameof(ToggleableGoodDisallower.AllowedAmount))]
    public static class ToggleableGoodDisallowerPatch
    {
        private static void Postfix(ref int __result, ToggleableGoodDisallower __instance, string goodId)
        {
            if (__result == 0 ||
                !__instance.TryGetComponent<WarehouseMaxComponent>(out var warehouseMaxComponent) ||
                warehouseMaxComponent.DesiredIsMax == false)
            {
                return;
            }

            if (__instance.TryGetComponent<GoodDesirer>(out var goodDesirer))
            {
                __result = goodDesirer.DesiredAmount(goodId);
            }

        }
    }
}

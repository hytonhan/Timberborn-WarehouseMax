using HarmonyLib;
using System.Linq;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Warehouses;
using Timberborn.WarehousesUI;
using UnityEngine;
using UnityEngine.UIElements;
namespace Hytone.Timberborn.Plugins.WarehouseMax.Patches
{
    /// <summary>
    /// Patch the stockpilefragment to show our custom toggle that
    /// replaces desired with max
    /// </summary>
    [HarmonyPatch]
    public class StockpileInventoryFragmentPatch
    {
        private static WarehouseMaxComponent _warehouseMax;
        private static Toggle _desiredIsMaxToggle;
        private static ILoc _loc;

        [HarmonyPatch(typeof(StockpileInventoryFragment), nameof(StockpileInventoryFragment.InitializeFragment))]
        [HarmonyPostfix]
        public static void InitializeFragmentPostfix(ref VisualElement __result)
        {
            var uiBuilder = DependencyContainer.GetInstance<UIBuilder>();

            var foo = uiBuilder.CreateComponentBuilder()
                               .CreateVisualElement()
                               .AddPreset(factory =>
                                    factory.Toggles()
                                           .CheckmarkInverted(
                                                name: "DesiredIsMaxToggle",
                                                locKey: "warehousemax.DesiredIsMax",
                                                color: new StyleColor(new Color(0.8f, 0.8f, 0.8f, 1f))))
                               .BuildAndInitialize();

            __result.Insert(0, foo);

            _loc = DependencyContainer.GetInstance<ILoc>();

            _desiredIsMaxToggle = __result.Q<Toggle>("DesiredIsMaxToggle");
            _desiredIsMaxToggle.RegisterValueChangedCallback(ToggleChanged);

        }

        [HarmonyPatch(typeof(StockpileInventoryFragment), nameof(StockpileInventoryFragment.ShowFragment))]
        [HarmonyPrefix]
        public static void ShowFragmentPostfix(GameObject entity, StockpileInventoryFragment __instance)
        {
            Stockpile component = entity.GetComponent<Stockpile>();
            if ((object)component != null)
            {
                WarehouseMaxComponent warehouseComponent = entity.GetComponent<WarehouseMaxComponent>();
                if ((object)warehouseComponent != null)
                {
                    _warehouseMax = warehouseComponent;
                }
            }
        }

        [HarmonyPatch(typeof(StockpileInventoryFragment), nameof(StockpileInventoryFragment.ClearFragment))]
        [HarmonyPostfix]
        public static void ClearFragmentPostfix()
        {
            _warehouseMax = null;
        }

        [HarmonyPatch(typeof(StockpileInventoryFragment), nameof(StockpileInventoryFragment.UpdateFragment))]
        [HarmonyPostfix]
        public static void UpdateFragmentPostfix(StockpileInventoryFragment __instance)
        {
            var footer = __instance._root.Q<VisualElement>("Footer");
            var desiredLabel = __instance._content.Q<Label>("Desired");

            if (desiredLabel == null)
            {
                return;
            }

            if (_warehouseMax.DesiredIsMax)
            {
                desiredLabel.text = _loc.T("warehousemax.Max");
                ((LocalizableLabel)footer.Children().ElementAt(1).Children().First()).text = _loc.T("warehousemax.Max");
            }
            else
            {
                desiredLabel.text = _loc.T("Inventory.Desired");
                ((LocalizableLabel)footer.Children().ElementAt(1).Children().First()).text = _loc.T("Inventory.Desired");
            }

            _desiredIsMaxToggle.SetValueWithoutNotify(_warehouseMax.DesiredIsMax);
        }


        private static void ToggleChanged(ChangeEvent<bool> changeEvent)
        {
            _warehouseMax.DesiredIsMax = changeEvent.newValue;
        }
    }
}

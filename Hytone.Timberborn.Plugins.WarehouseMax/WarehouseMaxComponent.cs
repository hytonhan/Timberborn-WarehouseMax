using Timberborn.Persistence;
using UnityEngine;

namespace Hytone.Timberborn.Plugins.WarehouseMax
{
    public class WarehouseMaxComponent : MonoBehaviour, IPersistentEntity
    {
        private static readonly ComponentKey WarehouseMaxKey = new ComponentKey(nameof(WarehouseMaxComponent));
        private static readonly PropertyKey<bool> DesiredIsMaxKey = new PropertyKey<bool>(nameof(DesiredIsMax));

        public bool DesiredIsMax = false;

        public void Save(IEntitySaver entitySaver)
        {
            IObjectSaver component = entitySaver.GetComponent(WarehouseMaxKey);
            component.Set(DesiredIsMaxKey, DesiredIsMax);
        }

        public void Load(IEntityLoader entityLoader)
        {
            if (!entityLoader.HasComponent(WarehouseMaxKey))
            {
                return;
            }
            IObjectLoader component = entityLoader.GetComponent(WarehouseMaxKey);
            if (component.Has(DesiredIsMaxKey))
            {
                DesiredIsMax = component.Get(DesiredIsMaxKey);
            }
        }
    }
}

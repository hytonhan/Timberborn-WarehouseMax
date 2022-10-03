using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.TemplateSystem;
using Timberborn.Warehouses;

namespace Hytone.Timberborn.Plugins.WarehouseMax
{
    [Configurator(SceneEntrypoint.InGame)]
    public class WarehouseMaxConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.MultiBind<TemplateModule>().ToProvider(ProvideTemplateModule).AsSingleton();
        }

        private static TemplateModule ProvideTemplateModule()
        {
            TemplateModule.Builder builder = new TemplateModule.Builder();
            builder.AddDecorator<Stockpile, WarehouseMaxComponent>();
            return builder.Build();
        }
    }
}

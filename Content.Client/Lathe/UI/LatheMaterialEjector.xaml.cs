using Content.Shared.Materials;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;

namespace Content.Client.Lathe.UI;

/// <summary>
/// This widget is one row in the lathe eject menu.
/// </summary>

[GenerateTypedNameReferences]
public sealed partial class LatheMaterialEjector : PanelContainer
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public string Material;
    public Action<string, int>? OnEjectPressed;
    public int VolumePerSheet;

    public LatheMaterialEjector(string material, Action<string, int>? onEjectPressed, int volumePerSheet, int maxEjectableSheets)
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        Material = material;
        OnEjectPressed = onEjectPressed;
        VolumePerSheet = volumePerSheet;

        PopulateButtons(maxEjectableSheets);
    }

    public void PopulateButtons(int maxEjectableSheets)
    {
        int[] sheetsToEjectArray = { 1, 5, 10, 30 };

        foreach (int sheetsToEject in sheetsToEjectArray)
        {
            var button = new Button()
            {
                Name = $"{sheetsToEject}",
                Access = AccessLevel.Public,
                Text = Loc.GetString($"{sheetsToEject}"),
                MinWidth = 45,
            };

            button.OnPressed += (_) =>
            {
                OnEjectPressed?.Invoke(Material, sheetsToEject);
            };

            button.Disabled = maxEjectableSheets < sheetsToEject;

            if (_prototypeManager.TryIndex<MaterialPrototype>(Material, out var proto))
            {
                button.ToolTip = Loc.GetString("lathe-menu-tooltip-display", ("amount", sheetsToEject * VolumePerSheet), ("material", Loc.GetString(proto.Name)));
            }

            Content.AddChild(button);
        }
    }
}

using System.Numerics;
using Content.Shared.Explosion;
using JetBrains.Annotations;
using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Client.Explosion;

[UsedImplicitly]
public sealed class ExplosionOverlay : Overlay
{
    [Dependency] private readonly IRobustRandom _robustRandom = default!;
    [Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly IEntityManager _entMan = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    private readonly SharedTransformSystem _transform;

    public override OverlaySpace Space => OverlaySpace.WorldSpaceBelowFOV;

    private ShaderInstance _shader;

    public ExplosionOverlay()
    {
        IoCManager.InjectDependencies(this);
        _shader = _proto.Index<ShaderPrototype>("unshaded").Instance();
        _transform = _entMan.System<SharedTransformSystem>();
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        var drawHandle = args.WorldHandle;
        drawHandle.UseShader(_shader);

        var xforms = _entMan.GetEntityQuery<TransformComponent>();
        var query = _entMan
            .EntityQuery<ExplosionVisualsComponent, ExplosionVisualsTexturesComponent, AppearanceComponent>(true);

        foreach (var (visuals, textures, appearance) in query)
        {
            if (visuals.Epicenter.MapId != args.MapId)
                continue;

            if (!appearance.TryGetData(ExplosionAppearanceData.Progress, out int index))
                continue;

            index = Math.Min(index, visuals.Intensity.Count - 1);
            DrawExplosion(drawHandle, args.WorldBounds, visuals, index, xforms, textures);
        }

        drawHandle.SetTransform(Matrix3.Identity);
        drawHandle.UseShader(null);
    }

    private void DrawExplosion(
        DrawingHandleWorld drawHandle,
        Box2Rotated worldBounds,
        ExplosionVisualsComponent visuals,
        int index,
        EntityQuery<TransformComponent> xforms,
        ExplosionVisualsTexturesComponent textures)
    {
        Box2 gridBounds;
        foreach (var (gridId, tiles) in visuals.Tiles)
        {
            if (!_mapManager.TryGetGrid(gridId, out var grid))
                continue;

            var xform = xforms.GetComponent(grid.Owner);
            var (_, _, worldMatrix, invWorldMatrix) = _transform.GetWorldPositionRotationMatrixWithInv(xform, xforms);

            gridBounds = invWorldMatrix.TransformBox(worldBounds).Enlarged(grid.TileSize * 2);
            drawHandle.SetTransform(worldMatrix);

            DrawTiles(drawHandle, gridBounds, index, tiles, visuals, grid.TileSize, textures);
        }

        if (visuals.SpaceTiles == null)
            return;

        gridBounds = Matrix3.Invert(visuals.SpaceMatrix).TransformBox(worldBounds).Enlarged(2);
        drawHandle.SetTransform(visuals.SpaceMatrix);

        DrawTiles(drawHandle, gridBounds, index, visuals.SpaceTiles, visuals, visuals.SpaceTileSize, textures);
    }

    private void DrawTiles(
        DrawingHandleWorld drawHandle,
        Box2 gridBounds,
        int index,
        Dictionary<int, List<Vector2i>> tileSets,
        ExplosionVisualsComponent visuals,
        ushort tileSize,
        ExplosionVisualsTexturesComponent textures)
    {
        for (var j = 0; j <= index; j++)
        {
            if (!tileSets.TryGetValue(j, out var tiles))
                continue;

            var frameIndex = (int) Math.Min(visuals.Intensity[j] / textures.IntensityPerState, textures.FireFrames.Count - 1);
            var frames = textures.FireFrames[frameIndex];

            foreach (var tile in tiles)
            {
                var centre = (tile + Vector2Helpers.Half) * tileSize;

                if (!gridBounds.Contains(centre))
                    continue;

                var texture = _robustRandom.Pick(frames);
                drawHandle.DrawTextureRect(texture, Box2.CenteredAround(centre, new Vector2(tileSize, tileSize)), textures.FireColor);
            }
        }
    }
}

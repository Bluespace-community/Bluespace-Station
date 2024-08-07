﻿using System.Numerics;
using Content.Shared.CardboardBox;
using Content.Shared.CardboardBox.Components;
using Content.Shared.Examine;
using Content.Shared.Movement.Components;
using Robust.Client.GameObjects;

namespace Content.Client.CardboardBox;

public sealed class CardboardBoxSystem : SharedCardboardBoxSystem
{
    [Dependency] private readonly EntityLookupSystem _entityLookup = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeNetworkEvent<PlayBoxEffectMessage>(OnBoxEffect);
    }

    private void OnBoxEffect(PlayBoxEffectMessage msg)
    {
        if (!TryComp<CardboardBoxComponent>(msg.Source, out var box))
            return;

        var xformQuery = GetEntityQuery<TransformComponent>();

        if (!xformQuery.TryGetComponent(msg.Source, out var xform))
            return;

        var sourcePos = xform.MapPosition;

        //Any mob that can move should be surprised?
        //God mind rework needs to come faster so it can just check for mind
        //TODO: Replace with Mind Query when mind rework is in.
        var mobMoverEntities = new HashSet<EntityUid>();

        //Filter out entities in range to see that they're a mob and add them to the mobMoverEntities hash for faster lookup
        foreach (var moverComp in _entityLookup.GetComponentsInRange<MobMoverComponent>(xform.Coordinates, box.Distance))
        {
            if (moverComp.Owner == msg.Mover)
                continue;

            mobMoverEntities.Add(moverComp.Owner);
        }

        //Play the effect for the mobs as long as they can see the box and are in range.
        foreach (var mob in mobMoverEntities)
        {
            if (!xformQuery.TryGetComponent(mob, out var moverTransform) || !ExamineSystemShared.InRangeUnOccluded(sourcePos, moverTransform.MapPosition, box.Distance, null))
                continue;

            var ent = Spawn(box.Effect, moverTransform.MapPosition);

            if (!xformQuery.TryGetComponent(ent, out var entTransform) || !TryComp<SpriteComponent>(ent, out var sprite))
                continue;

            sprite.Offset = new Vector2(0, 1);
            _transform.SetParent(ent, entTransform, mob);
        }
    }
}

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TargetToEntitySystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithAll<ChaserTag>()
            .ForEach((ref MoveData moveData, ref Rotation rot, in Translation pos, in TargetData targetData) =>
            {
                ComponentDataFromEntity<Translation> allTranslations = GetComponentDataFromEntity<Translation>(true);
                if (!(allTranslations.Exists(targetData.targetEntity))) 
                { return; }

                Translation targetPos = allTranslations[targetData.targetEntity];
                float3 dirToTarget = targetPos.Value - pos.Value;
                moveData.direction = dirToTarget;
                

            }).Schedule();
    }
}

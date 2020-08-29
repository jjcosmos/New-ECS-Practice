using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class WaveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Translation trans, ref WaveData waveData, in MoveSpeedData moveData) =>
        {
            float zPos = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveData.Value + trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPos);
        }).Run();

        return default;
    }
}

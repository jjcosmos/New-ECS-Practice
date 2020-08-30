using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


public class WaveSystem : SystemBase
{
    //dependencies applied in order
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation trans, in WaveData waveData, in MoveSpeedData moveData) =>
        {
            float zPos = waveData.amplitude * math.sin(elapsedTime * moveData.Value + trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPos);
        }).Schedule();

    }
}

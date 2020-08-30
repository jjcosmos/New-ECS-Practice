using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;

public class MoveForwardSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities
            .WithAny<AsteroidTag, ChaserTag>()
            .ForEach((ref Translation pos, in MoveData moveData, in Rotation rot) =>
        {
            float3 forwardDir = math.forward(rot.Value);
            pos.Value += forwardDir * moveData.speed * deltaTime;

        }).ScheduleParallel();
    }
}

using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;



public class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities
            .WithAll<PlayerTag>()
            .ForEach((ref Translation pos, in MoveData moveData) =>
        {
            float3 normDir = math.normalizesafe(moveData.direction);
            pos.Value += normDir * moveData.speed * deltaTime;
        }).Run();
    }
}

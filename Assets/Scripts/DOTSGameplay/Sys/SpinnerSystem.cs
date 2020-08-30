using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;


public class SpinnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities
            .WithAll<SpinnerTag>()
            .ForEach((ref Rotation rot, in MoveData moveData) =>
        {
            quaternion normalizerRot = math.normalizesafe(rot.Value);
            quaternion angleToRot = quaternion.AxisAngle(math.up(), moveData.turnSpeed * deltaTime);
            rot.Value = math.mul(normalizerRot, angleToRot);
        }).ScheduleParallel();
    }
}

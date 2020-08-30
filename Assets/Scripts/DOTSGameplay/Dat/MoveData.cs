using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MoveData : IComponentData
{
    public float3 direction;
    public float speed;
    [Range(0f,1f)]
    public float turnSpeed;
}

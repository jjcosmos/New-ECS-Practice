using Unity.Entities;
using System;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerInputSys : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MoveData moveData, in InputData inputData) =>
        {
            bool isRightPressed = Input.GetKey(inputData.rightKey);
            bool isLeftPressed = Input.GetKey(inputData.leftKey);
            bool isUpPressed = Input.GetKey(inputData.upKey);
            bool isDownPressed = Input.GetKey(inputData.downKey);

            moveData.direction.x = Convert.ToInt32(isRightPressed);
            moveData.direction.x -= Convert.ToInt32(isLeftPressed);
            moveData.direction.z = Convert.ToInt32(isUpPressed);
            moveData.direction.z -= Convert.ToInt32(isDownPressed);

        }).Run();

    }
}

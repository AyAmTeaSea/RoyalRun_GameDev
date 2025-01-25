using System;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] private float changeSpeed = 3f;
    
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(changeSpeed);
    }
}

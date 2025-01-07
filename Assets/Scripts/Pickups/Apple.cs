using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeChunkMoveSpeedAmount = 3f;
    LevelGenerator levelGen;

    public void Init(LevelGenerator levelGen)
    {
        this.levelGen = levelGen;
    }
    protected override void OnPickup()
    {
        levelGen.ChangeChunkMoveSpeed(adjustChangeChunkMoveSpeedAmount);
    }
}

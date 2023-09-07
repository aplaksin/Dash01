using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridGenerator
{
    void BuildGrid(Vector3 scaleVector, int gridHeight, int gridWidth, List<Vector2> blocksList, GameObject player, float cellSpace = 0.0f)
    
}

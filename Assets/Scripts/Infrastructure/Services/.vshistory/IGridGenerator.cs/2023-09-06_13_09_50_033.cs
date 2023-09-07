using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridGenerator
{
    void BuildGrid(List<Vector2> blocksList, Dictionary<Vector2, Vector3> cellPositionByCoords, Dictionary<Vector2, Vector3> blocksByCoords, float cellSpace = 0.0f);
    
}

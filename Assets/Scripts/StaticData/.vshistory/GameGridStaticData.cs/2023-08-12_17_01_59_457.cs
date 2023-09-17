
using UnityEngine;

[CreateAssetMenu(fileName = "GameGridData", menuName = "StaticData/GameGrid")]
public class GameGridStaticData : ScriptableObject
{
    
    public int GridWidth = 7;
    public int GridHeight = 4;
    public float CellSpace = 0.2f;
    public Vector2[] BlocksCoords;


}

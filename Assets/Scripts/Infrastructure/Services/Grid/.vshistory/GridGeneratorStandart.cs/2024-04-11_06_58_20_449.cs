using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneratorStandart : IGridGenerator
{
    private IAssetProvider _assetProvider;
    //private GameObject _player;
    private int _gridHeight;
    private int _gridWidth;
    private Vector3 _scaleVector;
    private Vector2 _padding;

    private List<GameObject> _cellList = new List<GameObject>();
    private Vector2 _startExpandPosition = Vector2.zero;


    public GridGeneratorStandart(IAssetProvider assetProvider, int gridHeight, int gridWidht, Vector2 padding, Vector3 scaleVector)
    {
        _assetProvider = assetProvider;
        _gridHeight = gridHeight;
        _gridWidth = gridWidht;
        _scaleVector = scaleVector;
        _padding = padding;
    }

    public void BuildGrid(List<Vector2> blocksList, 
                            Dictionary<Vector2, Vector3>  cellPositionByCoords,
                            Dictionary<Vector2, GameObject>  blocksByCoords, 
                            Dictionary<Vector2, Vector3> blocksCoords,
                            float cellSpace = 0.0f)
    {

        GameObject grid = _assetProvider.Instantiate(AssetPath.GridPath);
        //float positionByScalePointerVertical = _padding.y;
        float positionByScalePointerVertical = _padding.y;
        for (int i = 0; i < _gridHeight; i++)
        {
            float positionByScalePointerHorizontal = _padding.x;
            //float positionByScalePointerHorizontal = 0;

            for (int j = 0; j < _gridWidth; j++)
            {
                Vector2 currentCoords = new Vector2(j, i);

                GameObject prefab;
                //TODO clear comments
                if (blocksList.Contains(currentCoords))
                {
                    //prefab = _blockPrefab; 0.6318452
                    prefab = _assetProvider.Instantiate(AssetPath.BlockPath);
                }
                else
                {
                    prefab = _assetProvider.Instantiate(AssetPath.CellPath);
                }

                //var cell = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                var cell = prefab;
                cell.name = $"{cell.name}-{j}-{i}";
                cell.transform.localScale = new Vector3(cell.transform.localScale.x * _scaleVector.x, cell.transform.localScale.y * _scaleVector.y, cell.transform.localScale.z * _scaleVector.z);
                //cell.transform.localScale = _scaleVector;
                
                cell.transform.position = new Vector3(
                    positionByScalePointerHorizontal + _scaleVector.x / 2 ,
                    positionByScalePointerVertical + _scaleVector.y / 2,
                    0);
                cellPositionByCoords.Add(currentCoords, cell.transform.position);

                if (blocksList.Contains(currentCoords))
                {
                    blocksByCoords.Add(currentCoords, cell);
                    blocksCoords.Add(currentCoords, cell.transform.position);
                }

                cell.transform.SetParent(grid.transform);
                _cellList.Add(cell);

                positionByScalePointerHorizontal += _scaleVector.x + cellSpace;
            }

            positionByScalePointerVertical += _scaleVector.y + cellSpace;
        }

        Vector3 start;
        cellPositionByCoords.TryGetValue(new Vector2(0, _gridWidth%2), out start);
        _startExpandPosition = start;
    }

    private void ExpandGrid()
    {
        List<CellExpandAnimator> cellExpandAnimators = new List<CellExpandAnimator>(_cellList.Count);

        for (int i = 0; i < _cellList.Count; i++)
        {
            GameObject cell = _cellList[i];
            CellExpandAnimator cellExpandAnimator = cell.GetComponent<CellExpandAnimator>();
            cellExpandAnimator.TargetPosition = cell.transform.position;
            cellExpandAnimator.ExpandSpeed = 5f;
            cell.transform.position = _startExpandPosition;

            cellExpandAnimators.Add(cellExpandAnimator);
        }

        for (int i = 0; i < cellExpandAnimators.Count; i++)
        {
            cellExpandAnimators[i].StartExpand();
        }
    }
}

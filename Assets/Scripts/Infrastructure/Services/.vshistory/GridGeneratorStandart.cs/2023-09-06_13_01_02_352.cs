using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneratorStandart : IGridGenerator
{
    private IAssetProvider _assetProvider;

    public GridGeneratorStandart(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void BuildGrid(Vector3 scaleVector, int gridHeight, int gridWidth, List<Vector2> blocksList, GameObject player, float cellSpace = 0.0f)
    {

        GameObject grid = _assetProvider.Instantiate(AssetPath.GridPath);
        float positionByScalePointerVertical = 0.0f;
        for (int i = 0; i < gridHeight; i++)
        {
            float positionByScalePointerHorizontal = 0.0f;

            for (int j = 0; j < gridWidth; j++)
            {
                Vector2 currentCoords = new Vector2(j, i);

                GameObject prefab;

                if (blocksList.Contains(currentCoords))
                {
                    //prefab = _blockPrefab;
                    prefab = _assetProvider.Instantiate(AssetPath.BlockPath);
                }
                else
                {
                    prefab = _assetProvider.Instantiate(AssetPath.CellPath);
                }

                //var cell = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                var cell = prefab;
                cell.name = $"{cell.name}-{j}-{i}";
                //cell.transform.localScale = new Vector3(cell.transform.localScale.x * scaleVector.x, cell.transform.localScale.y * scaleVector.y, cell.transform.localScale.z * scaleVector.z);
                cell.transform.localScale = scaleVector;
                cell.transform.position = new Vector3(positionByScalePointerHorizontal + cell.transform.localScale.x / 2, positionByScalePointerVertical + cell.transform.localScale.y / 2, 0);
                _cellPositionByCoords.Add(currentCoords, cell.transform.position);

                if (blocksList.Contains(currentCoords))
                {
                    _blocksByCoords.Add(currentCoords, cell);
                    _blocksCoords.Add(currentCoords, cell.transform.position);
                }

                cell.transform.SetParent(grid.transform);

                positionByScalePointerHorizontal += scaleVector.x + cellSpace;
            }

            positionByScalePointerVertical += scaleVector.y + cellSpace;
        }
        player.transform.position = _cellPositionByCoords[player.transform.position];
    }
}

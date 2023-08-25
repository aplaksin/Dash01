using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;


public class SwipeDetection
{

    private float minSwipeDistance = 0.2f;
    private float maxSwipeTime = 1f;
    private float _directionTrashold = 0.9f;

    public Vector2 DetectSwipe(Vector2 startPosition, float startTime, Vector2 endPosition, float endTime)
    {
        Vector2 directionVector = Vector2.zero;

        if (Vector3.Distance(startPosition, endPosition) >= minSwipeDistance 
            && endTime - startTime <= maxSwipeTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 2f);
            //Debug.Log("swipe");
            Vector2 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            directionVector = SwipeDirection(direction2D);
            //CalcMovePlayerPosition(directionVector);
            
        }

        return SwipeDirection(directionVector);

    }

    private Vector2 SwipeDirection(Vector2 direction)
    {

        Vector2 directionVector = Vector2.zero;

        if (Vector2.Dot(Vector2.up, direction) > _directionTrashold)
        {
            //Debug.Log("Swipe UP");
            directionVector = Vector2.up;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionTrashold)
        {
            //Debug.Log("Swipe DOWN");
            directionVector = Vector2.down;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionTrashold)
        {
            //Debug.Log("Swipe LEFT");
            directionVector = Vector2.left;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionTrashold)
        {
            //Debug.Log("Swipe RIGHT");
            directionVector = Vector2.right;
        }

        return directionVector;
    }

    /*    private void CalcMovePlayerPosition(Vector2 direction)
        {
            Vector2 currentDirection = direction;
            Vector3 moveTarget = Vector3.zero;

            if(!_isMoving && currentDirection != Vector2.zero)
            {
                while(_cellPositionByCoords.ContainsKey(_currentPlayerCoords+direction) && !_blocksByCoords.ContainsKey(_currentPlayerCoords + direction))
                {
                    currentDirection = _currentPlayerCoords + direction;
                    _currentPlayerCoords = currentDirection;
                    moveTarget = _cellPositionByCoords[currentDirection];
                    //Debug.Log(moveTarget);
                    if(_blocksByCoords.ContainsKey(_currentPlayerCoords + direction))
                    {
                        _fireBlock = _blocksByCoords[currentDirection+direction];
                    }
                }

                _movePosition = moveTarget;
            }



        }*/

    

}

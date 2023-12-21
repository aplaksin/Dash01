using UnityEngine;



public class SwipeDetection
{

    private float minSwipeDistance = 0.2f;
    private float maxSwipeTime = 1f;
    private float _directionTrashold = 0.9f;//0.9f

    public Vector2 DetectSwipe(Vector2 startPosition, float startTime, Vector2 endPosition, float endTime)
    {
        Vector2 directionVector = Vector2.zero;

        if (Vector3.Distance(startPosition, endPosition) >= minSwipeDistance 
            && endTime - startTime <= maxSwipeTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 2f);

            Vector2 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            directionVector = SwipeDirection(direction2D);

            
        }

        return SwipeDirection(directionVector);

    }

    private Vector2 SwipeDirection(Vector2 direction)
    {

        Vector2 directionVector = Vector2.zero;

        if (Vector2.Dot(Vector2.up, direction) > _directionTrashold)
        {
            
            directionVector = Vector2.up;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionTrashold)
        {
            
            directionVector = Vector2.down;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionTrashold)
        {
            
            directionVector = Vector2.left;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionTrashold)
        {
            
            directionVector = Vector2.right;
        }

        return directionVector;
    }

}

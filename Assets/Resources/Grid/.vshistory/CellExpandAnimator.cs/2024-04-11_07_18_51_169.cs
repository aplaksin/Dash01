using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellExpandAnimator : MonoBehaviour
{
    public Vector3 TargetPosition = Vector3.zero;
    public float ExpandSpeed = 1f;
    
    private bool _readyToExpand = false;
    private Vector3 _verticalTaget;
    private Vector3 _horizontalTaget;
    private bool _isHorisontalStage = false;
    // Start is called before the first frame update
    private void Start()
    {
        _verticalTaget = new Vector3(transform.position.x, TargetPosition.y, transform.position.z);
        _horizontalTaget = new Vector3(TargetPosition.x, TargetPosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       if(_readyToExpand)
        {
            Expand();
        }
    }

    public void StartExpand()
    {
        _readyToExpand = true;
    }

    private void Expand()
    {
        if(!_isHorisontalStage && Vector3.Distance(transform.position, _verticalTaget) > 0.00f)
        {
            var step = ExpandSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _verticalTaget, step);
            _isHorisontalStage = true;
        }
        else
        {
            var step = ExpandSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _horizontalTaget, step);

            Debug.Log("_horizontalTaget " + _horizontalTaget + " - TargetPosition " + TargetPosition + " pos " + transform.position);

            if(Vector3.Distance(transform.position, _horizontalTaget) <= 0.001f)
            {
                _readyToExpand = false;
            }
        }



    }
}

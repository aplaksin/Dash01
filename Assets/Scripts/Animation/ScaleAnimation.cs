using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 9f;
    [SerializeField] private float scaleFactor = 1.05f;
    [SerializeField] bool isLooped = false;

    private Vector3 _initionalScale;
    private bool _isReversed = false;
    private Vector3 _finalScale = Vector3.zero;

    private void Start()
    {
        _initionalScale = transform.localScale;
        _finalScale = transform.localScale * scaleFactor;
    }
    // Update is called once per frame
    private void Update()
    {

        Animate();

    }

    private void Animate()
    {
        if (!_isReversed)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _finalScale, animationSpeed * Time.deltaTime);
            if(transform.localScale == _finalScale)
            {
                _isReversed = true;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _initionalScale, animationSpeed * Time.deltaTime);
            if (transform.localScale == _initionalScale)
            {
                _isReversed = false;
            }
        }
        
    }
}

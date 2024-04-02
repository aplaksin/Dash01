using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float scaleFactor = 1f;
    [SerializeField] bool isLooped = false;

    private Vector3 _initionalScale;
    private bool _isReversed = false;
    private Vector3 _finalScale = Vector3.zero;
    private int count = 0;
    private void Start()
    {
        _initionalScale = transform.localScale;
        _finalScale = transform.localScale * scaleFactor;
    }
    // Update is called once per frame
    void Update()
    {
        do
        {
            Animate();
            count++;
        } while (count < 10);
    }

    private void Animate()
    {
        if (!_isReversed)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _finalScale, duration * Time.deltaTime);
            if(transform.localScale == _finalScale)
            {
                _isReversed = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _initionalScale, duration * Time.deltaTime);
            if (transform.localScale == _initionalScale)
            {
                _isReversed = true;
            }
        }
        
    }
}

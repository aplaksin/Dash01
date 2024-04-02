using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float scalefactor = 1f;
    [SerializeField] bool isLooped = false;

    private Vector3 _initionalScale;
    private bool _isReversed = true;
    private Vector3 _finalScale = Vector3.zero;
    private void Start()
    {
        _initionalScale = transform.localScale;
        _finalScale = transform.localScale * scalefactor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Animate()
    {
        if (!_isReversed)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _finalScale, duration * Time.deltaTime);
            if(transform.localScale == )
        }
        

        if()
    }
}

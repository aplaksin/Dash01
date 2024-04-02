using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float scalefactor = 1f;

    private Vector3 _initionalScale;

    private void Start()
    {
        _initionalScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Animate()
    {

    }
}

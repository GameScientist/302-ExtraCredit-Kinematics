using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float wiggleOffset;
    public float wiggleTimeScale;
    public float wiggleAmp;
    public Bone parent;

    // Start is called before the first frame update
    void Start()
    {
        wiggleOffset = Random.Range(0, 6.28f);
        wiggleAmp = Random.Range(.5f, 3f);
        wiggleTimeScale = Random.Range(.25f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.worldEnd;
        transform.Rotate(Mathf.Sin((Time.time + wiggleOffset) * wiggleTimeScale) * wiggleAmp * transform.forward);
    }
}

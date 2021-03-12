using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    Vector3 worldStart;
    Vector3 worldEnd;
    Bone parent;
    float worldDir;
    float dir;
    float wiggleOffset;
    float wiggleAmp;
    float wiggleTimeScale;
    float mag;

    private void Start()
    {
        parent = GetComponentInParent<Bone>();
        dir = Random.Range(-1, 1);
        wiggleOffset = Random.Range(0, 6.28f);
        wiggleAmp = Random.Range(.5f, 2);
        wiggleTimeScale = Random.Range(.25f, 1);
        mag = Random.Range(0.5f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Calc();
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, new Vector3(worldStart.x, worldStart.y, worldStart.z));
        line.SetPosition(1, new Vector3(worldEnd.x, worldEnd.y, worldEnd.z));
    }

    private void Calc()
    {
        if (transform.parent != null)
        {
            worldStart = parent.worldEnd;
            worldDir = parent.worldDir + dir;
        }
        else
        {

            worldStart = new Vector3(1, 1, 1);
            worldDir = dir;
        }

        worldDir += Mathf.Sin((Time.time + wiggleOffset) * wiggleTimeScale) * wiggleAmp;

        Vector3 localEnd = new Vector3(mag * Mathf.Cos(worldDir), mag * Mathf.Sin(worldDir));
        localEnd *= mag;

        worldEnd = worldStart + localEnd;
    }
}

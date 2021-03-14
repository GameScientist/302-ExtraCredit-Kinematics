using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public LineRenderer line;
    public float dir;
    public Vector3 worldStart;
    public Vector3 worldEnd;
    public float worldDir;
    public Bone parent;
    public float wiggleOffset;
    public float wiggleTimeScale;
    public float wiggleAmp;
    public float mag;
    public int chainLength;
    public bool bonesAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (chainLength == 0) chainLength = 5;
        line = GetComponentInChildren<LineRenderer>();
        dir = Random.Range(-1, 2);
        wiggleOffset = Random.Range(0, 6.28f);
        wiggleAmp = Random.Range(.5f, 3f) / chainLength;
        wiggleTimeScale = Random.Range(.25f, 1);
        mag = Random.Range(0.6f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            worldStart = parent.worldEnd;
            worldDir = parent.worldDir + dir;
        }
        else
        {
            worldStart = new Vector3(0, 0, 0);
            worldDir = 1.57f;
            chainLength = 5;
        }
        worldDir += Mathf.Sin((Time.time + wiggleOffset) * wiggleTimeScale) * wiggleAmp;

        Vector3 localEnd = new Vector3(mag * Mathf.Cos(worldDir), mag * Mathf.Sin(worldDir));
        localEnd *= mag * chainLength;

        worldEnd = worldStart + localEnd;

        line.SetPosition(0, new Vector3(worldStart.x, worldStart.y, worldStart.z));
        line.SetPosition(1, new Vector3(worldEnd.x, worldEnd.y, worldEnd.z));

        if (chainLength > 1 && bonesAdded == false)
        {
            int numOfChildren = (int)Random.Range(2, 4);

            for (int i = 0; i < numOfChildren; i++)
            {
                Bone newBone = Instantiate(gameObject.GetComponent<Bone>());
                newBone.parent = this;
                newBone.chainLength = chainLength - 1;
            }
            bonesAdded = true;
        }
    }
}

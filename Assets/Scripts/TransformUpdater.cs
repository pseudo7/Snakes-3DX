using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUpdater : MonoBehaviour
{
    public TransformCollector collector;

    void Update()
    {
        TransformInfo transformInfo = collector.GetTransformInfo();
        transform.SetPositionAndRotation(transformInfo.Position, transformInfo.Rotation);
    }
}

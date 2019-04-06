using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCollector : MonoBehaviour
{
    TransformInfo[] Transforms;
    int currentIndex;

    void Start()
    {
        Transforms = new TransformInfo[Constants.COLLECTION_COUNT];
    }

    public TransformInfo GetTransformInfo()
    {
        return Transforms[currentIndex];
    }

    void AddInfo(Vector3 position, Quaternion rotation)
    {
        Transforms[currentIndex++ % Constants.COLLECTION_COUNT] = new TransformInfo(position, rotation);
        currentIndex %= Constants.COLLECTION_COUNT;
    }

    void Update()
    {
        AddInfo(transform.position, transform.rotation);
    }
}
public struct TransformInfo
{
    public Vector3 Position { private set; get; }
    public Quaternion Rotation { private set; get; }

    public TransformInfo(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }
}
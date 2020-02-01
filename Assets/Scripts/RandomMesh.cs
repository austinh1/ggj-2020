using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMesh : MonoBehaviour
{
    [SerializeField]
    private List<Model> listOfModels = new List<Model>();

    // Start is called before the first frame update
    void Start()
    {
        var modelIndex = Random.Range(0, listOfModels.Count);
        var currentModel = listOfModels[modelIndex];
        GetComponent<MeshFilter>().mesh = currentModel.mesh;
        GetComponent<MeshRenderer>().material = currentModel.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    class Model
    {
        public Mesh mesh;
        public Material material;
    }
}

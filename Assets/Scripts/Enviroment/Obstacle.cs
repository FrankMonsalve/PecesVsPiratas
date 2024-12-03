using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _obstacle;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] string _tag;

    private void Awake() {
        _obstacle = gameObject;
        Setup();
    }

    public void Setup()
    {
        _obstacle.layer = _layerMask;
        _obstacle.tag = _tag;
    }
}

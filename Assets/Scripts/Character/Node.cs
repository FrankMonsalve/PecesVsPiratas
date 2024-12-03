using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Node : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Vector3 _Position;
    [SerializeField] private int _cost;

    [SerializeField] bool _isAvailable;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] string _tagObstacle;
    private Character _enemy;
    private Tilemap _tilemap;
    public LayerMask  LMask => _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _character = transform.GetComponentInParent<Character>();
        _tilemap = GameTurnManager.Instance.Tilemap;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public Vector3Int Position() => transform.position;

    public void Action()
    {
        Debug.Log($"Action | Node: {_isAvailable}");

        if (_enemy != null)
        {
            Debug.Log($"Attack");
            _character.Attack(_enemy);

            if (!_enemy.IsAlive)
            {
                MoveCharacter();
            }
        }
        else
        {
            if (_isAvailable)
            {
                MoveCharacter();
            }

        }
    }

    public void MoveCharacter()
    {
        Debug.Log("Entra al nodo");
        Vector3Int cellPos = _tilemap.WorldToCell(_Position);
        _character.Movement.MoveToCell(cellPos, _tilemap, _cost);
        //Debug.Log($"Character {_selectedCharacter.name} Se movio a la casilla {cellPos}");
    }

    public void AttackCharacter()
    {

    }

    public void CheckNode()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero, 1f, LMask);

        Debug.Log(hit.collider);

        if (hit.collider == null)
        {
            _spriteRenderer.color = Color.white;
            _isAvailable = true;
            ResetNode();
            Show();
            return;
        }

        Debug.Log(hit.collider.gameObject.tag +  " gameobject");
        Debug.Log(_tagObstacle);

        Debug.Log(hit.collider.gameObject.CompareTag(_tagObstacle));
        if(hit.collider.gameObject.CompareTag(_tagObstacle))
        {
            _isAvailable = false;
            Hiden();
            return;
        }

        Character character = hit.collider.GetComponent<Character>();

        if (character != null)
        {
             if (IsAlly(character))
                {
                    _spriteRenderer.color = Color.green;

                    _isAvailable = false;
                    Show();
                    return;
                }
                else
                {

                    _enemy = character;
                    _spriteRenderer.color = Color.red;

                    _isAvailable = false;
                    Show();
                    return;
                }

        }
    }

    public bool IsAlly(Character character)
    {
        return GameTurnManager.Instance.IsCharacterInCurrentPlayer(character);
    }

    public bool IsObstacle()
    {
        return false;
    }

    public void Inicializate()
    {
        _Position = transform.position;
        _layerMask = LayerMask.GetMask("InteractableLayer");
        CheckNode();
    }


    public void Hiden()
    {
        //_spriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }
    public void Show()
    {
        // _spriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }

    public void ResetNode()
    {
        _enemy = null;
    }

}

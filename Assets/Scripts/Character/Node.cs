using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Vector3 _Position;
    [SerializeField] private int _cost;

    [SerializeField] bool isAvailable;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Character _enemy;
    private Tilemap _tilemap;

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
        if(_enemy != null)
        {
            _enemy.TakeDamage(_character.Attack);
            if(!_enemy.IsAlive)
            {
                MoveCharacter();
            }
        }else
        {
            MoveCharacter();
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
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero);

        Debug.Log(hit.collider);

        if(hit.collider == null)
        {
            _spriteRenderer.color = Color.white;
            return;
        }

        if (hit.collider.TryGetComponent<Character>(out Character character)) 
        {
            if(IsAlly(character))
            {
                _spriteRenderer.color = Color.green;
                return;
            }else
            {
                _enemy = character;
                _spriteRenderer.color = Color.red;
                return;
            }
        }
    }

    public bool IsAlly(Character character)
    {
        return GameTurnManager.Instance.IsCharacterInCurrentPlayer(character);
    }

    public void Inicializate()
    {
        _Position = transform.position;
        CheckNode();
        Show();
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

}

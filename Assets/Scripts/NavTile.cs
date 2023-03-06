using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTile : MonoBehaviour
{
    public bool navigable = true;
    public bool canMoveUp = false;
    public bool canMoveDown = false;
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public int x = 0;
    public int y = 0;
    public Sprite roadRight;
    public Sprite roadLeft;
    public Sprite roadUp;
    public Sprite roadDown;
    public Sprite cross4;
    public Sprite crossTRight3;
    public Sprite crossTDown3;
    public Sprite crossTLeft3;
    public Sprite crossTUp3;
    public Sprite turnDownRight;
    public Sprite turnDownLeft;
    public Sprite turnUpRight;
    public Sprite turnUpLeft;
    public Sprite block;
    public Sprite start;
    public Sprite end;
    public char TileType = '-';
    //0 = normal, 1 = start, 2 = exit
    public int specialNode = 0;
    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetCorrectSprite();

        if(specialNode > 0)
        {
            if(specialNode == 1)
            {
                _spriteRenderer.sprite = start;
            }
            else
            {
                _spriteRenderer.sprite = end;
            }
        }
    }

    void  SetCorrectSprite()
    {
        switch (TileType)
        {
            case 'X':
                _spriteRenderer.sprite = block;
                navigable = false;
                break;
            case '-':
                _spriteRenderer.sprite = roadLeft;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                break;
            case '|':
                _spriteRenderer.sprite = roadUp;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                break;
            case '+':
                _spriteRenderer.sprite = cross4;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                canMoveRight = true;
                break;
            case 'E':
                _spriteRenderer.sprite = crossTRight3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveRight = true;
                break;
            case 'T':
                _spriteRenderer.sprite = crossTDown3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveDown = true;
                break;
            case '3':
                _spriteRenderer.sprite = crossTLeft3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                break;
            case 'W':
                _spriteRenderer.sprite = crossTUp3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveUp = true;
                break;
            case 'C':
                _spriteRenderer.sprite = turnDownRight;
                navigable = true;
                canMoveDown = true;
                canMoveRight = true;
                break;
            case 'Z':
                _spriteRenderer.sprite = turnDownLeft;
                navigable = true;
                canMoveDown = true;
                canMoveLeft = true;
                break;
            case 'U':
                _spriteRenderer.sprite = turnUpRight;
                navigable = true;
                canMoveUp = true;
                canMoveRight = true;
                break;
            case 'V':
                _spriteRenderer.sprite = turnUpLeft;
                navigable = true;
                canMoveUp = true;
                canMoveLeft = true;
                break;
        }
    }

}

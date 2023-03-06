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
    public Vector2 identifyXY;
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

    SpriteRenderer _spriteRenderer;
    int changeType = 0;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetCorrectSprite();
    }

    void  SetCorrectSprite()
    {
        switch (TileType)
        {
            case 'X':
                _spriteRenderer.sprite = block;
                navigable = false;
                changeType = 0;
                break;
            case '-':
                _spriteRenderer.sprite = roadLeft;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                changeType = 1;
                break;
            case '|':
                _spriteRenderer.sprite = roadUp;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                changeType = 2;
                break;
            case '+':
                _spriteRenderer.sprite = cross4;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                canMoveRight = true;
                changeType = 3;
                break;
            case 'E':
                _spriteRenderer.sprite = crossTRight3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveRight = true;
                changeType = 4;
                break;
            case 'T':
                _spriteRenderer.sprite = crossTDown3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveDown = true;
                changeType = 5;
                break;
            case '3':
                _spriteRenderer.sprite = crossTLeft3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                changeType = 6;
                break;
            case 'W':
                _spriteRenderer.sprite = crossTUp3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveUp = true;
                changeType = 7;
                break;
            case 'C':
                _spriteRenderer.sprite = turnDownRight;
                navigable = true;
                canMoveDown = true;
                canMoveRight = true;
                changeType = 8;
                break;
            case 'Z':
                _spriteRenderer.sprite = turnDownLeft;
                navigable = true;
                canMoveDown = true;
                canMoveLeft = true;
                changeType = 9;
                break;
            case 'U':
                _spriteRenderer.sprite = turnUpRight;
                navigable = true;
                canMoveUp = true;
                canMoveRight = true;
                changeType = 10;
                break;
            case 'V':
                _spriteRenderer.sprite = turnUpLeft;
                navigable = true;
                canMoveUp = true;
                canMoveLeft = true;
                changeType = 11;
                break;
        }
    }

    public void ChangeTileType()
    {
        changeType++;

        if(changeType > 11)
        {
            changeType = 0;
        }

        switch (changeType)
        {
            case 0:
                _spriteRenderer.sprite = block;
                navigable = false;
                break;
            case 1:
                _spriteRenderer.sprite = roadLeft;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                break;
            case 2:
                _spriteRenderer.sprite = roadUp;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                break;
            case 3:
                _spriteRenderer.sprite = cross4;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                canMoveRight = true;
                break;
            case 4:
                _spriteRenderer.sprite = crossTRight3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveRight = true;
                break;
            case 5:
                _spriteRenderer.sprite = crossTDown3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveDown = true;
                break;
            case 6:
                _spriteRenderer.sprite = crossTLeft3;
                navigable = true;
                canMoveUp = true;
                canMoveDown = true;
                canMoveLeft = true;
                break;
            case 7:
                _spriteRenderer.sprite = crossTUp3;
                navigable = true;
                canMoveLeft = true;
                canMoveRight = true;
                canMoveUp = true;
                break;
            case 8:
                _spriteRenderer.sprite = turnDownRight;
                navigable = true;
                canMoveDown = true;
                canMoveRight = true;
                break;
            case 9:
                _spriteRenderer.sprite = turnDownLeft;
                navigable = true;
                canMoveDown = true;
                canMoveLeft = true;
                break;
            case 10:
                _spriteRenderer.sprite = turnUpRight;
                navigable = true;
                canMoveUp = true;
                canMoveRight = true;
                break;
            case 11:
                _spriteRenderer.sprite = turnUpLeft;
                navigable = true;
                canMoveUp = true;
                canMoveLeft = true;
                break;
        }
    }
}

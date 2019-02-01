using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private Vector2 PosBegan;
    private Vector2 PosEnded;
    private Vector2 PosSum;
    public Text Level;
    private int _level;
    public Text Score;
    private int _score;
    private int Figure;
    private int RejectMoveAsideA;
    private int RejectMoveAsideD;
    private int[,] Tmp = new int[4, 4];
    private bool MoveASaide;
    private int Xtemp;
    private int Ytemp;
    private float Speed;
    private float SpeedTmp;
    public GameObject PrfCube;
    public GameObject[,] allCube;
    public int[,] pole = new int[,]
    {
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,0,0,0,0},
    };
    // Start is called before the first frame update
    void Start()
    {
        allCube = new GameObject[16, 8];
        SpeedTmp = Speed;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                allCube[y, x] = GameObject.Instantiate(PrfCube);
                allCube[y, x].transform.position = new Vector3(x, 15 - y, 0);
            }

        }
        Xtemp = 2;
        Ytemp = 0;

        Speed = 1f;
        _level = 0;

        _score = 0;

        Create();

    }

    void Update()
    {
        CheckTouch();

        if (_level > 0)
        {
            Speed = 1f / _level;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            MoveAside();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Rotate();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
        
        if (SpeedTmp > 0)
        {
            SpeedTmp -= Time.deltaTime;
        }
        else
        {
            MoveDown();
            SpeedTmp = Speed;
        }

        Fill();

        CheckSpeedLevel();
    }


    void Create()
    {
        Xtemp = 2;
        Ytemp = 0;

        Figure = Random.Range(1, 7);

        switch (Figure)
        {
            case 1 :
                pole[1, 2] = 1;
                pole[1, 3] = 1;
                pole[1, 4] = 1;
                pole[1, 5] = 1;
                break;

            case 2 :
                pole[0, 3] = 1;
                pole[1, 3] = 1;
                pole[1, 4] = 1;
                pole[1, 5] = 1;
                break;

            case 3:
                pole[0, 5] = 1;
                pole[1, 3] = 1;
                pole[1, 4] = 1;
                pole[1, 5] = 1;
                break;

            case 4:
                pole[2, 3] = 1;
                pole[1, 3] = 1;
                pole[1, 4] = 1;
                pole[2, 4] = 1;
                break;

            case 5:
                pole[2, 2] = 1;
                pole[2, 3] = 1;
                pole[1, 3] = 1;
                pole[1, 4] = 1;
                break;

            case 6:
                pole[2, 2] = 1;
                pole[2, 3] = 1;
                pole[1, 3] = 1;
                pole[2, 4] = 1;
                break;

            case 7:
                pole[1, 2] = 1;
                pole[2, 3] = 1;
                pole[1, 3] = 1;
                pole[2, 4] = 1;
                break;
        }

    }

    void Fill()
    {
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (pole[y, x] == 0)
                {
                    allCube[y, x].SetActive(true);
                    allCube[y, x].GetComponent<SpriteRenderer>().material.color = Color.black;
                }
                if (pole[y, x] == 1)
                {
                    allCube[y, x].SetActive(true);
                    allCube[y, x].GetComponent<SpriteRenderer>().material.color = Color.white;
                }
                if (pole[y, x] == 2)
                {
                    allCube[y, x].SetActive(true);
                    allCube[y, x].GetComponent<SpriteRenderer>().material.color = Color.red;
                }
            }
        }
    }
    void MoveDown()
    {   

        for (int y = 15; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                if (pole[y, x] == 1 && y == 15)
                {
                    for (int i = 15; i >= 0; i--)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pole[i, j] == 1)
                            {
                                pole[i, j] = 2;
                            }

                        }

                    }
                    CheckLine();
                    CheckGameOver();
                    Create();
                    return;
                }

                if (pole[y, x] == 1 && pole[y + 1, x] == 2)
                {
                    for (int i = 15; i >= 0; i--)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pole[i, j] == 1)
                            {
                                pole[i, j] = 2;
                            }

                        }

                    }
                    CheckLine();
                    CheckGameOver();
                    Create();
                    return;
                }
            }
        }
        for (int y = 15; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                if (pole[y, x] == 1 && y != 15)
                {
                    pole[y, x] = 0;
                    pole[y + 1, x] = 1;
                }
            }
        }
        Ytemp += 1;
    }

    void MoveAside()
    {
        for (int y = 15; y >= 0; y--)
        {
            for (int x = 6; x >= 0; x--)
            {
                if (pole[y, x] == 1 && pole[y, x + 1] == 2)
                {
                    RejectMoveAsideD = 1;
                }
            }
        }
        for (int y = 15; y >= 0; y--)
        {
            for (int x = 1; x < 8; x++)
            {
                if (pole[y, x] == 1 && pole[y, x - 1] == 2)
                {
                    RejectMoveAsideA = 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && RejectMoveAsideD == 0)
        {
            for (int y = 15; y >= 0; y--)
            {
                if (pole[y, 7] == 1)
                {
                    return;
                }

            }
            for (int y = 15; y >= 0; y--)
            {
                for (int x = 7; x >= 0; x--)
                {
                    if (pole[y, x] == 1)
                    {
                        pole[y, x] = 0;
                        pole[y, x + 1] = 1;
                        MoveASaide = true;
                    }
                }
            }
            if (MoveASaide)
            {
                Xtemp += 1;
                MoveASaide = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && RejectMoveAsideA == 0)
        {
            for (int y = 15; y >= 0; y--)
            {
                if (pole[y, 0] == 1)
                {
                    return;
                }
            }
            for (int y = 15; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (pole[y, x] == 1)
                    {
                        pole[y, x] = 0;
                        pole[y, x - 1] = 1;
                        MoveASaide = true;
                    }
                }
            }
            if (MoveASaide)
            {
                Xtemp -= 1;
                MoveASaide = false;
            }
        }
        RejectMoveAsideA = 0;
        RejectMoveAsideD = 0;
    }
    void Rotate()
    {

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Tmp[i, j] = 0;
            }
        }

        for (int y = Ytemp; y < Ytemp + 4; y++)
        {
            for (int x = Xtemp; x < Xtemp + 4; x++)
            {
                if (x < 0 || x > 7)
                {
                    return;
                }
                if (pole[y, x] == 1)
                {
                    Tmp[x - Xtemp, 3 - (y - Ytemp)] = pole[y, x];
                }

            }

        }
        for (int y = Ytemp; y < Ytemp + 4; y++)
        {
            for (int x = Xtemp; x < Xtemp + 4; x++)
            {
                if (Tmp[y - Ytemp, x - Xtemp] == 1 && pole[y, x] == 2)
                {
                    return;
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log($"Rotate is On");
            for (int y = Ytemp; y < Ytemp + 4; y++)
            {
                for (int x = Xtemp; x < Xtemp + 4; x++)
                {

                    if (pole[y, x] == 1)
                    {
                        pole[y, x] = 0;
                    }
                }
            }
            for (int y = Ytemp; y < Ytemp + 4; y++)
            {
                for (int x = Xtemp; x < Xtemp + 4; x++)
                {
                    if (Tmp[y - Ytemp, x - Xtemp] == 1)
                    {
                        pole[y, x] = Tmp[y - Ytemp, x - Xtemp];
                    }
                }
            }
        }

    }
    void CheckLine()
    {
        for (int y = 0; y < 16; y++)
        {
            if (SumLine(y, 7) == 16)
            {
                for (int x = 0; x < 8; x++)
                {
                    pole[y, x] = 0;
                    _score += 10;
                    Score.text = _score.ToString();
                }

                for (int i = y; i > 0; i--)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pole[i, j] == 0 && pole[i - 1, j] == 2)
                        {
                            pole[i, j] = 2;
                            pole[i - 1, j] = 0;
                        }
                    }
                }
            }
        }

    }

    public int SumLine(int y, int x)
    {
        if (x < 0)
        {
            return 0;
        }

        else
        {
            return pole[y, x] + SumLine(y, x - 1);
        }

    }

    void CheckGameOver()
    {
        for (int y=0; y < 3; y++)
        {
            for (int x=0; x<8; x++)
            {
                if(pole [y,x]==2)
                {
                    Debug.Log("Game Over");
                    _score = 0;
                    Score.text = _score.ToString();
                    for (int i = 0; i < 16; i++)
                    {
                        for(int j = 0; j < 8; j++)
                        {
                            pole[i, j] = 0;
                        }
                    }

                }
            }
        }
    }
    void CheckSpeedLevel()
    { if (_score > 0)
        {
            _level = Mathf.RoundToInt(_score / 1000);
            Debug.Log(_level);
        }
        Level.text = _level.ToString();
    }
   void CheckTouch()
    {
        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);

            if (myTouch.phase == TouchPhase.Began)
            {
                PosBegan = myTouch.position;
                Debug.Log(PosBegan);
            }
            
            if (myTouch.phase == TouchPhase.Ended)
            {
                PosEnded = myTouch.position;
                PosSum = PosEnded - PosBegan;
                if (Mathf.Abs(PosSum.x) > Mathf.Abs(PosSum.y))
                {
                    Debug.Log("Горизонталь");
                }
                else
                {
                    Debug.Log("Вертикаль");
                }
            }
        }
    }
}

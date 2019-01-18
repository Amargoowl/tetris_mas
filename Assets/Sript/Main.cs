using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private int[,] Tmp = new int[4, 4];
    private bool MoveASaide;
    public int Xtemp;
    public int Ytemp;
    public float Speed = 10f;
    private float SpeedTmp;
    public GameObject PrfCube;
    public GameObject[,] allCube;
    public int[,] pole = new int[,]
    {
   {0,0,0,0,0,0,0,0},
   {0,0,0,0,1,0,0,0},
   {0,0,1,1,1,0,0,0},
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
   {0,0,2,0,2,0,0,0},
    };
    // Start is called before the first frame update
    void Start()
    {
        allCube = new GameObject[16, 8];
        SpeedTmp = Speed;
        for (int y=0; y<16; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                allCube[y, x] = GameObject.Instantiate(PrfCube);
                allCube[y, x].transform.position = new Vector3(x, 15- y, 0);
            }

        }
        Xtemp = 2;
        Ytemp = 0;

    }

    void Update()
    {
        MoveAside();

        Rotate();

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
    }


    void Create()
    {
        Xtemp= 2;
        Ytemp= 0;

        pole[2, 2] = 1;
        pole[2, 3] = 1;
        pole[2, 4] = 1;
        pole[1, 4] = 1;
        return;
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
                if (pole[y, x] == 1 && y != 15)
                {
                    pole[y, x] = 0;
                    pole[y + 1, x] = 1;
                }
            }
        }
        Ytemp += 1;

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
                    Create();
                    return;
                }
            }
        }
    }

    void MoveAside()
    {
        if(Input.GetKeyDown(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.A))
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
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
    }
    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.W))
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
                    if (pole[y , x] == 1)
                    {
                        Tmp[x - Xtemp, 3 - (y - Ytemp)] = pole[y, x];
                        pole[y , x ] = 0;
                    }
                }
            }
            for (int y = Ytemp; y < Ytemp + 4; y++)
            {
                for (int x = Xtemp; x < Xtemp + 4; x++)
                {
                    pole[y, x] = Tmp[y - Ytemp, x - Xtemp];
                }
            }
        }
    }
}

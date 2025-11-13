using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 4
이름 : 배성훈
내용 : 수상한 어릿광대
    문제번호 : 33560번

    시뮬레이션, 구현 문제다.
    각 경우는 O(1)에 해결된다.
    그래서 시뮬레이션 방법이 유효하다.

    이에 문제대로 구현하고 시뮬레이션 돌리며 정답을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1962
    {

        static void Main1962(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int score = 0;
                int add = 1;
                int time = 0;
                int turn = 4;
                int[] ret = new int[4];

                for (int i = 0; i < n; i++)
                {

                    if (IsEnd()) 
                    { 
                        
                        Add();
                        Init();
                    }

                    Query(arr[i]);
                }

                for (int i = 0; i < 4; i++)
                {

                    Console.WriteLine(ret[i]);
                }

                void Query(int i)
                {

                    if (i == 1) Query1();
                    else if (i == 2) Query2();
                    else if (i == 3) Query3();
                    else if (i == 4) Query4();
                    else if (i == 5) Query5();
                    else if (i == 6) Query6();
                }

                void Query6()
                {

                    if (add < 32) add *= 2;

                    score += add;
                    time += turn;
                }

                void Query5()
                {

                    if (turn > 1) turn--;

                    score += add;
                    time += turn;
                }

                void Query4()
                {

                    time += 56;

                    score += add;
                    time += turn;
                }

                void Query3() 
                {

                    score += add;
                    time += turn;
                }

                void Query2()
                {

                    if (add > 1) add /= 2;
                    else turn += 2;

                    score += add;
                    time += turn;
                }

                void Query1()
                {

                    Add();
                    Init();
                }

                void Init()
                {

                    score = 0;
                    time = 0;
                    add = 1;
                    turn = 4;
                }

                bool IsEnd()
                    => time > 240;

                void Add()
                {

                    if (score < 35) return;
                    else if (score < 65) ret[0]++;
                    else if (score < 95) ret[1]++;
                    else if (score < 125) ret[2]++;
                    else ret[3]++;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}

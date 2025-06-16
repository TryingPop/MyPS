using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 15
이름 : 배성훈
내용 : 행운의 수
    문제번호 : 9575번

    브루트포스 문제다.
    가능한 경우는 50^3 = 125_000이다.
    그래서 브루트포스로 가닥을 잡았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1704
    {

        static void Main1704(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            int[][] arr;
            HashSet<int> luck;

            Init();

            int t = ReadInt();

            for (int i = 0; i < t; i++)
            {

                Input();

                GetRet();
            }

            void Init()
            {

                arr = new int[3][];
                for (int i = 0; i < 3; i++)
                {

                    arr[i] = new int[51];
                }

                luck = new(1 << 6);
            }

            void GetRet()
            {

                luck.Clear();

                DFS();

                sw.Write($"{luck.Count}\n");

                void DFS(int _dep = 0, int _val = 0)
                {

                    if (_dep == 3)
                    {

                        if (Chk(_val)) luck.Add(_val);
                        return;
                    }

                    for (int i = 1; i <= arr[_dep][0]; i++)
                    {

                        DFS(_dep + 1, _val + arr[_dep][i]);
                    }

                    bool Chk(int _val)
                    {

                        while (_val > 0)
                        {

                            int chk = _val % 10;
                            _val /= 10;

                            if (chk == 5 || chk == 8) continue;
                            return false;
                        }

                        return true;
                    }
                }
            }

            void Input()
            {

                for (int i = 0; i < 3; i++)
                {

                    arr[i][0] = ReadInt();
                    for (int j = 1; j <= arr[i][0]; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }
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

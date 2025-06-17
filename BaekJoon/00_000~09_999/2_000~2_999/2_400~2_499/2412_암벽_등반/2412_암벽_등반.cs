using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 17
이름 : 배성훈
내용 : 암벽 등반
    문제번호 : 2412번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1708
    {

        static void Main1708(string[] args)
        {
            
            int n, t;
            Dictionary<(int x, int y), int> pos;

            Input();

            GetRet();

            void GetRet()
            {

                int NOT_VISIT = -1;

                BFS();

                Output();

                void Output()
                {

                    int ret = NOT_VISIT;
                    foreach(var item in pos)
                    {

                        if (item.Key.y != t) continue;

                        if (item.Value == NOT_VISIT) continue;
                        else if (ret == NOT_VISIT || item.Value < ret) ret = item.Value;
                    }

                    Console.Write(ret);
                }

                void BFS()
                {

                    Queue<(int x, int y)> q = new(n);
                    q.Enqueue((0, 0));

                    int[] dirX = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
                    int[] dirY = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();
                        int cur = pos[(node.x, node.y)];

                        for (int i = 0; i < 24; i++)
                        {

                            int nX = node.x + dirX[i];
                            int nY = node.y + dirY[i];

                            if (!pos.ContainsKey((nX, nY))) continue;

                            int move = pos[(nX, nY)];
                            if (move != NOT_VISIT) continue;
                            pos[(nX, nY)] = cur + 1;
                            q.Enqueue((nX, nY));
                        }
                    }
                }
            }

            void Input() 
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                t = ReadInt();

                pos = new(n + 1);
                pos[(0, 0)] = 0;

                for (int i = 1; i <= n; i++)
                {

                    pos[(ReadInt(), ReadInt())] = -1;
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

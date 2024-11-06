using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 26
이름 : 배성훈
내용 : 햄버거 분배
    문제번호 : 19941번

    그리디 알고리즘 문제다
    큐를 이용해 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_1083
    {

        static void Main1083(string[] args)
        {

            StreamReader sr;
            int n, k;
            string line;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                Queue<int> q = new(n);
                for (int i = 0; i < n; i++)
                {

                    if (line[i] == 'P') continue;
                    q.Enqueue(i);
                }

                for (int i = 0; i < line.Length; i++)
                {

                    if (line[i] == 'H') continue;
                    else if (q.Count == 0) break;

                    while (q.Count > 0)
                    {

                        int peek = q.Peek();
                        int dis = Math.Abs(peek - i);
                        if (k < dis) 
                        {

                            if (i < peek) break;

                            q.Dequeue();
                            continue;
                        }
                        q.Dequeue();
                        ret++;
                        break;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();
                line = sr.ReadLine();
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}

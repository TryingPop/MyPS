using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : 정보 상인 호석
    문제번호 : 22252번

    우선 순위 큐, 해시 문제다.
    Dictionary에 이름을 key로 가리키는 우선순위 큐를 value로 놓는다.
    우선 순위 큐에 내림차순으로 값을 보관하면 가장 비싼것 k개를 구매할 수 있다.
    이렇게 쿼리를 일일히 진행하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1579
    {

        static void Main1579(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            Dictionary<string, PriorityQueue<int, int>> dic = new();
            StringBuilder sb = new(15);

            long ret = 0;
            int q = ReadInt();

            while (q-- > 0)
            {

                int op = ReadInt();
                string name = ReadString();
                int n = ReadInt();

                if (op == 1)
                {

                    if (!dic.ContainsKey(name)) dic[name] = new(n);

                    PriorityQueue<int, int> cur = dic[name];
                    for (int i = 0; i < n; i++)
                    {

                        int add = ReadInt();
                        cur.Enqueue(add, -add);
                    }
                }
                else
                {

                    if (!dic.ContainsKey(name)) continue;

                    PriorityQueue<int, int> cur = dic[name];
                    for (int i = 0; i < n; i++)
                    {

                        if (cur.Count == 0) break;
                        ret += cur.Dequeue();
                    }
                }
            }

            Console.Write(ret);

            string ReadString()
            {

                sb.Clear();
                int c;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    sb.Append((char)c);
                }

                return sb.ToString();
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

                    while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
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

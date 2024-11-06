using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : queuestack
    문제번호 : 24511번

    큐, 스택, 덱 문제다
    문제를 보면, 큐 자료구조에 한해 순차적으로 스택처럼 넣어주고
    이후에는 입력한 값을 큐처럼 넣고 큐처럼 빼주면 된다

    두 포인터 알고리즘을 이용해 배열을 큐, 스택처럼 활용했다
    매번 1개씩 넣고 1개를 빼기에 많아야 n + 1 개의 크기만 사용한다
    (처음에 해당 부분을 캐치 못해 n개로만 하다가 4%에서 틀렸다)
*/

namespace BaekJoon._59
{
    internal class _59_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] queue;
            int head, tail;
            int[] type;

            Solve();
            void Solve()
            {

                Input();

                int m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    int cur = ReadInt();

                    tail = Up(tail);
                    queue[tail] = cur;

                    sw.Write($"{queue[head]} ");
                    head = Up(head);
                }

                sr.Close();
                sw.Close();
            }

            int Up(int _n)
            {

                return _n == n ? 0 : _n + 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();

                type = new int[n];
                queue = new int[n + 1];

                tail = -1;
                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    type[i] = t;
                    if (t == 0) tail++;
                }

                head = 0;
                int chk = tail;

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    if (type[i] == 1) continue;

                    queue[chk--] = cur;
                }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 0과 1
    문제번호 : 8111번

    정수론, BFS 탐색 문제다

    가지고 노는 1 문제(etc_0209)와 비슷하다
    우선 1, 0 으로 만드는 방법은 항상 존재한다! (길이가 문제다!)
        가지고 노는 1에서 2 or 5와 서로소이면 1로만 표현 가능했다

        2 or 5와 서로소가 아닌 경우만 확인하면 된다
        이는 m이 주어지고, 2 or 5와 서로소가 아니라고 하면
        m을 소인수분해하면 p * 2 ^i * 5 ^j로 소인수 분해가 가능하다
        여기서, p는 2와 5와 서로소인 수다!
        그러면, p의 배수 중에 1로만 표현 가능한 수가 있다
        이를 x라하고, 이제 i와 j중 큰 값을 n이라 하자
        그러면 x * 10^n이 1, 0으로만 표현되면서 m의 배수가된다

    풀이 방법은 다음과 같다
    1, 0으로 수를 만들어 나가고 n의 배수인지 확인한다
    그런데 1, 0 의 길이가 100이 넘기에 정수형 자료로는(long, int) 수를 모두 담을 수 없다
    담을 수 있다고 해도 수가 커서 나머지를 확인하는데 시간이 오래 걸릴것이다

    그래서 나눠 떨어지는거 확인하기 때문에 mod연산으로 크기를 줄여 진행한다
    그리고 1, 0을 이어붙인 숫자를 문자열로 표현하기에는 수가 너무 많아지기에
    따로 이전에 뭐를 추가했는지 이전 인덱스의 경우와 최근에 추가된 char를 저장하는 배열을 쓴다

    그리고 모듈러 연산에서 중복 방문하는 경우
    같은 경우를 돌기에 끊어줘야한다 이를 안하면 2^100 연산을해서
    시간초과 뜰 것이다

    매 경우 2^100승이 아닌 2만번 아래로 BFS 탐색을 진행할 것이다
    해의 존재성은 보장되나 길이 부분은 100 이하인지 판별이 필요하지만
    for문으로 돌려본 결과 100 이상인 경우는 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0390
    {

        static void Main390(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

            int test = ReadInt();
            bool[] visit = new bool[20_001];
            int[] before = new int[20_001];
            char[] add = new char[20_001];

            Queue<int> q = new Queue<int>(20_000);
            Stack<int> s = new Stack<int>(20_000);

            StringBuilder sb = new StringBuilder(20_000);
            while(test-- > 0)
            {

                Array.Fill(visit, false);
                Array.Fill(before, -1);
                Array.Fill(add, ' ');

                int chk = ReadInt();
                q.Enqueue(1);
                add[1] = '1';
                visit[1] = true;

                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < 2; i++)
                    {

                        // 다음 숫자
                        int next = 10 * node + i;
                        next %= chk;

                        // 이미 확인한 경우 같은 루프만 돌기에 넘긴다
                        if (visit[next]) continue;
                        visit[next] = true;

                        before[next] = node;
                        add[next] = (char)(i + '0');

                        q.Enqueue(next);
                    }
                }

                if (visit[0])
                {

                    int find = before[0];
                    s.Push(0);
                    while (find > -1)
                    {

                        s.Push(find);
                        find = before[find];
                    }

                    if (s.Count > 100)
                    {

                        // 길이 100제한으로 길이 확인
                        sb.Append($"{chk} BRAK");
                        s.Clear();
                    }
                    else
                    {

                        // 100 이하인 경우만 쓴다
                        while (s.Count > 0)
                        {

                            sb.Append(add[s.Pop()]);
                        }
                    }
                }
                // 해가 없는 경우 <- 자연수인 경우 여기로 오는 경우는 불가능하다!
                // else sb.Append($"{chk} BRAK");

                sw.Write(sb);
                sw.Write('\n');

                sb.Clear();
            }

            sr.Close();
            sw.Close();

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

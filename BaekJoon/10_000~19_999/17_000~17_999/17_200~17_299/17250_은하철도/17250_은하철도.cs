using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 은하철도
    문제번호 : 17250번

    분리집합, 자료구조 문제다

    문제에서 요구하는건 다음과 같다
    철도를 만들 때, 해당 철도를 이용할 수 있는 최대 행성수를 출력하는 것이다
    그래서 철도가 연결될 때마다 그룹을 짓고
    다른 그룹이면 행성수를 합치고 해당 합친 값을 출력한다
    이미 같은 그룹이면 해당 그룹의 행성을 출력하면 된다

    그룹을 연결짓는데 유니온 파인드 알고리즘을 썼다
    아래는 이를 코드로 작성한 것이고 112ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0256
    {

        static void Main256(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] group = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                // 그룹 번호
                group[i] = i;
            }

            // 행성 수
            int[] cnts = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                cnts[i] = ReadInt(sr);
            }

            Stack<int> s = new Stack<int>();
            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                f = Find(group, f, s);
                b = Find(group, b, s);

                if (f > b)
                {

                    int temp = f;
                    f = b;
                    b = temp;
                }

                if (f < b)
                {

                    // 철도 연결
                    group[b] = f;
                    cnts[f] += cnts[b];
                    cnts[b] = 0;
                }

                // 해당 그룹의 행성 출력
                sw.WriteLine(cnts[f]);
            }

            sw.Close();
            sr.Close();

        }

        static int Find(int[] _group, int _chk, Stack<int> _s)
        {

            while(_chk != _group[_chk])
            {

                _s.Push(_chk);
                _chk = _group[_chk];
            }

            while(_s.Count > 0)
            {

                _group[_s.Pop()] = _chk;
            }

            return _chk;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

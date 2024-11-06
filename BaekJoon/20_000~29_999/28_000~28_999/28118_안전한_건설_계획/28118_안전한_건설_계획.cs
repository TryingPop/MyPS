using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 안전한 건설 계획
    문제번호 : 28118번

    연결된 집합 점들에서는 0의 비용이 들어간다
    예제 몇 개를 해보면 답이 보장된 경우 분리집합의 개수 -1 임을 알 수 있다

    유니온 파인드 알고리즘을 써서 연결된 집합들을 확인했고
    답이 보장된다고 했으므로 -1을 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0081
    {

        static void Main81(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            // 연결 집합 찾기
            int[] groups = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                groups[i] = i;
            }

            int ret = n - 1;
            Stack<int> s = new Stack<int>(n);
            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                f = Find(groups, f, s);
                b = Find(groups, b, s);

                if (f == b) continue;

                groups[f] = b;
                ret--;
            }

            Console.WriteLine(ret);
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

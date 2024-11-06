using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18 
이름 : 배성훈
내용 : 연결 요소의 개수
    문제번호 : 11724번

    유니온 파인드 알고리즘을 쓰는 문제
*/

namespace BaekJoon.etc
{
    internal class etc_0059
    {

        static void Main59(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int len = ReadInt(sr);

            // 유니온 파인드 쓴다
            int[] group = new int[n + 1];
            // 재귀 방지용 스택
            Stack<int> s = new Stack<int>();

            for (int i = 1; i <= n; i++)
            {

                group[i] = i;
            }

            for (int i = 0; i < len; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                f = Find(group, f, s);
                b = Find(group, b, s);

                if (f == b) continue;

                group[f] = b;
            }
            sr.Close();

            // 이제 서로다른 그룹을 센다
            bool[] calc = new bool[n + 1];
            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                int chk = Find(group, i, s);

                if (calc[chk]) continue;

                ret++;
                calc[chk] = true;
            }

            Console.WriteLine(ret);
        }

        static int Find(int[] _group, int _chk, Stack<int> _calc)
        {

            while(_chk != _group[_chk])
            {

                _calc.Push(_chk);
                _chk = _group[_chk];
            }

            while(_calc.Count > 0)
            {

                _group[_calc.Pop()] = _chk;
            }

            return _chk;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

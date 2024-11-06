using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 로또
    문제번호 : 6603번

    간단한 구현 문제다 
    DFS 탐색을 통해 모든 경우를 출력했다
*/

namespace BaekJoon.etc
{
    internal class etc_0072
    {

        static void Main72(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] cur = new int[13];

            int[] memo = new int[6];
            while (true)
            {

                int len = ReadInt(sr);

                if (len == 0) break;

                for (int i = 0; i < len; i++)
                {

                    cur[i] = ReadInt(sr);
                }

                DFS(sw, cur, memo, len);

                sw.Flush();
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        static void DFS(StreamWriter _sw, int[] _cur, int[] _memo, int _len, int _next = 0, int _depth = 0)
        {

            if (_depth == 6)
            {

                for (int i = 0; i < 6; i++)
                {

                    _sw.Write(_memo[i]);
                    _sw.Write(' ');
                }
                _sw.Write('\n');
                return;
            }

            int before = _memo[_depth];
            for (int i = _next; i < _len; i++)
            {

                _memo[_depth] = _cur[i];
                DFS(_sw, _cur, _memo, _len, i + 1, _depth + 1);
            }

            _memo[_depth] = before;
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

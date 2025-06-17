using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 고인물이 싫어요
    문제번호 : 25187번

    유니온 파인드를 알고리즘을 쓰는 문제다
    group의 초기화 부분 인덱스를 잘못해서 한 번 틀렸다
    (예제 케이스가 통과된게 신기하다;)

    주된 아이디어는 다음과 같다
    그룹을 합칠 때, 전체 통과, 정화통의 개수를 합치면서 그룹화 했다

    그리고 조건에 맞게 연산하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0269
    {

        static void Main269(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);
            int k = ReadInt(sr);

            int[] group = new int[n + 1];
            int[] clean = new int[n + 1];
            int[] cnt = new int[n + 1];
            
            for (int i = 1; i <= n; i++)
            {

                clean[i] = ReadInt(sr);
                group[i] = i;
                cnt[i] = 1;
            }

            Stack<int> s = new();
            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                f = Find(group, f, s);
                b = Find(group, b, s);

                if (b < f)
                {

                    int temp = f;
                    f = b;
                    b = temp;
                }
                else if (f == b) continue;

                group[b] = f;
                clean[f] += clean[b];
                clean[b] = 0;

                cnt[f] += cnt[b];
                cnt[b] = 0;
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < k; i++)
            {

                int f = ReadInt(sr);
                f = Find(group, f, s);

                int calc = cnt[f] - clean[f];
                if (calc < clean[f]) sw.WriteLine(1);
                else sw.WriteLine(0);
            }

            sr.Close();
            sw.Close();
        }

        static int Find(int[] _group, int _chk, Stack<int> _s)
        {

            while (_group[_chk] != _chk)
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

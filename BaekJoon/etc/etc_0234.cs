using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 가장 가까운 세 사람의 심리적 거리
    문제번호 : 20529번

    비둘기집 원리를 쓰는 문제다
    33을 넘는 경우 비둘기집 원리로 적어도 같은 유형을 가진 3명의 사람이 존재하고
    33 미만의 경우는 브루트포스로 해결했다

    브루트 포스는 DFS 탐색을이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0234
    {

        static void Main234(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            string[] calc = new string[3];
            int test = int.Parse(sr.ReadLine());
            bool[] visit = new bool[32];

            while(test-- > 0)
            {

                int n = int.Parse(sr.ReadLine());

                string[] str = sr.ReadLine().Split(' ');
                int ret = 0;
                // 33보다 큰 경우 비둘기 집 원리에 의해 3개가 중복된 경우가 적어도 1개 존재!
                // 33보다 적은 경우는 완전탐색한다
                if (str.Length < 33) ret = DFS(str, visit, calc, 0, -1);

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
        }

        static int DFS(string[] _str, bool[] _visit, string[] _temp, int _depth, int _before)
        {

            if (_depth == 3)
            {

                int diff = 0;
                for (int i = 0; i < 4; i++)
                {

                    if (_temp[0][i] != _temp[1][i]) diff++;
                    if (_temp[1][i] != _temp[2][i]) diff++;
                    if (_temp[2][i] != _temp[0][i]) diff++;
                }

                return diff;
            }

            int ret = 100;
            for (int i = _before + 1; i < _str.Length; i++)
            {

                if (_visit[i]) continue;
                _visit[i] = true;

                _temp[_depth] = _str[i];
                int calc = DFS(_str, _visit, _temp, _depth + 1, i);
                if (calc < ret) ret = calc;
                _visit[i] = false;
            }

            return ret;
        }
    }
}

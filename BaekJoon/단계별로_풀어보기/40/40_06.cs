using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 23
이름 : 배성훈
내용 : 선분 그룹
    문제번호 : 2162번

    ... 40_04, 40_05와 유니온 파인드 알고리즘을 이용하면 쉽게 풀린다
    그런데, 초기값 설정을 잘못해서 틀렸다;

    모든 선분이 독립인 경우가 있다!;
*/

namespace BaekJoon._40
{
    internal class _40_06
    {

        static void Main6(string[] args)
        {


            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] lines = new int[len][];

            for (int i = 0; i < len; i++)
            {

                lines[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }
            sr.Close();

            (int group, int mem)[] groups = new (int group, int mem)[len];
            
            // 유니온 파인드 알고리즘!
            for (int i = 0; i < len; i++)
            {

                groups[i] = (i, 1);
            }

            // 유니온
            int cnt = len, max = 1;
            Stack<int> s = new Stack<int>();
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < i; j++)
                {

                    // 연결 안된 경우
                    if (!ChkConnect(lines[i], lines[j])) continue;
                    int chk1 = Find(groups, i, s);
                    int chk2 = Find(groups, j, s);

                    if (chk1 == chk2) continue;

                    groups[chk1].group = chk2;
                    groups[chk2].mem += groups[chk1].mem;
                    cnt--;
                    if (groups[chk2].mem > max) max = groups[chk2].mem;
                }
            }

            Console.WriteLine($"{cnt}\n{max}");
        }

        static int Find((int group, int mem)[] _group, int _chk, Stack<int> _calc)
        {

            while(_chk != _group[_chk].group)
            {

                _calc.Push(_chk);
                _chk = _group[_chk].group;
            }

            while(_calc.Count > 0)
            {

                _group[_calc.Pop()].group = _chk;
            }

            return _chk;
        }

        static bool ChkConnect(int[] _line1, int[] _line2)
        {

            // 연결 확인!
            int r1 = CCW(_line1[0], _line1[1], _line1[2], _line1[3], _line2[0], _line2[1]);
            int r2 = CCW(_line1[0], _line1[1], _line1[2], _line1[3], _line2[2], _line2[3]);

            if (r1 * r2 > 0) return false;
            if (r1 == 0 && r2 == 0)
            {

                if (_line1[0] == _line1[2]) return ChkBoundary(_line1, _line2, true);

                return ChkBoundary(_line1, _line2, false);
            }

            r1 = CCW(_line2[0], _line2[1], _line2[2], _line2[3], _line1[0], _line1[1]);
            r2 = CCW(_line2[0], _line2[1], _line2[2], _line2[3], _line1[2], _line1[3]);

            if (r1 * r2 > 0) return false;

            return true;
        }

        static bool ChkBoundary(int[] _line1, int[] _line2, bool _isY)
        {

            int calc = _isY ? 1 : 0;
            int min1 = _line1[calc] < _line1[calc + 2] ? _line1[calc] : _line1[calc + 2];
            int max1 = _line1[calc] < _line1[calc + 2] ? _line1[calc + 2] : _line1[calc];

            int min2 = _line2[calc] < _line2[calc + 2] ? _line2[calc] : _line2[calc + 2];
            int max2 = _line2[calc] < _line2[calc + 2] ? _line2[calc + 2] : _line2[calc];

            if (min1 > max2 || min2 > max1) return false;

            return true;
        }

        static int CCW(int _x1, int _y1, int _x2, int _y2, int _x3, int _y3)
        {

            int det = _x1 * _y2 
                + _x2 * _y3 
                + _x3 * _y1
                - _x1 * _y3 
                - _x2 * _y1 
                - _x3 * _y2;

            if (det == 0) return 0;
            if (det > 0) return 1;
            return -1;
        }
    }
}

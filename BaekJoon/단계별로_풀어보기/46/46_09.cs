using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 사탕상자
    문제번호 : 2243번

    코드에 연산식을 잘못 넣어 한 번 틀렸다
    이외는 46_08과 비슷한 아이디어로 푸니 이상없이 통과했다!

    여기서는 사탕을 주는 것이기에 사탕을 빼내면 값을 빼줘야한다!
*/

namespace BaekJoon._46
{
    internal class _46_09
    {

        static void Main9(string[] args)
        {

            // 총 100만개!
            int MAX = 1_000_000;
            int log = (int)Math.Ceiling(Math.Log2(MAX)) + 1;
            int size = 1 << log;
            int[] seg = new int[size];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                if (temp[0] == 1)
                {

                    int result = GetArr(seg, 1, MAX, temp[1]);
                    sw.WriteLine(result);
                }
                else
                {

                    ChangeArr(seg, 1, MAX, temp[1], temp[2]);
                }
            }

            sr.Close();
            sw.Close();
        }

        static void ChangeArr(int[] _seg, int _start, int _end, int _changeIdx, int _addValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] += _addValue;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_changeIdx > mid) ChangeArr(_seg, mid + 1, _end, _changeIdx, _addValue, 2 * _idx + 1);
            else ChangeArr(_seg, _start, mid, _changeIdx, _addValue, 2 * _idx);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetArr(int[] _seg, int _start, int _end, int _chk, int _idx = 1)
        {

            _seg[_idx - 1] -= 1;

            if (_start == _end) return _start;

            int l = _seg[_idx * 2 - 1];
            int mid = (_start + _end) / 2;
            int result;

            // 해당 부분으로만 진입!
            if (_chk > l) result = GetArr(_seg, mid + 1, _end, _chk - l, _idx * 2 + 1);
            else result = GetArr(_seg, _start, mid, _chk, _idx * 2);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 4
이름 : 배성훈
내용 : 구간 곱 구하기
    문제번호 : 11505번

    46_01과 같이 세그먼트 트리를 이용한 방법으로 풀었다
    덧셈이 곱셈으로 바꼈고, 여기는 입력값이 낮아 큰 제한이 없다
*/

namespace BaekJoon._46
{
    internal class _46_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] nums = new int[info[0] + 1];
            int log = (int)Math.Ceiling(Math.Log2(info[0])) + 1;
            int size = 1 << log;
            
            long[] seg = new long[size];

            for(int i = 1; i <= info[0]; i++)
            {

                nums[i] = int.Parse(sr.ReadLine());
            }

            SetArr(seg, nums, 1, info[0]);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int len = info[1] + info[2];
            for (int i = 0; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                if (temp[0] == 1) ChangeArr(seg, 1, info[0], temp[1], temp[2]);
                else sw.WriteLine(GetArr(seg, 1, info[0], temp[1], temp[2]));
            }

            sr.Close();
            sw.Close();
        }

        static void SetArr(long[] _seg, int[] _num, int _start, int _end, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = _num[_start];
                return;
            }

            int mid = (_start + _end) / 2;
            SetArr(_seg, _num, _start, mid, _idx * 2);
            SetArr(_seg, _num, mid + 1, _end, _idx * 2 + 1);

            // 이제 부모
            _seg[_idx - 1] = _seg[_idx * 2 - 1] * _seg[_idx * 2];
            _seg[_idx - 1] %= 1_000_000_007;
        }

        static void ChangeArr(long[] _seg, int _start, int _end, int _chkIdx, int _changeValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = _changeValue;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_chkIdx > mid) ChangeArr(_seg, mid + 1, _end, _chkIdx, _changeValue, 2 * _idx + 1);
            else ChangeArr(_seg, _start, mid, _chkIdx, _changeValue, 2 * _idx);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] * _seg[_idx * 2];
            _seg[_idx - 1] %= 1_000_000_007;
        }

        static long GetArr(long[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_chkEnd < _start || _end < _chkStart) return 1;
            else if (_start >= _chkStart && _end <= _chkEnd) return _seg[_idx - 1];

            int mid = (_start + _end) / 2;
            long l = GetArr(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            long r = GetArr(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);

            return (l * r) % 1_000_000_007;
        }
    }
}

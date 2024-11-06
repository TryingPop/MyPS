using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 4
이름 : 배성훈
내용 : 최솟값과 최댓값
    문제번호 : 2357번

    세그먼트 트리를 이용해 풀었다
    앞(46_02)에서는 구간 곱이였으나 여기서는 튜플을 이용해 구간 최대값과 최소값을 보관했다

    그리고 원하는 구간을 입력하면 해당 노드의 최대값과 최소값이 바로 나오는게 아닌
    해당되는 두 구간의 노드들의 크기를 비교해서 나온다

    겹치는 구간이 없는 경우 최소값은 가질 수 있는 최대값, 최대값은 가질 수 있는 최소값을 반환한다
    이외에는 앞과 다른게 없다
*/

namespace BaekJoon._46
{
    internal class _46_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] nums = new int[info[0] + 1];

            for (int i = 1; i <= info[0]; i++)
            {

                nums[i] = int.Parse(sr.ReadLine());
            }

            // seg 설정
            int log = (int)(Math.Ceiling(Math.Log2(info[0]))) + 1;
            int size = 1 << log;

            (int min, int max)[] seg = new (int min, int max)[size];
            SetArr(seg, nums, 1, info[0]);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                var result = GetArr(seg, 1, info[0], temp[0], temp[1]);

                sw.Write(result.min);
                sw.Write(' ');
                sw.Write(result.max);
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        static void SetArr((int min, int max)[] _seg, int[] _num, int _start, int _end, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1].min = _num[_start];
                _seg[_idx - 1].max = _num[_start];
                return;
            }

            int mid = (_start + _end) / 2;
            SetArr(_seg, _num, _start, mid, _idx * 2);
            SetArr(_seg, _num, mid + 1, _end, _idx * 2 + 1);

            _seg[_idx - 1].min = Math.Min(_seg[_idx * 2 - 1].min, _seg[_idx * 2].min);
            _seg[_idx - 1].max = Math.Max(_seg[_idx * 2 - 1].max, _seg[_idx * 2].max);
        }

        static (int min, int max) GetArr((int min, int max)[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            // 겹치는 구간이 없으므로
            // 최소값과 최대값은 각각 가질 수 있는 최대값과 최소값을 갖게한다
            if (_start > _chkEnd || _chkStart > _end) return (1_000_000_000, 1);
            else if (_start >= _chkStart && _end <= _chkEnd) return _seg[_idx - 1];

            int mid = (_start + _end) / 2;
            var l = GetArr(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            var r = GetArr(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);

            // 비교해서 최소값과 최대값 출력
            return (Math.Min(l.min, r.min), Math.Max(l.max, r.max));
        }
    }
}

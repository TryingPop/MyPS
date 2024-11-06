using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 부분수열의 합
    문제번호 : 1182

    중간에서 만나기로 풀었다
    브루트 포스로도 풀린다
    해시를 안쓰고 이분? 진? 탐색으로 해도된다
*/

namespace BaekJoon.etc
{
    internal class etc_0154
    {

        static void Main154(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            // 중간에서 만나기! 
            Dictionary<int, int> left = new Dictionary<int, int>(1 << ((info[0] + 1) / 2));
            List<int> right = new List<int>(1 << (info[0] / 2));

            FillLeft(arr, 0, 0, (info[0] / 2) + 1, left);
            FillRight(arr, (info[0] / 2) + 1, 0, info[0], right);

            int ret = 0;

            // 탐색 시작
            for (int i = 0; i < right.Count; i++)
            {

                int find = info[1] - right[i];
                if (left.ContainsKey(find)) ret += left[find];
            }
            if (info[1] == 0) ret--;

            Console.WriteLine(ret);
        }

        static void FillLeft(int[] _arr, int _curIdx, int _curVal, int _end, Dictionary<int, int> _sum)
        {

            if (_curIdx == _end)
            {

                if (_sum.ContainsKey(_curVal)) _sum[_curVal]++;
                else _sum[_curVal] = 1;
                return;
            }

            FillLeft(_arr, _curIdx + 1, _curVal, _end, _sum);
            FillLeft(_arr, _curIdx + 1, _curVal + _arr[_curIdx], _end, _sum);
        }

        static void FillRight(int[] _arr, int _curIdx, int _curVal, int _end, List<int> _sum)
        {

            if (_curIdx == _end)
            {

                _sum.Add(_curVal);
                return;
            }

            FillRight(_arr, _curIdx + 1, _curVal, _end, _sum);
            FillRight(_arr, _curIdx + 1, _curVal + _arr[_curIdx], _end, _sum);
        }
    }
}

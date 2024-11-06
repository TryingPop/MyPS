using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 5
이름 : 배성훈
내용 : 수열과 쿼리 21
    문제번호 : 16975번

    세그먼트 트리에서 변환과 읽는 로직을 바꿨다

    세그먼트 트리에서는 리프(자식이 없는 노드)까지 쭉 바꿔주는데 반해 
    여기서는 해당 구간이 다 포함되면 해당 구간만 변경하고 자식은 변경하지 않았다
    겹치는 구간이 없다면 탐색 종료 시키고, 겹치는 구간이 있다면 겹치는 구간이 변경되는 구간에 포함될 때까지 쪼갰다

    그리고, 읽는 것은 기존에는 해당 노드를 만족하면 자식까지 안가고 바로 반환하는데 반해,
    여기서는 자식까지 쭉 읽으면서 값을 합쳤다

    그리고, 문제 조건에서 1회 변경당 100만씩 변경되고 최대 10만번 변경하는데,
    설마 테스트 에서 int 범위 벗어나겠어? 했는데 진짜 벗어났다 (8%쯤?에서 틀렸다)

    그래서 long으로 바꾸니 바로 통과되었다

    질문게시판 보니 펜윅 트리(BIX 트리, 바이너리 인덱서 트리)라는 것을 이용한다고 하는데,
    찾아보니 N의 메모리를 더 할당해서, 인덱서로 저장하는 방식인거 같다
    0 ~ 7번 인덱스가 있을 때, 8개의 인덱스를 할당하면
    0, 0 ~ 1, 2, 0 ~ 3, 4, 4 ~ 5, 6, 0 ~ 7 순으로 저장한다
    0 ~ 1은 0, 1의 구간합이라 보면 된다

    그리고 레이지 프로퍼게이션이라는 용어도 나오는데 이도 다음에 알아봐야겠다!
*/

namespace BaekJoon._46
{
    internal class _46_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] nums = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int log = (int)(Math.Ceiling(Math.Log2(len))) + 1;
            int size = 1 << log;
            long[] seg = new long[size];

            int test = int.Parse(sr.ReadLine());

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int t = 0; t < test; t++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                if (temp[0] == 1)
                {

                    ChangeArr(seg, 1, len, temp[1], temp[2], temp[3]);
                }
                else
                {

                    long result = GetArr(seg, 1, len, temp[1]);
                    result += nums[temp[1] - 1];
                    sw.WriteLine(result);
                }
            }

            sr.Close();
            sw.Close();
        }

        static void ChangeArr(long[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _add, int _idx = 1)
        {

            if (_start >= _chkStart && _end <= _chkEnd)
            {

                _seg[_idx - 1] += _add;
                return;
            }
            else if (_chkEnd < _start || _end < _chkStart) return;

            int mid = (_start + _end) / 2;

            ChangeArr(_seg, _start, mid, _chkStart, _chkEnd, _add, _idx * 2);
            ChangeArr(_seg, mid + 1, _end, _chkStart, _chkEnd, _add, _idx * 2 + 1);
        }

        static long GetArr(long[] _seg, int _start, int _end, int _getIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                return _seg[_idx - 1];
            }

            int mid = (_start + _end) / 2;
            long get = _seg[_idx - 1];
            if (mid < _getIdx) get += GetArr(_seg, mid + 1, _end, _getIdx, _idx * 2 + 1);
            else get += GetArr(_seg, _start, mid, _getIdx, _idx * 2);

            return get;
        }
    }
}

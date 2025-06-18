using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 기차 여행
    문제번호 : 10713

    세그먼트 트리로 풀었다
    가격이 모두 10만, 건너는 정거장이 10만개
    10만 * 10만 = 100억으로
    제한이 없는 경우 정답 범위가 int 범위를 벗어날 수 있다
    이를 상기하지 못해 한 번 틀렸다;
    이를 수정하니 이상없이 통과했다


    다른 사람 풀이를 찾아보니
    누적합으로 했고, 아이디어는 여기 사이트에 잘 설명되어져 있다
    https://burningjeong.tistory.com/326

    요약하면, 시작지점을 1, 끝지점을 -1로 표현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0214
    {

        static void Main214(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
            int[] seg = new int[1 << log];

            int dest = ReadInt(sr);
            int before = -1;
            for (int i = 0; i < dest; i++)
            {

                int dst = ReadInt(sr);
                if (before == -1) 
                {

                    before = dst;
                    continue; 
                }

                int min = before < dst ? before : dst;
                int max = before < dst ? dst : before;

                Update(seg, 1, n - 1, min, max - 1);
                before = dst;
            }

            long ret = 0;
            for (int i = 1; i < n; i++)
            {

                long cur = GetVal(seg, 1, n - 1, i);

                int ticket = ReadInt(sr);
                long calc1 = ticket * cur;

                int icTicket = ReadInt(sr);
                int icCard = ReadInt(sr);

                long calc2 = icCard + icTicket * cur;

                ret += calc1 < calc2 ? calc1 : calc2;
            }
            sr.Close();

            Console.WriteLine(ret);
        }

        static void Update(int[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_chkStart <= _start && _end <= _chkEnd)
            {

                _seg[_idx - 1]++;
                return;
            }
            else if (_end < _chkStart || _chkEnd < _start) return;

            int mid = (_start + _end) / 2;
            Update(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            Update(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);
        }

        static int GetVal(int[] _seg, int _start, int _end, int _getIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                return _seg[_idx - 1];
            }

            int mid = (_start + _end) / 2;
            int ret = _seg[_idx - 1];
            if (_getIdx > mid) ret += GetVal(_seg, mid + 1, _end, _getIdx, _idx * 2 + 1);
            else ret += GetVal(_seg, _start, mid, _getIdx, _idx * 2);

            return ret;
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

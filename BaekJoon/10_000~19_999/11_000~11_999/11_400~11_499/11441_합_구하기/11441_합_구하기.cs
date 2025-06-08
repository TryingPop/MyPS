using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 12
이름 : 배성훈
내용 : 합 구하기
    문제번호 : 11441번

    dp를 이용하면 O(1)에 풀린다
    이후에 세그먼트 트리를 이용해 O(log N) 으로 했다

    dp : 112ms, seg : 184ms
    걸렸다

    dp의 방법은 간단하다
    dp[n]을 1항부터 n항까지의 합을 나타내는 배열을 만든다(항은 1항부터 시작)
    dp[0] = 0으로 한다

    그리고 s ~ e항까지의 합을 구할 때,
    dp[e] - dp[s - 1]을 계산하면 된다

    그러면 구간합이 된다

    세그먼트 트리는 기존과 같다
    구간 s ~ e까지의 합을 노드에 보관한다
    그리고 탐색 과정에서 찾는 구간에 포함되면 해당 노드의 값을 바로 반환하고 해당 값을 합친다
    그러면 마지막에 다 더한 값이 구간 합이 된다

    바이너리 인덱스 트리도 가능하나, 이는 다른 문제에서 해볼 생각이다!
*/

namespace BaekJoon.etc
{
    internal class etc_0020
    {

        static void Main20(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = ReadInt(sr);

#if dp

            int[] nums = new int[len];
            int[] dp = new int[len + 1];
            int sum = 0;
           
            for (int i = 0; i < len; i++) 
            { 

                nums[i] = ReadInt(sr);
                sum += nums[i];
                dp[i + 1] = sum;
            }

#else
            // segment
            int[] seg;
            {

                int log = (int)Math.Ceiling(Math.Log2(len)) + 1;
                seg = new int[1 << log];
            }

            for (int i = 1; i <= len; i++)
            {

                int num = ReadInt(sr);
                Update(seg, 1, len, i, num);
            }
#endif


            int test = ReadInt(sr);
#if dp
            for (int t = 0; t < test; t++)
            {

                int s = ReadInt(sr);
                int e = ReadInt(sr);

                sw.WriteLine(dp[e] - dp[s - 1]);
            }
#else
            for (int t = 0; t < test; t++)
            {

                int s = ReadInt(sr);
                int e = ReadInt(sr);

                int ret = GetValue(seg, 1, len, s, e);
                sw.WriteLine(ret);
            }
#endif
            sw.Close();
            sr.Close();
        }

#if !dp
        static void Update(int[] _seg, int _start, int _end, int _changeIdx, int _changeValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = _changeValue;
                return;
            }

            int mid = (_start + _end) / 2;

            if (_changeIdx > mid) Update(_seg, mid + 1, _end, _changeIdx, _changeValue, 2 * _idx + 1);
            else Update(_seg, _start, mid, _changeIdx, _changeValue, 2 * _idx);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetValue(int[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_end < _chkStart || _chkEnd < _start) return 0;
            else if (_chkStart <= _start && _end <= _chkEnd) return _seg[_idx - 1];

            int mid = (_start + _end) / 2;
            int l = GetValue(_seg, _start, mid, _chkStart, _chkEnd, 2 * _idx);
            int r = GetValue(_seg, mid + 1, _end, _chkStart, _chkEnd, 2 * _idx + 1);

            return l + r;
        }
#endif

        static int ReadInt(StreamReader _sr)
        {

            bool minus = false;
            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    minus = true;
                    continue;
                }

                ret *= 10;
                ret += c - '0';
            }

            ret = minus ? -ret : ret;
            return ret;
        }
    }
}

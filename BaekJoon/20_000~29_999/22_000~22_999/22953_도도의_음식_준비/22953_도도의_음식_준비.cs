using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 도도의 음식 준비
    문제번호 : 22953번

    브루트포스, 이분탐색, 백트래킹, 매개변수 탐색 문제다
    처음에는 DFS를 간단하게 인덱스를 0부터 시작하는 인덱스로 짜서 452ms에 통과했다

    다른 사람 시간과 비교해보던 도중, 자바 빠른 사람이랑 4배 가까이 차이나서,
    로직이 잘못되었나 의문을 가졌고, 중복되는 경우를 많이 세는 것을 확인했다

    격력 부분을 조합 형태로 짜야함을 알게되었다
    그래서 해당 부분을 바꾸니 7배 가까이 줄어 68ms로 통과했다

    이분 탐색은 요리사가 완성하는 시간을 찾는데 사용했다
    그리고, 백트래킹, 브루트 포스는 요리사 격려하는데 썼다
*/

namespace BaekJoon.etc
{
    internal class etc_0307
    {

        static void Main307(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);
            int c = ReadInt(sr);

            int[] time = new int[n];
            for (int i = 0; i < n; i++)
            {

                time[i] = ReadInt(sr);
            }

            sr.Close();

            long ret = DFS(time, k, c);
            Console.WriteLine(ret);
        }
        
        static long DFS(int[] _time, int _make, int _c, int _s = 0)
        {

            // 격려 다하면 경우의 최소 경우 계산
            if (_c == 0) return Calc(_time, _make);

            // 격려하기
            long ret = 1_000_000_000_000;
            for (int i = _s; i < _time.Length; i++)
            {

                // 조합식 격려
                int before = _time[i];
                for (int d = 1; d <= _c; d++)
                {

                    _time[i]--;
                    if (_time[i] < 1) _time[i] = 1;
                    long calc = DFS(_time, _make, _c - d, i + 1);
                    ret = calc < ret ? calc : ret;
                }
                _time[i] = before;
            }

            return ret;
        }

        static long Calc(int[] _time, int _make)
        {

            // 만드는 최소 시간 이분탐색으로 계산
            long l = 1;
            long r = 1_000_000_000_000;

            while(l <= r)
            {

                long mid = (l + r) / 2;
                long val = 0;

                for (int i = 0; i < _time.Length; i++)
                {

                    val += mid / _time[i];
                }

                if (val < _make) l = mid + 1;
                else r = mid - 1;
            }

            return r + 1;
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

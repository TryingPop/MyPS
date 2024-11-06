using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 동전 게임
    문제번호 : 9079번

    브루트 포스, 비트마스킹 문제다
    가능한 경우는 2^9 개 많아야 512 경우가 존재한다
    그래서 모든 경우를 BFS 탐색하고 dp에 횟수를 저장했다
    문제가 주어지면 dp를 참조해 바로 답이 나오게 코드를짰다

    디버깅 현황을 보기 위해 배열로 기록하고 
    바꾼 배열을 숫자로 바꾸고 값을 저장하는 2중 일을 했다

    비트 연산만으로도 충분히 가능하다
    9개 중 특정 3개 i, j, k뒤집는 방법은
        int calc = (1 << i);
        calc |= (1 << j);
        calc |= (1 << k);

        num ^= calc;
        
    연산으로 찾아가면 된다
    배열 쓴게 더빠르게 나왔다?
*/

namespace BaekJoon.etc
{
    internal class etc_0252
    {

        static void Main252(string[] args)
        {
#if first
            bool[] calc = new bool[9];
            int[] dp = new int[1 << 9];

            Array.Fill(dp, -1);

            dp[0] = 0;
            int end = (1 << 9) - 1;
            dp[end] = 0;
            Queue<int> q = new Queue<int>(511);
            q.Enqueue(0);
            q.Enqueue(end);

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                IntToArr(calc, node);
                int ret;
                for (int i = 0; i < 3; i++)
                {

                    ChangeRow(calc, i);
                    ret = ArrToInt(calc);

                    if (dp[ret] == -1)
                    {

                        dp[ret] = dp[node] + 1;
                        q.Enqueue(ret);
                    }
                    ChangeRow(calc, i);

                    ChangeCol(calc, i);
                    ret = ArrToInt(calc);

                    if (dp[ret] == -1)
                    {

                        dp[ret] = dp[node] + 1;
                        q.Enqueue(ret);
                    }

                    ChangeCol(calc, i);
                }

                ChangeDia(calc, true);
                ret = ArrToInt(calc);
                if (dp[ret] == -1)
                {

                    dp[ret] = dp[node] + 1;
                    q.Enqueue(ret);
                }

                ChangeDia(calc, true);
                ChangeDia(calc, false);

                ret = ArrToInt(calc);
                if (dp[ret] == -1)
                {

                    dp[ret] = dp[node] + 1;
                    q.Enqueue(ret);
                }

                ChangeDia(calc, false);
            }
#else

            int s = 0;
            int e = (1 << 9) - 1;

            Queue<int> q = new(e);
            q.Enqueue(s);
            q.Enqueue(e);

            int[] dp = new int[1 << 9];
            Array.Fill(dp, -1);

            dp[s] = 0;
            dp[e] = 0;

            while(q.Count > 0)
            {

                var cur = q.Dequeue();

                int num;
                for (int i = 0; i < 3; i++)
                {

                    num = Change(cur, 3 * i, 3 * i + 1, 3 * i + 2);
                    if (Record(dp, cur, num)) q.Enqueue(num);

                    num = Change(cur, i, i + 3, i + 6);
                    if (Record(dp, cur, num)) q.Enqueue(num);
                }

                num = Change(cur, 0, 4, 8);
                if (Record(dp, cur, num)) q.Enqueue(num);

                num = Change(cur, 2, 4, 6);
                if (Record(dp, cur, num)) q.Enqueue(num);
            }
#endif
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            while(test-- > 0)
            {

#if first
                ReadBoard(sr, calc);
                int num = ArrToInt(calc);

                sw.WriteLine(dp[num]);
#else

                int n = ReadBoard(sr);
                sw.WriteLine(dp[n]);
#endif
            }

            sw.Close();
            sr.Close();
        }
#if first
        static void ReadBoard(StreamReader _sr, bool[] _arr)
        {

            int c;
            for (int i = 0; i <3; i++)
            {

                int chk = _sr.Read();

                if (chk == 'H') _arr[3 * i] = true;
                else _arr[3 * i] = false;

                _sr.Read();

                chk = _sr.Read();
                if (chk == 'H') _arr[3 * i + 1] = true;
                else _arr[3 * i + 1] = false;

                _sr.Read();

                chk = _sr.Read();
                if (chk == 'H') _arr[3 * i + 2] = true;
                else _arr[3 * i + 2] = false;

                _sr.ReadLine();
            }
        }

        static void ChangeDia(bool[] _arr, bool _isUp)
        {

            int idx = _isUp ? 2 : 0;
            _arr[idx] = !_arr[idx];

            idx = 4;
            _arr[idx] = !_arr[idx];

            idx = _isUp ? 6 : 8;
            _arr[idx] = !_arr[idx];
        }

        static void ChangeRow(bool[] _arr, int _row)
        {

            int s = _row * 3;

            for (int i = 0; i < 3; i++)
            {

                _arr[i + s] = !_arr[i + s];
            }
        }

        static void ChangeCol(bool[] _arr, int _col)
        {

            for (int i = 0; i < 3; i++)
            {

                _arr[3 * i + _col] = !_arr[3 * i + _col];
            }
        }

        static void IntToArr(bool[] _arr, int _num)
        {

            for (int i = 0; i < 9; i++)
            {

                if (((1 << i) & _num) == 0) _arr[i] = false;
                else _arr[i] = true;
            }
        }

        static int ArrToInt(bool[] _arr)
        {

            int ret = 0;
            for (int i = 0; i < 9; i++)
            {

                if (_arr[i]) ret |= 1 << i;
            }

            return ret;
        }
#else
        static int ReadBoard(StreamReader _sr)
        {

            int c;

            int ret = 0;
            for (int i = 0; i < 3; i++)
            {

                c = _sr.Read();
                if (c == 'H') ret |= 1 << 3 * i;
                _sr.Read();

                c = _sr.Read();
                if (c == 'H') ret |= 1 << 3 * i + 1;
                _sr.Read();

                c = _sr.Read();
                if (c == 'H') ret |= 1 << 3 * i + 2;
                _sr.ReadLine();
            }

            return ret;
        }
        
        static bool Record(int[] _dp, int _cur, int _idx)
        {

            if (_dp[_idx] != -1) return false;

            _dp[_idx] = _dp[_cur] + 1;
            return true;
        }

        static int Change(int _val, int _idx1, int _idx2, int _idx3)
        {

            int calc = 1 << _idx1;
            calc |= 1 << _idx2;
            calc |= 1 << _idx3;

            return _val ^ calc;
        }
#endif
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 리모컨
    문제번호 : 1107번

    브루트포스 문제다
    처음에는 문자열 계산을 이용해 가장 가까운 값을 찾으려고했다
    자리수가 바뀌는 경우, 존재 안하는 경우 등 복잡한 코드가 되었고
    5번 가까이 틀린 결과 범위가 50만밖에 안되기에,
    100만까지만 확인하면 되는 브루트포스로 처리했다

    자리수가 1억을 넘어가면 복잡한 코드가 되겠지만,
    자리수 연산을 통해 풀거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0478
    {

        static void Main478(string[] args)
        {

            int MIN = -1;
            int MAX = 1_000_001;
            StreamReader sr = new(Console.OpenStandardInput());

            string str = sr.ReadLine();
            int find = int.Parse(str);
            int btns = ReadInt();

            bool[] broken = new bool[10];
            for (int i = 0; i < btns; i++)
            {

                broken[ReadInt()] = true;
            }

            sr.Close();


            int min = MIN;
            for (int i = find; i > MIN; i--)
            {

                if (ChkInvalid(i)) continue;
                min = i;
                break;
            }

            int max = MAX;
            for (int i = find; i < MAX; i++)
            {

                if (ChkInvalid(i)) continue;
                max = i;
                break;
            }

            int ret = GetDiff(100, find);
            if (min != MIN)
            {

                int calc = GetDiff(min, find) + GetDigit(min);
                ret = calc < ret ? calc : ret;
            }

            if (max != MAX)
            {

                int calc = GetDiff(max, find) + GetDigit(max);
                ret = calc < ret ? calc : ret;
            }

            Console.WriteLine(ret);

            // 자리수 찾기
            int GetDigit(int _n)
            {

                if (_n == 0) return 1;
                
                int ret = 0;
                while(_n > 0)
                {

                    _n /= 10;
                    ret++;
                }

                return ret;
            }

            // 유효한 숫자인지 확인
            bool ChkInvalid(int _n)
            {

                if (_n == 0) return broken[0];
                while(_n > 0)
                {

                    int calc = _n % 10;
                    if (broken[calc]) return true;
                    _n /= 10;
                }
                return false;
            }

            // 거리차 계산
            int GetDiff(int _from, int _to)
            {

                int ret = _from - _to;
                ret = ret < 0 ? -ret : ret;
                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other1
using System;
using System.Linq;

namespace acmicpc.net.Problems.BruteForce
{
    public class P001107
    {
        private static bool[] fail;
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int[] x = new int[0];
            if (m > 0)
                x = Array.ConvertAll(Console.ReadLine().Split(), x => int.Parse(x));

            fail = new bool[10];
            for (int i = 0; i < m; i++)
            {
                fail[x[i]] = true;
            }

            int ans = Math.Abs(n - 100);

            if (m < 10)
            {
                ans = Find(n, -1, -1, ans);
                ans = Find(n, n + ans, 1, ans);
            }

            Console.WriteLine(ans);
        }

        private static int Find(int x, int limit, int a, int ans)
        {
            int i = x;
            while (i != limit)
            {
                bool check = true;
                int j = i;
                int count = 0;
                while (j > 0 || i == 0)
                {
                    int d = j % 10;
                    check = !fail[d];
                    if (check is false)
                        break;

                    j /= 10;
                    count++;

                    if (i == 0)
                        break;
                }

                if (check && Math.Abs(x - i) + count < ans)
                {
                    ans = Math.Abs(x - i) + count;
                    break;
                }

                i += a;
            }

            return ans;
        }
    }
}
#elif other2
using System;
using System.IO;

namespace AlgorithmProblem
{
    class _1107_remote_controller
    {
        static bool[] remote = new bool[10];
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int N = int.Parse(sr.ReadLine());
            int M = int.Parse(sr.ReadLine());
            int[] brokenNum;
            if (M > 0)
            {
                brokenNum = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                for (int i = 0; i < M; ++i)
                {
                    // 부서진 버튼 true
                    remote[brokenNum[i]] = true;
                }
            }

            int iPMMove = abs(100 - N); // +/-로만 이동
            int iCount = 0; // 
            while(true)
            {
                if (iCount >= iPMMove)
                {
                    break;
                } 
                // N을 기준으로 +/-1씩 점진적으로 부서진 버튼의 숫자를 포함하고 있는지 체크
                // 포함하고 있지 않다면 움직인 거리 + 해당 숫자의 길이
                if (isContainBrokenNumber(N - iCount) == true)
                {
                    iCount += GetNumberLength(N - iCount);
                    break;
                }
                else if (isContainBrokenNumber(N + iCount) == true)
                {
                    iCount += GetNumberLength(N + iCount);
                    break;
                }
                ++iCount;
            }

            sw.WriteLine(minCount(iCount, iPMMove));
            sw.Flush();
            sr.Close();
            sw.Close();

            return;
        }
        static bool isContainBrokenNumber(int n)
        {
            if (n < 0)
            {
                return false;
            }

            while(true)
            {
                if (remote[n % 10] == true)
                {
                    return false;
                }
                n /= 10;
                if (n == 0)
                {
                    break;
                }
            }

            return true;
        }

        static int GetNumberLength(int n)
        {
            int length = 0;
            while(true)
            {
                ++length;
                n /= 10;
                if (n == 0)
                {
                    break;
                }
            }
            return length;
        }

        static int minCount(int n, int m)
        {
            return n < m ? n : m;
        }

        static int abs(int n)
        {
            return n < 0 ? (~n + 1) : n;
        }
    }
}

#endif
}

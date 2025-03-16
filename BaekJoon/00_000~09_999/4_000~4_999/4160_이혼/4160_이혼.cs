using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 15
이름 : 배성훈
내용 : 이혼
    문제번호 : 4160번

    중간에서 만나기 문제다.
    아이디어는 다음과 같다.

    단순 브루트포스로 접근하면 3^24 > 2000억이다.
    그래서 시간초과와 메모리 초과가 날 것이다.

    반면 중간에서 만나기로 접근하면,
    3^12 < 100만 이므로 3^12 + 3^12 < 200만 연산으로 접근 할만하다.

    잭과 질이 갖는 집의 가치를 알아야 한다.
    그리고 잭과 질의 가격이 같은 경우 잭과 질의 집의 가치가 최대가 되게 해야 한다.
    그래서 잭과 질의 집값이 아닌 차를 키로, 집 가치의 차이와 최대 집의 가치를 벨류로 Dictionary에 저장해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1409
    {

        static void Main1409(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, sum;
            int[] arr = new int[24];
            Dictionary<int, int> dic = new(540_000);

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                dic.Clear();

                int half = n / 2;

                FillSet();

                int ret = 0;

                GetRet(half);

                ret += ret;
                sw.Write(sum - ret);
                sw.Write('\n');

                void GetRet(int _idx, int _sum1 = 0, int _sum2 = 0)
                {

                    if (_idx == n)
                    {

                        int sub = Math.Abs(_sum1 - _sum2);
                        
                        if (dic.ContainsKey(sub))
                        {

                            int min = Math.Min(_sum1, _sum2);
                            ret = Math.Max(ret, min + dic[sub]);
                        }

                        return;
                    }

                    GetRet(_idx + 1, _sum1 + arr[_idx], _sum2);
                    GetRet(_idx + 1, _sum1, _sum2 + arr[_idx]);
                    GetRet(_idx + 1, _sum1, _sum2);
                }

                void FillSet(int _idx = 0, int _sum1 = 0, int _sum2 = 0)
                {

                    if (_idx == half)
                    {

                        int sub = Math.Abs(_sum1 - _sum2);
                        int max = Math.Max(_sum1, _sum2);
                        if (dic.ContainsKey(sub))
                            dic[sub] = Math.Max(dic[sub], max);
                        else
                            dic[sub] = max;

                        return;
                    }

                    FillSet(_idx + 1, _sum1 + arr[_idx], _sum2);
                    FillSet(_idx + 1, _sum1, _sum2 + arr[_idx]);
                    FillSet(_idx + 1, _sum1, _sum2);
                }
            }

            bool Input()
            {

                n = ReadInt();
                sum = 0;
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
                }

                return n != 0;

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}

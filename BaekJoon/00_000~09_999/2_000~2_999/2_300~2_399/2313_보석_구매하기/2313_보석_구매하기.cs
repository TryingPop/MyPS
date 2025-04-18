using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 보석 구매하기
    문제번호 : 2313번

    처음에는 세그먼트 트리로 하려고 했으나, 어떻게 비교할지 안떠올랐다
    그래서 검색의 힘을 빌렸다

    찾아보니 Kadane's 알고리즘?
    https://shoark7.github.io/programming/algorithm/4-ways-to-get-subarray-consecutive-sum

    해당 설명글을 보고 dp 방법으로 풀었다

    주된 아이디어는 다음과 같다
    dp를 i항을 끝으로 포함하는 최대 연속 부분수열 합을 잡는다
    그러면, dp는 이전항과, 현재 arr값만 비교하면 된다
    그리고 dp에서 가장 큰 값은 해당 배열에서 가장 큰 연속된 부분수열의 합이 보장된다!

    시간은 dp 제작 N, dp 탐색 N이다! 그래서 O(N)에 해결된다

    그리고, 초기값 부분을 간과해서 한 번 틀렸다
    예제 중에 0, 0에 가장 큰 경우가 있다!
*/

namespace BaekJoon.etc
{
    internal class etc_0033
    {

        static void Main33(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int test = ReadInt(sr);                 // 케이스
            int[] values = new int[1000];           // 보석 값어치
            int[] dpSum = new int[1000];            // 연속된 최대 총합 dp
            int[] dpStart = new int[1000];          // 연속된 최대 총합 dp의 시작접

            int[] startIdxs = new int[test];        // 출력용 시작인덱스
            int[] endIdxs = new int[test];          // 출력용 끝 인덱스

            int ret = 0;                            // 케이스별 연속인 최대 구간합들의 총합
            for (int t = 0; t < test; t++)
            {

                int len = ReadInt(sr);              // 보석 개수

                for (int i = 0; i < len; i++)
                {

                    // 보석 값어치
                    values[i] = ReadInt(sr);
                }

                // i를 끝으로 하는 가장 큰 부분수열 합 저장
                // 그리고 출력을 위해 시작 인덱스도 따로 저장한다
                dpSum[0] = values[0];
                for (int i = 1; i < len; i++)
                {

                    // 앞이 양수인 경우에만 이어 붙인다
                    // 길이 짧게하면서 늘릴려고 조건을 넣었다!
                    if (dpSum[i - 1] > 0)
                    {

                        dpStart[i] = dpStart[i - 1];
                        dpSum[i] = dpSum[i - 1] + values[i];
                    }
                    else
                    {

                        dpStart[i] = i;
                        dpSum[i] = values[i];
                    }
                }

                // dp에 가장 큰 값 찾기
                int max = dpSum[0];
                for (int i = 1; i < len; i++)
                {

                    // 큰 값 갱신인 경우
                    if (max < dpSum[i])
                    {

                        max = dpSum[i];
                        startIdxs[t] = dpStart[i];
                        endIdxs[t] = i;
                    }
                    else if (max == dpSum[i])
                    {

                        // 같은 경우면 길이 비교를 한다
                        int chk1 = i - dpStart[i];
                        int chk2 = endIdxs[t] - startIdxs[t];

                        if (chk1 < chk2)
                        {

                            startIdxs[t] = dpStart[i];
                            endIdxs[t] = i;
                        }
                    }
                }

                ret += max;
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(ret);
                for (int i = 0; i < test; i++)
                {

                    sw.Write(startIdxs[i] + 1);
                    sw.Write(' ');
                    sw.Write(endIdxs[i] + 1);
                    sw.Write('\n');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;
            bool plus = true;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}

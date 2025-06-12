using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 12
이름 : 배성훈
내용 : 줄임말
    문제번호 : 20191번

    dp, 이분탐색 문제다.
    아이디어는 다음과 같다.
    각 t의 알파벳이 출현한 위치를 dp에 저장한다.

    그리고 각 s의 알파벳을 조사한다.
    현재 위치를 cur이라고하면
    cur이상인 가장작은 s의 위치를 찾는다.
    
*/

namespace BaekJoon.etc
{
    internal class etc_1698
    {

        static void Main1698(string[] args)
        {

            // 20191 - 줄임말
            // 현재 정답확인 안됨 - > 채점만 40분 이상 밀려있다.
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            string s = sr.ReadLine();
            string t = sr.ReadLine();

            int ALPHABET = 26;
            int[][] dp = new int[ALPHABET][];

            for (int i = 0; i < ALPHABET; i++)
            {

                dp[i] = new int[t.Length + 2];
            }

            for (int i = 0; i < t.Length; i++)
            {

                int idx1 = aTi(t[i]);
                ref int idx2 = ref dp[idx1][0];
                dp[idx1][++idx2] = i;
            }

            for (int i = 0; i < ALPHABET; i++)
            {

                ref int idx2 = ref dp[i][0];
                dp[i][++idx2] = t.Length;
            }

            int cur = -1;
            int cnt = 1;
            bool impo = false;
            for (int i = 0; i < s.Length; i++)
            {

                int idx = aTi(s[i]);
                if (dp[idx][0] == 1)
                {

                    impo = true;
                    break;
                }

                int nextIdx = BinarySearch(idx, cur + 1);
                if (nextIdx == dp[idx][0])
                {

                    cnt++;
                    cur = dp[idx][1];
                }
                else cur = dp[idx][nextIdx];
            }

            if (impo) Console.Write(-1);
            else Console.Write(cnt);

            int aTi(int _alphabet)
                => _alphabet - 'a';

            int BinarySearch(int _idx, int _val)
            {

                int l = 1;
                int r = dp[_idx][0];

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (dp[_idx][mid] < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }
        }
    }
}

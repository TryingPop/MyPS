using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 고냥이
    문제번호 : 16472번

    두 포인터 문제다
    아이디어는 다음과 같다

    왼쪽 끝에서 두 포인터로 시작한다
    하나는 시작, 하나는 끝을 나타낸다
    문자열을 n개이하로 사용했다면 끝을 오른쪽으로 한칸씩 이동시키며 길이를 늘린다 
    반면 n개 초과로 문자를 사용했다면 시작을 오른쪽으로 한칸 이동 시켜 길이를 줄인다
    n개 이하로 사용했을 때 최장 길이를 찾아 정답으로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0406
    {

        static void Main406(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            string str = sr.ReadLine();
            sr.Close();
            int l = 0;
            int r = 0;
            int cur = 0;

            int[] cnt = new int[26];
            int ret = 0;

            while (r < str.Length)
            {

                if (cur <= n)
                {

                    int rIdx = str[r++] - 'a';
                    cnt[rIdx]++;
                    if (cnt[rIdx] == 1) cur++;

                    if (cur <= n && ret < r - l) ret = r - l;
                }
                else
                {

                    int lIdx = str[l++] - 'a';
                    cnt[lIdx]--;
                    if (cnt[lIdx] == 0) cur--;
                }
            }

            Console.WriteLine(ret);
        }
    }
}

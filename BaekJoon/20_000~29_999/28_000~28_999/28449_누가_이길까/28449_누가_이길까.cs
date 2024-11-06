using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 누가 이길까
    문제번호 : 28449번

    초과, 미만, 같은 경우를 찾는 문제다
    아이디어는 한 팀을 정렬을 한 뒤 이진탐색을 이용하면 쉽게 풀어진다

    다만 주의할 것은 값의 범위이다 10만개 입력이 되기에 10만 * 10만 = 100억의 값이 나올 수 있다
    그리고 이분 탐색 한 번으로는 못찾아 2번 탐색했다
    중복수 제한이 없기 때문이다!
    처음에는 미만인 개수를 찾고, 두 번째는 초과인 경우를 찾았다
    그리고 초과와 미만의 경우의 수를 합쳐서 전체 경우의 수와 차이만큼 같은 경우가 나오는 것을 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0083
    {

        static void Main83(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int hiLen = ReadInt(sr);
            int arcLen = ReadInt(sr);

            int[] hiMems = new int[hiLen];
            for (int i = 0; i < hiLen; i++)
            {

                hiMems[i] = ReadInt(sr);
            }

            Array.Sort(hiMems);

            long[] ret = new long[3];

            for (int i = 0; i < arcLen; i++)
            {

                int cur = ReadInt(sr);

                // 미만 찾기
                int left = 0;
                int right = hiLen - 1;
                while(left <= right)
                {

                    int mid = (left + right) / 2;

                    if (hiMems[mid] < cur) left = mid + 1;
                    else right = mid - 1;
                }

                int chk = right + 1;
                ret[1] += chk;

                // 초과 찾기
                left = 0;
                right = hiLen - 1;
                while (left <= right)
                {

                    int mid = (left + right) / 2;

                    if (hiMems[mid] <= cur) left = mid + 1;
                    else right = mid - 1;
                }

                ret[0] += hiLen - left;
                chk += hiLen - left;

                // 같은 경우 찾기
                if (chk < hiLen) ret[2] += hiLen - chk;
            }

            sr.Close();

            // 결과 출력
            for (int i = 0; i < 3; i++)
            {

                Console.Write(ret[i]);
                Console.Write(' ');
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

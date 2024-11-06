using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 숫자 더하기
    문제번호 : 9440번

    그리디 알고리즘, 정렬 문제다
    그리디하게 접근해서 풀었다

    아이디어는 다음과 같다
    먼저 사용된 숫자의 개수를 찾았다
    최소값은 두 수의 자리수의 차가 최소여야 한다(그리디)
    그리고 두 수를 설정할 때 앞에서부터 작은 수를 세우면 작은 수를 얻을 수 있다
    다만 예제를 보면 숫자 0은 맨 앞에 오지 못한다!

    N은 14 이하이므로 길어야 7자리 최소값의 최대값은 커봐야 1000만단위다
    그래서 int형으로 설정했다

    해당 아이디어를 구현해 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0437
    {

        static void Main437(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] cnt = new int[10];
            while (true)
            {

                int n = ReadInt();
                if (n == 0) break;

                for (int i = 0; i < n; i++)
                {

                    int c = ReadInt();
                    cnt[c]++;
                }

                int l = 0;
                int r = 0;
                int idx = 0;

                for (int j = 0; j < n; j++)
                {

                    
                    if ((j & 1) == 0)
                    {

                        idx = SetIdx(l == 0);
                        l = l * 10 + idx;
                    }
                    else
                    {

                        idx = SetIdx(r == 0);
                        r = r * 10 + idx;
                    }
                }

                sw.WriteLine(l + r);
            }

            sw.Close();
            sr.Close();

            int SetIdx(bool isFirst)
            {

                for(int i = isFirst ? 1 : 0; i < 10; i++)
                {

                    if (cnt[i] == 0) continue;
                    cnt[i]--;
                    return i; 
                }

                cnt[0]--;
                return 0;
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
}

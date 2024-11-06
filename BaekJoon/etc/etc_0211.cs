using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 문자열 변환
    문제번호 : 10453번

    에드혹 문제
    스와핑 부분 더 좋은 방법이 있을까 고민했으나,
    따로 떠오르는게 없어 일일히 찾아가는 브루트 포스로 했다
    브루트 포스라 불안으나 제출하니 208ms 로 통과했다
    (N^2인줄 알았다)

    other 부분을 주석으로 바꿔봤는데, 정답에는 이상이 없으나 속도가 더 느렸다;
    그런데, 주석의 other 부분이 N으로 기대되서 더 빠를거 같이 보인다;
*/

namespace BaekJoon.etc
{
    internal class etc_0211
    {

        static void Main211(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 2);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());
            int[] str1 = new int[100_000];
            int[] str2 = new int[100_000];

            while(test-- > 0)
            {

                int len1 = 0;
                int len2 = 0;

                int c;
                while((c = sr.Read()) != ' ')
                {

                    str1[len1++] = c;
                }

                while((c = sr.Read()) != '\n' && c != '\r')
                {

                    str2[len2++] = c;
                }

                if (c == '\r') sr.Read();

                if (len1 != len2) 
                { 
                    
                    sw.WriteLine(-1);
                    continue;
                }

                int ret = 0;
                int idx = 0;
                // int other = -1;
                while(idx < len1)
                {

                    if (str1[idx] == str2[idx])
                    {

                        idx++;
                        continue;
                    }

                    // other = other < idx + 1 ? idx + 1 : other;
                    int other = idx + 1;
                    for (int i = other; i < len1; i++)
                    {

                        if (str2[idx] != str1[i]) continue;

                        int temp = str1[idx];
                        str1[idx] = str1[i];
                        str1[i] = temp;

                        ret += i - idx;
                        break;
                    }

                    idx++;
                }

                sw.WriteLine(ret);
            }

            sw.Close();
            sr.Close();
        }
    }
}

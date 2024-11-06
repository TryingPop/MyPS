using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 사과나무
    문제번호 : 19539번

    수학, 그리디 알고리즘
    그리디하게 풀었다
    아이디어는 다음과 같다

    먼저 1, 2짜리 물뿌리개를 동시에 뿌려야하기에 전체 생장 길이는 3의 배수가 되어야한다
    그리고 1, 2 동시에 뿌리기에 홀수의 개수가 3의 배수보다 많으면 안된다
    해당 두 개로 검증하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0421
    {

        static void Main421(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int sum = 0;
            int one = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();

                sum += cur;
                if ((cur & 1) > 0) one++;
            }

            sr.Close();

            bool ret = false;
            if (sum % 3 == 0)
            {

                sum /= 3;
                ret = one <= sum;
            }

            Console.WriteLine(ret ? "YES" : "NO");

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
int n = int.Parse(Console.ReadLine());
int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
int sum = array.Sum();
Console.Write(sum % 3 == 0 && sum / 3 <= array.Sum(x => x / 2) ? "YES" : "NO");
#endif
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : GCD 합
    문제번호 : 9613번

    수학, 정수론, 유클리드 호제법 문제다
    오버플로우로 한 번 틀렸다
    입력 숫자 범위는 100만이고, 개수는 100개까지 들어온다
    그래서 100만 * 5000개이므로, int 범위를 초월한다
    이후에는 GCD를 모두 합치니 이상없이 통과했다

    2024. 11. 2 오늘 틀린 문제 기록을 보니 추가되어 있어 수정했다.
    이전 코드를 보니 논리엔 이상없어 보이고, 다른 사람 채점현황 보니 
    Format 에러가 있었다. Format 에러는 문자열 변환 에러이므로 입력 문제라 판단하고
    입력 부분만 수정하니 이상없이 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_0397
    {

        static void Main397(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            int[] arr = new int[100];

            while(test-- > 0)
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    arr[i] = ReadInt();
                }

                long ret = 0;

                for (int i = 0; i < len - 1; i++)
                {

                    for (int j = i + 1; j < len; j++)
                    {

                        int gcd = GetGcd(arr[i], arr[j]);
                        ret += gcd;
                    }
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int GetGcd(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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

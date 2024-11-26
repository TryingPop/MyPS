using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 26
이름 : 배성훈
내용 : COW Operations
    문제번호 : 24979번

    문자열, 누적합 문제다
    아이디어는 다음과 같다.
    구간에서 C + W는 홀수, O + W는 짝수여야 C로 바꾸는 것이 가능하다.
    여기서 C, O, W는 각각의 구간에 출현한 개수로 해석하면 된다.

    그래서 누적합으로 개수를 저장했고, 비트 연산자로 홀짝을 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1132
    {

        static void Main1132(string[] args)
        {

            StreamReader sr;
            
            int[] prefix;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int q = ReadInt();

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    while (q-- > 0)
                    {

                        int l = ReadInt();
                        int r = ReadInt();

                        sw.Write((prefix[r] ^ prefix[l - 1]) == 1 ? 'Y' : 'N');
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string temp = sr.ReadLine();
                prefix = new int[temp.Length + 1];
                int[] cTn = new int[128];
                cTn['C'] = 1;
                cTn['O'] = 2;
                cTn['W'] = 3;

                for (int i = 1; i <= temp.Length; i++)
                {

                    prefix[i] = prefix[i - 1] ^ cTn[temp[i - 1]];
                }
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
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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

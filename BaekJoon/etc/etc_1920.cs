using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 29
이름 : 배성훈
내용 : RedRover
    문제번호 : 15073번

    브루트프소, 문자열 문제다.
    모든 연속된 부분 문자열을 매크로로 만들어
    가장 짧은 것을 찾는다.

    여기서 부분 문자열의 길이는 1인 경우 1개를 더 보내므로
    1개짜리는 만들지 않고, 마찬가지로 길이가 기존 문자열의 절반을 넘어가는 경우도 만들지 않는다.

    그래서 만들 수 있는 전체 부분문자열은 문자열의 길이를 N이라 하면
    부분문자열의 개수는 N^2 / 4이 된다.

    그리고 부분문자열을 각각 M으로 몇 개 만들 수 있는지 찾는다.
    다만 주의할 것은 그리디로 먼저 매칭되는 것을 전체 매크로로 보내는게 최소임이 보장된다.
    여기서 KMP를 쓰면 O(N)에 찾을 수 있지만, 일일히 확인해도 O(N^2 / 2)이다.
    N이 100이하이므로 N^4 / 8이고 이는 시도해볼만하다 판단했고 KMP를 안쓰고 찾았다.
    그리디로 KMP를 쓴다면 매칭되면 초기 위치를 0으로 수정해줘야한다!
*/

namespace BaekJoon.etc
{
    internal class etc_1920
    {

        static void Main1920(string[] args)
        {

            string str;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = str.Length;

                int half = ret / 2;
                for (int len = 2; len <= half; len++)
                {

                    for (int start = 0; start < str.Length - len; start++)
                    {

                        ret = Math.Min(ret, Find(start, len));
                    }
                }

                Console.Write(ret);

                int Find(int s, int len)
                {

                    int ret = len;

                    for (int i = 0; i < str.Length; i++)
                    {

                        // 더빠르게 찾고 싶다면 여기를 KMP로 찾으면 된다.
                        // 매칭되는 경우 초기 값을 0으로 바꿔줘야 한다.
                        if (Match(s, i, len))
                            i += len - 1;
                        ret++;
                    }

                    return ret;
                }

                bool Match(int s1, int s2, int len)
                {

                    if (str.Length < s2 + len) return false;

                    for (int i = 0; i < len; i++)
                    {

                        if (str[i + s1] == str[i + s2]) continue;
                        return false;
                    }

                    return true;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();
            }
        }
    }
}

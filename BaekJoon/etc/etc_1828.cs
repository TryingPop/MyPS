using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 16
이름 : 배성훈
내용 : 심심한 준규
    문제번호 : 2892번

    비트마스킹 문제다.
    암호문과 key의 관계를 보면,
    아스키코드 값 기준으로 암호문[i] ^ key[i]로 값을 찾아간다.

    나오는 결과는 16비트로 표현하면 된다.
    그래서 나오는 결과에 10을 표현하는 a와 같은 문자가 올 수 있다.
    
    이렇게 진행해보면 '0' ~ '9'와 겹치는 값은 16이상 32를 넘지 못함을 알 수 있다.
    즉, '.' ^ '0', '.' ^ '1', ..., '.' ^ '9'와
    ' ' ^ '0', ..., ' ' ^ '9'를 진행해서 찾았다.

    이를 16비트로 표현하면 앞이 1임을 알 수 있다.
    이외 'a' ~ 'z'와 '0' ~ '9'를 ^ 연산을 진행한 결과
    50을 넘음을 알 수 있다.

    그래서 16비트로 표현했을 때 앞이 1이면 .으로
    아니라면 문자인 -로 해석해 제출했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1828
    {

        static void Main1828(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n = ReadInt();

            int[] cur = new int[2];
            for (int i = 0; i < n; i++)
            {

                ReadVal();
                if (cur[0] == '1') sw.Write('.');
                else sw.Write('-');
            }

            void ReadVal()
            {

                int c = sr.Read();
                if (c == ' ') c = sr.Read();

                cur[0] = c;
                cur[1] = sr.Read();
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
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

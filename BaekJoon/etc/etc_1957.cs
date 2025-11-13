using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 30
이름 : 배성훈
내용 : 문서 검색
    문제번호 : 1543번

    문자열, 브루트포스 문제다.
    먼저 매칭되면 매칭을 세는게 최대임이 그리디로 보장된다.
    브루트포스로 매칭되는지 조사해도 2500 x 50 으로 100만을 넘지 않아
    브루트포스로 조사하면서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1957
    {

        static void Main1957(string[] args)
        {

            string str, chk;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    bool flag = false;
                    for (int j = 0; j < chk.Length; j++)
                    {

                        if (i + j < str.Length && str[i + j] == chk[j]) continue;
                        flag = true;
                        break;
                    }

                    if (flag) continue;
                    i += chk.Length - 1;
                    ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                str = sr.ReadLine();
                chk = sr.ReadLine();
            }
        }
    }
}

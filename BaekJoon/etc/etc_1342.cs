using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 17
이름 : 배성훈
내용 : 유니대전 퀴즈쇼
    문제번호 : 20362번

    구현, 문자열 문제다.
    정답자 챗을 찾고, 해당 챗의 갯수를 세어주면 된다.
    챗의 갯수는 dictionary에 단어와 출현 횟수를 저장해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1342
    {

        static void Main1342(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            Dictionary<string, int> chat;
            string end;

            Solve();
            void Solve()
            {

                string[] temp = sr.ReadLine().Split();
                int n = int.Parse(temp[0]);
                end = temp[1];

                chat = new(n);
                int ret = 0;
                for(int i = 0; i < n; i++)
                {

                    temp = sr.ReadLine().Split();

                    if (temp[0] == end) 
                    {

                        if (chat.ContainsKey(temp[1])) ret = chat[temp[1]];
                        break;
                    }

                    if (chat.ContainsKey(temp[1])) chat[temp[1]]++;
                    else chat[temp[1]] = 1;
                }

                Console.Write(ret);
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 25
이름 : 배성훈
내용 : 임스와 함께하는 미니게임
    문제번호 : 25757번

    자료 구조, 문자열, 해시 문제다
    플레이할 사람들의 이름을 모아놓아 나누어 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_1081
    {

        static void Main1081(string[] args)
        {

            
            StreamReader sr;
            HashSet<string> user;
            int n, div;
            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < n; i++)
                {

                    user.Add(sr.ReadLine());
                }

                sr.Close();
                Console.Write(user.Count / div);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);

                if (temp[1] == "Y") div = 1;
                else if (temp[1] == "F") div = 2;
                else div = 3;

                user = new(n);
            }
        }
    }
}

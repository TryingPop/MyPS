using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 임진왜란
    문제번호 : 3077번

    문자열 데이터를 해시인 Dictionary로 저장하고
    이를 브루트 포스로 비교하며 정답을 제출하니 맞혔다
    다만 시간은 오래 걸렸다 452ms
*/

namespace BaekJoon.etc
{
    internal class etc_0157
    {

        static void Main157(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());
            Dictionary<string, int> sTon = new(n);
            {

                string[] temp = sr.ReadLine().Split(' ');
                for (int i = 0; i < n; i++)
                {

                    sTon.Add(temp[i], i);
                }
            }

            string[] chk = sr.ReadLine().Split(' ');
            sr.Close();

            int score = 0;
            for (int i = 0; i < n; i++)
            {

                for (int j = i + 1; j < n; j++)
                {

                    if (sTon[chk[i]] < sTon[chk[j]]) score++;
                }
            }

            Console.Write($"{score}/{((n - 1) * n) / 2}");
        }

        
    }
}

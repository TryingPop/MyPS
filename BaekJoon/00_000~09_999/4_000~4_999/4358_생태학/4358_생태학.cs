using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 24
이름 : 배성훈
내용 : 생태학
    문제번호 : 4358번

    해시 자료구조를 이용해 문제를 구현하는 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0085
    {

        static void Main85(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            // 나무 종 최대 1만
            Dictionary<string, int> dp = new(10_000);
            int total = 0;
            while (true)
            {

                string temp = sr.ReadLine();

                if (temp == null || temp == string.Empty) break;
                // 나무 종 입력
                total++;
                // 있으면 개수 추가, 없으면 새로 등록
                if (dp.ContainsKey(temp)) dp[temp] += 1;
                else dp[temp] = 1;
            }

            sr.Close();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // Key를 str로 정렬해서 하나씩 가져온다
            foreach(var key in dp.Keys.OrderBy(x => x))
            {

                double val = dp[key];
                double per = (val / total) * 100;
                sw.Write($"{key} {per:0.0000}\n");
            }

            sw.Close();
        }
    }
}

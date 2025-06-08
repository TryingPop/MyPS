using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 악마 게임
    문제번호 : 16677번

    구현 문자열 문제다
    기존 문자열이 살아있는지 확인하기 위해
    두 포인터의 알고리즘을 이용했고, 

    문자열에 따라 점수도 같이 포함해야하기에
    만족하는 문자열을 저장할 자료구조는 딕셔너리를 했다

    그리고 double로 연산을 해서 삽입별 
    점수를 비교는 double로 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0293
    {

        static void Main293(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string origin = sr.ReadLine();
            int originLen = origin.Length;

            int n = int.Parse(sr.ReadLine());

            Dictionary<string, int> save = new(n);

            for (int i = 0; i < n; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int idx = 0;

                for (int j = 0; j < temp[0].Length; j++)
                {

                    if (origin[idx] != temp[0][j]) continue;
                    idx++;

                    if (idx != originLen) continue;

                    // 기존 문자가 살아있으면 save에 넣는다
                    save[temp[0]] = int.Parse(temp[1]);
                    break;
                }
            }

            sr.Close();

            string ret = "";
            double max = -1;
            foreach(var item in save)
            {

                double b = item.Key.Length - originLen;
                double f = item.Value;

                // 기존 문자랑 무조건 다르기 때문에
                // zero division 에러 안뜬다!
                double calc = f / b;

                if (max >= calc) continue;

                // 효율이 높은지 확인
                ret = item.Key;
                max = calc;
            }

            if (max == -1) Console.WriteLine("No Jam");
            else Console.WriteLine(ret);
        }
    }
}

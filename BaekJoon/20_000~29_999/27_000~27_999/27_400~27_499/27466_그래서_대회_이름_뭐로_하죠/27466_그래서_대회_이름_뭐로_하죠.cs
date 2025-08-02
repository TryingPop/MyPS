using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 그래서 대회 이름 뭐로 하죠
    문제번호 : 27466번

    문자열, 구현, 그리디 알고리즘 문제다
    아무 경우나 출력하면 되기에 뒤에서 부터 넣으면서 먼저 발견되는 것을 출력되게 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0189
    {

        static void Main189(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            string str = sr.ReadLine();
            sr.Close();
            char[] ret = new char[info[1]];
            int findIdx = -1;

            // 맨끝 문자 확인
            for (int i = str.Length - 1; i>= 0; i--)
            {

                if (str[i] == 'A' || str[i] == 'E' || str[i] == 'I' || str[i] == 'O' || str[i] == 'U') continue;
                ret[0] = str[i];
                findIdx = i;
                break;
            }

            int findA = 0;
            // AA 2개 확인
            for (int i = findIdx - 1; i >= 0; i--)
            {

                if (str[i] == 'A')
                {

                    findA++;
                    ret[findA] = 'A';
                    findIdx = i;
                    if (findA == 2) break;
                }
            }

            // 앞부분 채워넣기
            int curIdx = 3;
            for (int i = findIdx - 1; i >= 0; i--)
            {

                if (curIdx == info[1]) break;
                ret[curIdx++] = str[i];
            }

            // 이제 다 채워졌는지 확인
            bool exsist = true;
            for (int i = 0; i < ret.Length; i++)
            {

                if (ret[i] == 0) 
                { 
                    
                    exsist = false;
                    break;
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            if (exsist)
            {

                sw.WriteLine("YES");
                // 반대로 넣었기에 출력에서 반대로 출력한다
                for (int i = info[1] - 1; i >= 0; i--)
                {

                    sw.Write(ret[i]);
                }
            }
            else sw.WriteLine("NO");

            sw.Close();
        }


    }
}

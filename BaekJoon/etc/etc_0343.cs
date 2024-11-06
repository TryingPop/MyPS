using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 페이지 세기
    문제번호 : 4821번

    구현, 문자열, 파싱 문제다
    조건대로 구현했다
    다만, Split연산을 여러 번 써서 메모리를 많이 먹었다
*/

namespace BaekJoon.etc
{
    internal class etc_0343
    {

        static void Main343(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            bool[] chk = new bool[1_001];

            while (true)
            {

                int n = int.Parse(sr.ReadLine());
                if (n == 0) break;

                string[] str = sr.ReadLine().Split(',');
                for (int i = 0; i < str.Length; i++)
                {

                    string[] temp = str[i].Split('-');
                    int low = int.Parse(temp[0]);
                    if(temp.Length > 1)
                    {

                        int high = int.Parse(temp[1]);
                        if (high < low) continue;

                        int e = high < n ? high : n;
                        for (int j = low; j <= e; j++)
                        {

                            chk[j] = true;
                        }
                    }
                    else if (low <= n) chk[low] = true;
                }

                int ret = 0;
                for (int i = 0; i <= n; i++)
                {

                    if (!chk[i]) continue;
                    chk[i] = false;
                    ret++;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
        }
    }
}

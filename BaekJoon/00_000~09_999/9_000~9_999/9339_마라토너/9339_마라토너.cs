using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 6
이름 : 배성훈
내용 : 마라토너
    문제번호 : 9339번

    구현 문제다.
    해시를 이용해 마라토너를 저장했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1380
    {

        static void Main1380(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = int.Parse(sr.ReadLine());
            HashSet<int> students = new(100);

            while (t-- > 0)
            {

                students.Clear();

                int n = int.Parse(sr.ReadLine());
                string[] temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    students.Add(int.Parse(temp[i]));
                }

                int h = 24;
                int m = 60;
                int ret1 = -1;
                int ret2 = 0;

                int len = int.Parse(sr.ReadLine());


                for (int i = 0; i < len; i++)
                {

                    temp = sr.ReadLine().Split();
                    int id = int.Parse(temp[0]);
                    if (!students.Contains(id)) continue;
                    int chkH = int.Parse(temp[1]);
                    if (chkH == -1) continue;

                    int chkM = int.Parse(temp[2]);
                    if (chkH < h || (chkH == h && chkM < m))
                    {

                        ret1 = id;
                        h = chkH;
                        m = chkM;
                    }

                    if (chkH < 6 || (chkH == 6 && chkM == 0)) ret2++;
                }

                sw.Write($"{ret1} {ret2}\n");
            }
        }
    }
}

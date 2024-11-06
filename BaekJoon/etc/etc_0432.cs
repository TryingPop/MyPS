using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : 순열 사이클
    문제번호 : 10451번

    순열 사이클 분할 문제다
    그냥 일일히 사이클을 찾아갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0432
    {

        static void Main432(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt();
            int[] arr = new int[1_001];
            bool[] use = new bool[1_001];

            while(test-- > 0)
            {

                int n = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    use[i] = false;
                }

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;
                    ret++;
                    int chk = arr[i];
                    while (!use[chk])
                    {

                        use[chk] = true;
                        chk = arr[chk];
                    }
                }

                sw.WriteLine(ret);
            }

            sw.Close();
            sr.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}

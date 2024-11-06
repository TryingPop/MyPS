using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 학번
    문제번호 : 3711번
*/

namespace BaekJoon.etc
{
    internal class etc_0312
    {

        static void Main312(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);
            int[] id = new int[300];
            HashSet<int> r = new(300);

            while(test-- > 0)
            {

                int len = ReadInt(sr);

                for (int i = 0; i < len; i++)
                {

                    id[i] = ReadInt(sr);
                }

                int ret = 0;
                for (int j = 1; j <= 1_000_000; j++)
                {

                    for (int i = 0; i < len; i++)
                    {

                        int div = id[i] % j;
                        r.Add(div);
                    }

                    if (r.Count == len)
                    {

                        ret = j;
                        break;
                    }
                    r.Clear();
                }

                sw.WriteLine(ret);
                r.Clear();
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

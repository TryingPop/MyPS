using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 암기왕
    문제번호 : 2776번

    정렬, 이분탐색 문제다
    입력 개수가 많아 시간이 오래 걸린다

    해시를 써서 풀 수도 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0359
    {

        static void Main359(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 512);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 1024 * 512);
            int test = ReadInt();
            int[] remem = new int[1_000_000];

            while(test-- > 0)
            {

                int rememLen = ReadInt();

                for (int i = 0; i < rememLen; i++)
                {

                    remem[i] = ReadInt();
                }

                Array.Sort(remem, 0, rememLen);

                int retLen = ReadInt();
                for (int i = 0; i < retLen; i++)
                {

                    bool find = false;
                    int chk = ReadInt();
                    int l = 0;
                    int r = rememLen - 1;
                    while(l <= r)
                    {

                        int mid = (l + r) / 2;

                        if (chk == remem[mid])
                        {

                            find = true;
                            break;
                        }
                        else if (chk < remem[mid]) r = mid - 1;
                        else l = mid + 1;
                    }

                    if (find) sw.WriteLine(1);
                    else sw.WriteLine(0);
                }

                sw.Flush();
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}

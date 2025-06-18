using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 공포의 면담실
    문제번호 : 30088번

    간단한 구현과 그리디 문제다
    풀이는 간단하다
    
    해당 부서의 끝나는 시간이 남은 부서들에게 계속해서 누적된다
    그래서 부서별로 면담하는게 끝나는 시간이 빨라진다

    그리고 반복해서 누적되기에 누적되는 값이 작으면 빨라진다
    그래서 빨리 끝나는 부서부터 보내는게 좋다
*/

namespace BaekJoon.etc
{
    internal class etc_0076
    {

        static void Main76(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            long ret = 0;

            int len = ReadInt(sr);

            // 부서별 총 시간을 저장한다
            int[] total = new int[len];
            for (int i = 0; i < len; i++)
            {

                int chk = ReadInt(sr);

                for (int j = 0; j < chk; j++)
                {

                    total[i] += ReadInt(sr);
                }
            }

            sr.Close();

            // 정렬해서 걸리는 시간을 구한다
            Array.Sort(total);
            long sum = 0;
            
            for (int i = 0; i < len; i++)
            {

                sum += total[i];
                ret += sum;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }
            
            return ret;
        }
    }
}

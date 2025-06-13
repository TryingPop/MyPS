using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 평행 우주
    문제번호 : 17451번

    수학, 그리디 문제다
    아이디어는 다음과 같다
    해당 행성을 지나기 위해서는 속도가 해당 행성에서 요구하는 값의 배수여야한다
    그리디하게 맨 뒤에서부터 출발해서 배수가 되게 최소한도로 올리면서 값을 세팅했다
*/

namespace BaekJoon.etc
{
    internal class etc_0315
    {

        static void Main315(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            long ret = arr[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {

                long r = ret % arr[i];
                if (r == 0) continue;
                ret += arr[i] - r;
            }

            Console.WriteLine(ret);
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

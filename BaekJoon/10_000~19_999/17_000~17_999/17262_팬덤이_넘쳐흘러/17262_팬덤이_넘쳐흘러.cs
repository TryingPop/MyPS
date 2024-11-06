using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 7
이름 : 배성훈
내용 : 팬덤이 넘쳐 흘러
    문제번호 : 17262번

    문제 해결력을 물어보는 문제이다
    시작의 최대값과 끝의 최소값을 구해 빼주면 된다
    만약 음수인 경우는 모두 겹치는 경우 뿐이므로 0으로 출력한다
*/

namespace BaekJoon.etc
{
    internal class etc_0002
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int end = 100_000;
            int start = 1;

            for (int i = 0; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');
                int s = int.Parse(temp[0]);
                int e = int.Parse(temp[1]);

                if (start < s) start = s;
                if (e < end) end = e;
            }

            int result = start - end;
            if (result < 0) result = 0;
            Console.WriteLine(result);
        }
    }
}

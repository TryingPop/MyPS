using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 오늘도 졌다
    문제번호 : 14582번

    구현 문제다
    역전 순간 찾는 것인데 점수가 앞선 순간이 단 한순간이라도 있을 때로 해석해야한다
    즉, 같은 회차인데, 선턴인 wt가 먼저 점수를 획득해도 역전패이다
*/

namespace BaekJoon.etc
{
    internal class etc_0372
    {

        static void Main372(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int[] wt = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] st = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int diff = 0;
            bool onePlus = false;
            for (int i = 0; i < wt.Length; i++)
            {

                diff += wt[i];
                if (diff > 0) onePlus = true;
                diff -= st[i];
            }

            if (onePlus) Console.WriteLine("Yes");
            else Console.WriteLine("No");
        }
    }
}

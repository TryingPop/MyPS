using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 12
이름 : 배성훈
내용 : 콜라츠
    문제번호 : 5393번

    1 ~ N개의 구멍이 있는 나무를 잘라 가는데 몇 개의 밧줄을 끊어야하는지 묻는 문제!
*/

namespace BaekJoon.etc
{
    internal class etc_0021
    {

        static void Main21(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int t = int.Parse(sr.ReadLine());
            while (t-- > 0)
            {

                // 짝수 a는 a / 2와 밧줄로 연결되어져 있다
                // 그리고 홀수 b는 3b + 1과 밧줄로 연결되어져 있다
                // n보다 큰 구멍과 연결된 밧줄을 끊어야한다
                long n = long.Parse(sr.ReadLine());

                // Math.Ceiling(n / 2)보다 큰 구멍들은 자신과 연결된 짝수들과 끊어야한다!
                long cnt = (n + 1) / 2;

                // 그리고 홀수 a에 대해, 3a + 1 > n과 연결된 밧줄들을 끊어야한다
                // 경우의 수로 구분하면 된다
                // 3a + 1수와 홀수이므로 6배수에 따라 결과가 1개씩 늘어난다
                // 1 2 3 4 5 6
                // 1 1 2 1 3 2
                cnt += (n % 6 == 0 || n % 6 == 4) ? n / 3 : n / 3 + 1;
                sw.WriteLine(cnt);
            }

            sw.Close();
        }
    }
}

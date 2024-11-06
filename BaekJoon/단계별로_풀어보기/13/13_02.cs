using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 9
이름 : 배성훈
내용 : 회의실 배정
    문제번호 : 1931번
*/

namespace BaekJoon._13
{
    internal class _13_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int length = int.Parse(sr.ReadLine());
            
            // 가변 배열
            // int[][] times = new int[length][];
            List<long[]> times = new List<long[]>();


            for (int i = 0; i < length; i++)
            {

                times.Add(Array.ConvertAll(sr.ReadLine().Split(' '), item => long.Parse(item)));
            }
            sr.Close();

            times = times.OrderBy(time => time[1]).ThenBy(time => time[0]).ToList();
            // times = times.OrderBy(time => time[1]).ThenByDescending(time => time[0]).ToList();   // 이걸 사용하면 시작 시간 = 종료시간인 회의가 있는 경우
                                                                                                    // 이를 먼저하기에 최대 숫자가 될 수 없다
            int result = 0;
            long time = 0;

            for (int i = 0; i < length; i++)
            {
                
                if (time <= times[i][0])
                {

                    result++;
                    time = times[i][1];
                }
            }

            Console.WriteLine(result);
        }
    }
}


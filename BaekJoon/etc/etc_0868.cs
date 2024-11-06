using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 7
이름 : 배성훈
내용 : 4번째 점
    문제번호 : 1894번

    수학, 구현, 기하학 문제다
    입력 데이터를 잘못 이해해 한 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0868
    {

        static void Main868(string[] args)
        {


            double E = 1e-9;
            int fix1, fix2;
            int oth1, oth2;
            Solve();


            void Solve()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                fix1 = -1;
                fix2 = -1;

                string temp = sr.ReadLine();
                while (!string.IsNullOrEmpty(temp))
                {

                    double[] arr = temp.Split().Select(double.Parse).ToArray();

                    FindFixed(arr);

                    if (fix1 == 0) oth1 = 1;
                    else oth1 = 0;

                    if (fix2 == 2) oth2 = 3;
                    else oth2 = 2;

                    sw.Write($"{arr[oth2 * 2] + arr[oth1 * 2] - arr[fix1 * 2]:0.000} {arr[oth2 * 2 + 1] + arr[oth1 * 2 + 1] - arr[fix1 * 2 + 1]:0.000}\n");
                    temp = sr.ReadLine();
                }

                sr.Close();
                sw.Close();
            }

            void FindFixed(double[] _arr)
            {

                for (int i = 0; i < 2; i++)
                {

                    for (int j = 2; j < 4; j++)
                    {

                        if (Math.Abs(_arr[i * 2] - _arr[j * 2]) < E
                            && Math.Abs(_arr[i * 2 + 1] - _arr[j * 2 + 1]) < E)
                        {

                            fix1 = i;
                            fix2 = j;
                            return;
                        }
                    }
                }
            }
        }
    }
}

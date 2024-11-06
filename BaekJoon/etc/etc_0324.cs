using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 신기한 소수
    문제번호 : 2023번

    수학, 정수론, 백트래킹, 소수판정 문제다
    맨 앞을 포함한 연속한 자리수들이 소수를 이뤄야한다
    그래서 생각해보니 4자리를 찾는다하면, 앞의 3자리는 3자리 경우에 포함되는 원소 중에 1개여야한다
    이러한 아이디어로 만들어가니 이상없이 풀렸다

    이전 개수 * 10개씩 판별하지만, 
    4에서 20개를 못넘긴다, 8에서 5개로 줄어든다
*/

namespace BaekJoon.etc
{
    internal class etc_0324
    {

        static void Main324(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            List<int>[] ret = new List<int>[n];
            for (int i = 0; i < n; i++)
            {

                ret[i] = new();
            }

            ret[0].Add(2);
            ret[0].Add(3);
            ret[0].Add(5);
            ret[0].Add(7);
            
            for (int i = 1; i < n; i++)
            {

                for (int j = 0; j < ret[i - 1].Count; j++)
                {

                    int cur = ret[i - 1][j] * 10;
                    for (int k = 1; k < 10; k++)
                    {

                        int chk = cur + k;
                        if (ChkPrime(chk)) ret[i].Add(chk);
                    }
                }

                ret[i].Sort();
            }

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            for (int i = 0; i < ret[n - 1].Count; i++)
            {

                sw.WriteLine(ret[n - 1][i]);
            }

            sw.Close();

            static bool ChkPrime(int _n)
            {

                for (int i = 2; i <= _n; i++)
                {

                    if (i * i > _n) break;
                    if (_n % i == 0) return false;
                }

                return true;
            }
        }

    }
}

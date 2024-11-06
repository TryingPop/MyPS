using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 9
이름 : 배성훈
내용 : 배너 걸기
    문제번호 : 27527번

    특정 구간 내에 90% 이상이 하나의 값으로 같은지 확인하는 문제이다
    입력 범위가 1 ~ 100만이므로 그냥 100만짜리 배열을 써서 O(N)의 시간으로 풀었다

    최대 입력이 20만이라 arr 읽는데 끊길가 조마조마 했다
    일단 한방에 읽어보고 틀리면 그때 구현하는 마음으로 먼저 제출했다
    앞으로는 그냥 하나씩 읽는 입력함수를 만들어 써야겠다

    실제로 하나씩 읽으니 속도와 메모리가 많이 절약되었다!
*/

namespace BaekJoon.etc
{
    internal class etc_0005
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // int[] arr = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] arr = new int[info[0]];

            for (int i = 0; i < info[0]; i++)
            {

                int n = 0;
                int c;

                while((c = sr.Read()) != ' ' && c != '\r' && c != '\n' && c != -1)
                {

                    n *= 10;
                    n += c - '0';
                }

                arr[i] = n;
            }

            sr.Close();

            
            int chk = (int)(Math.Ceiling((double)info[1] * 0.9));

            int[] dp = new int[1_000_000 + 1];

            bool result = false;
            for (int i = 0; i < info[1]; i++)
            {

                dp[arr[i]]++;
                int calc = dp[arr[i]];

                if (calc >= chk)
                {

                    result = true;
                    break;
                }
            }

            if (!result)
            {

                for (int i = info[1]; i < info[0]; i++)
                {

                    dp[arr[i - info[1]]]--;
                    dp[arr[i]]++;
                    int calc = dp[arr[i]];

                    if (calc >= chk)
                    {

                        result = true;
                        break;
                    }
                }
            }

            if (result) Console.WriteLine("YES");
            else Console.WriteLine("NO");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 크리 문자열
    문제번호 : 11059번

    브루트포스, 누적합 문제다
    누적합이 떠오르지 않아, 브루트포스로 해결했다
    이후에 누적합 힌트를 보고 누적합으로 다시 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0623
    {

        static void Main623(string[] args)
        {

            StreamReader sr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput());
                string str = sr.ReadLine();
                sr.Close();

                int[] sum = new int[str.Length + 1];
                sum[1] = str[0] - '0';
                for (int i = 0; i < str.Length; i++)
                {

                    sum[i + 1] = sum[i] + str[i] - '0';
                }

                int start = (str.Length / 2) * 2;
                for (int size = start; size >= 2; size -= 2)
                {

                    int half = size / 2;
                    for (int j = 0; j <= str.Length - size; j++)
                    {

                        int f = sum[j + half] - sum[j];
                        int b = sum[j + size] - sum[j + half];
                        if(f == b)
                        {

                            Console.WriteLine(size);
                            return;
                        }
                    }
                }
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {
    static int[] input, sum;
    static int ANSWER;

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        String s = br.readLine();

        input = new int[s.length() + 1];
        sum = new int[s.length() + 1];
        //input, sum구성
        for(int i = 0 ; i < s.length() ; ++i) {
            input[i+1] = s.charAt(i) - '0';
            sum[i+1] = input[i+1] + sum[i];
        }

        int size = s.length();

        ANSWER = size % 2 == 0 ? size : size-1; // 최고길이의 짝수부터 진행

        while(ANSWER != 0) {
            int start = 1;
            int end = start + ANSWER - 1;

            while(end <= s.length()) {
                int mid = (start + end)  / 2;

                int a = sum[mid] - sum[start-1];
                int b = sum[end] - sum[mid];

                if(a == b) {
                    System.out.println(ANSWER);
                    return;
                }

                start++;
                end++;
            }

            ANSWER -= 2; // 정답이 항상 짝수개이므로 2개씩 줄임
        }
    }
}

#endif
}

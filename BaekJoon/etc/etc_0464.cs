using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 그렇고 그런 사이
    문제번호 : 20921번

    그리디, 해 구성하기 문제다
    그냥 앞에서부터 가장 큰 수를 배치하면 뒤에 원소의 개수만큼 그렇고 그런 사이를 얻는다
    반면 가장 작은 수를 배치하면 그렇고 그런 사이를 하나도 못얻는다
    이러한 방법으로 빼가면서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0464
    {

        static void Main464(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536);
            int n = ReadInt();
            int k = ReadInt();

            int l = 1;
            int r = n;

            for (int i = n - 1; i >= 0; i--)
            {

                if (k - i >= 0)
                {

                    k -= i;
                    sw.Write(r);
                    r--;
                }
                else
                {

                    sw.Write(l);
                    l++;
                }

                sw.Write(' ');
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int N = input[0], K = input[1];
int[] ans = new int[N];
for (int i = 0; i < N; i++)
    ans[i] = i + 1;

while(K > 0)
{
    for(int i=0; i<N-1; i++)
    {
        if (ans[i] < ans[i+1])
        {
            int tmp = ans[i];
            ans[i] = ans[i + 1];
            ans[i + 1] = tmp;
            K--;
        }
        if (K == 0)
            break;
    }
}
for (int i = 0; i < N; i++)
    sw.Write(ans[i] + " ");
sr.Close();
sw.Close();

#endif
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 피보나치
    문제번호 : 9711번

    수학, dp 문제다
    1 1 반례로 한 번 틀렸다
    이외는 조건대로 구현하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0532
    {

        static void Main532(string[] args)
        {

            string CASE = "Case #";

            StringBuilder sb = new StringBuilder(150);
            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

            int test = ReadInt();
            for (int t = 1; t <= test; t++)
            {

                int p = ReadInt();
                uint q = (uint)ReadInt();

                uint c = 1 % q;
                uint b = 0;
                uint temp;
                for (int i = 1; i < p; i++)
                {

                    temp = c;
                    c = (b + temp) % q;
                    b = temp;
                }

                sb.Append(CASE);
                sb.Append($"{t}: {c}\n");
                sw.Write(sb);
                sb.Clear();
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// cs9711 - rbybound
// 2023-04-04 17:10:15
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
StringBuilder sb = new StringBuilder();
////////////////////////////////////////////////////////////////////////////////

int T = int.Parse(sr.ReadLine());
int[] line;
long P, Q;
long[] mem = new long[10010];

for(int t = 1; t <= T; t++)
{
    line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    (P, Q) = (line[0], line[1]);
    mem[0] = 0;
    mem[1] = 1 % Q;
    for (int i = 2; i <= P; i++)
        mem[i] = (mem[i - 1] + mem[i - 2]) % Q;

    sb.AppendFormat("Case #{0}: {1}\n", t, mem[P]);
}

sw.Write(sb);


sw.Close();
sr.Close();
return;
#elif other2
var reader = new Reader();
var n = reader.NextInt();

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
for (int i = 1; i <= n; i++)
{
    var p = reader.NextInt();
    var q = reader.NextInt();
    var dp = new int[10001]; 
    Array.Fill(dp, -1);
    dp[1] = 1 % q; dp[2] = 1 % q;

    w.WriteLine($"Case #{i}: {Fibo(dp, p, q)}");
}

int Fibo(int[] dp, int p, int q)
{
    if (dp[p] != -1)
        return dp[p];

    return dp[p] = (int)(((long)Fibo(dp, p - 1, q) + Fibo(dp, p - 2, q)) % q);
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif

}

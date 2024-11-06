using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 자두나무
    문제번호 : 2240번

    dp문제다
    배낭아이디어처럼 풀었다
    dp는 t초때 이동횟수에 따른 최대값을 저장시켰다
*/

namespace BaekJoon.etc
{
    internal class etc_0349
    {

        static void Main349(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int t = ReadInt();
            int w = ReadInt();

            int[,] dp = new int[t + 1, w + 1];

            for (int i = 1; i <= t; i++)
            {

                int cur = ReadInt() == 1 ? 0 : 1;

                // 0번 이동은 0번 이동에서만 넘어온다
                if (cur == 0) dp[i, 0] = dp[i - 1, 0] + 1;
                else dp[i, 0] = dp[i - 1, 0];

                for (int j = 1; j <= w; j++)
                {

                    // 1번 이상이동은 
                    // 현재 턴에 이동과 현재 턴에 이동안한 것에서 결정된다
                    if (j > i) break;
                    // 현재 턴에 이동 1회
                    int calc1 = dp[i - 1, j - 1];
                    // 현재턴에 이동안한 경우
                    int calc2 = dp[i - 1, j];
                    dp[i, j] = calc1 < calc2 ? calc2 : calc1;
                    if ((j & 1) == cur) dp[i, j]++;
                }
            }

            int ret = 0;
            for (int i = 0; i <= w; i++)
            {

                ret = ret < dp[t, i] ? dp[t, i] : ret;
            }

            Console.WriteLine(ret);

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
using static IO;
public class IO{
public static string? Cin()=>reader.ReadLine();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static void Cin(out int num)=>num=int.Parse(Cin());
public static void Cin(out string str)=>str=Cin();
public static void Cin(out string a,out string b,char c=' '){var r=Cin().Split(c);a=r[0];b=r[1];}
public static void Cin(out int[] numarr,char c= ' ')=>numarr=Array.ConvertAll(Cin().Split(c),int.Parse);
public static void Cin(out string[] strarr,char c=' ')=>strarr=Cin().Split(c);
public static void Cin(out double d)=>d=double.Parse(Cin());
public static void Cin(out string t,out int n){var s=Cin().Split();n=int.Parse(s[1]);t=s[0];}
public static void Cin(out int a,out int b,char c= ' '){Cin(out int[] s);(a,b)=(s[0],s[1]);}
public static void Cin(out int a,out int b,out int c,char e=' '){Cin(out int[] s);(a,b,c)=(s[0],s[1],s[2]);}
public static void Cin(out int a,out int b,out int c,out int d,char e = ' '){Cin(out int[] arr,e);(a,b,c,d)=(arr[0],arr[1],arr[2],arr[3]);}
public static void Cin(out int n,out string t) {var s=Cin().Split();n=int.Parse(s[0]);t=s[1];}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        Cin(out int time,out int limit);       
        int t = time;
        int[,] dp = new int[time+1,limit+1];
        while(time-->0) {
             Cin(out int position);
            bool one = position is 1;
            for(int x=limit;x>=0;x--) {
                bool thisone = x % 2 == 0;
                dp[time,x] = Math.Max(dp[time+1,x] + (thisone == one ? 1 : 0),dp[time,x]);
                if (x is 0) continue;
                //x가 짝수면 1번 나무
                //홀수면 2번 나무
                dp[time,x-1] = Math.Max(dp[time,x-1],dp[time+1,x]+(one != thisone ? 1 : 0));
            }
        }
        int max = 0;
        for(int i=0;i<=limit;i++) max = Math.Max(max,dp[0,i]);
#if test
        Cout = "limit";
        for(int x=limit;x>=0;x--) Cout = string.Format(" | {0,-2}",x);
        for(int y=t;y>=0;y--) {
            Cout = string.Format("\nno.{0,-2}",y);
            for(int x=limit;x>=0;x--) Cout = string.Format(" | {0,-2}",dp[y,x]);
        }
        Cout = "\nresult: ";
#endif
        Cout =  max;
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Program
{
    public static void Main(string[] args)
    {
        string[] s = Console.ReadLine().Split();
        int t = int.Parse(s[0]);
        int w = int.Parse(s[1]);
        int[] jadu = new int[t + 1];
        
        for (int i = 0; i < t; i++)
        {
            jadu[i + 1] = int.Parse(Console.ReadLine());
        }

        int[,] dp = new int[t + 1, w + 1];
        for(int i = 1; i <= t; i++)
        {
            if (jadu[i] == 1)
            {
                dp[i, 0] = dp[i - 1,0] + 1;
            }
            else
            {
                dp[i, 0] = dp[i - 1, 0];
            }

            for(int j = 1; j <= w; j++)
            {
                if(jadu[i] == 2 && j % 2 != 0)
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - 1]) + 1; 
                }

                else if (jadu[i] == 1 && j % 2 == 0)
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - 1]) + 1;
                }

                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - 1]);
                }
            }
        
        }
        int max_value = 0;
        for (int num = 0; num <= w; num++)
        {
            max_value = Math.Max(max_value, dp[t, num]);

        }
        Console.WriteLine(max_value);
    }
}

#endif
}

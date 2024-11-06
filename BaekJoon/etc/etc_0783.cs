using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 1
이름 : 배성훈
내용 : 박물관 견학
    문제번호 : 30405번

    그리디, 정렬 문제다
    다른 사람의 풀이를 보니, 정렬한 뒤 중앙값으로 해결했다
    해설에서도 중앙값을 기준으로 풀이를 작성했다

    그리디하게 봐서 출입구를 중앙값에서 이동시키는 경우
    이동거리가 줄어드는 지점보다 늘어나는 지점이 많아진다

    해당 아이디어를 못떠올려 브루트포스로 탐색했다
    우선 처음 누적합을 구하고, 이후 왼쪽, 오른쪽 개수를 확인한 뒤
    1씩 올려가면서 최소값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0783
    {

        static void Main783(string[] args)
        {

            StreamReader sr;
            int[] cnt;
            int n, k;

            Solve();

            void Solve()
            {

                Input();

                int ret = GetRet();
                Console.Write(ret);
            }

            int GetRet()
            {

                long min = 0;

                int right = 2 * n;
                int left = 0;
                for (int i = 1; i <= k; i++)
                {

                    min += cnt[i] * i;
                }

                long cur = min;
                int ret = 0;
                for (int i = 1; i <= k; i++)
                {

                    long chk = cur - right + left;
                    left += cnt[i];
                    right -= cnt[i];

                    if (chk < min)
                    {

                        min = chk;
                        ret = i;
                    }

                    cur = chk;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                cnt = new int[k + 1];

                for (int i = 0; i < n; i++)
                {

                    int len = ReadInt();
                    cnt[ReadInt()]++;
                    for (int j = 1; j < len - 1; j++)
                    {

                        ReadInt();
                    }

                    cnt[ReadInt()]++;
                }

                sr.Close();
            }

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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m) = (nm[0], nm[1]);

        var list = new List<int>();
        while (n-- > 0)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            list.Add(l[1]);
            list.Add(l[^1]);
        }

        list.Sort();
        sw.WriteLine(list[(list.Count - 1) / 2]);
    }
}
#elif other2
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
        Cin(out int cat_count,out int museum_size);
        int[] list = new int[cat_count<<1];
        for(int x=0;x<cat_count;x++) {
            Cin(out int[] input);
            int index = x<<1;
            list[index] = input[1];
            list[index|1] = input[input[0]];
        }
        Array.Sort(list);
        Cout = list[cat_count-1];
    }
}
#endif
}

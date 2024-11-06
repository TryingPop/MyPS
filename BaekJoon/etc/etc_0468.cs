using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 지정좌석 배치하기 1
    문제번호 : 31714

    그리디, 정렬 문제다
    그리디하게 접근해서 풀었다

    아이디어는 다음과 같다
    정렬해서 앞좌석이 나보다 크면 못본다
    그리고 이게 최선임이 보장된다
*/

namespace BaekJoon.etc
{
    internal class etc_0468
    {

        static void Main468(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
            int r = ReadInt();
            int c = ReadInt();

            int d = ReadInt();
            int[] f = new int[c];
            int[] b = new int[c];

            bool ret = true;
            for (int i = 0; i < r; i++)
            {

                for (int j = 0; j < c; j++)
                {

                    b[j] = ReadInt();
                }

                // 정렬 작은 순서로 앉힌다
                Array.Sort(b);

                for (int j = 0; j < c; j++)
                {

                    // 앞사람이 앉힐 수 있는 젤 작은 사람이 된다!
                    // 시야가 가려진다면 앞 사람을 좌우로 이동시킨다고 하자
                    // 큰사람쪽으로 가면, 더 큰 사람이라 앞이 가려지고
                    // 작은 쪽으로 바꾸면, 자신의 앞사람에서 시야가 가려지는 현상이 나타난다
                    if (f[j] >= b[j] + d) 
                    { 
                        
                        ret = false;
                        break;
                    }

                    f[j] = 0;
                }

                // 불가능하면 더 읽을 필요없다
                // 그냥 종료!
                if (!ret) break;
                int[] temp = f;
                f = b;
                b = temp;
            }
            sr.Close();
            Console.WriteLine(ret ? "YES" : "NO");

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
public static void Cin(out char c) {c = (char)reader.Read();}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        Cin(out int height,out int width,out int up);
        int[][] seat = new int[height][];
        for(int i=0;i<height;i++) {
            Cin(out seat[i]);
            Array.Sort(seat[i]);
        }
        for(int y=1;y<height;y++) {
            for(int x=0;x<width;x++) {
                //앞좌석이 나보다 크다면
                if (seat[y-1][x] - up >= seat[y][x]) {
                    Cout = "NO";
                    return;
                }
            }
        }
        Cout = "YES";
    }
}
#endif
}

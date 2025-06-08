using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : N과 M 5
    문제번호 : 15654번

    백트래킹 문제다
    문제 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0410
    {

        static void Main410(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 4);

            int n = ReadInt();
            int r = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            Array.Sort(arr);
            bool[] use = new bool[n];
            int[] calc = new int[r];

            DFS(0);

            sw.Close();

            void DFS(int _depth)
            {

                if (_depth == r)
                {

                    for (int i = 0; i < r; i++)
                    {

                        sw.Write(calc[i]);
                        sw.Write(' ');
                    }

                    sw.Write('\n');
                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;

                    calc[_depth] = arr[i];
                    DFS(_depth + 1);

                    use[i] = false;
                }
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
using System.Text;

class baek15654{
    public static StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
    public static StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
    public static StringBuilder sb = new StringBuilder();
    public static int count = 0;
    public static int[] result;
    public static int[] arr;
    public static bool[] isChecked;
    public static int n;
    public static int m;

    public static void Main(string[] args){
        int[] nm = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        Array.Sort(arr);

        n = nm[0]; m = nm[1];
        result = new int[m];
        isChecked = new bool[n];

        Func(0, 1);

        sw.Write(sb);
        sw.Close();
        sr.Close();
    }

    public static void Func(int count, int startIndex){
        if (count == m){
            foreach (int num in result){
                sb.Append(num).Append(" ");
            }
            sb.Append("\n");
            return;
        }

        for (int i = 0; i < n; i++){
            if (!isChecked[i]){
                isChecked[i] = true;
                result[count] = arr[i];
                Func(count+1, i);
                isChecked[i] = false;
            }
        }
    }
}
#endif
}

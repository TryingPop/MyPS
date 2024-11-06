using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 1
이름 : 배성훈
내용 : 분산처리
    문제번호 : 1009번

    수학, 구현 문제다
    분할 정복을 이용한 거듭 제곱을 통해 풀었다
    System -> SYstem오타, 그리고 나머지가 0인 경우 반례처리를 제대로 안해 총 2번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0748
    {

        static void Main748(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int test = ReadInt();
                while (test-- > 0)
                {

                    int a = ReadInt() % 10;
                    int b = ReadInt();

                    int ret = GetPow(a, b);
                    if (ret == 0) ret = 10;
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetPow(int _a, int _exp)
            {

                int ret = 1;
                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _a) % 10;
                    _a = (_a * _a) % 10;
                    _exp >>= 1;
                }

                return ret;
            }

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
using System;
using System.Collections.Generic;

namespace Coding
{
    public class Solution
    {
        public static void Main()
        {
            // VARIABLE
            using var streamReader = new StreamReader(Console.OpenStandardInput());
            using var streamWriter = new StreamWriter(Console.OpenStandardOutput());
            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            int repeatCount = int.Parse(streamReader.ReadLine());
            string[] arr;
            int A, B;

            // ACTION
            dic.Add(0, new int[4] { 10, 10, 10, 10 });
            dic.Add(1, new int[4] { 1, 1, 1, 1 });
            dic.Add(2, new int[4] { 2, 4, 8, 6 });
            dic.Add(3, new int[4] { 3, 9, 7, 1 });
            dic.Add(4, new int[4] { 4, 6, 4, 6 });
            dic.Add(5, new int[4] { 5, 5, 5, 5 });
            dic.Add(6, new int[4] { 6, 6, 6, 6 });
            dic.Add(7, new int[4] { 7, 9, 3, 1 });
            dic.Add(8, new int[4] { 8, 4, 2, 6 });
            dic.Add(9, new int[4] { 9, 1, 9, 1 });

            for (int i = 0; i < repeatCount; i++)
            {
                arr = streamReader.ReadLine().Split(" ");
                A = int.Parse(arr[0]) % 10;
                B = (int.Parse(arr[1]) - 1) % 4;
                streamWriter.WriteLine(dic[A][B]);
            }
        }
    }
}
#endif
}

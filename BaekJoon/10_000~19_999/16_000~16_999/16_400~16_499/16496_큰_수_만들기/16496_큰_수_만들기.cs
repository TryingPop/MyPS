using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 큰 수 만들기
    문제번호 : 16496번

    그리디, 정렬 문제
    다른 사람 풀이가 더 깔끔해 보인다;
    나누면서 비교했는데, 다른 사람은 1번에 해결된다..

    그리고 string 비교를 위해 구조체를 만들었는데,
    다른 사람은 Comp를 만들어 그냥 바로 비교했다
*/

namespace BaekJoon.etc
{
    internal class etc_0280
    {

        static void Main280(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

            int n = int.Parse(sr.ReadLine());

            MyString[] arr = new MyString[n];
            {

                string[] temp = sr.ReadLine().Split(' ');
                for (int i = 0; i < n; i++)
                {

                    arr[i].str = temp[i];
                }
            }

            sr.Close();

            Array.Sort(arr);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                if (arr[0].str == "0")
                {

                    sw.Write(0);
                }
                else
                {

                    for (int i = 0; i < n; i++)
                    {

                        sw.Write(arr[i]);
                    }
                }
            }
        }

        struct MyString : IComparable<MyString>
        {

            public string str;

            public override string ToString()
            {

                return str;
            }
            public int CompareTo(MyString other)
            {

                bool isEnd = false;
                int ret = 0;
                int idx1 = 0;
                int idx2 = 0;

                while (!isEnd)
                {

                    ret = CompStr(str, ref idx1, other.str, ref idx2, ref isEnd);
                }

                return ret;
            }

            private int CompStr(string _str1, ref int _idx1, string _str2, ref int _idx2, ref bool _end)
            {

                
                int len1 = _str1.Length - _idx1;
                int len2 = _str2.Length - _idx2;

                if (len1 < len2)
                {

                    for (int i = 0; i < len1; i++)
                    {

                        int ret = _str2[i + _idx2].CompareTo(_str1[i + _idx1]);
                        if (ret == 0) continue;

                        _end = true;
                        return ret;
                    }

                    _idx2 += len1;
                    return 0;
                }
                else if (len2 < len1)
                {

                    for (int i = 0; i < len2; i++)
                    {

                        int ret = _str2[i + _idx2].CompareTo(_str1[i + _idx1]);
                        if (ret == 0) continue;

                        _end = true;
                        return ret;
                    }

                    _idx1 += len2;
                    return 0;
                }

                for (int  i = 0; i < len1; i++)
                {

                    int ret = _str2[i + _idx2].CompareTo(_str1[i + _idx1]);
                    if (ret == 0) continue;

                    _end = true;
                    return ret;
                }

                _end = true;
                return 0;
            }
        }
    }

#if other
using System;
using static System.Console;
using System.Text;

namespace csharp_algo.baekjoon
{
    class Program
    {
        static int N;
        static string[] arr;
        static StringBuilder result;

        static void Main()
        {
            N = int.Parse(ReadLine());
            arr = ReadLine().Split(' ');

            Array.Sort(arr, (o1, o2) => (o2 + o1).CompareTo(o1 + o2));

            result = new StringBuilder();
            for(int i = 0; i < N; i++)
            {
                result.Append(arr[i]);
            }
            if (result.ToString()[0]=='0') WriteLine(0);
            else WriteLine(result);
        }
    }
}
#elif other2

// cs16496 - rby
// 2023-05-27 10:51:18
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs16496
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            List<string> list = sr.ReadLine().Split(' ').ToList();
            list.Sort(new Comparison<string>((n1, n2) => Compare(n1, n2)));

            if (list[0] == "0")
            {
                sb.AppendLine("0");
            }
            else
            {
                foreach (var item in list)
                    sb.Append(item);
            }

            sw.Write(sb);
            sw.Close();
            sr.Close();
        }

        static int Compare(string A, string B)
        {
            return (B + A).CompareTo(A + B);
        }

        //static int Compare(string A, string B)
        //{
        //    int len;
        //    if (A.Length < B.Length)
        //    {
        //        len = A.Length;
        //        for (int i = 0; i < len; i++)
        //        {
        //            if (A[i] != B[i])
        //                return A[i] > B[i] ? -1 : 1;
        //        }
        //        return A[len - 1] >= B[len] ? -1 : 1;
        //    }
        //    else if (A.Length > B.Length)
        //    {
        //        len = B.Length;
        //        for (int i = 0; i < len; i++)
        //        {
        //            if (A[i] != B[i])
        //                return B[i] > A[i] ? 1 : -1;
        //        }
        //        return B[len - 1] >= A[len] ? 1 : -1;
        //    }

        //    len = A.Length;
        //    for (int i = 0; i < len; i++)
        //    {
        //        if (A[i] != B[i])
        //            return A[i] > B[i] ? -1 : 1;
        //    }

        //    return 0;
        //}
    }
}

#endif
}

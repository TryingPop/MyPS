using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 좌표 정렬하기 2
    문제번호 : 11651번
*/

namespace BaekJoon._18
{
    internal class _18_07
    {

        static void Main7(string[] args)
        {

#if false
            int[][] nums;

            {

                StreamReader sr = new StreamReader(Console.OpenStandardInput());
                int len = int.Parse(sr.ReadLine());

                nums = new int[len][];

                for (int i = 0; i < len; i++)
                {

                    nums[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                }

                sr.Close();
            }

            {

                StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

                foreach (int[] item in nums.OrderBy(x => x[1]).ThenBy(x => x[0]))
                {

                    sw.WriteLine($"{item[0]} {item[1]}");
                }

                sw.Close();
            }
#else

            (int, int)[] c = new (int x, int y)[35];
            c[0] = (0, 0);
            // Console.WriteLine(0.GetType());             // Int32
            Console.WriteLine(c[0].GetType());          // System.ValueTuple 타입이라 한다
                                                        // System.Tuple 과 다르다! 둘의 차이는 값과 참조 차이!
            // Console.WriteLine(typeof(c[0].Item1));   // c는 변수이지만 형식처럼 사용된다며 컴파일 에러 뜬다
            Console.WriteLine(c[0].Item1);


            int count = 0;
            int sum = 0;
            var t = (count, sum);
            Console.WriteLine(t.count);
            Console.WriteLine(t.sum);


            // 자세한건 아래 링크 참조!
            // https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/value-tuples
#endif
        }
    }
    
}
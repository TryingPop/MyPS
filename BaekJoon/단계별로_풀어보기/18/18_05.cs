using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 소트인사이드
    문제번호 : 1427번
*/

namespace BaekJoon._18
{
    internal class _18_05
    {

        static void Main5(string[] args)
        {

#if false
            Console.WriteLine(
                string.Concat(                  // char[] -> string으로
                    Console.ReadLine().     
                    ToCharArray().              // string -> char[]
                    OrderByDescending(x => x).  // 내림차순 정렬해서 IEnumerable<char> 반환
                    ToArray()                   // IEnumerable<char> -> char[]
                    )
                );
#elif false
            Console.Write(
                Console.ReadLine().
                OrderByDescending(x => x).
                ToArray());
#else
            // 이제 직접 정렬
            char[] inputs = Console.ReadLine().ToArray();

            // 삽입 정렬
            for (int i = 0; i < inputs.Length; i++)
            {

                for (int j = 0; j <= i - 1; j++)
                {

                    // 삽입
                    if (inputs[i] >= inputs[j])
                    {

                        char temp = inputs[i];
                        for (int k = i; k > j; k--)
                        {

                            inputs[k] = inputs[k - 1];
                        }
                        inputs[j] = temp;
                        break;
                    }
                }
            }

            Console.Write(inputs);
#endif
        }
    }
}

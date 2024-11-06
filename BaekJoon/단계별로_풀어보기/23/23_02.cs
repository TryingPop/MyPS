using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 진법 변환 2
    문제번호 : 11005번
*/

namespace BaekJoon._23
{
    internal class _23_02
    {

        static void Main2(string[] args)
        {

            int nums, b;
            {

                // 값 입력
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                nums = inputs[0];
                b = inputs[1];
            }

            // 정답
            string result = "";
            {

                // 연산용
                int r;

                // 뒤에서 부터 단위를 이어 붙인다
                while (nums != 0)
                {


                    r = nums % b;

                    // 문자
#if false
                    if (r >= 10)
                    {

                        // result = (char)(r + 55) + result;
                        result += (char)(r + 55);   // 위와 같은 표현
                        
                    }
                    else
                    {

                        result = r + result;
                    }
#else
                    // 다른 사람이 푼 좋은 방법
                    string n = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    result += n[r]; // if 문을 하나의 단계로 생략.. 연산속도도 더 빠르다!
#endif
                    nums /= b;
                }
            }

            Console.WriteLine(result);
        }
    }
}

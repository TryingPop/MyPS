using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 수 정렬하기 3
    문제번호 : 10989번

    여기는 메모리 제한이 걸려서 메모리를 요구하는 머지나 
    잦은 재귀함수를 호출하는 퀵 정렬을 이용할 수없다
    
    선택정렬로 했는데도 메모리 초과가 떴다
    정렬 방법의 문제가 아니라 메모리 문제
*/

namespace BaekJoon._18
{
    internal class _18_04
    {

        static void Main4(string[] args)
        {
#if true


            const int max = 10000;
            int[] nums = new int[max + 1];

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            {


                int len = int.Parse(Console.ReadLine());
                {

                    int input;
                    for (int i = 0; i < len; i++)
                    {

                        input = int.Parse(Console.ReadLine());
                        nums[input]++;
                    }
                }

            }
            sr.Close();
#if true
            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())); 
            for (int i = 1; i < nums.Length; i++)
            {

                for (int j = 1; j <= nums[i]; j++)
                {

                    sw.WriteLine(i);
                }
            }
            sw.Close();
#else

            // 속도가 2배 가까이 느리다...
            // sw는 close 해야지 모은 것들이 출력된다 
            // sb 는 출력해도 내용물이 사라지지 않는다
            // 그래서 메모리 초과 많이 떴다
            // Clear 해줘야 빈다!
            StreamWriter sw;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < nums.Length; i++)
            {

                sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

                for (int j = 1; j <= nums[i]; j++)
                {

                    sb.AppendLine(i.ToString());
        
                    if (j % 1000 == 0)
                    {

                        sw.Write(sb);
                        sb.Clear();
            
                        sw.Close();
            
                        sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
                    } 
                }

                sw.Write(sb);
                sb.Clear();
                sw.Close();
            }
#endif

#elif false
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] nums;

            {

                int len = int.Parse(sr.ReadLine());
                nums = new int[len];

                for (int i = 0; i < len; i++)
                {

                    nums[i] = int.Parse(sr.ReadLine());
                }
            }

            sr.Close();

            StringBuilder sb = new StringBuilder();

            // 선택정렬
            {

                int min;
                int minIdx;

                for (int i = 0; i < nums.Length; i++)
                {

                    min = nums[i];
                    minIdx = i;

                    for (int j = i + 1; j < nums.Length; j++)
                    {

                        if (min >= nums[j])
                        {

                            min = nums[j];
                            minIdx = j;
                        }
                    }

                    if (minIdx != i)
                    {

                        int temp = nums[i];
                        nums[i] = min;
                        nums[minIdx] = temp;
                    }

                    sb.AppendLine(min.ToString());
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }

#endif
        }
    }
}

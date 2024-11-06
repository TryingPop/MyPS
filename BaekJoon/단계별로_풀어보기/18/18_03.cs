using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 커트라인
    문제번호 : 25305번

    추후에 퀵정렬 코드 살펴보기!
*/

namespace BaekJoon._18
{
    internal class _18_03
    {

        static void Main3(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] nums = Console.ReadLine().Split(' ').Select<string, int>(item => int.Parse(item)).ToArray();

            NotQuickSort(ref nums, 0, info[0] - 1);

            Console.WriteLine(nums[info[0] - info[1]]);
        }

        // 스스로 구현해본 퀵정렬 >> 맞는지 모르겠다
        // 기능은 제대로 작동한다
        static void NotQuickSort(ref int[] arr, int startIdx, int endIdx)
        {

            if (endIdx <= startIdx)
            {

                return;
            }

            int pivot = startIdx;

            int low = startIdx + 1, high = endIdx;
            int temp;
           
            while (low < high)
            {

                bool lowChk = false;
                bool highChk = false;

                for (int i = low; i <= high; i++)
                {

                    low = i;
                    if (arr[pivot] <= arr[i])
                    {

                        lowChk = true;
                        break;
                    }
                }

                for (int i = high; i >= low; i--)
                {

                    high = i;
                    if (arr[pivot] > arr[i])
                    {

                        highChk = true;
                        break;
                    }
                }

                if (lowChk && highChk)
                {

                    temp = arr[low];
                    arr[low] = arr[high];
                    arr[high] = temp;
                    low++;
                    high--;
                }
            }

            if (arr[pivot] < arr[high])
            {

                high--;
            }

            temp = arr[pivot];
            arr[pivot] = arr[high];
            arr[high] = temp;

            NotQuickSort(ref arr, startIdx, high - 1);
            NotQuickSort(ref arr, high + 1, endIdx);
        }


        // 다른 사람 글 참고해서 작성한 퀵정렬
        static void Swap(ref int x, ref int y)
        {

            int temp = x;
            x = y;
            y = temp;
            return;
        }

        static int Partition(ref int[] arr, int startIdx, int endIdx)
        {

            int pivot = arr[startIdx];
            int low = startIdx;
            int high = endIdx + 1;


            while (low < high)
            {

                low++;

                for (int i = low; i <= endIdx; i++)
                {

                    low = i;
                    if (arr[i] >= pivot)
                    {

                        break;
                    }
                }

                high--;

                for (int i = high; i >= startIdx; i--)
                {

                    high = i;
                    if (arr[i] <= pivot)
                    {

                        break;
                    }
                }

                if (low < high)
                {

                    Swap(ref arr[low], ref arr[high]);
                }

            }


            Swap(ref arr[startIdx], ref arr[high]);


            return high;
        }

        static void QuickSort(ref int[] arr, int startIdx, int endIdx)
        {

            if (startIdx < endIdx)
            {

                int q = Partition(ref arr, startIdx, endIdx);

                QuickSort(ref arr, startIdx, q - 1);
                QuickSort(ref arr, q + 1, endIdx);
            }
        }
    }
}

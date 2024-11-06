using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : 알고리즘 수업 병합정렬 1
    문제번호 : 24060번

    다른 사람의 풀이를 보니 C, C++ 처럼 배열은 참조형인거 같다
    그래서 ref가 따로 필요없고
    또한 merge함수에서 배열을 매번 생성할께 아닌
    이것도 참조하게해서 값을 변형시키는게 좋아 보인다!
*/

namespace BaekJoon._24
{
    internal class _24_03
    {

        static void Main3(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int calc = 0;
            int result = -1;
            MergeSort(nums, 0, nums.Length - 1, info[1], ref calc, ref result);

            Console.WriteLine(result);
        }
        static void MergeSort(int[] arr, int startIdx, int endIdx, int find, ref int calc, ref int result)
        {

            if (startIdx >= endIdx) return;

            int mid = (startIdx + endIdx) / 2;

            MergeSort(arr, startIdx, mid, find, ref calc, ref result);
            MergeSort(arr, mid + 1, endIdx, find, ref calc, ref result);

            Merge(arr, startIdx, mid, endIdx, find, ref calc, ref result);

            return;
        }
        static void Merge(int[] arr, int startIdx, int mid, int endIdx, int find, ref int calc, ref int result)
        {

            if (find <= calc) return; 
            int[] temp = new int[endIdx - startIdx + 1];
            int idx = 0;

            int left = startIdx,
                right = mid + 1;

            while (left <= mid && right <= endIdx)
            {


                if (arr[left] <= arr[right])
                {

                    temp[idx++] = arr[left];
                    left++;
                }
                else
                {

                    temp[idx++] = arr[right];
                    right++;
                }
            }

            for (int i = left; i <= mid; i++)
            {

                temp[idx++] = arr[i];
            }
            for (int i = right; i <= endIdx; i++)
            {

                temp[idx++] = arr[i];
            }

            for (int i = 0; i < temp.Length; i++)
            {
                calc++;
                arr[startIdx + i] = temp[i];

                if (find == calc)
                {

                    result = temp[i];
                    break;
                }
            }

            return;
        }
    }

}

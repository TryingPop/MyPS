#define first

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 수 정렬하기 2
    문제번호 : 2751번

    일단 퀵정렬 되는지 확인부터!
    퀵은 좋은 선택이 아닌거 같다

    최악의 경우(이미 정렬된 경우) 시간 복잡도가 O(n^2) 이므로 시간초과 위험성이 존재하고, 실제로 시간초과 떴다
    병합정렬은 최악도 O(logn * n) 복잡도이므로 상관 없다

    내장 메소드 이용하는게 가장 깔끔해 보인다
    다른 사람이 쓴거 보니 힙정렬도 이용했다
*/

namespace BaekJoon._18
{
    internal class _18_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());
            int[] nums = new int[len];

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                nums[i] = int.Parse(sr.ReadLine());
            }
            sr.Close();

#if first
            // 병합정렬
            MergeSort(ref nums, 0, len - 1);
            for (int i = 0; i < len; i++)
            {

                sb.AppendLine(nums[i].ToString());
            }
#else

            // 내장 함수 이용!
            foreach(int num in nums.OrderBy(x => x))
            {

                sb.AppendLine(num.ToString());
            }

#endif

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            sw.WriteLine(sb);

            sw.Close();
        }
#if first
        // 논리보고 스스로 구현해본 병합정렬
        static void MergeSort(ref int[] arr, int startIdx, int endIdx)
        {

            // 분할
            if (startIdx >= endIdx)
            {

                return;
            }

            int center = (startIdx + endIdx) / 2;

            MergeSort(ref arr, startIdx, center);
            MergeSort(ref arr, center + 1, endIdx);


            // 정렬
            int left = startIdx, right = center + 1;
            int idx = 0;

            int[] tempArr = new int[endIdx - startIdx + 1];

            // if문 4번 거치는거보다 아래꺼처럼 따로 빼는 방법이 좋아보인다
            while(idx < tempArr.Length)
            {

                if (right > endIdx)
                {

                    tempArr[idx] = arr[left];
                    left++;
                }
                else if (left > center)
                {

                    tempArr[idx] = arr[right];
                    right++;
                }
                else if (arr[left] <= arr[right])
                {

                    tempArr[idx] = arr[left];
                    left++;
                }
                else
                {

                    tempArr[idx] = arr[right];
                    right++;
                }
                idx++;
            }

            for (int i = 0; i < tempArr.Length; i++)
            {

                arr[startIdx + i] = tempArr[i];
            }

            return;
        }

        // 다른 사람이 구현한거
        static void mergeSort(ref int[] arr)
        {

            sort(ref arr, 0, arr.Length - 1);
        }

        static void sort(ref int[] arr, int start, int end)
        {

            if (end <= start)
            {

                return;
            }

            int mid = (start + end) / 2;
            sort(ref arr, start, mid);
            sort(ref arr, mid + 1, end);

            merge(ref arr, start, mid, end);
        }

        static void merge(ref int[] arr, int startIdx, int center, int endIdx)
        {

            int[] temp = new int[endIdx - startIdx + 1];
            int t = 0, l = startIdx, r = center + 1;

            while (l <= center && r <= endIdx)
            {

                if (arr[l] < arr[r])
                {

                    temp[t++] = arr[l++];
                }
                else
                {

                    temp[t++] = arr[r++];
                }
            }

            while (l <= center)
            {

                temp[t++] = arr[l++];
            }

            while(r <= endIdx)
            {

                temp[t++] = arr[r++];
            }

            for (int i = 0; i < temp.Length; i++)
            {

                arr[i + startIdx] = temp[i];
            }
        }
#endif
    }
}

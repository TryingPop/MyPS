using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 행운의 문자열
    문제번호 : 1342번

    브루트포스, 백트래킹 문제다.
    이전 알파벳과 다른 문자열이고 아직 남아있는 문자이면 사용하면서 갯수를 찾아가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1441
    {

        static void Main1441(string[] args)
        {

            string input = Console.ReadLine();
            int n = input.Length;
            int[] cnt = new int[26];
            for (int i = 0; i < n; i++)
            {

                cnt[input[i] - 'a']++;
            }

            Console.Write(DFS());

            int DFS(int _dep = 0, int _prev = -1)
            {

                if (_dep == n) return 1;
                int ret = 0;

                for (int i = 0; i < 26; i++)
                {

                    if (cnt[i] == 0 || _prev == i) continue;
                    cnt[i]--;
                    ret += DFS(_dep + 1, i);
                    cnt[i]++;
                }

                return ret;
            }
        }

    }
#if other
using System.Runtime.Intrinsics.X86;

namespace LuckyString
{
    internal class Program
    {
        static bool np(int[] arr)
        {
            int i = arr.Length - 1;
            while (i > 0 && arr[i] <= arr[i - 1]) i--;
            if (i == 0) return false;
            int j = arr.Length - 1;
            while (arr[j] <= arr[i - 1]) j--;
            swap(arr, j, i - 1);
            int k = arr.Length - 1;
            while (i < k) swap(arr, k--, i++);
            return true;
        }

        static void swap (int [] arr, int i, int j)
        {
            int tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }

        static void Main(string[] args)
        {
            string s = Console.ReadLine();


            int[] arr = new int[s.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = s[i];
            }

            Array.Sort(arr);

            int cnt = 0;
            do
            {
                bool flag = true;

                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] != arr[i - 1]) continue;
                    flag = false;
                    break;
                }
                if (flag) cnt++;

;
            } while (np(arr));

            Console.WriteLine(cnt);
        }
    }
}
#endif
}

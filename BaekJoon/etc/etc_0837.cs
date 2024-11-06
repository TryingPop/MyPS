using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 25
이름 : 배성훈
내용 : Moo 게임
    문제번호 : 5904번

    분할정복, 재귀 문제다
    moo 의 생성과정을 보면
    M(k)를 m에 o가 k + 1개 붙은 문자열이라 하자
    M(1) = "moo", M(2) = "mooo"이다

    S(k)를 S(1) = M(1)이고
    S(k) = S(k - 1) + M(k) + S(k - 1)
    인 문자열이라 하자
    S(2) = "moomooomoo"
    가된다
*/

namespace BaekJoon.etc
{
    internal class etc_0837
    {

        static void Main837(string[] args)
        {

            int MAX = 28;
            int n;
            int ret;
            int[] sum;
            int[] arr;
            Solve();
            void Solve()
            {

                Init();

                GetRet();

                Console.Write(ret == 1 ? 'm' : 'o');
            }

            void GetRet()
            {

                ret = 0;
                while (true)
                {

                    int idx = BinarySearch(n);
                    n -= arr[idx];

                    if (n <= idx + 3)
                    {

                        n %= idx + 3;
                        if (n == 1) ret = 1;
                        return;
                    }
                    else n -= idx + 3;
                }
            }

            int BinarySearch(int _find)
            {

                int l = 0;
                int r = MAX;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (_find < arr[mid]) r = mid - 1;
                    else l = mid + 1;
                }

                return r;
            }

            void Init()
            {

                arr = new int[MAX + 1];
                for (int i = 1; i < MAX; i++)
                {

                    arr[i] = 2 * arr[i - 1] + i + 2;
                }

                n = int.Parse(Console.ReadLine());
            }
        }
    }

#if other
using System.Diagnostics.CodeAnalysis;
using System.Numerics;


class Algo
{

    public static void Main()
    {
        Algo0710_Third();
    }

    private static void Algo0710_Third()
    {
        //1부터긴 한데..
        int index = int.Parse(Console.ReadLine());
        index--;
        List<int> lengthList = new List<int>();
        lengthList.Add(3); // 0번째는 3

        int kValue = 0;
        
        while(true)
        {
            if(index < lengthList[kValue])
            {
                break;
            }else
            {
                lengthList.Add(lengthList[kValue] * 2 + kValue + 2 + 2);// +1은 m 
                kValue++;
            }

        }
        getPosition(kValue, index);
        // S(k)는 index를 포함하는 최대의 문자열
        void getPosition(int nowKValue, int nowIndex)
        {
            if(nowKValue == 0)
            {
                if(nowIndex == 0) 
                {
                    Console.WriteLine("m");
                }
                else if (nowIndex == 1)
                {
                    Console.WriteLine("o");
                }
                else if (nowIndex == 2)
                {
                    Console.WriteLine("o");
                }
                return;
            }
            int beforLen = lengthList[nowKValue - 1];
            if(nowIndex < beforLen)
            {
                getPosition(nowKValue - 1, nowIndex);
            }
            else if(nowIndex < beforLen + nowKValue + 2 + 1)
            {
                if (nowIndex == beforLen) Console.WriteLine("m");
                else Console.WriteLine("o");
            }
            else
            {
                getPosition(nowKValue - 1, nowIndex - beforLen - nowKValue - 2 - 1);
            }

        }
    }}
#endif
}

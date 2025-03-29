using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 다음 팰린드롬 수
    문제번호 : 1334번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1497
    {

        static void Main1497(string[] args)
        {

            int[] arr;

            Input();

            // 값을 올릴필요가 있는지 확인
            if (ChkUp()) UpArr();

            // 현재 값으로 팰린드롬 생성
            GetRet();

            void GetRet()
            {

                GetLR(arr.Length, out int l, out int r);
                
                for (int i = 0; i <= l; i++)
                {

                    Console.Write(arr[i]);
                }

                for (int i = (l == r) ? l - 1 : l; i >= 0; i--)
                {

                    Console.Write(arr[i]);
                }
            }

            void UpArr()
            {

                GetLR(arr.Length, out int l, out int r);
                arr[l]++;
                for (int i = l; i >= 1; i--)
                {

                    if (arr[i] < 10) break;
                    arr[i] -= 10;
                    arr[i - 1]++;
                }

                if (arr[0] == 10)
                {

                    int[] newArr = new int[arr.Length + 1];
                    newArr[0] = 1;
                    newArr[1] = 0;
                    for (int i = 1; i < arr.Length; i++)
                    {

                        newArr[i + 1] = arr[i];
                    }

                    arr = newArr;
                }
            }

            // 중앙으로 나눴을 때 왼쪽과 오른쪽 값 반환
            void GetLR(int _len, out int _l, out int _r)
            {

                _r = _len >> 1;
                if ((_len & 1) == 0)
                    _l = _r - 1;
                else
                    _l = _r;
            }

            bool ChkUp()
            {

                GetLR(arr.Length, out int l, out int r);

                while (l >= 0)
                {

                    if (arr[l] < arr[r]) return true;
                    else if (arr[l] > arr[r]) return false;

                    l--;
                    r++;
                }

                return true;
            }

            void Input()
            {

                string temp = Console.ReadLine();
                arr = new int[temp.Length];
                for (int i = 0; i < arr.Length; i++)
                {

                    arr[i] = temp[i] - '0';
                }
            }
        }
    }
}

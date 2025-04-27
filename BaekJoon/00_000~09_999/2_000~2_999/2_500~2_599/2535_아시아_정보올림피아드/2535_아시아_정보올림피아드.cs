using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 아시아 정보올림피아드
    문제번호 : 2535번

    구현, 정렬 문제다
    조건에서 특이점 경우를 다 막아뒀기에 특이점 경우를 고려하지 않아도 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0364
    {

        static void Main364(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            (int nation, int id, int score)[] arr = new (int nation, int id, int score)[n];

            for (int i = 0; i < n; i++)
            {

                int nation = ReadInt();
                int id = ReadInt();
                int score = ReadInt();

                arr[i] = (nation, id, score);
            }

            sr.Close();

            Array.Sort(arr, ((x, y) => y.score.CompareTo(x.score)));

            int third = 2;
            int thirdImpossible = arr[0].nation == arr[1].nation ? arr[0].nation : -1;

            // 입력되는 나라는 적어도 2나라 이상이고, 선수는 3명이상이므로 동메달은 항상 존재!
            // 그리고 점수는 모두 달라서 금 은 역시 유일하게 존재함이 보장된다!
            for (int i = 2; i < n; i++)
            {

                if (thirdImpossible == arr[i].nation) continue;
                third = i;
                break;
            }

            Console.WriteLine($"{arr[0].nation} {arr[0].id}");
            Console.WriteLine($"{arr[1].nation} {arr[1].id}");
            Console.WriteLine($"{arr[third].nation} {arr[third].id}");

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}

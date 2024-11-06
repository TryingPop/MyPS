using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 1
이름 : 배성훈
내용 : IPv6
    문제번호 : 3107번

    구현, 문자열 문제다
    split으로 :을 쪼개고, char 배열을 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0670
    {

        static void Main670(string[] args)
        {

            string[] str = Console.ReadLine().Split(':');

            char[] ret = new char[32];
            Array.Fill(ret, '0');
            int d = 8;

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == "") d = i;
            }

            for (int i = str.Length - 1; i > d; i--)
            {

                int s = 4 * (9 - (str.Length - i));
                s -= str[i].Length;

                for (int j = 0; j < str[i].Length; j++)
                {

                    ret[s + j] = str[i][j];
                }
            }

            for (int i = 0; i < d; i++)
            {

                int s = 4 * (i + 1) - str[i].Length;

                for (int j = 0; j < str[i].Length; j++)
                {

                    ret[s + j] = str[i][j];
                }
            }


            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            for (int i = 0; i < 8; i++)
            {

                int s = 4 * i;

                for (int j = 0; j < 4; j++)
                {

                    sw.Write(ret[s + j]);
                }

                if (i != 7) sw.Write(':');
            }
            sw.Close();
        }
    }

#if other
// #include <stdio.h>
// #include <string.h>

int main(void) {
    char arr[40] = {0}, colon = 1, cnt = 3;
    scanf("%s", arr);
    for (int i = 1; i < strlen(arr); i++) {
        if (arr[i] == ':' && arr[i-1] != ':') colon++;
    }
    for (int i = 1; i < strlen(arr); i++) {
        if (arr[i] == ':') {
            if (arr[i - 1] == ':') {
                for (int j = 1; j <= (8 - colon) * 5; ++j) {
                    printf("%c", j % 5 ? '0' : ':');
                }
            } else {
                for (int j = 1; j < 5; j++) {
                    printf("%c", j > cnt ? arr[i - 5 + j] : '0');
                }
                printf(":");
            }
            cnt = 4;
        } else cnt--;
    }
    for (int j = 1; j < 5; j++) {
        printf("%c", j > cnt ? arr[strlen(arr) - 5 + j] : '0');
    }
}
#endif
}

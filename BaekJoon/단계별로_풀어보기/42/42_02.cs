using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 26
이름 : 배성훈
내용 : 광고
    문제번호 : 1305번

    가장 짧은 시간을 대면 된다

    예를들어 전광판의 길이를 5, 현재 출력된 글자가 aaaab라하면
        광고가 aaaabcdef, aaaabefsdfadsfadiofjasldgksadjlf, aaaab 상관없이
        가장 짧은 시간을 낼 수 있는건 aaaab가된다
        5초가 된다
            aaaba
            aabaa
            abaaa
            baaaa
            aaaab

        그래서 왼쪽으로 1칸씩 이동했을 때 언제 처음과 같아지는지 확인하는 문제로 바뀐다!
        그러면 KMP알고리즘을 이용하면 쉽게 찾아진다
        kmp에서 만들어지는 배열의 인덱스는 해당위치에서 맨 앞에서 시작해서 같은 것들의 개수를 알려주는 값이 담기므로
        문자열의 길이 len - 맨끝의 값 x을 하면 가장 짧은 시간이 된다
*/

namespace BaekJoon._42
{
    internal class _42_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            string ad = sr.ReadLine();

            sr.Close();

            int[] p = new int[len];

            int i = 1;
            int j = 0;

            while (i < len)
            {

                if (ad[i] == ad[j]) p[i++] = ++j;
                else if (j == 0) i++;
                else j = p[j - 1];
            }

            Console.WriteLine(len - p[len - 1]);
        }
    }
}

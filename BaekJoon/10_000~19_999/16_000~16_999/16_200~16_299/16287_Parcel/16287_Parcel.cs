using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : Parcel
    문제번호 : 16287번

    중간에서 만나기 문제이다
    그냥 풀면 N^4이나, a + b(N^2)을 연산하고
    find - ( a + b ) = c + d인지 확인하면 N^2이 된다
    그런데, 해당 값에 대한 중복은 허용안하므로 인덱스를 기록해줘야한다
    
    원소의 크기가 1 ~ 20만 사이이므로 크기를 40만짜리 배열 2개로 잡아 인덱스를 각각 기록해줬다    
    인덱스는 먼저 나오는거 한쌍만 기록하면 충분하다

    만약 중복되는 경우가 나온다면, 맨처음 발견되는 하나만 기록하면 충분하다
    탐색에서도 순서만 유지해주면 결과에 영향을 안준다!

    만약 정답이 없을 경우 어떻게 해도 발견 안된다
    그러면 정답이 존재하는 경우를 보자
    찾는걸 find라 하고 존재하는 하나의 경우를 입력된 순서대로 w, x, y, z라 하자

    그러면 입력 순서를 앞에서 부터 2개씩 탐색해서 넣는다고 가정하자!
    y, z,가 sumA[w + x], sumB[w + x]의 값이 절대로 될 수 없다!
    sumA[w + x], sumB[w + x]에 값은 x의 인덱스 보다 작거나 같은 수가 될 것이다!

    그리고 결과 확인해서도 같은 탐색 방법을 이용한다면!
    y + z에서 sumA[w + x]의 값이 존재하고 sumA[w + x], sumB[w + x] 의 값이 
    y, z가 아니므로 존재한다고 할 것이다
    그래서 동형인 경우의 수가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0121
    {

        static void Main121(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));


            int find = ReadInt(sr);
            int len = ReadInt(sr);
            int[] arr = new int[len];

            int[] sumA = new int[400_001];
            int[] sumB = new int[400_001];

            Array.Fill(sumA, -1);
            Array.Fill(sumB, -1);

            for (int i = 0; i < len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            for (int i = 1; i < len; i++)
            {

                for (int j = 0; j < i; j++)
                {

                    int sum = arr[i] + arr[j];

                    if (sumA[sum] != -1) continue;

                    sumA[sum] = i;
                    sumB[sum] = j;
                }
            }

            bool ret = false;
            for (int i = 1; i < len; i++)
            {

                for (int j = 0; j < i; j++)
                {

                    int chk = find - (arr[i] + arr[j]);

                    if (chk <= 0 || chk > 400_000) continue;

                    if (sumA[chk] == -1 || sumA[chk] == i || sumA[chk] == j || sumB[chk] == i || sumB[chk] == j) continue;

                    ret = true;
                    break;
                }

                if (ret) break;
            }

            if (ret) Console.WriteLine("YES");
            else Console.WriteLine("NO");
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}

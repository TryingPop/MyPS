using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 10. 23
이름 : 배성훈
내용 : 나머지 합
    문제번호 : 10986번

    정수론 지식이 필요한 문제
    
    i(>= 1) 에서 시작해서 j(>= i)까지의 연속된 부분합이 m으로 나눠 떨어진다는 것은
    <=> a[i] + a[i + 1] + ... + a[j] == 0 (mod m)
    <=> s[j] - s[i - 1] == 0 (mod m)
    <=> s[j] == s[i - 1] (mod m)
    
    즉, i에서 시작해서 연속된 부분합이 m으로 나눠 떨어지는 것의 개수는
    s[i - 1] == s[j](mod m)인 j(j >= i)의 개수와 이다


    그리고 문제에서 전체 수열이 유한 수열이므로, 
    문제는 각각의 i(>= 0)에 대해 s[i] == s[j] (mod m)를 만족하는 j(>=i) 갯수들의 총합을 찾는 것과 같다

    s[i] == k (mod m)인 i가 p개 존재하고,
    이들을 오름 차순으로 나열했을 때 i1, i2, i3, ... , ip 라 가정하자

    그러면 i1 일 때는 i2, i3, ... ip 들이 만족해서 p - 1개 존재하고,
    i2일 때는 i3, i4, ... ip 들이 만족해서 p - 2개 존재하고,
    ... i(p - 1) 일 때는 ip이 만족해서 1개 존재한다
    따라서 총 개수는 (p - 1) + (p - 2) + (p - 3) + ... + 1 
    == 1 + 2 + 3 + ... + p - 1 == (p * (p - 1)) / 2개 존재한다
    
    (혹은) 조합으로 해석해도 된다!

    그래서 폐구간 [0, m - 1] 사이의 모든 k에 대해 s[i] == k 인 i의 갯수를 찾아 연산하면 된다
*/

namespace BaekJoon._26
{
    internal class _26_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // info[1]로 나눈 나머지이다!
            long[] nums = sr.ReadLine().Split(' ').Select(long.Parse).ToArray();
            sr.Close();

            int[] remains = new int[info[1]];
            remains[0] = 1;
            
            long total = 0;

            for (int i = 0; i < nums.Length; i++)
            {

                total += nums[i];
                total %= info[1];
                remains[total]++;
            }

            total = 0;
            long result = 0;

            for (int i = 0; i < info[1]; i++)
            {

                result += remains[i] * (remains[i] - 1) / 2;
            }

            Console.WriteLine(result);

            /*
            int result = 0;
            int sum = 0;


            for (int i = 0; i < info[0]; i++)
            {

                sum = 0;
                for (int j = i; j < info[0]; j++)
                {

                    sum += nums[j];
                    sum %= info[1];
                    if (sum == 0) result++;
                }
            }

            Console.WriteLine(result);
            */
        }
    }
}

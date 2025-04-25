using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 26
이름 : 배성훈
내용 : 피하자
    문제번호 : 25379번

    그리디 문제다
    오버플로우로 한 번 틀렸다
    문제 아이디어는 다음과 같다
    그리디하게 입력과 동시에 홀수들을 왼쪽으로 몰아주는 최소의 경우를 찾았다
    그리고, 마찬가지로 짝수를 왼쪽으로 몰아줬을 때 경우를 찾았다

    몰아주는 경우를 찾는 방법은 두 포인터를 썼다
    홀수의 경우를 보면 하나는 현재 위치, 다른 하나는 홀수의 끝위치다
    그리고 홀수를 입력받으면 현재 위치에서 홀수의 끝위치로 이동시켜주는게 최소가 된다
    이를 누적해서 더해가면 왼쪽으로 홀수를 몰아넣은 최소값이 된다
    짝수도 똑같은 방법으로 찾았다

    이러한 방법으로 찾으니, 100만개의 수열에서는 int의 범위를 벗어나는 경우가 존재했다
    실제로 최악의 경우 50만 * 50만값이 나올 것이다(짝수 50만개 입력 -> 홀수 50만개 입력 되는경우다!)
    혹은 홀짝 홀짝으로만 배치해도 int 범위를 벗어남을 확인할 수 있다

    오버플로우 부분을 long으로 해결하니 124ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0356
    {

        static void Main356(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 512);

            int n = ReadInt();

            int[] arr = new int[n];
            int lE = 0;
            int lO = 0;

            // 오버플로우 문제로 long 설정
            long ret1 = 0;
            long ret2 = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                cur %= 2;
                arr[i] = cur;

                // 짝수를 왼쪽으로 옮기는 경우 찾기
                if (cur == 0)
                {

                    ret1 += i - lE;
                    lE++;
                }
                // 홀수를 왼쪽으로 옮기는 경우 찾기
                else
                {

                    ret2 += i - lO;
                    lO++;
                }
            }

            // 둘 중 작은 것이 결과값
            if (ret1 < ret2) Console.WriteLine(ret1);
            else Console.WriteLine(ret2);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System.IO;
using System.Text;
using System;

class Programs
{

    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static int[] a;
    static int n;
    static void Main(String[] args)
    {
         n = int.Parse(sr.ReadLine());
       string[] str= sr.ReadLine().Split();
        //대충 보면 짝수와 홀수로 나누는 것 같구만 중간엔 교차되서 1개 될테고 짝수 홀수로 나누고 홀수 짝수로 나눈 경우로 최소 값을 구해보면되려나?
        a = new int[n];
        long one = 0;
        for (int i = 0; i < n; i++)
        {
            a[i] = int.Parse(str[i])%2;
            if(a[i].Equals(1))
            {
                one++;
            }
        }
        //짝수 홀수인 경우로~ 정렬하면끝.
       
        //좌측으로 1을 정렬해본다. 내림차순
        long answer = 0,l=0,r=0,idx=0;
        for (int i = 0; i < n; i++)
        {
            if(a[i].Equals(1))
            {
               l += i - idx;
                idx++;
            }
        }
        idx = 0;
        //우측으로 1을 정렬해본다. 오름차순
        for (int i = 0; i < n; i++)
        {
            if (a[i].Equals(1))
            {
                r +=  n-1-idx-i;
                idx++;
            }
        }
        //음 단순히 LIst로 removeAt을 적용해서  idx를 찾아가면서 계산하니까 시간초과가 뜬다. 인덱스로 접근해도 사실 크게 상관없으니...
        answer = Math.Min(l,r);
        sw.Write(answer);
        sw.Close();
    }
}
#endif
}

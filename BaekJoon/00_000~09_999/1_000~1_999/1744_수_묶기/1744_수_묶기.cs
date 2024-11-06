using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. ?
이름 : 배성훈
내용 : 수 묶기
    문제번호 : 1744번, 2036번

    주된 아이디어는 다음과 같다
    먼저 수를 정렬한다

    가장 작은 음수와 그 다음으로 작은 음수를 곱한게 음수들 중 가장 큰 경우의 수가 된다!
    그래서 음수부분은 가장 작은것과 그 다음으로 작은거 2개씩 짝찌어서 제거할 수 있는만큼 해준다

    이후에 양수부분은 1보다 큰 수들에 대해서는 곱한게 더한것보다 항상 크거나 같다
    양수는 가장 큰 것과 그 다음으로 큰 것을 곱해주는게 가장 큰 경우의 수가 된다!
    그래서 양수 부분은 덧셈보다 곱셈이 큰 경우 가장 큰 것과 그 다음으로 큰 것을 곱하면서 제거해준다

    그리고 앞의 음수에서 음수가 1개 있으면 0과 곱할 수 있으면 0과 곱하고 제거한다
    이후에 남은 수들은 덧셈으로 처리하면 가장 큰 경우의 수에 포함되므로 더해준다

    1744, 2036 둘 다 쓸 수 있는 코드다!

    다른 사람 풀이를 보니 200만개 메모리를 써서 음수 양수 나눠서 O(M)에 풀었다 (M은 수의 범위)
*/

namespace BaekJoon.etc
{
    internal class etc_0061
    {

        static void Main61(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            long[] arr = new long[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = long.Parse(sr.ReadLine());
            }
            sr.Close();

            Array.Sort(arr);
            long ret = 0;

            {

                // 음수부분 처리
                int idx = 0;
                for (int i = 1; i < n; i += 2)
                {

                    if (arr[i] >= 0) break;
                    ret += arr[i] * arr[i - 1];
                    idx += 2;
                }

                // 이제 마이너스 곱셈 부분 끝!
                // 이 경우에 마이너스 개수는 많아야 1개거나 0개!

                // 나머지 부분 처리
                if (idx < n - 1)
                {

                    // 뒤에 원소가 2개 이상 남아있다!
                    int endIdx = n - 1;

                    // 양수 곱셈부분 처리!
                    // 많아야 idx + 1까지 조사한다!
                    // 양수부분이 없다면 해당 포문 건너띈다!
                    for (int i = n - 1; i > idx + 1; i -= 2)
                    {

                        long calc1 = arr[i] * arr[i - 1];
                        long calc2 = arr[i] + arr[i - 1];

                        if (calc1 < calc2) break;

                        endIdx -= 2;
                        ret += calc1;
                    }

                    if (idx == endIdx)
                    {

                        // 양수 부분 처리하고
                        // 1개 남은 경우
                        // 곱셈연산 불가능하고 덧셈으로 처리!
                        ret += arr[idx];
                    }
                    else
                    {

                        // 2개 이상 남음!
                        {

                            // 음수가 1개 있을 수 있으니 여기서 확인
                            long calc1 = arr[idx] * arr[idx + 1];
                            long calc2 = arr[idx] + arr[idx + 1];

                            ret += calc1 < calc2 ? calc2 : calc1;

                            // 이외는 더해주는게 가장 작다!
                            for (int i = idx + 2; i <= endIdx; i++)
                            {

                                ret += arr[i];
                            }
                        }
                    }
                }
                // 음수 홀수개로 이루어진 수열이면 마지막 원소가 여기에 온다!
                else if (idx == n - 1) ret += arr[idx];
            }
            Console.WriteLine(ret);
        }
    }

#if other
var reader = new Reader();
var n = reader.NextInt();

var arr = new int[2000001];
while (n-- > 0)
    arr[reader.NextInt() + 1000000]++;

long score = 0;
long temp = 0;
for (int i = 0; i <= 1000000; i++)
{
    for (;arr[i] > 0; arr[i]--)
    {
        if (temp == 0)
        {
            temp = i - 1000000;
        }
        else
        {
            score += temp * (i - 1000000);
            temp = 0;
        }
    }
}
score += temp;
temp = 0;

for (int i = 2000000; i > 1000001; i--)
{
    for (;arr[i] > 0; arr[i]--)
    {
        if (temp == 0)
        {
            temp = i - 1000000;
        }
        else
        {
            score += temp * (i - 1000000);
            temp = 0;
        }
    }
}
score += temp;

for (; arr[1000001] > 0; arr[1000001]--)
    score++;

Console.Write(score);

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 중복 없는 수
    문제번호 : 11037번

    N 이상이 아닌 N 보다 큰 수로 찾는 거였다!
    해당 부분 때문에 의미없는 수정으로 많이 틀렸다;

    아이디어는 간단하다
    그냥 해당 수보다 큰 중복없는 수를 만들면 된다!
    (1436번 영화감독 숌 처럼 직접 만들었다!)

    먼저 str을 입력 받으면 str을 숫자로 만든 뒤, 1을 더하고 문자열로 다시 만든다

    자리수로 검사하기에 int가 아닌 string으로 접근했다
    각 자리를 조사하는데, 중복되는 숫자나 0이 없으면 해당 자리에 숫자를 기록하고 썼음을 기록한다
    모든 자릿수를 확인했음에도 중복되는 숫자나 0이 없으면 바로 출력한다

    중복된게 발견되었다면 해당 수까지만 기록하고 뒤의 원소는 탐색안하고 탈출한다
    해당 인덱스부터 큰 수가 되게 중복 없는 수를 만든다
    현재 해당 자리에 있는 기존 값보다 큰 값이 와야한다! 
    그래서 큰 수 중에 중복된 수가 있는지 조사한다 중복된게 없다면 바로 그 수를 쓴다!
    그리고 뒤의 자리를 채우는데 1부터 중복안되는 숫자들로 채운다 (자리 수 조건으로 무조건 채울 수 있다!)

    만약 해당 자리에 있는 기존 값보다 큰 값에 대해 못찾았다면
    해당 자리수를 앞으로 당긴다 여기서, 앞 자리의 숫자를 중복 가능하게 설정한다!
    (for문에서 해당 수보다 큰 값부터 시작하기에 먼저 중복가능하게 했다)

    그리고 앞과 같은 방법을 진행한다
    그렇게 앞의 자리로 와도 못찾는다면 탐색을 종료하고
    해당 자리수 + 1해서 가장 작은 중복 없는 수를 만든다
    이는 1부터 늘린 자리수까지 앞의 숫자에 1씩 늘려가면 된다
        예를들어 999를 입력 받았을 경우 해당 4자리 중 가장 작은 중복 없는 수를 만들어야 하는데 1234이다

    만약 9자리인 경우에서 못찾았으면 못만드니깐 문제 조건에 맞게 0을 출력한다

    56ms에 이상없이 통과했다;
    해당 경우 정말 운이 없다면! 검사에서 인덱스 세는게 100번을 돈다
*/

namespace BaekJoon.etc
{
    internal class etc_0053
    {

        static void Main53(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 여기서 가능한 경우의 수 찾자!
            bool[] nums = new bool[10];

            // 수 기록
            int[] save = new int[10];
            while (true)
            {

                string str = sr.ReadLine();
                if (str == null || str == string.Empty) break;

                {
                    int calc = int.Parse(str) + 1;
                    if (calc != 1_000_000_000) str = calc.ToString();
                }

                for (int i = 1; i < 10; i++)
                {

                    nums[i] = false;
                    save[i] = 0;
                }

                // 0은 없어야하므로 이미 있다고 가정한다!
                nums[0] = true;

                int len = str.Length;

                int changePos = len + 1;
                for (int i = 1; i <= len; i++)
                {

                    int c = str[i - 1] - '0';
                    save[i] = c;

                    if (nums[c])
                    {

                        changePos = i;
                        break;
                    }

                    nums[c] = true;
                }

                while (changePos <= len && changePos > 0)
                {

                    bool findValue = false;
                    // 변화가 있다
                    // 해당 자리 조사
                    for (int i = save[changePos] + 1; i < 10; i++)
                    {

                        // 빈거 있으면 해당 수로 교체
                        if (!nums[i])
                        {

                            save[changePos] = i;
                            changePos++;
                            nums[i] = true;

                            // 다음 수를 채운다
                            // 10억 미만에서는 무조건 채울 수 있다!
                            if (changePos <= len)
                            {

                                for (int j = 1; j < 10; j++)
                                {

                                    if (!nums[j])
                                    {

                                        save[changePos++] = j;
                                        nums[j] = true;
                                    }

                                    if (changePos > len) break;
                                }
                            }

                            findValue = true;
                            break; 
                        }
                    }

                    // 못 찾은 경우 인덱스를 줄인다
                    if (!findValue)
                    {

                        // 그냥 0으로 세팅한다;
                        save[changePos] = 0;
                        changePos--;
                        // 여기서 0이 false로 될 수 있다!
                        nums[save[changePos]] = false;
                    }
                }

                // 못찾아서 전체 자리 수를 늘려 가장 작은 수를 찾아야한다
                if (changePos == 0)
                {

                    // 못만드는 경우!
                    if (len == 9) sw.WriteLine(0);
                    else
                    {

                        // 자리수 늘려서 가장 작은 수를 만든다
                        // 4자리 중 가장 작은건 1234, 5자리 중 가장 작은건 12345
                        for (int i = 1; i <= len + 1; i++)
                        {

                            sw.Write(i);
                        }

                        sw.Write('\n');
                    }
                }
                else
                {

                    for (int i = 1; i <= len; i++)
                    {

                        sw.Write(save[i]);
                    }
                    sw.Write('\n');
                }


                sw.Flush();
            }

            sr.Close();
            sw.Close();
        }
    }

#if other
from sys import stdin
input = stdin.readline

def main():
    N = input().rstrip()
    n_len = len(N)
    
    if int(N) >= MAX:
        return 0
    
    if int(N) >= int(str(MAX)[:n_len]):
        return int(str(MIN)[:(n_len+1)])
    
    visited = [False] * 10
    answer = []

    def dfs(rank, flag):
        if rank == n_len:
            intanswer = 0
            for i in range(n_len):
                intanswer += answer[i]*(10**(n_len-1-i))

            if intanswer > int(N):
                return intanswer
                
            return 0

        for i in range(min(flag,int(N[rank])), 10):
            if i==0 or visited[i]:
                flag = 1
                continue
                
            visited[i] = True
            answer.append(i)
            
            temp = dfs(rank+1, flag)
            if temp > 0:
                return temp
                
            visited[i] = False
            answer.pop()
            flag = 1

        return 0

    x = dfs(0, 9)
    return x

MAX = 987654321
MIN = 123456789
while True:
    try:
        print(main())
    except:
        break

#elif other2
// 중복 없는 수
import sys
input = sys.stdin.read

def dfs(m):
    if len(s) == m:
        lst.append(conv(s))
        return 
    
    for i in num:
        if not i in s:
            s.append(i)
            dfs(m)
            s.pop()
    
def find(i):
    lo,hi = -1,n
    while lo+1 < hi:
        mid = (lo+hi)//2

        if lst[mid] > i:
            hi = mid
        else:
            lo = mid

    return lst[hi]

conv = lambda x:int(''.join(x))
num = '123456789'
inf = 987654321

lst = []
s = []
for i in range(1,10):
    dfs(i)

lst.sort()
n = len(lst)

d = list(map(int,input().split()))

for i in d:
    res = find(i) if i < inf else 0
    print(res)
#endif
}

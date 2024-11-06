using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : King’s Task
    문제번호 : 21995번

    구현, 시뮬레이션, 애드 혹 문제다
    n이 짝수인 경우 2번 안에 정렬 여부를 찾을 수 있고,
    n이 홀수면 n번 연산을 해야 정렬 여부를 알 수 있다

    짝수인 경우 1번에 찾은 것인데 3번에 찾았다고 답을 내서 2번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0639
    {

        static void Main639(string[] args)
        {

            StreamReader sr;
            int[] arr;
            int n;
            int len;

            Solve();

            void Solve()
            {

                Input();
                int len = n % 2 == 1 ? n : 2;
                int ret = GetRet();
                ret = len < ret ? 2 * len - ret : ret;
                Console.WriteLine(ret);
            }

            int GetRet()
            {

                int ret = 0;
                if (ChkOrder()) return ret;
                len = n % 2 == 0 ? 2 : n;
                for (int i = 0; i < len; i++)
                {

                    ret++;
                    Change1();
                    if (ChkOrder()) return ret;
                    ret++;
                    Change2();
                    if (ChkOrder()) return ret;
                }

                return -1;
            }

            bool ChkOrder()
            {

                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i] == i + 1) continue;
                    return false;
                }

                return true;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                arr = new int[2 * n];
                for (int i = 0; i < arr.Length; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            void Change1()
            {

                for (int i = 0; i < n; i++)
                {

                    int temp = arr[i];
                    arr[i] = arr[i + n];
                    arr[i + n] = temp;
                }
            }

            void Change2()
            {

                for (int i = 0; i < n; i++)
                {

                    int temp = arr[2 * i];
                    arr[2 * i] = arr[2 * i + 1];
                    arr[2 * i + 1] = temp;
                }
            }

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
def simulation(p,n):
    
    target = list(range(1,2*n+1))

    answer = 0

    for _ in range(max(n,4)):
        
        if p != target:

            if p[0] < p[n]:
                
                for i in range(0,2*n,2):
                    
                    p[i],p[i+1] = p[i+1],p[i]
            
            else:
                
                for i in range(n):
                    
                    p[i],p[i+n] = p[i+n],p[i]
        
        else:
            
            break
        
        answer += 1
    
    if p == target:
        
        return answer
    
    else:
        
        return -1

n = int(input())

p = list(map(int,input().split()))

print(simulation(p,n))
#endif
}

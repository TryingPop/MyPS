using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 포스택
    문제번호 : 25556번

    그리디, 자료구조, 스택 문제다
    그리디하게 접근해서 해결했다

    스택 4개로 조건대로 정렬한다면 수를 담을 때, 
    적어도 자기보다 큰 수가 위에 담겨야 정렬이 된다
    작은 수가 있으면 조건대로 정렬이 불가능하다

    여기서 그리디하게 접근해서
    1번 스택을 4개 중 가장 큰값이 담기게하는 스택, 
    2번 스택을 4개 중 2번째로 큰 값이 담기게 하는 스택,
    ... 4번 스택까지 이렇게 역할을 부여했다

    그리고 해당 스택으로 오름차순 정렬이 가능한지 
    몇 가지 예시를 들어 확인했고 
        - 내림차순으로 자기보다 작은게 5개 있는 경우 4개 있는경우, 3개 있는 경우, 2개 있는경우, 1개 있는경우 0개 있는 경우만 했다
        (5개 이상은 불가능함을 예제로 미리 찾았기 때문!)

    이상없이 오름차순 정렬이 가능해
    해당 방법으로 제출하니 68ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0553
    {

        static void Main553(string[] args)
        {

            string YES = "YES";
            string NO = "NO";
            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);

            Solve();
            sr.Close();

            void Solve()
            {

                int n = ReadInt();

                int[] max = new int[4];
                bool ret = true;
                for (int i = 0; i < n; i++)
                {

                    int c = ReadInt();
                    ret = false;
                    for (int j = 0; j < 4; j++)
                    {

                        if (max[j] < c) 
                        { 
                            
                            max[j] = c;
                            ret = true;
                            break;
                        }
                    }

                    if (ret) continue;
                    break;
                }

                Console.WriteLine(ret ? YES : NO);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			//bj 25556 /g5 /포스택 /240312
			//4개의 스택을 순회하면서, 오름차순으로 넣을 수 있는지를 계속 확인한다. 
			//4개의 스택을 순회했는데 전부 넣을 수 없는 수가 있다면 그건 틀린 포스택이다. 
			//모든 수열을 정상적으로 순회했다면 그건 맞는 포스택이다.

			//선언 
			Stack<int>[] stack = new Stack<int>[4]; //스택배열 생성
			for(int i = 0; i < 4; i++) {
				stack[i] = new Stack<int>();
				stack[i].Push(0); //초기 예외처리용 숫자, 얘 없으면 값 비었는지도 확인해야함
			}
			//첫째줄 N입력
			int N = int.Parse(Console.ReadLine());
			//수열 입력
			int[] seq = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

			//모든 수열 순회
			foreach(int num in seq){
				//0~3
				//하나의 요소가 모든 스택에 넣어질 수 있는지 하나하나 확인
				for(int i = 0; i < 4; i++){
					//4개 스택 순회중에 오름차순 정렬이 된다면
					if(num > stack[i].Peek()){
						stack[i].Push(num);
						break; //다음요소로 넘어감
					}
					//4개의 스택을 전부 봐도 정렬이 안된다면
					if(i == 3){
						Console.Write("NO"); 
						return; //프로그램을 종료함
					}
				}
			}
			//전부 순회했으면
			Console.Write("YES");
		}
	}
}
#elif other2

int N = int.Parse(Console.ReadLine());
int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

int[] s = new int[4];
for(int i = 0; i < N;i++)
{
    //Console.WriteLine("[{0}]: {1}",i,line[i]);
    if (Max(line[i]) != -1) s[Max(line[i])] = line[i];
    else { Console.WriteLine("NO"); return; }
}

Console.WriteLine("YES");

return;

int Max(int num)
{
    int id = -1;
    int max = -1;
    if (s[0] > max && s[0] < num) { max = s[0]; id = 0; }
    if (s[1] > max && s[1] < num) { max = s[1]; id = 1; }
    if (s[2] > max && s[2] < num) { max = s[2]; id = 2; }
    if (s[3] > max && s[3] < num) { max = s[3]; id = 3; }
    return id;
}
#endif
}

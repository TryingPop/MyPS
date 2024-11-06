using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 6
이름 : 배성훈
내용 : 통나무 건너뛰기
    문제번호 : 11497번

    그리디, 정렬 문제다
    아이디어는 다음과 같다
    arr을 정렬 한 뒤 가장 작은 값을 중앙에 두고 좌우로 번갈아가면서 가장 작은 값을 둔다
    그리고 왼쪽은 왼쪽끼리, 오른쪽은 오른쪽기리 크기를 비교해 격차의 최대값 ret라하면
    ret는 max(arr[i + 2] - arr[i]) 이고, ret는 문제에서 찾는 최소값이라 추측해서 제출하니 통과했다

    이제 ret가 최소값임을 보이자
    최소값인 경우를 r이라하면 r <= ret임은 자명하다
    귀류법으로 r < ret라 가정하자

    그러면 ret의 정의로 max(arr[i + 2] - arr[i])이므로
    적당한 k에대해 ret = arr[k + 2] - arr[k]이다
    그리고, r < ret이므로 k, k + 1, k + 2는 같은 곳에 있어야한다
    arr은 정렬된 배열이므로
    k + 3이 존재하는 경우면, 
        arr[k + 3] - arr[k] > arr[k + 2] - arr[k]이고,
    k - 1이 존재하는 경우면,
        arr[k + 2] - arr[k - 1] > arr[k + 2] - arr[k]임을 알 수 있다
    그러므로 r의 정의로 k - 1, k, k + 1, k + 2, k + 3은 같은 곳에 있게되고
    이를 계속해서 확장해가면 페르마의 강하법으로 결국 0 ~ len - 1까지 모두 나란히 위치해야한다
    그러면 0번과 len -1 의 차이는 r = arr[len - 1] - arr[0] 이 될 수 밖에 없고
    이는 r >= arr[k + 2] - arr[k]이므로 가정에 모순이다
    귀류법으로 r == ret일 수 밖에 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0679
    {

        static void Main679(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int len;
            int test;
            int[] arr;

            Solve();

            void Solve()
            {

                Init();

                while(test-- > 0)
                {

                    len = ReadInt();
                    for (int i = 0; i < len; i++)
                    {

                        arr[i] = ReadInt();
                    }
                    Array.Sort(arr, 0, len);

                    int ret = 0;
                    for (int i = 0; i < len - 2; i++)
                    {

                        ret = Math.Max(ret, arr[i + 2] - arr[i]);
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                test = ReadInt();
                arr = new int[10_000];
            }

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

#if other
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
class Baekjoon{
    static void Main(String[] args){
        int rept = int.Parse(Console.ReadLine());
        StringBuilder st = new StringBuilder();
        for(int i = 0; i<rept ; i++){
            int num = int.Parse(Console.ReadLine());
            int[] index = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
            int[] arr = new int[num];
            Array.Sort(index);

            int max = 0;
            for(int j =2 ; j<index.Length ; j++){
                max = Math.Max(Math.Abs(index[j]-index[j-2]),max);
            }
            st.AppendLine(max.ToString());
        }
        Console.Write(st.ToString());
    }  
}   
#endif
}

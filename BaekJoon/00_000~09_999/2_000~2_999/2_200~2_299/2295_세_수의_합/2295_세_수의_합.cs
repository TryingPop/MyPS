using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 세 수의 합
    문제번호 : 2295번

    etc_0100을 위한 준비문제!
    중간에서 만나기 아이디어 있어 풀었는데;
    ... 이게 중간에서 만나기인지 모르겠다;

    그냥 for문을 돌려 풀 시에 N^4 시간이 걸리는데,
            x + y + z = k 
            |   |   |
            N   N   N 
        k만드는데 N^3, k가 arr에 있는지 검색하는데 N

    해시를 쓰면, k가 N에 있는지 확인하는데, 1의 시간으로 줄어든다
    해시만 쓰면 N^3이 걸린다

    그런데 x + y 와 k - z로 분할해서 보면
    메모리를 N^2을 더 쓰는 대신에 탐색시간을 N^2으로 줄일 수 있다(해시를 써야 N^2이 나온다!)
    x + y의 결과들을 모아둔다 여기서 N^2의 시간과 N^2의 메모리를 더 쓴다
    
    이후 k -z 연산을 하며 x + y에 포함되어있는지 검사한다
    해시를 안쓰고 그냥 탐색하면 N^2이다, 이분 탐색하면 log(N^2) = 2 * logN, 해시는 1
    k - z연산을 N^2해야하기에,
    그냥 탐색은 N^4, 이분 탐색은 N^2 * logN, 해시는 N^2으로 예상된다

    처음에 x, y, z가 같을 수 있다를 무시해버려서 한 번 틀렸다;
    이후엔 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0054
    {

        static void Main54(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            long[] arr = new long[len];

            for (int i = 0; i < len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            // x + y 연산 결과 저장
            HashSet<long> sums;
            {

                sums = new HashSet<long>(len * len);
                for (int i = 0; i < len; i++)
                {

                    for (int j = 0; j < len; j++)
                    {

                       sums.Add(arr[i] + arr[j]);
                    }
                }
            }

            // k - z 값이 x + y에 있는지 확인
            long max = 0;
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < len; j++)
                {

                    if (sums.Contains(arr[i] - arr[j]))
                    {

                        if (max < arr[i]) max = arr[i];
                    }
                }
            }

            Console.WriteLine(max);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
    class No_2295
    {
        static void Main()
        {
            using StreamReader input = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            using StreamWriter output = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int n = int.Parse(input.ReadLine());
            int[] arr = new int[n];
            
            for(int i=0 ;i<n ;i++)
                arr[i] = int.Parse(input.ReadLine());

            Array.Sort(arr);

            List<int> two = new List<int>();
            for(int i=0 ;i<n ;i++)
                for(int j=i ;j<n ;j++)
                    two.Add(arr[i] + arr[j]);

            two.Sort();

            for(int i=n-1 ;i>0 ;i--)
            {
                for(int j=0 ;j<i ;j++)
                {
                    if(BinarySearch(two, arr[i] - arr[j]))
                    {
                        output.Write(arr[i]);
                        return;
                    }
                }
            }
        }

        static bool BinarySearch(List<int> list, int target)
        {
            int start = 0;
            int end = list.Count-1;

            while(start<=end)
            {
                int mid = (start+end)/2;

                if(list[mid] == target)
                    return true;
                else if(list[mid] < target)
                    start = mid + 1;
                else
                    end = mid - 1;
            }

            return false;
        }
    }
#endif
}
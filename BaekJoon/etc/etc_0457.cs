using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 로봇 프로젝트
    문제번호 : 3649번

    정렬, 이분탐색, 두 포인터 문제다
    이분탐색 연습할겸 정렬과 이분탐색으로 풀었다
    딕셔너리로 이분탐색을 대체할 수 있다

    아이디어는 다음과 같다
    먼저 레고 크기에따라 정렬한다
    이후 뒤에 부분 중에 부품으로 구멍을 막을 수 있는지 확인한다
    만약 막을 수 있다면 막는 차가 최대이므로 거기서 탐색종료다! (그리디) 
    막을 수 없다면 막을 수 있을 때까지 찾는다

    그리고 못찾으면 못찾는다고 한다
    여기서, yes부분에 줄바꿈 문자열을 안넣어 한 번 틀렸다;

    시간은 정렬하는데 NlogN, 그리고 각 원소에 대해 이분탐색하므로 NlogN
    전체적으로 M * N * logN 시간이 걸린다 여기서, N은 구멍 수, M은 케이스 수이다
    이후에는 제출하니 2988ms로 통과했다 데이터가 많아 시간이 많이걸리는거 같아
    해당 부분을 약간 수정하니 876ms까지 줄였다
*/

namespace BaekJoon.etc
{
    internal class etc_0457
    {

        static void Main457(string[] args)
        {

            string NO = "danger\n";
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 1024 * 8);

            string temp;
            int[] arr = new int[1_000_001];

            while ((temp = sr.ReadLine()) != null && temp != string.Empty)
            {

                int chk = int.Parse(temp);
                int len = ReadInt();
                
                for (int i = 0; i < len; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr, 0, len);
                chk *= 10_000_000;
                int ret1 = -1;
                int ret2 = -1;

                for (int i = 0; i < len; i++)
                {

                    int l = i + 1;
                    int r = len - 1;

                    int find = chk - arr[i];
                    while(l <= r)
                    {

                        int mid = (l + r) / 2;

                        if (arr[mid] < find) l = mid + 1;
                        else if (arr[mid] > find) r = mid - 1;
                        else
                        {

                            ret2 = mid;
                            break;
                        }
                    }

                    if (ret2 > -1)
                    {

                        ret1 = i;
                        break;
                    }
                }

                if (ret1 == -1) sw.Write(NO);
                else sw.Write($"yes {arr[ret1]} {arr[ret2]}\n");

                sw.Flush();
            }

            sr.Close();
            sw.Close();

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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                string hole = input.ReadLine();
                if (hole == null)
                    break;
                int nh = int.Parse(hole) * 10000000;
                int n = int.Parse(input.ReadLine());
                int[] rego = new int[n];
                for (int i = 0; i < n; i++)
                    rego[i] = int.Parse(input.ReadLine());
                Array.Sort(rego);
                int left = 0;
                int right = n - 1;
                bool check = false;
                while(left < right)
                {
                    int sum = rego[left] + rego[right];
                    if(sum == nh)
                    {
                        sb.Append($"yes {rego[left]} {rego[right]}\n");
                        check = true;
                        break;
                    }
                    if (sum < nh)
                        left++;
                    else
                        right--;
                }
                if (!check)
                    sb.Append("danger\n");
            }            

            output.Write(sb);

            input.Close();
            output.Close();
        }
    }
}
#endif
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 전깃줄 2
    문제번호 : 2568번

    가장 긴 증가하는 부분수열 (LIS) 문제다
    시간 복잡도를 N log N 으로 해야 된다
    
    왼쪽 값에 대해 오름차순으로 먼저 정렬한다
    그리고 오른쪽 값에 대해 자기보다 위에 있는 정상적인 전깃줄들을 찾는다
    (그리디 알고리즘이다)

    정상적인 전깃줄을 빨리 찾기 위해
    자기보다 위에 있는 전깃줄 중 길이에 따라 가장 높이 있는 값을 memo 해둔다
    (dp 알고리즘 + 그리디 알고리즘 =  LIS 알고리즘)

    여기서 빠르게 찾아야 하므로 이진탐색이 쓰인다
    (각 원소에 log N이므로 전체 시간은 N log N)

    그리고 컷할 것을 알아야하기에 별도의 메모리도 필요하다
    컷할것을 판별하기 위해 컷 여부 bool 배열과 
    컷 여부에 쓸 자신 위에 존재할 수 있는 전선의 개수를 기록하는 int 배열을 더 할당했다

    아래 코드는 해당 아이디어를 구현한 것이다
    그리고 제출하니 116ms 로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0260
    {

        static void Main260(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            // 전깃줄 데이터
            MyData[] lines = new MyData[len];

            for(int i = 0; i < len; i++)
            {

                // 앞쪽, 뒤쪽 번호
                int go = ReadInt(sr);
                int to = ReadInt(sr);

                lines[i].Set(go, to);
            }

            sr.Close();

            // 앞쪽을 기준으로 정렬해주면 되는데,
            // 앞쪽이 같을 경우 뒤쪽을 내림차순으로 해줬다
            // 밑에 연산을 간편하게 하기 위해 내림차순을 차선으로 했다
            // 앞쪽을 기준으로만 정렬 해주면 이상없다!
            Array.Sort(lines);

            // LIS 기록용 보드판
            int[] memo = new int[len];

            // i 인덱스의 값은 i 인덱스 전깃줄을 포함한 최장 길이
            int[] myLen = new int[len];

            // 현재 길이 -> 0번 넣고 시작한다
            int memoLen = 1;
            memo[0] = lines[0].to;
            myLen[0] = 1;
            for (int i = 1; i < len; i++)
            {

                // 자신보다 위에 있는 전깃줄 중에
                // 길이에 따라 가장 높이 있는 애를 기록한다
                // 자신보다 작은 전깃줄의 최대 갯수와 동형이 된다
                int l = 0;
                int r = memoLen - 1;

                // 현재 높이
                int curTo = lines[i].to;

                while(l <= r)
                {

                    int mid = (l + r) / 2;
                    if (curTo <= memo[mid])
                    {

                        r = mid - 1;
                    }
                    else
                    {

                        l = mid + 1;
                    }
                }

                int curLen = r + 2;
                // 나의 최장 길이 저장
                myLen[i] = curLen;

                // go가 같은 경우 to에서 내림차순으로 넣었기에 부담없이
                // memo[r + 1]의 값을 갱신해주면된다
                // 만약 안했다면 여기서 확인하는 절차가 필요하다
                memo[r + 1] = lines[i].to;
                if (curLen > memoLen)
                {

                    memoLen = curLen;
                }
            }

            // 이제 짜를거 판별!
            bool[] save = new bool[len];
            int cur = memoLen;
            for (int i = len - 1; i >= 0; i--)
            {

                // 뒤에서부터 최장 길이를 살린다!
                if (cur == myLen[i])
                {

                    save[i] = true;
                    cur--;
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(len - memoLen);
                for (int i = 0; i < len; i++)
                {

                    if (save[i]) continue;
                    sw.WriteLine(lines[i].go);
                }
            }
        }

        struct MyData : IComparable<MyData>
        {

            public int go;
            public int to;

            public void Set(int _go, int _to)
            {

                go = _go;
                to = _to;
            }

            public int CompareTo(MyData other)
            {

                int ret = go.CompareTo(other.go);
                if (ret == 0) other.to.CompareTo(to);
                return ret;
            }
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

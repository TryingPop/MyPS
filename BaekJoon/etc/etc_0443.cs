using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 블랙 프라이데이
    문제번호 : 18114번

    브루트포스, 정렬, 이분탐색 문제다
    삼중 포문으로 접근하면 N^3 시간이 걸린다
    N = 5,000이므로 시간초과가 날것이다
    해시를 써서 N^2으로 계산해서 풀었다

    아이디어는 다음과 같다
    1개로 1만원 받을 수 있는지 확인하는건 입력과 동시에 진행한다
    2개와 3개로 만들 수 있는건 동시에 진행한다
    이는 서로 다른 두개를 합쳐서 전체에서 남은 하나를 뺀게 되는지 확인했다

    다른 사람 풀이를 보니 해시보다 이분 탐색으로 처리하는게 더 빨라보인다
    이분탐색은 다음과 같다 확인하는 부분에서, 이분ㅇ탐색을 진행한다
*/

namespace BaekJoon.etc
{
    internal class etc_0443
    {

        static void Main443(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 8);
            int len = ReadInt();
            int k = ReadInt();

            Dictionary<int, int> dic = new(len + 1);
            dic[0] = -1;
            bool find = false;
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {

                int cur = ReadInt();
                arr[i] = cur;
                if (cur == k) find = true;
                dic.Add(cur, i);
            }
            sr.Close();
            for (int i = 0; i < len - 1; i++)
            {

                int target = k - arr[i];
                for (int j = i + 1; j < len; j++)
                {

                    int chk = target - arr[j];
                    if (dic.ContainsKey(chk))
                    {

                        int cur = dic[chk];
                        if (cur == i || cur == j) continue;
                        find = true;
                        break;
                    }
                }

                if (find) break;
            }

            Console.WriteLine(find ? 1 : 0);

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
// #include <cstdio>
// #include <algorithm>

using namespace std;

int a[5000];

int main() {
  int N, C;
  scanf("%d %d", &N, &C);
  bool ok = false;
  for (int i = 0; i < N; ++i) {
    scanf("%d", a + i);
    ok |= (a[i] == C);
  }
  sort(a, a + N);
  int L = 0;
  int R = N - 1;
  while (L < R) {
    int v = C - (a[L] + a[R]);
    if (v < 0) {
      R -= 1;
    } else {
      ok |= (v == 0 || (v != a[L] && v != a[R] && binary_search(a, a + N, v)));
      L += 1;
    }
  }
  return !printf("%d\n", ok);
}

#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
    static int n, c;
    static int data[];
    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        n = Integer.parseInt(st.nextToken());
        c = Integer.parseInt(st.nextToken());
        data = new int[n];
        st = new StringTokenizer(br.readLine());
        for(int i = 0; i < n;i++) {
            data[i] = Integer.parseInt(st.nextToken());
            if(data[i] == c) {
                System.out.println(1);
                System.exit(0);
            }
        }

        Arrays.sort(data);

        boolean result = false;
        int maxIdx = Arrays.binarySearch(data, c);
        if(maxIdx < 0) maxIdx = Math.abs(maxIdx)-1;
        for(int i = 0, j = maxIdx-1; i < j; ) {
            int sum = data[i] + data[j];
            if(sum == c) {
                result = true;
                break;
            } else if(sum > c) {
                j--;
            } else {
                int idx = Arrays.binarySearch(data, i+1, j, c-sum);
                if(idx > i && idx < j) {
                    result = true;
                    break;
                }
                i++;
            }
        }
        System.out.println(result ? 1 : 0);
    }
}
#endif
}

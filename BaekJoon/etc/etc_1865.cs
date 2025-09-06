using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 4
이름 : 배성훈
내용 : 수집합
    문제번호 : 4373번

    중간에서 만나기 문제다.
    맞췄을 당시에는 추론으로 풀었으나, 풀이를 작성하면서 확인하니 논리적으로 이상없다.


    찾아야할 것은 서로 다른 a, b, c, d 중 a + b + c = d를 만족하는 가장 큰 d를 찾아야 한다.

    이는 다음 식과 동치이다.
    기존 수열에서 a + b + c = d를 만족하는 서로 다른 a, b, c, d를 가져오자.
    a, b, c, d의 위치를 각각 pa, pb, pc, pd라하면

    pa, pb, pc, pd 이를 오름차순 정렬하면 pd의 위치는 다음 4가지 중 1개이다.
    pd ? ? ? / ? pd ? ? / ? ? pd ? / ? ? ? pd
    여기서 ? 에는 pa, pb, pc가 들어간다.

    먼저 pd가 ? ? pd ? / ? ? ? pd인 경우를 보자.
    편의상 작은 2개의 값을 pa, pb라 하자.
    a + b + c = d이므로 a + b = d - c가 된다.
    그래서 i번째를 조사하는데 set = { arr[j] + arr[k] | j < k < i }에 저장한다.

    이제 i < m 에대해 arr[i] - arr[m]이 set에 포함되는지 확인한다.
    이는 ? ? pd ? 를 확인하는 것이다.
    만약 포함되면 arr[i] = d가 된다.
    
    그리고 i < m 에대해 arr[m] - arr[i]가 set에 포함되는지 확인한다.
    이는 ? ? ? pd를 확인하는 것이다.
    만약 포함되면 arr[m] = d가 된다.

    그리고 포함 여부를 해시셋에 저장하는 경우 각 판별은 O(1)에 가깝게 해결된다.
    그래서 arr의 개수를 n이라 하면 각 i에 대해 O(n)의 시간에 해결이 가능하다.
    그래서 확인하는 데는 O(n^2)의 시간이 걸린다.

    set에 저장하는 원소는 naive하게 구하면 i에 대해 O(n^2)의 시간이 걸린다.
    그래서 naive하게 하는 경우 O(n^3)으로 시간 초과날 수 있다.
    
    i 와 i + 1의 set의 원소를 비교해보면 단순히 arr[i] + arr[j], j < i인 원소들이 i + 1에 추가될뿐이다.
    그래서 해당 원소들만 추가하는 식으로 하면 set에 원소를 채워넣는 것은 각 i에 대해 O(n)에 해결된다.
    그래서 set을 채우는데 걸리는 시간은 O(n^2)이다.

    이제 pd ? ? ?와 ? pd ? ?인 경우는 역순으로하면 O(n^2)에 해결된다.
    그래서 전체 시간 복잡도는 O(n^2)이고 n이 1000으로 시도할만한 방법이다.
    
    다른 사람의 풀이를 보니 i, j의 위치를 저장해 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1865
    {

        static void Main1865(string[] args)
        {

            // 4373번
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int NOTEXIST = int.MinValue;
            int MAX = 1_000;
            string N = "no solution\n";

            int n;
            int[] arr = new int[MAX];
            HashSet<int> set = new(MAX);

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int ret = NOTEXIST;

                // 중간에서 만나기
                set.Clear();
                for (int i = 0; i < n; i++)
                {

                    // 먼저 가능한 수 찾기
                    for (int j = i + 1; j < n; j++)
                    {

                        int chk = arr[j] - arr[i];
                        if (set.Contains(chk)) ret = Math.Max(arr[j], ret);
                        chk = arr[i] - arr[j];
                        if (set.Contains(chk)) ret = Math.Max(arr[i], ret);
                    }

                    for (int j = 0; j < i; j++)
                    {

                        set.Add(arr[i] + arr[j]);
                    }
                }

                set.Clear();
                for (int i = n - 1; i >= 0; i--)
                {

                    for (int j = i - 1; j >= 0; j--)
                    {

                        int chk = arr[j] - arr[i];
                        if (set.Contains(chk)) ret = Math.Max(arr[j], ret);
                        chk = arr[i] - arr[j];
                        if (set.Contains(chk)) ret = Math.Max(arr[i], ret);
                    }

                    for (int j = i + 1; j < n; j++)
                    {

                        set.Add(arr[i] + arr[j]);
                    }
                }

                if (ret == NOTEXIST) sw.Write(N);
                else sw.Write($"{ret}\n");
            }

            bool Input()
            {

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                return n != 0;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    bool positive = c != '-';

                    ret = positive ? c - '0' : 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <vector>
// #include <algorithm>
using namespace std;

const int MAX = 1000;
struct Wow {
	int k, a, b;
	bool operator<(const Wow& x) const {
		return k < x.k;
	}
};
int n;
int data[MAX + 1];
vector<Wow> rdata;

bool proc() {
	scanf("%d", &n);
	if (n == 0) {
		return false;
	}
	for (int i = 0; i < n; ++i) {
		scanf("%d", &data[i]);
	}
	sort(data, data + n);

	rdata.clear();
	for (int i = 0; i < n; ++i) {
		for (int j = i + 1; j < n; ++j) {
			rdata.push_back(Wow{ data[i] + data[j], i, j });
		}
	}
	sort(rdata.begin(), rdata.end());

	for (int i = n - 1; i >= 0; --i) {
		for (int j = 0; j < n; ++j) {
			if (i == j) {
				continue;
			}

			auto it = lower_bound(rdata.begin(), rdata.end(), Wow{ data[i] - data[j] });
			if (it == rdata.end() || it->k != data[i] - data[j]) {
				continue;
			}
			if (it->a == i || it->a == j || it->b == i || it->b == j) {
				continue;
			}

			printf("%d\n", data[i]);
			return true;
		}
	}
	printf("no solution\n");
	return true;
}

int main() {
	while (proc()) {
	}
	return 0;
}
#endif
}

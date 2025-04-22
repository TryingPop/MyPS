# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그래프 이론
  - 애드 혹
  - 해 구성하기
  - 최단 경로
  - 데이크스트라
  - 플로이드-워셜

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
오늘날 세상에는 많은 프로그래밍 대회가 있다. 대회에 사용할 좋은 프로그래밍 문제를 만드는 일은 매우 어렵다. 그 중 가장 어려운 일은 테스트 데이터를 만드는 일이다. 좋은 테스트 데이터는 문제의 의도에 맞게 짠 코드와 그렇지 않은 코드를 구별해 낼 수 있어야 한다. 또, 대부분의 경우에 올바른 결과를 내지만, 특별한 케이스에서는 틀리는 소스를 찾아낼 수도 있어야 한다.<br/>
이 문제는 지금까지 풀어왔던 문제와는 다르게 문제를 푸는 소스를 제출하는 문제가 아니다. 바로 테스트 케이스를 만드는 문제이다.<br/>
지금 상근이는 그래프 문제의 데이터 하나를 만들어야 한다. 이때, 상근이가 만들 데이터 X는 코드 A와 코드 B에 대해서 다음 조건을 만족해야 한다.<br/>

  1. 코드 A는 데이터 X를 수행할 때, 시간 초과 (TLE)가 발생하면 안 된다.
  2. 코드 B는 데이터 X를 수행할 때, 결과가 시간 초과 (TLE)이어야 한다.

또, 데이터는 작을 수록 좋기 때문에, 최대 T개의 정수로 이루어져 있어야 한다.<br/>
위의 조건을 만족하는 입력 데이터를 만드는 프로그램을 작성하시오.<br/>

## 입력
이 문제는 입력이 없다.<br/>
상근이가 데이터를 만들 문제는 7612번 SSSP이고, 사용할 코드 A는 ModifiedDijkstra, B는 FloydWarshall이다. (소스는 아래 힌트에 있다)<br/>
코드를 보면 counter 변수가 있다. 이 변수 값이 1,000,000을 넘으면 TLE이다.<br/>
또, T는 107이다.<br/>


## 출력
입력 데이터를 출력하면 된다.<br/>


## 힌트
  - ModifiedDijkstra


	#include <algorithm>
	#include <cstdio>
	#include <iostream>
	#include <vector>
	#include <queue>
	using namespace std;
	
	typedef pair<int, int> IntPair;
	typedef vector<IntPair> VectorIntPair;
	
	#define INF 1000000000
	
	int i, j, V, n, w, counter, Q, s, t, d, u;
	vector<VectorIntPair> AdjList;
	
	// scroll down to line 92 for the
	// short C++ implementation (with STL priority_queue)
	int main() {
	  scanf("%d", &V);
	  AdjList.assign(V, VectorIntPair());
	  for (i = 0; i < V; i++) {
	    scanf("%d", &n);
	    while (n--) {
	      scanf("%d %d", &j, &w);
	      AdjList[i].push_back(IntPair(j, w));
	
	    }
	  }
	
	  counter = 0;
	  scanf("%d", &Q);
	  while (Q--) {
	    scanf("%d %d", &s, &t);
	
	    vector<int> dist(V, INF);
	    dist[s] = 0;
	    priority_queue< IntPair, vector<IntPair>, greater<IntPair> > pq;
	    pq.push(IntPair(0, s));
	    while (!pq.empty()) {
	      counter++;
	      if (counter > 1000000) {
	        printf("TLE because iteration counter > 1000000\n");
	        return 1;
	      }
	
	      IntPair front = pq.top(); pq.pop();
	      d = front.first; u = front.second;
	      if (d == dist[u]) {
	        for (j = 0; j < (int)AdjList[u].size(); j++) {
	          IntPair v = AdjList[u][j];
	          if (dist[u] + v.second < dist[v.first]) {
	            dist[v.first] = dist[u] + v.second;
	            pq.push(IntPair(dist[v.first], v.first));
	          }
	        }
	      }
	    }
	
	    printf("%d\n", dist[t]);
	  }
	
	  printf("The value of counter is: %d\n", counter);
	  return 0;
	}


  - FloydWarshall


	#include <algorithm>
	#include <cstdio>
	using namespace std;
	
	int i, j, k, V, n, w, M[300][300], counter, Q, s, t;
	
	int main() {
	  scanf("%d", &V);
	  for (i = 0; i < V; i++)
	    for (j = i+1; j < V; j++)
	      M[i][j] = M[j][i] = 1000000000;
	  for (i = 0; i < V; i++)
	    M[i][i] = 0;
	
	  for (i = 0; i < V; i++) {
	    scanf("%d", &n);
	    while (n--) {
	      scanf("%d %d", &j, &w);
	      M[i][j] = min(M[i][j], w);
	    }
	  }
	
	  counter = 0;
	  for (k = 0; k < V; k++)
	    for (i = 0; i < V; i++)
	      for (j = 0; j < V; j++) {
	        counter++;
	        if (counter > 1000000) {
	          printf("TLE because iteration counter > 1000000\n");
	          return 1;
	        }
	        M[i][j] = min(M[i][j], M[i][k] + M[k][j]);
	      }
	
	  scanf("%d", &Q);
	  while (Q--) {
	    scanf("%d %d", &s, &t);
	    printf("%d\n", M[s][t]);
	  }
	
	  printf("The value of counter is: %d\n", counter);
	  return 0;
	}
	


## 풀이
플로이드-워셜 코드를 보면 정점의 갯수가 삼중 포문이 있다.<br/>
단순히 정점의 갯수를 101개로 하면 TLE를 발생할 수 있다.<br/>


데이터의 갯수는 107개를 넘겨선 안되는데 정점의 정보는 101개를 넘겼으므로 106개만 넘길 수 있다.
먼저 쿼리를 최소한으로 하자. 쿼리는 최소 1개이고, s1, t1의 정보를 넘겨야 하므로 3개를 넘길 수 있다.<br/>
그래서 간선에 많아야 103개의 정보를 넘길 수 있다.


이제 간선에 0번 노드만 1번 노드로 1거리로 이동하게 데이터를 넘긴다.<br/>
그러면 남은건 간선의 갯수, 간선의 목적지, 간선의 거리 총 3개의 데이터를 넘겼다.<br/>

남은건 100개의 데이터이다.<br/>
0번을 제외한 남은 100개의 노드는 간선의 갯수가 0으로만 넘긴다.<br/>
그러면 전달하는 정수는 107개로 된다.<br/>


그리고 다익스트라는 이상없이 통과한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/7140
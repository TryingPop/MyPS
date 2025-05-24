# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 자료 구조
  - 그래프 이론
  - 분리 집합

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB


## 문제
Some of the towns in the province G are connected with roads; roads always go both ways. The budget to support the road network is scarce, therefore it was decided to leave the bare minimum of the roads. However, if it is currently possible to reach the town B from the town A, this possibility must remain even after the reduction.<br/>
Help to define the minimum number of roads that must remain.<br/>


## 입력
The first line of the input file contains a single integer T --- the number of tests (1 ≤ T ≤ 50,000). It is followed by the description of T tests.<br/>
The first line of the test number t contains a single integer N_t --- the number of towns (1 ≤ N_t ≤ 200).<br/>
The following N_t lines contain N_t integers; each integer is either 0 or 1. If the line with the number i has 0 in the jth position, then the town j can not be reached from the town i (even by driving through other towns); if it is 1, then there is a passage. It is assumed that there is a passage from a town to the same town, so there will always be 1 in the ith line in the ith position (1 ≤ i ≤ N_t).<br/>
It is guaranteed that the sum of N_t^2 over all tests is not greater than 50,000.<br/>


## 출력
For each test, print a single integer on a separate line --- the minimum number of roads to be kept.<br/>


## 예제 입력
2<br/>
4<br/>
1 1 1 1<br/>
1 1 1 1<br/>
1 1 1 1<br/>
1 1 1 1<br/>
7<br/>
1 1 1 0 0 0 0<br/>
1 1 1 0 0 0 0<br/>
1 1 1 0 0 0 0<br/>
0 0 0 1 1 0 1<br/>
0 0 0 1 1 0 1<br/>
0 0 0 0 0 1 0<br/>
0 0 0 1 1 0 1<br/>


## 예제 출력
3<br/>
4<br/>


## 풀이
이전 연결관계를 유지하면서 도로를 최소한으로 남겨야 한다.<br/>
이는 트리 형태로 만들어라는 의미이다.<br/>
그래서 크루스칼 알고리즘 방법으로 해결했다.<br/>


각 간선의 양 끝점인 두 지점에 대해 유니온 - 파인드로 그룹이 같은지 확인한다.<br/>
같은 경우 해당 간선을 제거해도 된다.<br/>
반면 다른 경우면 해당 간선을 살리고 같은 그룹으로 만든다.<br/>


이렇게 정답을 찾아갔다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/20757
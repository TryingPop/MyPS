# C#

## 난이도 : 플레티넘 1

## 알고리즘 분류
  - 자료 구조
  - 정렬
  - 트리
  - 세그먼트 트리
  - 오프라인 쿼리
  - 최소 공통 조상
  - heavy-light 분할
  - 머지 소트 트리

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Farmer John is planning to build N (1 ≤ N ≤ 10^5) farms that will be connected by N-1 roads, forming a tree (i.e., all farms are reachable from each-other, and there are no cycles). Each farm contains a cow with an integer type T_i between 1 and N inclusive.<br/>
Farmer John's M friends (1 ≤ M ≤ 10^5) often come to visit him. During a visit with friend i, Farmer John will walk with his friend along the unique path of roads from farm A_i to farm B_i (it may be the case that A_i = B_i). Additionally, they can try some milk from any cow along the path they walk. Since most of Farmer John's friends are also farmers, they have very strong preferences regarding milk. Each of his friends will only drink milk from a certain type of cow. Any of Farmer John's friends will only be happy if they can drink their preferred type of milk during their visit.<br/>
Please determine whether each friend will be happy after visiting.<br/>


## 입력
The first line contains two integer N and M.<br/>
The second line contains N space-separated integers T_1, T_2, ..., T_N. The type of the cow in the i-th farm is denoted by T_i.<br/>
The next N-1 lines each contain two distinct integers X and Y (1 ≤ X, Y ≤ N), indicating that there is an edge between farms X and Y.<br/>
The next M lines contain integers A_i, B_i, and C_i. A_i and B_i represent the endpoints of the path walked during friend i's visit, while C_i (1 ≤ C_i ≤ N) indicates the type of cow whose milk the friend enjoys drinking.<br/>


## 출력
Print a binary string of length M. The ith character of the string should be '1' if the ith friend will be happy, or '0' otherwise.<br/>


## 예제 입력
5 5<br/>
1 1 2 1 2<br/>
1 2<br/>
2 3<br/>
2 4<br/>
1 5<br/>
1 4 1<br/>
1 4 2<br/>
1 3 2<br/>
1 3 1<br/>
5 5 1<br/>


## 예제 출력
10110<br/>


## 힌트
In this example, the path from 1 and 4 involves farms 1, 2, and 4. All of these contain cows of type 1, so the first friend will be satisfied while the second one will not.<br/>

## 풀이
사이클이 없는 그래프가 주어진다고 한다. 이는 트리를 뜻한다.<br/>
A에서 B를 방문하면서 중복된 노드를 거치지 않고 노드마다 우유를 마신다.<br/>
그리고 노드마다 우유의 양은 다르기에 트리에서 우유의 양의 누적합을 범위를 다뤄야 한다.<br/>
그래서 HLD 알고리즘을 써서 해결했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/18263
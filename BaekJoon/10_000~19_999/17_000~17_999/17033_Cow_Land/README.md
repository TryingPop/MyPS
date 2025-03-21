# C#

## 난이도 : 플레티넘 1

## 알고리즘 분류
  - 자료 구조
  - 트리
  - 세그먼트 트리
  - 최소 공통 조상
  - heavy-light 분할

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Cow Land is a special amusement park for cows, where they roam around, eat delicious grass, and visit different cow attractions (the roller cowster is particularly popular).<br/>
There are a total of N different attractions (2 ≤ N ≤ 10^5). Certain pairs of attractions are connected by pathways, N-1 in total, in such a way that there is a unique route consisting of various pathways between any two attractions. Each attraction i has an integer enjoyment value e_i, which can change over the course of a day, since some attractions are more appealing in the morning and others later in the afternoon.<br/>
A cow that travels from attraction i to attraction j gets to experience all the attractions on the route from i to j. Curiously, the total enjoyment value of this entire route is given by the bitwise XOR of all the enjoyment values along the route, including those of attractions i and j.<br/>
Please help the cows determine the enjoyment values of the routes they plan to use during their next trip to Cow Land.<br/>


## 입력
The first line of input contains N and a number of queries Q (1 ≤ Q ≤ 10^5). The next line contains e_1 ... e_N (0 ≤ e_i ≤ 10^9). The next N-1 lines each describe a pathway in terms of two integer attraction IDs a and b (both in the range 1 ... N). Finally, the last Q lines each describe either an update to one of the e_i values or a query for the enjoyment of a route. A line of the form "1 i v" indicates that e_i should be updated to value v, and a line of the form "2 i j" is a query for the enjoyment of the route connecting attractions i and j.<br/>
In test data worth at most 50% of points, there will be no changes to the values of the attractions.<br/>


## 출력
For each query of the form "2 i j", print on a single line the enjoyment of the route from i to j.<br/>


## 예제 입력
5 5<br/>
1 2 4 8 16<br/>
1 2<br/>
1 3<br/>
3 4<br/>
3 5<br/>
2 1 5<br/>
1 1 16<br/>
2 3 5<br/>
2 1 5<br/>
2 1 3<br/>


## 예제 출력
21<br/>
20<br/>
4<br/>
20<br/>


## 풀이
N - 1개의 경로가 주어지고, 특정 쌍에서는 고유한 경로가 있다고 한다.<br/>
이는 명소를 노드로, 경로를 간선으로 그래프를 그리면 트리가 됨을 뜻한다.<br/>
그리고 A에서 B노드로 가는 경로에 있는 데이터들을 다뤄야 하기에 HLD 알고리즘을 써서 인덱스를 관리한다.<br/>
그리고 즐거움을 범위로 다룰 누적합 세그먼트 트리를 이용해 풀었다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/17033
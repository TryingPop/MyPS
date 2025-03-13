# C#

## 난이도 : 플레티넘 3

## 알고리즘 분류
  - 트리
  - 최소 공통 조상

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
New Bangkok is a newly built province of Thailand that is floating in the sky. In order for the province to be able to float, each city is supported by a spaceship. Because all spaceships are moving in same direction and velocity, the structure of the province stays still.<br/>
Cities are connected by sky ways. A sky way is a floating road connects two cities of New Bangkok together, so a citizen can commute from a city to another city by sky ways. With well urban planning, it is guaranteed that one can commute from any city to all other cities. Also, from a city to another city, there is only one simple route, i.e., no skyway that is used twice.<br/>
Because it is new, the province changes its capital city rapidly. This province also has a strange rule, this is, a city A must handle tax from a city B if a route from B to the capital city must pass through A. So it could be that a city have to handle taxes of many cities.<br/>
In this problem, we provide that structure of New Bangkok, an initial capital city, and number of queries. For each query, we ask you to either (1) move the capital city of the province to city R. or (2) given a city X, tell us how many cities that X has to handle taxes.<br/>


## 입력
The first line contains an integer T ≤ 10, number of test cases. Then for each test case:<br/>
The first line of the test case contains three integers 1 ≤ N ≤ 100 000, 1 ≤ Q ≤ 50 000, 1 ≤ R ≤ N, denote number of cities, number of queries, and an initial capital city in respective order.<br/>
Each of next N - 1 lines gives an information of a sky way. It contains two integers 1 ≤ A ≤ N and 1 ≤ B ≤ N, A ≠ B, there is a sky way connects A and B.<br/>
Each of next Q lines gives an information about query. It contains two integers 0 ≤ S ≤ 1 and 1 ≤ U ≤ N.<br/>
If S is 0, then it asks you to move the capital city to city U. Otherwise, it asks you to compute number of cities that U needs to handle taxes.<br/>


## 출력
You must print answer of test cases and queries in the order given in the input.<br/>
For test case I, you must start with a line containing "Case #I:" (without double quotes).<br/>
Then, for each query that needs an answer, print the answer in a line.<br/>


## 예제 입력
2<br/>
5 5 1<br/>
1 5<br/>
3 4<br/>
3 5<br/>
2 1<br/>
1 1<br/>
1 2<br/>
0 5<br/>
1 5<br/>
1 3<br/>
1 5 1<br/>
1 1<br/>
1 1<br/>
0 1<br/>
1 1<br/>
1 1<br/>


## 예제 출력
Case #1:<br/>
5<br/>
1<br/>
5<br/>
2<br/>
Case #2:<br/>
1<br/>
1<br/>
1<br/>
1<br/>


## 풀이
S = 1인 쿼리의 값은 해당 노드의 자식의 수가 된다.<br/>
해당 문제에서는 루트가 바뀌는데, 매번 자식 수를 갱신하기에는 쿼리의 수와 노드의 수가 많아 시간초과 날거 같다.<br/>


그래서 초기 그래프 형태에서 루트가 바뀔 때를 확인했다.<br/>
그러니 새로 선언된 루트 노드가 찾는 노드의 서브트리 안에 있지 않는 경우와 있는 경우로 상황이 나뉘었다.<br/>


먼저 새로 선언된 루트 노드가 찾는 노드의 서브트리 안에 있지 않는 경우면,<br/>
기존 자식 수를 그대로 출력하면 된다.<br/>

반면 새로 선언된 루트 노드가 찾는 노드의 서브 트리 안에 있는 경우면,<br/>
새로 선언된 루트 노드와 찾는 노드와 같은 경우면 n을 출력하면 된다.<br/>

이외에 다른 경우는 찾는 노드에서 루트 노드로 가는 경로 A들이 있을 때,<br/>
찾는 노드에서 B 노드로 이동할 때 해당 A에 있는 간선을 적어도 하나라도 지나는 경우 B노드는 빼야 한다.<br/>
이는 전체에서 찾는 노드에서 루트노드로 가는 루트에 있는 자식 노드의 자식 수를 뺀 값이 됨을 알 수 있다.<br/>

이렇게 찾은 규칙을 코드로 구현했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/13896
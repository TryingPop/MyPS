# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 트리
  - 깊이 우선 탐색

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
두 도시 사이에 도로를 만드는 일은 매우 비싸다. 때문에 북쪽나라는 특정 도시를 두 번 이상 지나가지 않고서 임의의 두 도시간을 이동하는 경로가 유일하도록 도로가 설계되어 있다.<br/>
또한 북쪽나라의 모든 도시는 다른 모든 도시로 이동할 수 있다고 한다. 이때, 거리가 가장 먼 두 도시 사이의 거리를 출력하는 것이 당신의 임무이다.<br/>
북쪽나라에는 최대 10,000개의 도시가 있을 수 있고, 도시는 1 부터 숫자로 이름이 매겨져 있다.<br/>

## 입력
입력은 여러줄에 걸쳐 주어진다. 입력의 각 줄은 세 개의 양의 정수로 구성되어있는데, 각각은 차례대로 서로 다른 두 도시의 번호와 두 도시를 연결하는 도로의 길이를 의미한다. 모든 도로는 양방향으로 통행이 가능하다.<br/>

## 출력
가장 거리가 먼 두 도시간의 거리를 나타내는 정수 하나를 출력하면 된다.<br/>

## 예제 입력
5 1 6<br/>
1 4 5<br/>
6 3 9<br/>
2 6 8<br/>
6 1 7<br/>

## 예제 출력
22<br/>

## 풀이
트리의 지름을 찾는 문제다.<br/>
그래프 이론에 의하면 임의의 노드에서 시작해 가장 멀리 있는 노드 A를 찾는다.<br/>
그리고 노드 A에서 시작해 가장 멀리 있는 노드 B를 찾는다.<br/>
A와 B의 거리가 트리의 지름이 된다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/1595
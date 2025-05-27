# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 너비 우선 탐색
  - 깊이 우선 탐색

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
A prison has been built as a labyrinth.<br/>
The labyrinth is composed of huts labelled + or *. If you are in hut +, you can only move to another hut + near your hut. One hut is considered near one another if the two huts have a side in common. If you are in hut *, you can only move to another hut * near your hut.<br/>
The labyrinth can be seen as a rectangle of huts of width W and length L. W and L are integers.<br/>
A hut is identified by its position on the horizontal side and by its position on the vertical side of the labyrinth.<br/>
In this prison, all huts are “entry huts” but there is only one “exit hut”.<br/>
Given the labyrinth, the hut of the prisoner (called entry hut) and the exit hut, your task is to determine if the prisoner can escape.<br/>
In the example of Prison A (labyrinth of length 4 and width 2), the prisoner can escape. In the second example (labyrinth of length 3 and width 2), the prisoner cannot escape.<br/>

||1|2|3|4|
|:---:|:---:|:---:|:---:|:---:|
|2|+|+|*|*|
|1|*|+|+|+|



## 입력
The input will begin with a single integer P on the first line, indicating the number of cases that will follow.<br/>
Each case begins with a single line made of 6 natural numbers with the following format:<br/>
L W A B C D where :<br/>

  - L is the length of the labyrinth and W is the width of the labyrinth
  - A is the length of the entry hut and B is the width of the entry hut of the prisoner
  - C is the length of the exit hut and D is the width of the exit hut

followed by W lines containing L characters. Each character will be + or *.<br/>


## 출력
For each prison, print YES if the prisoner can escape and NO if not.<br/>


## 예제 입력
2<br/>
4 2 1 2 4 1<br/>
++**<br/>
*+++<br/>
3 2 2 2 3 1<br/>
+*+<br/>
*+*<br/>


## 예제 출력
YES<br/>
NO<br/>


## 풀이
문제를 보면 행이 큰거부터 입력됨을 알 수 있다.<br/>
그래서 행을 역순으로 입력받아야 한다.<br/>


시작지점에서 시작해 규칙대로 이동했을 때 끝지점에 방문했다면 방문할 수 있고 아니면 방문할 수 없다.<br/>
그래서 BFS로 이동하며 정답지점에 방문했는지 판별했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/14546
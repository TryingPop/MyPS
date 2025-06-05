# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 자료 구조
  - 해시를 사용한 집합과 맵

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
A class has been divided into groups of three. This division into groups might violate two types of constraints: some students must work together in the same group, and some students must work in separate groups.<br/>
Your job is to determine how many of the constraints are violated.<br/>


## 입력
The first line will contain an integer X with X ≥ 0. The next X lines will each consist of two different names, separated by a single space. These two students must be in the same group.<br/>
The next line will contain an integer Y with Y ≥ 0. The next Y lines will each consist of two different names, separated by a single space. These two students must not be in the same group.<br/>
Among these X + Y lines representing constraints, each possible pair of students appears at most once.<br/>
The next line will contain an integer G with G ≥ 1. The last G lines will each consist of three different names, separated by single spaces. These three students have been placed in the same group.<br/>
Each name will consist of between 1 and 10 uppercase letters. No two students will have the same name and each name appearing in a constraint will appear in exactly one of the G groups.<br/>


## 출력
Output an integer between 0 and X +Y which is the number of constraints that are violated.<br/>


## 예제 입력
3<br/>
A B<br/>
G L<br/>
J K<br/>
2<br/>
D F<br/>
D G<br/>
4<br/>
A C G<br/>
B D F<br/>
E H I<br/>
J K L<br/>


## 예제 출력
3<br/>


## 힌트
The first constraint is that A and B must be in the same group. This is violated.<br/>
The second constraint is that G and L must be in the same group. This is violated.<br/>
The third constraint is that J and K must be in the same group. This is not violated.<br/>
The fourth constraint is that D and F must not be in the same group. This is violated.<br/>
The fifth constraint is that D and G must not be in the same group. This is not violated.<br/>
Of the five constraints, three are violated.<br/>


## 풀이
꼭 팀이 되어야 하는 인원들을 s에 넣는다.<br/>
반면 팀이 되어서는 안되는 인원들을 d에 넣는다.<br/>


이제 그룹을 입력받는데 Dictionary 자료 구조에 key로 이름을 그리고 value로 그룹 번호를 넣는다.<br/>
그룹 번호는 구분 지을 수 있게 해주면 되는데 입력 순서대로 0, 1, 2, 3...을 배정했다.<br/>


이후 s를 조사하는데 s의 두 이름의 그룹 번호가 다른 경우 규칙을 위반한다. 이렇게 위반한 갯수를 누적한다.<br/>
그리고 d를 조사하는데 d의 두 이름의 그룹 번호가 같은 경우 규칙을 위반한다. 이렇게 위반한 갯수를 누적한다.<br/>
그러면 s의 길이 + d의 길이에 위반한 갯수를 찾을 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/24571
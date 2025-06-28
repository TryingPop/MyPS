# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 구현
  - 자료 구조
  - 방향 비순환 그래프
  - 큐

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Farmer John is throwing a party and wants to invite some of his cows to show them how much he cares about his herd. However, he also wants to invite the smallest possible number of cows, remembering all too well the disaster that resulted the last time he invited too many cows to a party.<br/>
Among FJ's cows, there are certain groups of friends that are hard to separate. For any such group (say, of size k), if FJ invites at least k-1 of the cows in the group to the party, then he must invite the final cow as well, thereby including the entire group. Groups can be of any size and may even overlap with each-other, although no two groups contain exactly the same set of members. The sum of all group sizes is at most 250,000.<br/>
Given the groups among FJ's cows, please determine the minimum number of cows FJ can invite to his party, if he decides that he must definitely start by inviting cow #1 (his cows are conveniently numbered 1..N, with N at most 1,000,000).<br/>


## 입력
  - Line 1: Two space-separated integers: N (the number of cows), and G (the number of groups).
  - Lines 2..1+G: Each line describes a group of cows. It starts with an integer giving the size S of the group, followed by the S cows in the group (each an integer in the range 1..N).


## 출력
  - Line 1: The minimum number of cows FJ can invite to his party.


## 예제 입력
10 4<br/>
2 1 3<br/>
2 3 4<br/>
6 1 2 3 4 6 7<br/>
4 4 3 2 1<br/>


## 예제 출력
4<br/>


## 힌트
Input Details<br/>
There are 10 cows and 4 groups. The first group contains cows 1 and 3, and so on.<br/>
Output Details<br/>
In addition to cow #1, FJ must invite cow #3 (due to the first group constraint), cow #4 (due to the second group constraint), and also cow #2 (due to the final group constraint).<br/>


## 풀이
위상정렬 아이디어를 이용해 풀었다.<br/>
해당 i번 참가자가 포함된 그룹을 큐에 넣어 기록한다.<br/>
그리고 i번 참가자가 파티에 초대될 경우 큐에 있는 그룹에 인원을 1명씩 뺀다.<br/>
이제 그룹에 있는 인원이 1명인 경우 해당 그룹의 모든 인원을 조사하며 초대받지 못한 남은 인원 1명을 초대한다.<br/>
이렇게 시뮬레이션 돌려 찾았다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5856
# C#

## 난이도 : 골드 2

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 비트마스킹
  - 비트필드를 이용한 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 256 MB

## 문제
A map maker wants to colour different countries or provinces on his maps using different colours. That is, he wants to colour countries that share a common border with a different colour. He has heard that any map can be coloured with four colours, but even after a lot of puzzling he is unable to colour some of his maps using four colours. Interested in the problem, and hoping that he can publish four-coloured maps, the map maker visits you for help.<br/>
After inspecting his maps you explain to the map maker that not every map can be coloured with four colours. You tell him that when countries are made up of different nonconnected parts (such as Alaska and the rest of the United States), it can be the case that one needs more colours. Also, if the maps contains a quadripoint (a point where four countries touch), it can also be the case that one needs more colours. The same is true when more than four countries meet in one point.<br/>
Disappointed, the map maker asks you whether you cannot help him in quickly deciding how many colours he needs. While you try to explain that this is a quite difficult question, he shows you his maps. After a lot of persuasion by him, and the realisation that all his maps are actually very small, you agree to make a computer program for him. You agree to write a program for his small maps (at most 16 countries) that determines whether his map can be coloured with 1, 2, 3, 4 or more colours. This program should still work if there are non-connected countries, quadripoints, etc.<br/>


## 입력
The input starts with a line containing an integer T, the number of test cases. Then for each test case:<br/>

  - One line with two space-separated integers C and B, indicating the number of countries C (1 ≤ C ≤ 16) and the number of borders B (0 ≤ B ≤ 120).
  - B lines with two space-separated integers i and j (0 ≤ i, j ≤ C − 1 and i ≠ j), indicating that there is a border between country i and country j.


## 출력
For each test case, output a single line containing either an integer K, or the string ’many’. Your program should output the integer K, where K is the minimum number of colours needed to colour the map if this number is 1, 2, 3 or 4. Your program should output ’many’ if this number is larger.<br/>

## 예제 입력
3<br/>
4 0<br/>
4 4<br/>
0 1<br/>
1 2<br/>
2 3<br/>
3 0<br/>
5 10<br/>
0 1<br/>
0 2<br/>
0 3<br/>
0 4<br/>
1 2<br/>
1 3<br/>
1 4<br/>
2 3<br/>
2 4<br/>
3 4<br/>


## 예제 출력
1<br/>
2<br/>
many<br/>


## 풀이
dp[idx] = val를 idx를 현재 선택된 국가들을 비트마스킹 한 것이고, val는 색칠하는데 필요한 노드로 잡는다.<br/>
그러면 idx를 1씩 늘려가면서 선택한 국가에 대해 간선이 존재하는지 확인한다.<br/>


간선이 없다면 모든 국가들은 끊어진 상태이므로 현재 필요한 최소 색의 개수는 1개이다.<br/>
반면 간선이 존재한다면 독립된 노드 상태를 찾는다.<br/>
간선이 존재하지 않는 독립된 노드가 있는 경우 독립된 노드를 제외한 부분에서 이으면서 색을 1번 칠해야 한다.<br/>
이렇게 최소가 되게 이어가면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/11630
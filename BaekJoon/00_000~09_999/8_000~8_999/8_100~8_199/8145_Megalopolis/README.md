# C#

## 난이도 : 플레티넘 3

## 알고리즘 분류
  - 자료 구조
  - 트리
  - 세그먼트 트리
  - 오일러 경로 테크닉
  - heavy-light 분할

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Byteotia has been eventually touched by globalisation, and so has Byteasar the Postman, who once roamed the country lanes amidst sleepy hamlets and who now dashes down the motorways. But it is those strolls in the days of yore that he reminisces about with a touch of tenderness.<br/>
In the olden days n Byteotian villages (numbered from 1 to n) were connected by bidirectional dirt roads in such a way, that one could reach the village number 1 (called Bitburg) from any other village in exactly one way. This unique route passed only through villages with number less or equal to that of the starting village. Furthermore, each road connected exactly two distinct villages without passing through any other village. The roads did not intersect outside the villages, but tunnels and viaducts were not unheard of.<br/>
Time passing by, successive roads were being transformed into motorways. Byteasar remembers distinctly, when each of the country roads so disappeared. Nowadays, there is not a single country lane left in Byteotia - all of them have been replaced with motorways, which connect the villages into Byteotian Megalopolis.<br/>
Byteasar recalls his trips with post to those villages. Each time he was beginning his journey with letters to some distinct village in Bitburg. He asks you to calculate, for each such journey (which took place in a specific moment of time and led from Bitburg to a specified village), how many country roads it led through.<br/>
Write a programme which:<br/>

  - reads from the standard input:
    - descriptions of roads that once connected Byteotian villages,
    - sequence of events: Byteasar's trips and the moments when respective roads were transformed into motorways,
  - for each trip, calculates how many country roads Byteasar has had to walk,
  - writes the outcome to the standard output.


## 입력
In the first line of the standard input there is a single integer n (1 ≤ n ≤ 250,000), denoting the number of villages in Byteotia. The following n-1 lines contain descriptions of the roads, in the form of two integers a, b (1 ≤ a < b ≤ n) separated by a single space, denoting the numbers of villages connected with a road. In the next line there is a single integer m (1 ≤ m ≤ 250,000), denoting the number of trips Byteasar has made. The following n+m-1 lines contain descriptions of the events, in chronological order:<br/>

  - A description of the form "A a b” (for a < b) denotes a country road between villages a and b being transformed into a motorway in that particular moment.
  - A description of the from "W a” denotes Byteasar's trip from Bitburg to village a.


## 출력
Your programme should write out exactly m integers to the standard output, one a line, denoting the number of country roads Byteasar has travelled during his successive trips.<br/>


## 예제 입력
5<br/>
1 2<br/>
1 3<br/>
1 4<br/>
4 5<br/>
4<br/>
W 5<br/>
A 1 4<br/>
W 5<br/>
A 4 5<br/>
W 5<br/>
W 2<br/>
A 1 2<br/>
A 1 3<br/>


## 예제 출력
2<br/>
1<br/>
0<br/>
1<br/>


## 풀이
트리 그래프가 주어진다.<br/>
주어지는 쿼리가 트리에서 A 노드에서 B로 가는 경로의 간선들의 데이터가 변형되거나 찾아야 한다.<br/>
이에 HLD 알고리즘으로 간선들을 관리했다.<br/>
그리고 A노드에서 B노드로 가는 경로들의 값이 변형되기에 느리게 변형되는 세그먼트 트리 자료 구조를 써야 한다.<br/>
그런데 변형되는 값이 같고 1번 변형이 이뤄지면 뒤에 변형은 무시해도 되기에 중간에 모두 변형되었다면 끊는식으로 자식까지 갈 필요가 없는 성질을 이용하면 세그먼트 트리로 해결가능하다.<br/>
이렇게 쿼리를 진행하며 풀었다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/8145
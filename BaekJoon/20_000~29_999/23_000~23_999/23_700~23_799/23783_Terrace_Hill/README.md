# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 자료 구조
  - 스택

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
All explored mountain terraces in Girotti hills in Charitum Montes on the southern Martian hemisphere have a peculiar feature — their sizes are approximately equal, and they all lie on a hypothetical straight line.<br/>
The flat surface of the terraces is ideal for future housing development. Unusual configuration of the terraces allows for a daring engineering project which will connect some terraces by bridges.<br/>
Due to relative geological instability in the surrounding region, the surface of any two terraces connected by a bridge has to be in the same height. Obviously, a bridge between two terraces can be built only when the height of all terraces between the given two is less than the height of the two terraces to be connected.<br/>
The engineers of the project want to know the maximum total length of all bridges that can be built. To simplify the introductory calculations, the following assumptions are made. The distance between two neighbour terraces is negligibly small, it is considered to be zero in all cases. The width of a terrace is considered to be one length unit.<br/>


## 입력
The first line contains an integer N (1 ≤ N ≤ 3 · 10^5) the number of the terraces. The second line contains N integers a1, a2, . . . , aN (1 ≤ ai ≤ 106) where ai is the height of i-th terrace. The heights are given in the order of terraces on the (hypothetical) line.<br/>


## 출력
Print one integer – the maximum possible total length of all bridges.<br/>


## 예제 입력
5<br/>
1 2 3 3 1<br/>


## 예제 출력
0<br/>


## 풀이
뒤에 자신과 같은 값이 나올 때 중앙에 자신 보다 낮은 값만 존재할 때 두 거리만큼 다리를 지을 수 있다.<br/>
단순히 O(N^2)으로 접근하면 N이 10^5이상 오므로 시간초과 난다.<br/>
스택을 쓰면 O(N)에 풀 수 있다.<br/>



스택에 자기보다 이전에 큰 값들만 남겨놓는다.<br/>
만약 자기보다 작은게 있으면 스택에서 뺀다.<br/>
해당 높이들은 현재 빌딩 때문에 다리를 놓을 수 없기 때문이다.<br/>
이렇게 스택에서 빼는데 자신과 같은 거리가 나온다면 다리를 놓고 거리를 계산해 누적한다.<br/>
이후 자신보다 높은 것들만 스택에 남겨뒀다면 현재 값을 스택에 저장한다.<br/>
이렇게 진행해가면서 나온 누적된 다리의 합이 정답이 된다.<br/>


주의할 건, N이 30만까지 되므로 최대 경우 29만 9999 + 29만 9997 + ... + 1로 int 범위를 초과할 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/23783
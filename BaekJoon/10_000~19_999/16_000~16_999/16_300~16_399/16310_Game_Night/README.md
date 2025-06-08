# C#

## 난이도 : 골드 2

## 알고리즘 분류
  - 브루트포스 알고리즘
  - 누적 합
  - 슬라이딩 윈도우

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
It is finally Bobby’s birthday, and all of his Acquaintances, Buddies and Colleagues have gathered for a board game night. They are going to play a board game which is played in up to three big teams. Bobby decided to split his guests into how well he knows them: the Acquaintances on team A, the Buddies on team B, and the Colleagues on team C.<br/>
While Bobby was busy explaining the rules to everyone, all his guests already took seats around his large, circular living room table. However, for the game it is crucial that all people sitting on a team are sitting next to each other. Otherwise, members of other teams could easily eavesdrop on their planning, ruining the game. So some people may need to change seats to avoid this from happening.<br/>
Bobby wants to start playing the game as soon as possible, so he wants people to switch seats as efficiently as possible. Given the current arrangement around the circular table, can you figure out the minimal number of people that must switch seats so that the teams are lined up correctly?<br/>

## 입력
  - The first line of the input contains the integer n, where 1 ≤ n ≤ 105 is the number of players (as well as seats).
  - The second line contains a string of length n, consisting only of the characters in ABC. This indicates the teams of the people sitting around the table in order.

## 출력
Print a single integer: the minimal number of people you have to ask to move seats to make sure the teams sit together.<br/>

## 예제 입력
9<br/>
ABABCBCAC<br/>

## 예제 출력
3<br/>

## 풀이
결과적으로 그룹별로 앉히는데 이동하는 사람의 최솟값을 찾아야 한다.<br/>
잘못 앉은 사람이 1명인 경우는 존재할 수 없다.<br/>
잘못 앉은 사람이 2명인 경우 a <-> b 바꿔서 이동시킨다.<br/>
잘못 앉은 사람이 3명인 경우는 순열 사이클 처럼 a -> b -> c 순이나 a -> c -> b로 자리를 바꾸면 3번에 이동된다.<br/>
4명 이상인 경우는 순열 사이클처럼 이동하던지 2명 2명으로 쪼개지는 경우 뿐이다.<br/>
이렇게 이동하는 사람은 그룹별로 앉혔을 때 자기 자리에 앉지 않은 사람과 같다.<br/>


제대로 앉은 경우 A팀을 앞에 두면 ABC 혹은 ACB 일 것이다.<br/>
ABC 방법을 ACB에 맞춰서 적용하면되니 ABC만 알아본다.<br/>
먼저 A, B, C가 각각 몇명인지 알아야 하니 세어준다.<br/>
A를 A 인원만큼 나열하고 B를 B만큼 나열하고, C를 C만큼 나열한 뒤 기존 문자열과 다른 것의 갯수를 비교하면 된다.<br/>
그리고 개수를 기록한다.<br/>
원형이므로 이후 A의 시작지점을 1칸 오른쪽(인덱스 + 1)으로 이동시킨다.<br/>
A의 시작지점은 C로 바뀌고, B의 시작지점은 A로 바뀌고, C의 시작지점은 B로 바뀐다.<br/>
즉, 슬라이딩 윈도우를 적용해 잘못 앉은 사람은 O(1)에 찾을 수 있다.<br/>
이렇게 오른쪽으로 계속 이동시켜 이미 확인한 경우가 나올때까지 잘못 앉은 사람의 최소값을 찾는다.<br/>
그러면 ABC로 앉는 경우를 모두 조사한 것이다.(브루트포스)<br/>
ACB 역시 똑같이 확인하고, 둘의 최솟값을 제출하니 정답이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/16310
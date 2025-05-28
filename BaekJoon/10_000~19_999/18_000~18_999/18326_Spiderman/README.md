# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 수학

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Little Ivan likes to play Yamb and read Marvel superhero comics. His favorite superhero is spider-man, a friendly neighbourhood teenager named Peter Parker who got his superpowers via a radioactive spider bite. Ivan fantasizes that one day he will be able to jump from one skyscraper to another, just like spider-man does in the comics. During one such fantasy, he fell asleep.<br/>
In his dream he was no longer named Ivan, his name was Peter Parkour and, you guessed it, he was able to use his parkour1 skills to jump between skyscrapers. He quickly realized that there are exactly N skyscrapers in his surroundings and he somehow knew that i-th of those skyscrapers is hi meters tall. He knows that he is able to jump from the i-th skyscraper to the j-th skyscraper if the remainder when dividing hi with hj is equal to K. Help Ivan determine, for every skyscraper, the number of other skyscrapers he can jump to.<br/>
1Internet sensation of 2004., it was in the Bond films, the goal is to get from point A to point B as creatively as possible.<br/>


## 입력
The first line contains two integers N (1 ≤ N ≤ 300 000) and K (0 ≤ K < 10^6) from the task description.<br/>
The next line contains N integers hi (1 ≤ hi ≤ 10^6) from the task description.<br/>


## 출력
In a single line you should output N space-separated integers such that the i-th of those integers represents the number of different skyscrapers on which Peter Parkour can jump on if he jumps from the i-th skyscraper.<br/>


## 예제 입력
5 1<br/>
1 3 5 7 2<br/>


## 예제 출력
4 1 1 2 0<br/>


## 힌트
Clarification of the third example:<br/>

  - From the first skyscraper of height 1 Peter can jump on any other skyscraper.
  - From the second skyscraper of height 3 Peter can jump only on a skyscraper of height 2.
  - From the third skyscraper of height 5 Peter can jump only on a skyscraper of height 2.
  - From the fourth skyscraper of height 7 Peter can jump on skyscrapers of heights 2 and 3.
  - From the fifth skyscraper of height 2 Peter cannot jump on any other skyscraper.


## 풀이
각 i번째가 j번째 빌딩으로 이동할 수 있다는 것은 hi = a * hj + k인 a가 존재하는 것이다.<br/>
여기서 hj > k여야 한다.<br/>


각 i에 대해 모든 j를 조사하면 N^2으로 시간초과 난다.<br/>


반면 배수에 초점을 맞추면 N log M으로 해결이 가능하다.<br/>
여기서 M은 수의 범위이다.<br/>


높이 j에 대해 a = 0, 1, ... └100만 / j┘에 대해 y = a * j + k인 y에 높이 j인 건물의 갯수를 누적해주면 된다.<br/>
이는 높이 j인 건물에서 높이 y인 건물로 이동할 수 있는 것과 같으므로 성립함을 알 수 있다.<br/> 
여기서 └x┘는 x를 넘지 않는 가장 큰 정수를 뜻한다.<br/>


해당 방법으로 찾는 경우 k = 0인 경우 자기자신이 자기자신으로 이동할 수 있는 경우도 세기에 이 경우는 1씩 빼줘야 한다.<br/>
그리고 k < j임은 문제 조건이다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/18326
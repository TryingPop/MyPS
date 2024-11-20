# C#

## 난이도 : 브론즈 4

## 알고리즘 분류
  - 수학
  - 사칙연산

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Dota 2 is a multiplayer online battle arena (MOBA) video game developed and published by Valve. Dota 2 is played in matches between two teams of five players, with each team occupying and defending their own separate base on the map. Each of the ten players independently controls a powerful character, known as a 'hero', who all have unique abilities and differing styles of play. During a match players collect experience points and items for their heroes to successfully defeat the opposing team's heroes in player versus player combat. A team wins by being the first to destroy the other team's 'Ancient', a large structure located within their base.<br/>
The International is an annual esports world championship tournament for the video game Dota 2, hosted and produced by the game's developer, Valve. The tournament consists of 18 teams; 12 based on final results from the Dota Pro Circuit and six more from winning regional playoffs from North America, South America, Southeast Asia, China, Eastern Europe, and Western Europe regions.<br/>
In Year 3021, The International is held in Guilin, China. Once again, just like 1000 years ago, Team LGD from China will compete against Team Spirit from Russia. But as the championship developing, the rule is that whoever wins the best of n (n is an odd positive integer) games will win the champion, so a team should win at least (n+1) / 2 games. (In 2021, n equals to only 5 and Team Spirit won by 3:2).<br/>
Before the game starts, teams can choose to ban specific heroes from being used by the opponent team. Among these 1000 years, everyone knows that Team Spirit is very good at using a hero called Magnus, which once helped them defeat Team LGD in 2021.<br/>
Although everyone thinks Team LGD will choose to ban Magnus from the beginning, team LGD thinks differently. Somehow they think that they are strong enough to beat the opponent's Magnus and they will only start to ban Magnus in the x-th game if there is one.<br/>
To simplify the problem, if team LGD choose to ban Magnus, they will certainly win the game. Otherwise, they may have a 50\% possibility to win the game.<br/>
As one of Team LGD's fans, JB wants to know what's the minimum number of n that team LGD can win the champion in the worst case.<br/>


## 입력
The first line contains an integer T (1 ≤ T ≤ 10^5), indicating the number of test cases.<br/>
In the next following T lines, each line contains an integer x (1 ≤ x ≤ 2 x 10^9), indicating that Team LGD will start to ban Magnus in the x-th game.<br/>


## 출력
For each test case, please output an integer in one line, indicating the minimum total number of games to let Team LGD win.<br/>


## 예제 입력
2<br/>
1<br/>
3<br/>

## 예제 출력
1<br/>
5<br/>


## 노트
Ignoring everyone's strongest wish, there exists x>1 in the test data, which means Team LGD won't always choose to ban Magnus from the beginning.<br/>


## 풀이
문제를 해석하면 입력된 값을 n이라할 때, n x 2 - 1을 반환하면 됨을 알 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/31134
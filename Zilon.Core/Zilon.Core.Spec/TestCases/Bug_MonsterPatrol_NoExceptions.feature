﻿Feature: Bug_MonsterPatrol_NoExceptions
	Чтобы было интереснее
	Как игроку
	Мне нужно, чтобы не было ошибок, когда монстры патрулируют комнаты.

@monsters @dev0 @bug
Scenario: Монстры выполняют логику патрулирования в фиксированной комнате с параллельными маршрутами.
	Given Одна большая комната с несколькими стенами
	And Два монстра с параллельными маршрутами
	When Обновляем состояние однокомнатного сектора 100 раз
	Then Не было выброшено исключений
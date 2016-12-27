OperatDo v1.0-beta, 27 grudnia 2016
---
Importuj zeskanowane operaty do bazy danych Ośrodka

# Pomoc

1. Wczytać zeskanowane operaty z dysku
2. Odczytać operaty z bazy danych Ośrodka
2.1 Wybrać operaty, które znaleziono w bazie danych Ośrodka
2.2 Zapisać raport z operatami, których nie znaleziono
3. Odczytać dokumenty operatów z bazy danych Ośrodka
3.1 Wybrać operaty, które nie posiadają żadnych dokumentów w bazie danych Ośrodka
3.2 Zapisać raport z operatami, które posiadają dokumenty
4. Zapisać operaty w bazie danych Ośrodka

> OperatDo.exe db id[:asIdZasobu or :asUid]

[OperatDo_id.log]
[OperatDo_id_insert.sql]
[OperatDo_id_delete.sql]

> OperatDoQuery.exe db id

[OperatDo_id_query.log]

# Historia

Do zrobienia

* [x] testy rozdzielczości 200, 300, 400, 600
* [ ] testy treści pliku
* [ ] testy numeracji dokumentów
* [ ] testy nazwy i rodzaju dokumentu
* [ ] wybór użytkownika przypisanego do importu
* [ ] raporty importu w postacji plików tekstowych
* [ ] skrypty sql do zatwierdzania i wycofania zmian
* [ ] aplikacja okienkowa do wsadowego importu operatów
* [ ] symulacja importu operatów

2016-12-27 v1.0-beta

* nowość: import operatu do bazy danych Ośrodka
* nowość: wczytywanie zeskanowanych operatów z dysku
* nowość: odczytywanie operatów z bazy danych Ośrodka
* nowość: odczytywanie dokumentów operatu z bazy danych Ośrodka

2016-12-16 v1.0-alfa

* propozycja projektu

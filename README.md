# WeatherForecast
Прогноз погоды для выбранного города
1.Получение данных о текущей погоде в заданном городе.
Выводит следующую информацию о погоде:
- Температура;
- Описание;
- Скорость ветра;
SQL:


**1. Вывести менеджеров у которых имеется номер телефона** 
SELECT * from Managers WHERE Phone IS NOT NULL


**2. Вывести кол-во продаж за 20 июня 2021**
SELECT COUNT(*) FROM Sells WHERE Date='2021-06-20'


**3. Вывести среднюю сумму продажи с товаром 'Фанера';**
SELECT AVG(Sum) AS Average_Sum 


FROM Sells as s


JOIN Products as p


ON s.ID_Product=p.ID


WHERE p.Name='Фанера'


**4. Вывести фамилии менеджеров и общую сумму продаж для
каждого с товаром 'ОСБ'**
select m.Fio,SUM(Sum) as 'Sum'
from Sells s
left join Managers m
ON m.ID=s.ID_Manager
left join Products p
on p.ID=s.ID_Product
where p.Name='ОСБ'
group by ID_Manager,m.Fio


**5. Вывести менеджера и товар, который продали 22 августа 2021**
SELECT p.Name AS Product_name,
m.Fio as Manager_name
FROM Sells as s
JOIN Products as p
ON s.ID_Product=p.ID
JOIN Managers as m
ON s.ID_Manager=m.ID
WHERE s.Date='2021-08-22'


**6. Вывести все товары, у которых в названии имеется 'Фанера' и
цена не ниже 1750**
select * 
from Products p
where p.Name like '%Фанера%'
and p.Cost>1750


**7. Вывести историю продаж товаров, группируя по месяцу продажи
и наименованию товара**
select p.Name,MONTH(s.Date)
from Sells s
join Products p
ON p.ID=s.ID_Product
group by MONTH(s.Date),p.Name


**8. Вывести количество повторяющихся значений и сами значения**
из таблицы 'Товары', где количество повторений больше 1.
select Name,count(*)
from Products
group by Name
having count(*)>1

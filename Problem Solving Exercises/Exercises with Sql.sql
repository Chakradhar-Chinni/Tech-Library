#1 https://www.hackerrank.com/challenges/revising-aggregations-the-count-function/problem?isFullScreen=true
  Q: Query a count of the number of cities in CITY having a Population larger than 1 lakh
  Notes: use COUNT() function to retrieve the count 
  SELECT COUNT(*) FROM CITY WHERE Population>100000

#2 https://www.hackerrank.com/challenges/revising-aggregations-sum/problem?isFullScreen=true
  Q:Query the total population of all cities in CITY where the District is California.
  Notes: Use SUM() function to calculate total sum of numeric column
  SELECT SUM(Population) from CITY WHERE DISTRICT='California';

#3 https://www.hackerrank.com/challenges/revising-the-select-query-2/problem?isFullScreen=true
  Q: Query the NAME field for all American cities in the CITY table with populations larger than 120000. The CountryCode for America is USA.
  Notes: Using AND Clause
  SELECT NAME FROM CITY WHERE COUNTRYCODE='USA' AND POPULATION>120000

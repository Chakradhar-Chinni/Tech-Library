#1 https://www.hackerrank.com/challenges/revising-aggregations-the-count-function/problem?isFullScreen=true
  Q: Query a count of the number of cities in CITY having a Population larger than 1 lakh
  Notes: use COUNT() function to retrieve the count 
  SELECT COUNT(*) FROM CITY WHERE Population>100000

#2 https://www.hackerrank.com/challenges/revising-aggregations-sum/problem?isFullScreen=true
  Q:Query the total population of all cities in CITY where the District is California.
  Notes: Use SUM() function to calculate total sum of numeric column
  SELECT SUM(Population) from CITY WHERE DISTRICT='California';

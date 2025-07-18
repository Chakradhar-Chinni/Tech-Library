
//Every line of JavaScript code must be in script tag

alert("Welcome!! Press ok ");  //alert on the browser
document.write("<h1>Javascript Github</h1>"); //printing on the browser
var a = 10 // variable declaration

/* 
Comment blocks for para-graphs
*/


    

    
<<h2>> Switch case in Java Script
var num = 4;
switch(num)
{
    case 1:
        document.write("1");
        break;      
    case 2:
        document.write("2"); 
        break;  
    case 3:
        document.write("3");                                      
        break;
    case 4:
        document.write("4");
        break;
    case 5:
        document.write("5");
        break;
    default:
        document.write("Enter appropriate value");
}






<<h2>>Functions 
Note: -> After a function is created the function
         needs to be called to execute the specific function
//Type 1:
function add()
{
    var total = 10+20+30+40;
    return total; // returns the result to line of function call
}
var r = add(); // add() function is called
document.write("Result is : "+r);

//Type 2:
function add(a,b,c,d) // accepting values from function call
{
    var total = a+b+c+d;
    document.write("Sum is : "+total);
}

add(1,5,8,9); // calling add() function [passing values to function]

//Exp (inner functions)
function Alpha()
{   
    A();
    function A()
    {
        document.write("From A");
    }
    document.write("From Alphabets");     
}
Alpha();







<<h2>> understanding NaN, undefined
1. f2 function gets called because js passes undefined by default - look at output
2. NaN - Not a Number. When a arithmetic operation is tried to perform on a non-number this error is thrown
3. use === undefined to check if value is available
    

function f1(param1,param2)
{
    console.log("Function f1 called");
}
function f2(param1,param2)
{
    if (param1 === undefined && param2 === undefined)
    {
        console.log("values are not provided");
        return;
    }
    
    console.log("Function f2 called",param1,param2,param1+param2);
}
function admin()
{
    f1(10,20);
    f2(20);
}

admin();

Output:
Function f1 called
Function f2 called 20 undefined NaN



End of Functions

    




    
<<h2>>Array
var alpha = ["A","B","C","D"]; // array
alpha.push(10);        //adding new element to array
alpha.push("New Car"); //adding new element to array
document.write(alpha); //printing array






<<h2>> Objects
//Type 1
var cars =  //creating object cars
{
    car_brand : "Bugati",
    car_model : "Class A",
    car_price : 35000,

   pilot : function()   // initializing func in object
    {
       document.write("This is about Bugati");
     }
}

document.write(cars.car_brand);
document.write(cars.car_model);
document.write(cars.car_price);
cars.pilot();
cars.fuel = "Diesel"; //adding a new property to object
//document.write(cars.fuel);
delete cars.car_price; // deleting a property
*/





    
<<h2>>DOM Manipulations
<html>
     <head>
         <script type="text/javascript">
        function B()
        {
            document.getElementById("A").innerHTML="Dynamic Text";
        }           
        </script>            
     </head>
     <body bgcolor = "skyblue">
                 <h1 id ="A">Static Text</h1>
                 <button onclick="B()">Tap Here</button>
     </body>
</html>


    
<<h2>>Taking input from text-box

<html>
    <head>
        <script type="text/javascript">
        function B()
        {
            var str = document.getElementById("text1").value;
            alert(str);
        }
        </script>
    </head>
    <body bgcolor = "Red">
        <input id = "text1" placeholder = "username"><br>
        <input  type="password" id = "text2" placeholder = "password"><br>
        <button onclick ="B()" id = "q" ">Submit</button>
    </body>
</html>





    
<<h2>> Form Validations  part-1
<html>
    <head>
        <script type="text/javascript">
        <title>Form Validations Part-1</title>
        function validate()     
        {
            var username = document.getElementById("uname");      
            var password = document.getElementById("pass");       
            
            if(username.value.trim() =="" || password.value.trim() =="") //trim() removes WhiteSpace from both sides of a string
            {
               alert("Provide the values");
               return false;
            }
            else
            {
                true;
            }
        }
        </script>
    </head>

    <body bgcolor="Red">
    <form onsubmit = "return validate();"action = "message.html">
        <input id = "uname" placeholder="username" type="text"/><br><br>
        <input id = "pass" placeholder="password" type="password"/><br><br>
        <button type = "submit">Submit</button>
    </form>
    </body>
</html>
*/
If validate function returns false then it stays on same page


            
            
<<h2>>Form Validations Working Part-2
<html>
    <head>
        <title>Form Validations</title>
        <script type = "text/javascript">
        
            function validate()
            {
                var uname = document.getElementById("uname");
                var password = document.getElementById("pass");

                if(uname.value.trim()=="")  //trim for ident of blank spaces
                {
                    alert("Blank Username");
                    uname.style.border = "solid 2px red"
                    return false;  //prevents onsubmit if false
                    
                }
                else if(pass.value.trim()=="")
                {
                    alert("Blank Password");
                    pass.style.border = "solid 2px red"
                    return false;  //prevents onsubmit if false
                }
                else if(pass.value.trim().length<5)
                {
                    alert("Provide proper Password")
                    pass.style.border = "solid 2px red"
                    return false;
                }
            }
        </script>
    </head>
    <body>
        <form onsubmit="return validate();" action="message.html">
            <input id="uname" placeholder = "Username" type="text"/>
            <br><br>
            <input id = "pass" placeholder = "Password" type="password"/>
            <br><br>
            <button type = "submit">Submit</button> 
        </form>
    </body>
</html>
*/

/*onmouseover & onmouseout events
<html>
    <head>
        <script type="text/javascript">
            function over()
            {
                document.getElementById('img1').src="img2.jpg";
            }
            function out()
            {
                document.getElementById('img1').src="img1.jpg";
            }
9

        </script>
    </head>
    <body bgcolor="pink">
        <img id = "img1" src ="img1.jpg" onmouseover= "over()" onmouseout="out()"  width =288>

    </body>
</html>
*/

/*onmouseup & onmousedown
<!DOCTYPE html>
<html>
<head> 

<script type="text/javascript">
function down() {
  document.getElementById("img1").src = "img2.jpg";
}

function up() {
  document.getElementById("img1").src = "img3.jpg";
}
</script>
</head>
<body bgcolor='pink'>
<img id="img1" src="img1.jpg" onmousedown="down()" onmouseup="up()" width = 500>

</body>
</html>
*/
/*onmouseenter & onmouseleave

<html>
<head> 

<script type="text/javascript">

function enter()
{
   document.getElementById("img1").src = "img2.jpg";
}
function leave()
{
    document.getElementById("img1").src = "img3.jpg";
}
</script>
</head>
<body bgcolor='pink'>
<img id="img1" src="img1.jpg" onmouseenter="enter()" onmouseleave="leave()" width = 500>

</body>
</html>
*/




            

            
<<h2>> Arrow Functions
1. introduced in ES6
2. useful for inline function calls. 
3. using arrow functions, traditional function syntax can be minimized

function fun1()
{
    alert("Heyyy");
}

function fun2(num1,num2)
{
    alert(num1+num2);
}

converting above traditional function synta to arrow function

fun1 = () => alert("Heyyy");

fun2 = (num1,num2) => alert(num1+num2);


const admin = () => {
    var1 = 10;
    var2 = 20;
    console.log("Sum =", var1+var2);
}





    


<<h2>> SetTimeout

1. The setTimeout() method calls a function after a number of milliseconds.
2. 5000 milliseconds = 5 seconds
3. 

function fun1()
{
    const myTimer = setTimeout(fun2,5000);
    function fun2()
    {
        console.log("HEYyy after 5 seconds");
    }
}

fun1();
    
-- arrow function equivalent of same code
const fun1 = () => { 
    const myTimer = setTimeout(()=>{
        console.log("hey after 5 seconds");
    },5000);
 }--
    


<<h2>> Clear Timeout


    
<<h2>> SetInterval


    
<<h2>> Clear Interval






<<h2>> const, var, let



<<h2>> Call backs
"I will call back later!"

1. A callback is a function passed as an argument to another function
2. This technique allows a function to call another function
3. why not directly use another function inplace of callback?
    - In Example1, userName("Hulk",userAddress); the function 'userAddress' is passed as an argument which is not poosible without callback


Example1:
function userName(uname,callback)
{
    console.log("User name",uname);
    callback();
}

function userAddress()
{
    console.log("user address")
}

function userOrders()
{
    console.log("User orders");
}

function admin()
{
    userName("Hulk",userAddress);
    userOrders();
}

Output:
User name is:  Hulk
user address
User orders



Example 2: async approach to callback - executing callback after timeout
function userName(uname,callback)
{
    setTimeout(()=>{
        console.log("after 5 seconds- User name is: ",uname);
        callback();
    },5000);
}

function userAddress()
{
    console.log("user address")
}

function userOrders()
{
    console.log("User orders");
}

function admin()
{
    userName("Hulk",userAddress);
    userOrders();
}


output:
User orders
after 5 seconds- User name is:  Hulk
user address




Example 3: calling callback with params
function myDisplayer(something) {
  document.getElementById("demo").innerHTML = something;
}

function myCalculator(num1, num2, myCallback) {
  let sum = num1 + num2;
  myCallback(sum);
}

myCalculator(5, 5, myDisplayer);

output:
10


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
/*
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

|||||||||||||||||||||||||||||||||||||||||||
|||||||||||||||||||||||||||||||||||||||||||
JQuery

regular expressions->
pattern matching
validations
search & replace

regular expressions are treated as objects
/literal way 

//JQuery selectors - element selector
<html>
    <head>
        <title>JQuery</title>
    </head>
    <body bgcolor="pink">
        <h1>Text</h1>
        <button onclick="fn1()">Button to Submit</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        function fn1()
        {
            $("h1").fadeToggle();
        }
    </script>
</html>
--------------------
//JQuery selectors using id tag (#)
<html>
    <head>
        <title>JQuery</title>
    </head>
    <body bgcolor="pink">
        <h1 id="oneh1">Text</h1>
        <h1 id="oneh1">Text</h1>
        <h1>Text</h1><h1>Text</h1>
        <h1>Text</h1>
        <button onclick="fn1()">Button to Submit</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        function fn1()
        {
            $("#oneh1").fadeToggle();
        }
    </script>
</html>
--------------------
//JQuert selector using class name (.)
<html>
    <head>
        <title>JQuery</title>
    </head>
    <body bgcolor="pink">
        <h1 class="one" id="oneh1">Text</h1>
        <h1 class="one">Text</h1>
        <h1 class="one">Text</h1><h1>Text</h1>     
        <h1>Text</h1>
        <button onclick="fn1()">Button to Submit</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        function fn1()
        {
            $(".one").fadeToggle();
        }
    </script>
</html>
//Multiple actions using selectors
<html>
    <head>
        <title>JQuery</title>

    </head>

    <body>
        <h2>JQuery Multiple Selectors</h2>
        <div id="firstdiv" class="mydivs">
            <p>Text in the Paragraph 1</p>
            <p>Text in the Paragraph 1</p>
        </div>
        <div id="seconddiv" class="mydivs">
            <p>Text in the Paragraph 2 </p>
            <p>Text in the Paragraph 2</p>
        </div>
        <div id="thirddiv" class="mydivs">
            <p>Text in the Paragraph 3</p>
            <p>Text in the Paragraph 3</p>
        </div>
        <div id="fourthdiv" class="mydivs">
            <p>Text in the Paragraph 4</p>
            <p>Text in the Paragraph 4</p>
            <ul>
                <li>1</li>
                <li>2</li>
                <li>3</li>
                <li>4</li>    
            </ul>
        </div>
        <button onclick="fn1()">fade</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        function fn1()
        {
          // $("#fourthdiv").fadeToggle();  //fades id with fourthdiv
          // $(".mydivs").fadeToggle();     //fades calls with mydivs
          // $("#firstdiv,#seconddiv").fadeToggle(); //fades first & second div
          // $("p").fadeToggle();        //fades all p elements
          // $("li").fadeToggle();     // fades all li elements
          // $("#fourthdiv p ").fadeToggle(); //fades p elements in fourthdiv
          //   $("#firstdiv,#fourthdiv ul").fadeToggle();  //fades firstdiv & ul elements in fourthdiv

        }
    </script>
</html>
------------------------
JQuery events with ready function
<html>
    <head>
        <title>JQuery Events</title>
    </head>

    <body>
        <div class="one" id="1">
            <p>This is one-1 </p>
            <p>This is one-1 </p>
        </div>
        <div class="one" id="2">
            <p>This is one-2 </p>
            <p>This is one-2 </p>
        </div>
        <div class="two" id="1">
            <p>This is two-1 </p>
            <p>This is two-1 </p>
        </div>
        <div class="two" id="2">
            <p>This is two-2 </p>
            <p>This is two-2 </p>
        </div>
        <button>Click me</button>   //only creating a button
        <script src="jquery.js"></script>
        <script>
           $("document").ready(function(){  //ready method donot enable jquery events until entire page is loaded

            $("button").click(fn1)    // accessing above button using element selector and calling fn1()
            
            function fn1()
              {
                $(".one").fadeToggle();
              }

           });
        </script>
    </body>
</html>

--JQuery Effects-- hide,show,toggle,fadein,fadeout
<html>
    <head>
        <title>Effects in JQuery</title>
    </head>
    <body>
        <h2 id="h2">
            H2 Line1<br>
            H2 Line2<br>
            H2 Line3<br>
            H2 Line4<br>
        </h2> 
        <button>FadeToggle</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        $("document").ready(function()
        {
            $("button").click(fn1);
            function fn1()
            {
                $("#h2").slideToggle();
            }

        });
    </script>
</html>
-------------
----Callback in JQuery--
<html>
    <head>
        <title>Effects in JQuery</title>
    </head>
    <body>
        <h2 id="h2">
            H2 Line1<br>
            H2 Line2<br>
            H2 Line3<br>
            H2 Line4<br>
        </h2> 
        <button>FadeToggle</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        $("document").ready(function()
        {
            $("button").click(fn1);
            function fn1()
            {
                $("#h2").slideUp(2000,function()
                {
                    alert("Slideup has occured");
                }); 
            }

        });
    </script>
</html>

--------------
----Chaining in JQuery---- 
Chaining is used to occur multiple effects one by one
<html>
    <head>
        <title>Effects in JQuery</title>
    </head>
    <body>
        <h2 id="h2">
            H2 Line1<br>
            H2 Line2<br>
            H2 Line3<br>
            H2 Line4<br>
        </h2> 
        <button>FadeToggle</button>
    </body>
    <script src="jquery.js"></script>
    <script>
        $("document").ready(function()
        {
            $("button").click(fn1);
            function fn1()
            {
                $("#h2").slideUp(2000).slideDown(2000).fadeOut(1000).fadeIn(2000);
            }

        });
    </script>
</html>
----DOM Manipulations---Document Object Model
text(),html(),attr(),css()
--text()--
<html>
    <head>
        <title>Effects in JQuery</title>
    </head>
    <body>
       <div id="one" class="A">
           Text from div of id-one class-A
        </div>
           <button>Click Me</button>    
    </body>
    <script src="jquery.js"></script>
    <script>
        $("document").ready(function()
        {
            $("button").click(fn1);
            function fn1()
            {
                var a = $("#one").text("JQuery");
                //JS- document.getElementById("one");
            }
        });
    </script>
</html>
--html()--

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

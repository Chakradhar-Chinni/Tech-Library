<<h2>> Testing API End Points

Non-Microsoft Ways:
HTTP_REPL: 
  - REPL: Read Eval Print Loop
  - a CLI tool to test API end points and inspect responses
.http files:
  - released in .net 8


<<h2>> Installing HTTP_REPL

https://www.nuget.org/packages/Microsoft.dotnet-httprepl#versions-body-tab

cmd > dotnet tool install --global Microsoft.dotnet-httprepl --version 8.0.0
  - this installs globally

- check globally installed dotnet tools
cmd > dotnet tool list -g
Package Id                     Version      Commands
----------------------------------------------------
microsoft.dotnet-httprepl      8.0.0        httprepl



<<h2>> Testing APi with HTTP_REPL

- see available cmds
cmd > httprepl -h
      /*
      Welcome to HttpRepl 8.0!
      ------------------------
      
      Telemetry
      ---------
      The .NET tools collect usage data in order to help us improve your experience. The data is collected by Microsoft and shared with the community. You can opt-out of telemetry by setting the DOTNET_HTTPREPL_TELEMETRY_OPTOUT environment variable to '1' or 'true' using your favorite shell.
      
      Read more about HttpRepl telemetry: https://aka.ms/httprepl-telemetry
      Read more about .NET CLI Tools telemetry: https://aka.ms/dotnet-cli-telemetry
      
      Usage:
        httprepl [<BASE_ADDRESS>] [options]
      
      Arguments:
        <BASE_ADDRESS> - The initial base address for the REPL.
      
      Options:
        -h|--help - Show help information.
      
      Once the REPL starts, these commands are valid:
      
      Setup Commands:
      Use these commands to configure the tool for your API server
      
      connect        Configures the directory structure and base address of the api server
      set header     Sets or clears a header for all requests. e.g. `set header content-type application/json`
      add query-paramAdds a key and value pair to the query string
      clear query-paramClears the query string of all key and values
      
      HTTP Commands:
      Use these commands to execute requests against your application
      
      GET            get - Issues a GET request
      POST           post - Issues a POST request
      PUT            put - Issues a PUT request
      DELETE         delete - Issues a DELETE request
      PATCH          patch - Issues a PATCH request
      HEAD           head - Issues a HEAD request
      OPTIONS        options - Issues a OPTIONS request
      
      Navigation Commands:
      The REPL allows you to navigate your URL space and focus on specific APIs that you are working on
      
      ls             Show all endpoints for the current path
      cd             Append the given directory to the currently selected path, or move up a path when using `cd ..`
      
      Shell Commands:
      Use these commands to interact with the REPL shell
      
      clear          Removes all text from the shell
      echo [on/off]  Turns request echoing on or off, show the request that was made when using request commands
      exit           Exit the shell
      
      REPL Customization Commands:
      Use these commands to customize the REPL behavior
      
      pref [get/set] Allows viewing or changing preferences, e.g. 'pref set editor.command.default 'C:\\Program Files\\Microsoft VS Code\\Code.exe'`
      run            Runs the script at the given path. A script is a set of commands that can be typed with one command per line
      ui             Displays the Swagger UI page, if available, in the default browser
      
      Use `help <COMMAND>` for more detail on an individual command. e.g. `help get`
      For detailed tool info, see https://aka.ms/http-repl-doc
      */



cmd > httprepl https://localhost:7167/
(Disconnected)> connect https://localhost:7167/
Using a base address of https://localhost:7167/
Unable to find an OpenAPI description
For detailed tool info, see https://aka.ms/http-repl-doc


As different versions are enabled in program.cs, httprepl is unable to find openapi(swagger) specification at default location

cmd changes to base url as https://localhost:7167/ > 
-------------

cmd > https://localhost:7167/swagger/2.0/swagger.json/> connect https://localhost:7167 --openapi https://localhost:7167/swagger/2.0/swagger.json
Checking https://localhost:7167/swagger/2.0/swagger.json... Found
Parsing... Successful

Using a base address of https://localhost:7167/
Using OpenAPI description at https://localhost:7167/swagger/2.0/swagger.json
For detailed tool info, see https://aka.ms/http-repl-doc

--------------------------
https://localhost:7167/> ls
.     []
api   []

https://localhost:7167/> cd api
/api    []

https://localhost:7167/api> ls
.    []
..   []
v2   []

https://localhost:7167/api> cd v2
/api/v2    []

https://localhost:7167/api/v2> ls
.        []
..       []
cities   []

https://localhost:7167/api/v2> cd cities
/api/v2/cities    []

https://localhost:7167/api/v2/cities>

- see all end points
View > Other Windows > EndPointsExplorer




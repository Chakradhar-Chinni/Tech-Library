IIS Hosting

<<h2>>  Quick Reference
1. Install IIS (if not available)
2. Install Hosting Bundle → Restart IIS
3. Create App Pool (No Managed Code)
4. Publish app to folder (D: drive)
5. Add Application under Default Web Site
6. Assign App Pool → Check permissions → Test
7. Use web.config → Enable stdout logging for debugging
8. Verify via browser → Adjust bindings/firewall if necessary

## **1️⃣ Prerequisites**
1. Windows with IIS installed
2. .NET Hosting Bundle 
    - matching your app version (.net x64)
    - IIS can host any tech apps like Java, python, ruby, .net etc... 
    - .Net Hosting Bundle is required to connect IIS to .net app
    - .Net Hosting bundle includes runtime + ANCM/ANCMv2 (AspNetCore Module)
    - restart IIS after installing Hosting Bundle
  
3. Admin privileges for installing/configuring IIS

Note: dotnet --info (shows all installed SDKs, runtimes)
---

## **3️⃣ Application Pool**

### Create a new App Pool:

1. IIS Manager → Server Name → Application Pools → Add Application Pool
2. Settings:

   * Name: `MyDotNetCoreAppPool`
   * .NET CLR: `No Managed Code` (for .NET Core)
   * Managed Pipeline Mode: `Integrated`
3. Start the App Pool

> **Notes:** Built-in pools (`DefaultAppPool`, `.NET v4.5`, etc.) exist automatically for legacy ASP.NET apps.

---

## **4️⃣ Adding the App in IIS**

### Option 1: Add as **Application under Default Web Site** (Recommended for local testing)

1. Right-click **Default Web Site → Add Application**
2. Fill:

   * Alias: `PavilionApp`
   * Physical path: Published folder (`D:\PavilionApp`)
   * Application Pool: `MyDotNetCoreAppPool`
3. Resulting URL:

   ```
   http://localhost/PavilionApp
   ```

### Option 2: Add as **New Site**

1. Right-click **Sites → Add Website**
2. Fill:

   * Site name: `PavilionApp`
   * Physical path: Published folder
   * Port: 8080 (avoid conflict with Default Web Site on 80)
   * Application Pool: `MyDotNetCoreAppPool`
3. Bindings:

   * IP: All Unassigned
   * Port: 8080
   * Hostname: optional, requires hosts file mapping if used
4. Access URL:

   ```
   http://localhost:8080
   ```

> ⚠️ Common issue: Port conflict with Default Web Site → new site won’t start

---

## **5️⃣ Folder Permissions**

* Ensure **IIS_IUSRS** has **Read & Execute** access to app folder
* Required for both new sites and applications

---

## **6️⃣ web.config for .NET Core**

Place in root of published folder:

```xml
<configuration>
  <system.webServer>
    <handlers>
      <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified"/>
    </handlers>
    <aspNetCore processPath="dotnet" arguments="Pavilion.Local.dll" stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout" hostingModel="inprocess"/>
  </system.webServer>
</configuration>
```

* `stdoutLogEnabled="true"` is useful for debugging
* Create `logs` folder if logging is enabled

---

## **7️⃣ Running & Validating the DLL**

1. Navigate to published folder:

```cmd
cd D:\PavilionApp
dotnet Pavilion.Local.dll
```

* Confirms DLL is valid and runtime-compatible
* File name with dot (`Pavilion.Local.dll`) is valid; must match exactly in web.config

---

## **8️⃣ Troubleshooting Common Issues**

| Symptom                                             | Fix                                                                 |
| --------------------------------------------------- | ------------------------------------------------------------------- |
| Site not opening                                    | Ensure App Pool started, correct bindings, and firewall allows port |
| 403 Forbidden                                       | Check IIS_IUSRS folder permissions                                  |
| 500 Internal Server Error                           | Check `stdout` logs in folder or Windows Event Viewer               |
| Port conflict                                       | Use alternative port or stop Default Web Site                       |
| New site fails but app under Default Web Site works | Port binding conflict, firewall, host header, App Pool not assigned |

---

## **9️⃣ IIS Reset**

```cmd
iisreset /restart
```

* Run **Command Prompt as Administrator**
* Required after Hosting Bundle installation or configuration changes

---

## **10️⃣ Root Cause Summary (RCA)**

* Default Web Site works: inherits port 80, proper App Pool, no firewall issue
* New Site fails: often due to:

  * Port conflicts (80 already used)
  * Firewall blocking new port
  * App Pool not assigned / stopped
  * Host header without hosts file mapping

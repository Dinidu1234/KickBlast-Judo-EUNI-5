# FixBuildAndRun – Debug Executable Does Not Exist

If Visual Studio shows a startup debug executable error:

1. **Set Startup Project**
   - Right-click `KickBlastStudentForms` project -> **Set as Startup Project**

2. **Clean and Rebuild**
   - Build -> **Clean Solution**
   - Build -> **Rebuild Solution**

3. **Delete bin/obj manually**
   - Close Visual Studio
   - Delete `KickBlastStudentForms/bin` and `KickBlastStudentForms/obj`
   - Reopen solution and rebuild

4. **Check Build Configuration**
   - Confirm solution is set to **Debug** and **Any CPU**

5. **Check launch profile**
   - Ensure `Properties/launchSettings.json` has only:

```json
{
  "profiles": {
    "KickBlastStudentForms": {
      "commandName": "Project"
    }
  }
}
```

- Do not use `executablePath`
- Do not use a profile targeting an external `.exe`

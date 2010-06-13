@echo off
msbuild /nologo /verbosity:quiet
IF ERRORLEVEL 1 GOTO end
Fx\Victoria.Test.Console\bin\debug\Victoria.Test.Console.exe
:end
@echo on
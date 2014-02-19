@echo off
set PROJECT_ROOT=%cd%\..

set TESTING_FOLDER=%cd%

set TESTS_DLL_FOLDER=%PROJECT_ROOT%\Bin\SwdPageRecorder.Tests

mono --runtime=v4.0.30319 "%TESTS_DLL_FOLDER%\SwdPageRecorder.Tests.exe"

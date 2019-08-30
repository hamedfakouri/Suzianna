@pushd %~dp0
@set profile=%1
@if "%profile%" == "" set profile=Default
dotnet test --filter TestCategory!=NotSpecTest
pause
@popd
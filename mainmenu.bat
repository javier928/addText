@echo off
:MENU
cls
echo =================================================================================================
echo                                    MARKETING-TOOLS-BY-GARCIA
echo =================================================================================================
echo.
echo  1. Run createPPT.exe to create an .ODP file using photos archived in a ZIP file
echo  2. Run createODP.exe to create a presentation with one slide called presentation.odp
echo  3. Run joinODPfiles.exe to join two presentations (.odp files)
echo  4. Run pdf4.exe to create a single A4 PDF file with a centered image     
echo  5. Run createCover.exe to create a PDF file with your NAME and SURNAME (centered)
echo  6. RUN pdfJoiner.exe to join PDF files. Files file1.pdf and file2.pdf must be in C:\dbase
echo  7. RUN addText.exe to add the text #Javier928# to the file background.png. Preview [output.png] 
echo  9. Exit
echo.

set /p choice= Enter your choice (1-9): 

if "%choice%"=="1" goto RUN_ODP
if "%choice%"=="2" goto RUN_PPT
if "%choice%"=="3" goto RUN_JOIN
if "%choice%"=="4" goto RUN_PDF4
if "%choice%"=="5" goto RUN_COVER
if "%choice%"=="6" goto RUN_PDFJOINER
IF "%choice%"=="7" goto RUN_ADDTEXT
if "%choice%"=="9" goto END

echo Invalid option. Try again.
pause
goto MENU

:RUN_ODP
echo Running createPPT.exe...
createPPT.exe
pause
goto MENU

:RUN_PPT
echo Running createODP.exe...
createODP.exe
pause
goto MENU

:RUN_JOIN
echo Loading...
join.bat
pause
goto MENU


:RUN_PDF4
echo Loading...
pdf4.exe
pause
goto MENU

:RUN_COVER
echo Loading...
createCover.exe
pause
goto MENU


:RUN_PDFJOINER
echo Loading...
pdfjoiner.exe
pause
goto MENU
               
:RUN_ADDTEXT
echo Loading
addtext.exe
pause
goto MENU

:END
echo Exiting...
exit

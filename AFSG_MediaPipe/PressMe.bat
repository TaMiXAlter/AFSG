
echo Opening venv...


call .\venv\Scripts\activate.bat
if errorlevel 1 (
    echo Failed to activate virtual environment!
    pause
    exit /b
)

echo Opening Mediapipe
python main.py
pause

call  .\venv\Scripts\deactivate.bat

@echo off
SETLOCAL EnableDelayedExpansion

set BATPATH=%~dp0
"C:\Program Files (x86)\CSR\BlueSuite 2.6.8\BlueFlashCmd.exe" "!BATPATH!\release\spp_uart_bc358_16mhz"
pause

TemperatureAlertSystem
======================

A desktop utility which notify when the cpu temperature exceeds a certain limit, also the time interval which the temperature should be checked is editable

The solution includes a domain program, whith the TemperatureWatcher class and a UI program (a tray icon)

The cpu temperature value is provided by using the OpenHardwareMonitorLib.dll from the [Open Hardware Monitor] (http://openhardwaremonitor.org/) Project.

The domain project as is supports additional implementations for the different sensors provided by the library and multiple event subscrivers

PS: sorry for the graphics and the layout :grin:

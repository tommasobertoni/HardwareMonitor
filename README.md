#HardwareMonitor

##Abstract
Application for displaying hardware values. At this development stage the app covers the following modules:
<br>
#####Temperature:
&nbsp;&nbsp;&nbsp;&nbsp;shows the average cpu temperature, allowing the user to be notified when it exceeds an alert level
<br>
##Implementation
Based on the [OpenHardwareMonitor project](http://openhardwaremonitor.org/), this SOA solution contains a windows service providing the hardware's data and a WinForm consumer application with different modules (ideally one for each hardware available).

I tried to develop the project with low coupling, so new modules can be added with minimum impact on the existing source code.
<br><br><br>
*explicit license coming soon*

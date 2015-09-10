#HardwareMonitor

![Screenshot](https://cloud.githubusercontent.com/assets/8939890/9720902/57457ee2-5593-11e5-8879-2547479d8328.png)
<br/>
##Abstract
Application for displaying hardware sensors values.<br>
At this development stage the app covers the following modules:
<br>
#####- Temperature
* shows the average cpu temperature, allowing the user to be notified when it exceeds an alert level

<br>
##Implementation
Based on the [OpenHardwareMonitor project](http://openhardwaremonitor.org/), this SOA solution contains a windows service providing the hardware's data and a WinForm consumer application with different modules (ideally one for each hardware sensor available).

I tried to develop the project with low coupling, so new modules can be added with minimum impact on the existing source code.
<br><br><br>
*explicit license coming soon*

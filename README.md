# IoT-Project

## Temperature and humidity sensor with adjustable request rate using rotary encoder, programmed in .Net and Microphyton

**Author:** `Emil Clementz` **Course Id** `ec222vw`

## Project overview
This project is about measuring temperature and humidity and displaying information in a intuitive way. also notifications are automatically sent via mail when adjustable threasholds are met to inform the user. 

**Project structure for solution:**
The Microphyton code in production would live on the `Raspberry Pico WH` and comunicate to the system that lives on a server via wifi through the WebApi. The .Net solution dependencies can be seen in the matrix below.
| Simple | Detailed | 
|:--------: |:--------:|
|![image](https://github.com/em-cl/IoT-Project/assets/76754841/9ca6474b-ccc7-4474-a5ac-199224f11259)|![image](https://github.com/em-cl/IoT-Project/assets/76754841/d1a57983-c54e-4f0a-a4fe-9b27e1b5edf9)|


**Nuget packages**:
| Name | Description | version | project |
|:-------- |:--------:| --------:| --------:| 
| Swashbuckle.AspNetCore | api documentation and testing with swagger | 6.4.0 | WebApi |
| Microsoft.AspNetCore.OpenApi | Api | 7.0.0 | WebApi |
| Microsoft.EntityFrameworkCore.Design | shared components for EF Core tools | 7.0.7 | WebApi |
| MailKit     |   Mail   | 3.2.0 | Infrastructure |
| MimeKit     |   Mail   | 3.2.0 | Infrastructure |
| Microsoft.Extensions.Options |   Getting settings from configuration via dependency injection   | 7.0.0 | Infrastructure |
| Microsoft.Extensions.DependencyInjection. Abstractions |  provides abstractions for dependency injections  | 7.0.0 | Infrastructure |
| Microsoft.Extensions.Configuration. Abstractions | Abstractions of key value pair based configuration | 7.0.0 | Infrastructure |
| Microsoft.EntityFrameworkCore.Tools | Working with code first for database | 7.0.5 | Presistence |
| Microsoft.EntityFrameworkCore.SqlServer | SQL server database provider | 7.0.5 | Presistence |
| Microsoft.EntityFrameworkCore.Proxies | for lazy loading from database | 7.0.5 | Presistence |
| Microsoft.EntityFrameworkCore | Object relational mapping and Linq | 7.0.5 | Presistence |
| MediatR | Implements prefab mediator pattern | 12.0.1 | Application, Presentation |
| FluentValidation. DependencyInjectionExtensions | DP inject for FluentValidation | 11.5.2 | Application, Infrastructure |



:::    info
**Small description of technology used**

The dashboard is developed using Blazor server, C#, HTML, CSS, and SVG. The microcontroller utilized is Raspberry Pi Pico WH, programmed with Micro Python. The sensors used include DHT11 for temperature and humidity measurement, as well as the HW-040 Rotary Encoder.

The tech stack is a .NET solution, employing a microservice architecture with multiple projects designed in a clean architecture style.

Why microservices and clean architecture? This approach promotes loose coupling and enhances the maintainability of the distributed system. Additionally, microservices can be easily containerized with Docker and independently scaled in the future if required.

All libraries utilized in the project are free and scalable, suitable for enterprise-level implementation.

For the database, a SQL relational database is employed, built code-first with generated migration scripts. Microsoft SQL Server Management Studio 18 is used as the database editor.

Google SMTP Service is leveraged for sending email notifications, implemented with MailKit and MimeKit using port 587 with TLS. To utilize this feature, a free Gmail account with 2-step verification is required.

For easier deployment on a real server (rather than a laptop), an alternative option such as SendGrid can be considered.

The MediatR library is used for command execution and in-memory publication and subscription of events.
:::

**Code metrics**
![image](https://github.com/em-cl/IoT-Project/assets/76754841/4bed83db-ecaa-425e-a2e7-61087b281680)
As indicated by the low maintainability index for the WebApi, dependency injection requires a bit of magic to work when using multiple projects with a single program.cs

**Database diagram**
The database is very simple with no unnecessary complexity.
it have 2 tables one for logs and one for measurements.
Tracelogs
| TraceId | Message | Type | ComponentName | LoggedDate |
|:-------: |:-------:| :----:| :-------------:| :----------:| 
| Guid | "Started" | "information" | "PicoW" | DateTime |

Measurements
| Id | Time | Temperature | Humidity |
|:-------: |:-------:| ----:| -------------:| 
| Guid | DateTime | 32 | 50 |

## Time approximation

_Some prior knowledge of the .Net stack is adviced!_

Writing a similar project with the information provided in this tutorial.
**Beginers** to **intermediate** level programmers in **25+ hours**
**Advanced** programmers **5-10+ hours**


## Objective

**Why was this project chosen?**
The choice of this tech stack was driven by a lack of experience in microservice architecture and its suitability for IoT. I wanted to test the Mediator, Repository, and Unit of Work design patterns in this new, modern approach to step outside my comfort zone and learn.

**The main objective**
The objective of the digital artifact is to monitor humidity and temperature with adjustable rates and batch sizes, allowing for single requests approximately every 3 seconds or any multiple of the same ratio. This approach aims to avoid unnecessary strain on the Wi-Fi.

**...Learning?**
The knowledge artifact from building a project like this covers many important core concepts for distributed systems and tie them together. 


**What insights will u gain from a project like this?**
How to build scalable modern systems in the .NET stack:
How to use the Raspberry Pi Pico W Microcontroller with MicroPython.
The necessary information and insight about indoor temperature and humidity to optimize your home for optimal living conditions.

## Material
**Microcontroller**
For this project the Microcontroller `Raspberry pie Pico WH` was used. But any microcontroller with wifi programmable in `Micro Phyton` should work. the sensors `DHT11` and `HW-040.`are both digital and can use the general purpose pins GPIO, GP0-28 on the pico, for more detailed information se the [data sheet](https://datasheets.raspberrypi.com/picow/pico-w-datasheet.pdf)    
for a simple explaination of the pins this site can be used [PicoW Pinout](https://picow.pinout.xyz/)

the microcontrollers role in the project is to control the sensors and work as a powersource for them aswell as a gateway to the internet.

### Sensors & specification

**DHT11 Temperature and humidity sensor**
In this project a DHT11 is used to collect temperature and humidity measurements every 3 seconds. the measurement range and accuracy is: 
  
Temperature C°∈[0,50]±2 
Humidity % RH ∈[20,90]5± 

This range is good for monitoring the temperature and humidity in indors enviorments. more detailed technical specifications can be found in the [Data sheet](https://www.electrokit.com/uploads/productfile/41015/DHT11.pdf)  

DHT11 is digital and works with GIPO pins.


**HW-040. Rotary encoder**
This sensor is used to adjust request rate and batch size in the project based on absolute offset, left or right does not matter. the rotary encoders code establishes rotation direction by monitoring in witch order the CLK and DT pin get impulses when turning the knob.

HW-040 is digital and works with GIPO pins.

**Cables**
The male to male Jumper cables are used for all connections on the bread board except for the rotary encoder that uses male to female jumper cables.

### List of material and cost

>All materials was bought from [electrokit](https://www.electrokit.com/)

| Material | Price (sek)|
|:--------:| :----------:|  
| Raspberry Pie Pico WH |	109.00 SEK |
| Bread board 400 connections | 49.00 SEK  |
|Jumper cables Male to Male |29.00 SEK|
|Jumper wires female/male|29.00 SEK|
| Micro-Usb 1m |39.00 SEK|
|DHT11 Temperature and humidity sensor|49.00 SEK|
|HW-040. Rotary encoder|29.00 SEK|


### Computer setup
I am using Windows 11 on my laptop. Blazor apps are fairly lightweight to develop so most computers should be fine. If you have less then 8gb ram you might encounter lagg.
the section below covers the following:
* How the PicoW device programmed.
* Which IDE:s is used.
* Installing software.
* Installing Templates
* Installing extensions and nugets.
* Step by step from flashing the firmware.
* How flashing is done on MicroPython.

**Chosen IDE:s related software**

>Required

The Raspberry pie Pico WH can be programmed in Micro Phyton.
Install Phyton if you don't allready have it.
Python https://www.python.org/downloads/

>Required

Install the IDE:s 
the recomended IDE:s used for this project are `Visual studio code` and `Visual studio 2022`. ==You don't need pro version, community is fine.== [Visual Studio](https://visualstudio.microsoft.com/)

>Optional for comfort 

I use vs code and Visual studio for the familiar keybinds wich gives me the comfort of ergonomy. (Free!) I also recomend to try the free VsVim 2022, Add New File and Editor Guidelines extensions in visual studio.

>Required

Install PyMakr to be able to upload code on the pico
You need the `PyMakr` extension in visual studio code to upload code to the Pico WH. [PyMakr](https://docs.pycom.io/gettingstarted/software/vscode/) 

>Required

PyMakr requires `Node.js` to work so istall this software aswell. ==Use the recomended version== [Node.js](https://nodejs.org/en.) The database editor used for this project is `Microsoft SQL Server Management Studio 18` ==Scroll down and pick the developer edition== [Ms SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

>Required

You need the following project templates for visual studio, Empty solution, ASP.Net Core Web Api, Class Library, Console App, Blazor Server App Empty. Use the installer to add them.

>If you whant to deploy to customers in the future it is recomended to build automated tests for key functionality.

Test templates
Automated tests with XUnit NUnit or MSTest 
use with nugets packages: Mock and Fluent Assertions.


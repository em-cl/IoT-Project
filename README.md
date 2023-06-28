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
Install Python if you don't allready have it.
[Python](https://www.python.org/downloads/)

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

**Step by step getting started**
- Install the Required software
- Install the Required templates
- Install the Required extensions
  - Setting up Pico WH
    - Update the firmware on the Raspberry Pi Pico WH:
      - Download the firmware for Micro Phyton https://micropython.org/download/rp2-pico-w/ pick the latest under releases.
      - connect the micro usb first in the pico, then hold the BOOTSEL button while connecting the other end of the cable to a usb port on your computer.
      - the driver RPI-RP2 for the Pico should appear in the file explorer, drag the .uf2 firmware  into the driver and wait for the drive to automatically  disconnect.

>After this you can start codeing in MicroPhyton on the pico.

  - Setting up the projects in the **.Net stack**
    - In Visual studio Create a empty solution
    - Add a empty folders source and Test inside the solutions
    - Add the Domain, Infrastructure, Presistence and application projects as project type Class Library
    - Add the Web Api as a Asp.Net Core Web Api
    - Add a Blazor Server App Empty project and change output type to class library via: properties > application > general > Output type.
    - Set the available project references like the detailed column in the first matrix.
    - install the nugetpackages in the second matrix in the correct projects
    - Set the WebbApi as startup project.
    - Move the correct Program.cs code in precentation to WebApi Program.cs **in the correct order and place.**
      - _Warning! the project www.root and images needs to be in WebApi, scoped css files wont work properly out of the box. find a solution if you can't live with it ;)_
    - Build the dependecy injection containers and the program.cs please look at
      - [WebAPI Program.cs](https://github.com/em-cl/IoT-Project/blob/main/WebAPI/Program.cs), [Application DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Application/DependencyInjection.cs), [Presentation DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Presentation/DependencyInjection.cs), [Presistence DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Presistence/DependencyInjection.cs), [Infrastructure DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Infrastructure/DependencyInjection.cs) if you are uncertain of what to do.
    - Set the Ip address and port in launchSettings.Json in the WebApi project use ipconfig to se IPv4 Address. . . . . . . . . . . : 192.xxx.x.xx dont't use 5G wifi it will not work with Pico WH
    - if you dont have a  static ip adress you can run ncpa.cpl if you whant to and add one.

>The ip and port needs to match the requests from the pico to the WebApi

## Puting everything together
this diagram shows how to connect the sensors to the pico. optionally use the Male to Female cables for the `rotary encoder`.
![IoTProjBoard_bb](https://github.com/em-cl/IoT-Project/assets/76754841/b61a564a-2a5f-4c7b-881e-d5b7ed3a0e66)

>In order to see changes when you program the microphyton code turn on autosave in vs code and develeoper mode in the PyMakr tab.

Calculate electricity.

## Platform
im just hosting the software on my laptop. it should work well for most enviorments if you whant to put it on a server, because core is cross platform. 
>the recomended docker image is linux if you want to try docker support.











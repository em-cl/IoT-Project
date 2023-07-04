# IoT-Project

## Temperature and humidity sensor with adjustable request rate using rotary encoder, programmed in .Net and MicroPython

**Author:** `Emil Clementz` **Course Id** `ec222vw`

## Project overview
This project is about measuring temperature and humidity and displaying information in a intuitive way. also notifications are automatically sent via mail when adjustable thresholds are met to inform the user. 

**Project structure for solution:**
The MicroPython code in production would live on the `Raspberry Pico WH` and communicate to the system that lives on a server via Wi-Fi through the WebApi. The .Net solution dependencies can be seen in the matrix below.
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

_Some prior knowledge of the .Net stack is advised!_

Writing a similar project with the information provided in this tutorial.
**Beginners** to **intermediate** level programmers in **25+ hours**
**Advanced** programmers **5-10+ hours**


## Objective

**Why was this project chosen?**
The choice of this tech stack was driven by a lack of experience in microservice architecture and its suitability for IoT. I wanted to test the Mediator, Repository, and Unit of Work design patterns in this new, modern approach to step outside my comfort zone and learn.

**The main objective**
The objective of the digital artifact is to monitor humidity and temperature with adjustable rates and batch sizes, allowing for single requests approximately every 3 seconds or any multiple of the same ratio. This approach aims to avoid unnecessary strain on the Wi-Fi.

**...Learning?**
The knowledge artifact from building a project like this covers many important core concepts for distributed systems and tie them together. 


**What insights will you gain from a project like this?**
How to build scalable modern systems in the .NET stack:
How to use the Raspberry Pi Pico W Microcontroller with MicroPython.
The necessary information and insight about indoor temperature and humidity to optimize your home for optimal living conditions.

## Material
**Microcontroller**
For this project the Microcontroller `Raspberry pie Pico WH` was used. But any microcontroller with Wi-Fi programmable in `Micro Phyton` should work. the sensors `DHT11` and `HW-040.`are both digital and can use the general purpose pins GPIO, GP0-28 on the Pico, for more detailed information se the [data sheet](https://datasheets.raspberrypi.com/picow/pico-w-datasheet.pdf)    
for a simple explanation of the pins this site can be used [PicoW Pinout](https://picow.pinout.xyz/)

the microcontrollers role in the project is to control the sensors and work as a power source for them as well as a gateway to the internet.

### Sensors & specification

**DHT11 Temperature and humidity sensor**
In this project a DHT11 is used to collect temperature and humidity measurements every 3 seconds. the measurement range and accuracy is: 
  
Temperature C°∈[0,50]±2 

Relative Humidity % RH ∈[20,90]±5 

This range is good for monitoring the temperature and humidity in indoors environments. more detailed technical specifications can be found in the [Data sheet](https://www.electrokit.com/uploads/productfile/41015/DHT11.pdf)  

DHT11 is digital and works with GIPO pins.


**HW-040. Rotary encoder**
This sensor is used to adjust request rate and batch size in the project based on absolute offset, left or right does not matter. the rotary encoders code establishes rotation direction by monitoring in witch order the CLK and DT pin get impulses when turning the knob.

HW-040 is digital and works with GIPO pins.

**Cables**
The male-to-male Jumper cables are used for all connections on the bread board except for the rotary encoder that uses male to female jumper cables.

### List of material and cost

>All materials was bought from [electrokit](https://www.electrokit.com/)

| Material | Price (SEK)|
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
* Step by step flashing the firmware.

**Chosen IDE:s related software**

>Required

The Raspberry pie Pico WH can be programmed in Micro Phyton.
Install Python if you don't already have it.
[Python](https://www.python.org/downloads/)

>Required

Install the IDE:s 
the recommended IDE:s used for this project are `Visual studio code` and `Visual studio 2022`. ==You don't need pro version, community is fine.== [Visual Studio](https://visualstudio.microsoft.com/)

>Optional for comfort 

I use vs code and Visual studio for the familiar key binds which gives that are ergonomic. (Free!) I also recomend to try the free VsVim 2022, Add New File and Editor Guidelines extensions in visual studio.

>Required

Install PyMakr to be able to upload code on the pico
You need the `PyMakr` extension in visual studio code to upload code to the Pico WH. [PyMakr](https://docs.pycom.io/gettingstarted/software/vscode/) 

>Required

PyMakr requires `Node.js` to work so install this software as well. ==Use the recommended version== [Node.js](https://nodejs.org/en.) The database editor used for this project is `Microsoft SQL Server Management Studio 18` ==Scroll down and pick the developer edition== [Ms SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

>Required

You need the following project templates for visual studio, Empty solution, ASP.Net Core Web Api, Class Library, Console App, Blazor Server App Empty. Use the installer to add them.

>It is best practice to build automated tests for key functionality, if you have time.

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

>After this you can start programming in MicroPython on the Pico.

  - Setting up the projects in the **.Net stack**
    - In Visual studio Create a empty solution
    - Add a empty folders source and Test inside the solution
    - Add the Domain, Infrastructure, Presistence and application projects as project type Class Library
    - Add the Web Api as a Asp.Net Core Web Api
    - Add a Blazor Server App Empty project and change output type to class library via: properties > application > general > Output type.
    - Set the available project references like the detailed column in the first matrix.
    - install the NuGet packages in the second matrix in the correct projects
    - Set the WebApi as startup project.
    - Move the correct Program.cs code in presentation to WebApi Program.cs **in the correct order and place.**
      - _Warning! the project www.root and images needs to be in WebApi, scoped CSS files wont work properly out of the box. find a solution if you can't live with it ;)_
    - Build the dependency injection containers and the program.cs please look at
      - [WebAPI Program.cs](https://github.com/em-cl/IoT-Project/blob/main/WebAPI/Program.cs), [Application DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Application/DependencyInjection.cs), [Presentation DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Presentation/DependencyInjection.cs), [Presistence DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Presistence/DependencyInjection.cs), [Infrastructure DP Injection](https://github.com/em-cl/IoT-Project/blob/main/Infrastructure/DependencyInjection.cs) if you are uncertain of what to do.
    - Set the Ip address and port in launchSettings.Json in the WebApi project use ipconfig to se IPv4 Address. . . . . . . . . . . : 192.xxx.x.xx dont't use 5G wifi it will not work with Pico WH
    - if you don’t have a static ip-address you can run ncpa.cpl on Windows if you want to and add one.

>The ip and port needs to match the requests from the Pico to the WebApi

## Putting everything together
this diagram shows how to connect the sensors to the pico. optionally use the Male to Female cables for the `rotary encoder`.
![IoTProjBoard_bb](https://github.com/em-cl/IoT-Project/assets/76754841/b61a564a-2a5f-4c7b-881e-d5b7ed3a0e66)

>In order to see changes when you program the MicroPython code turn on autosave in vs code and developer mode in the PyMakr tab.

## Electricity.

**PICO W**
In order to supply the Pico W with power you can either use the VBUS 5V± 10% via Micro USB or feed the VSYS pin from a battery: external source V ∈[1.8,5.5]. A inductor in the pico lowers or raises the Voltage to 3.3V before providing the sensors with power.

for this project this is all you need to know pick one or the other don't use both at the same time. Note that Pico is capable of more for example: There is a protection diode between VBUS and VSYS that protects against incorrect polarity and backfeeding power into the USB supply as described in [this video](https://www.youtube.com/watch?v=3PH9jzRsb5E). 
for more detailed specifications and instructions look at the [datasheet](https://datasheets.raspberrypi.com/picow/pico-w-datasheet.pdf).
 
If you for some other project want to connect both the micro usb and battery via VSYS at the same time, again please look at the [datasheet](https://datasheets.raspberrypi.com/picow/pico-w-datasheet.pdf) to do this safely.

**DHT11**  
According to the [Data sheet for DHT11](https://www.electrokit.com/uploads/productfile/41015/DHT11.pdf).  
The minimum condition status is chosen for its proximity to the Pico W Voltage output level.  
V = 3V  
I<sub>1</sub> = 0,2mA avg  
Ohms law gives  
R<sub>1</sub> =  V / I = 3V / 0,2mA = 15kΩ    

**HW040**  
According to the [Data sheet for HW040/KY040](https://www.rcscomponents.kiev.ua/datasheets/ky-040-datasheet.pdf)
When CLk or DT pin sends 

R<sub>2</sub> = 10kΩ  
V = 3.3V supplied from the Pico W    
Ohms law gives   
I<sub>2</sub> = V / R = 3.3V / 10kΩ = 0,33mA

**Parallel circuit of sensors**  
The sensors are connected in parallel to the Pico W.  
The resistance for the sensors in parallel is according to:  
1/R<sub>s</sub> = 1/R<sub>1</sub> + 1/R<sub>2</sub>

R<sub>s</sub> = 1 / (1/R<sub>1</sub> + 1/R<sub>2</sub>) = R<sub>1</sub> * R<sub>2</sub> / (R<sub>1</sub> + R<sub>2</sub>) = 15000 Ω * 10000 Ω / (15000 Ω + 10000 Ω) = 150 000 000 Ω / 25000 Ω = 6000 Ω  
I<sub>s</sub> = V/R<sub>s</sub> = 3.3V / 6000 Ω = 0,55mA  

Sensor circuit Voltage V<sub>s</sub> = 3.3V,  
Sensor circuit total resistance R<sub>s</sub> = 6kΩ,  
Sensor circuit total current I<sub>s</sub> = 0,55mA  

![image](https://github.com/em-cl/IoT-Project/assets/76754841/43b06bce-6028-49e3-863b-29f775ee926c)


## Platform
As previously mentioned I’m using a .Net stack with Blazor Server frontend.
It is secure near native performance and helps rendering interactive websites with low bandwidth.
I choose this solution because I wanted to code a scalable project with a responsive dashboard without relying on JavaScript frameworks like React or Vue. 
I also tried using a Clean architecture with DDD typical for microservices for the first time to improve code quality and learn how it works, you can read more [Here](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice.) if you are interested.  
I am hosting the software on my laptop. It should work well for most environments if you want to put it on a server, because core is cross platform. 
This project will work as intended without much effort if published on a IIS server.
The recommended docker image is Linux if you want to try docker support.

## Transmitting Data and Code examples
From the Pico WH to the dashboard, a http GET request are sent containing data from the sensors.  
The wireless protocol used is Wi-Fi.  
The transport layer protocol used is TCP.  
I choose to use this to easily communicate between my laptop and Pico using a Rest API.  
**Power and reception**  
Using wifi in a home is likely free and motivates use of the power hungry Wi-Fi component on the Pico, since the device don´t need to run on battery. it can live in a power jack coupled with a transformer to reduce the voltage and talk on the home Wi-Fi. To not use a battery increases the lifetime of the IoT device and reduces e waste. The range of the Wi-Fi likely covers the entire home environment and intended use case.  
**security**
The project works with http or https but uses http, http is less secure then https. HTTPS uses self signed certificates "or trusted certificates from real certificate authorities if you need to have them". if you want to set up https for the pico please look at the [documentation here](https://docs.micropython.org/en/latest/library/ssl.html).  
The mail function uses TLS and SMTP and is secure this requires a google account with 2 step verification and application password to work with google.

**Getting sensor data**

MicroPython code

_How often is the data sent?_
Every 3 seconds measurements are recorded. The rotary encoder adjusts how many measurements are collected before a HTTP request is sent.  
This is the code for the rotary encoder that reads left or right rotation, the rotations absolute position left or right, is used in the life loop to adjust number of measurements collected.
```Python
def rotaryComponent():
    #constants
    DT = 0 
    CLK = 1

    #Pins
    _directionPin = Pin(DT,Pin.IN)
    _clockPin = Pin(CLK,Pin.IN)
    
    #sources:
    # irq https://gurgleapps.com/learn/electronics/ky-040-rotary-encoder-on-a-raspberry-pi-pico-detailed-explanation-and-step-by-step-code
    # left or right? https://www.youtube.com/watch?v=3fAFNwA-aEY
    def rotaryChange(pin):
        global position
        global previousValue
        if previousValue != _clockPin.value():
            if _clockPin.value() == False:
                if _directionPin.value() == False:
                    position = position - 1
                else:
                    position = position + 1      
            previousValue = _clockPin.value()
    #events
    _clockPin.irq(handler=rotaryChange, trigger=Pin.IRQ_FALLING | Pin.IRQ_RISING)
    _directionPin.irq(handler=rotaryChange, trigger=Pin.IRQ_FALLING | Pin.IRQ_RISING)
```
This is the life loop of the Pico. it syncs time from a timeserver to stamp measurements, and builds the json sends.
```Python
dataList = []
run = True
def temperature_and_humidity_sensor():
    import dht
    tempSensor = dht.DHT11(machine.Pin(2))     # DHT11 Constructor 
    retrycounter = 0
    while run:
        
        #time
        try:
            ntptime.settime()
        except Exception:
            retrycounter+=1
            print_dots("({})not connected to time server...".format(retrycounter),retrycounter,3)
            time.sleep(0.5)
            clear()
            if retrycounter == 3:
                boot.do_connect()
                retrycounter = 0
            continue

        retrycounter = 0
        current_time = time.time()
        local_time = time.localtime(current_time)
        tempSensor.measure()
        temperature = tempSensor.temperature()
        humidity = tempSensor.humidity()
        Data = {
            "Time": local_time,
            "Temperature": temperature,
            "Humidity" : humidity
        }
        dataList.append(Data)
        clear()
        absolutePos = abs(position)
        print("number of measurements",absolutePos)
        
        if position!=0:
            print_dots("collecting",len(dataList),absolutePos)
            message = []
            for d in dataList:
                message.append(json.dumps(d))  
            jsonBody = ('['+(','.join(message))+']')
            print(jsonBody)
            if len(dataList) >= absolutePos:
                apiSend(absolutePos,jsonBody)
        elif position == 0:
            dataList.clear()
        
        time.sleep(3)
```

**Data flow**

1. The TCP socket is used to send http Get requests to the Rest API over the Wi-Fi. 
```Python
def get_http(message=""):
    import socket
    host = secrets.secrets["ip"]
    path = '/api/Measurement/Test?data={}'.format(message)
    
    # start TCP connection
    sock = socket.socket()
    addr = socket.getaddrinfo(host, secrets.secrets["port"])[0][-1]
    sock.connect(addr)

    # Send GET request
    sock.send("GET {} HTTP/1.1\r\nHost: {}\r\nAccept: */*\r\n\r\n".format(path, host))
    # Api response
    response = sock.recv(4096).decode("utf-8")
    print(response)
    
    #close TCP connection
    sock.close()
```
**C# code**

2. The Rest Api receives the Json array and starts a command using the data.   
```C#
	` [HttpGet("Test")]
		// GET: 
		public async Task<ActionResult> Test([FromQuery] string data){
			
			try
			{
				var measurements = await _mediator.Send(new SaveBatchCommand(data));
			}
			catch (Exception ex)
			{
				await _mediator.Send(new LogErrorCommand(ex,
					(GetType().DeclaringType?.Name ?? string.Empty)
					+ "." + nameof(SaveBatch)));

				return BadRequest("Failed to complete data data conversion for display.");
			}

			return Ok(data);
		}
```
3. The string is deserialized to data transfer objects and converted to domain objects, then checked for duplicates and saved in the database. If everything was correct the new measurements are published in memory as a MediatR notification.
```C#
	public record SaveBatchCommand (string Data) : IRequest<IEnumerable<Measurement>>
	{
		public class Handler : IRequestHandler<SaveBatchCommand, IEnumerable<Measurement>>
		{

			private readonly IUnitOfWork _unitOfWork;
			private readonly IPublisher _publisher;
			public Handler(
				IUnitOfWork unitOfWork,
				IPublisher publisher)
			{
				_unitOfWork = unitOfWork;
				_publisher = publisher;
			}
			public async Task<IEnumerable<Measurement>> Handle(
				SaveBatchCommand request, 
				CancellationToken cancellationToken = default)
			{
				if(string.IsNullOrEmpty(request.Data)) 
					return Enumerable.Empty<Measurement>();
				
				var measurements = Measurement
					.MapToModels(JsonSerializer
						.Deserialize<IEnumerable<MeasurementDto>>(request.Data) 
					             ?? Enumerable.Empty<MeasurementDto>()).ToList();

				if (measurements == null || !measurements.Any()) 
					return Enumerable.Empty<Measurement>();


				var newData = new List<Measurement>();
				foreach (var measurement in measurements)
				{
					if (await _unitOfWork.Measurements
						    .GetAsNoTrackingAsync(
							    filter: m => m.Time == measurement.Time) != null)
						continue;
					else
						newData.Add(measurement);
				}

				measurements.Clear();

				if (newData == null || !newData.Any()) 
					return Enumerable.Empty<Measurement>();
				
				await _unitOfWork.Measurements.AddRangeAsync(newData);
				
				await _unitOfWork.SaveAsync();

				_unitOfWork.ClearChangeTracker();

				await _publisher.Publish(new BatchSavedEvent(newData), cancellationToken);

				return newData;
			}
		}
	}
```
This is the notification. A notification can have many notification handlers that listen to the published notification.
```C#
	public record BatchSavedEvent(List<Measurement> Measurements) : INotification;
```
3. The notification handler populates the charts in the Blazor pages and Blazor components "the dashboard" with data and updates the page.
The component `@implements INotificationHandler<BatchSavedEvent>` in order for this to work.
```C#
  	protected override async Task OnInitializedAsync()
	{
		OnBatchSaved += async (sender, changed) =>
		{
			_times = changed.Time;
			var separatedTimes = _times.Split(' ');
			_cacheCount = separatedTimes.Length;
			_temperature = changed.TemperaturePolyLine;
			_pointsTemperature = GeneratePlotsWithTimeFragments(_temperature, separatedTimes);
			_humidity = changed.HumidityPolyLine;
			_pointsHumidity = GeneratePlotsWithTimeFragments(_humidity, separatedTimes);
			_tempPolygon = "0,0 " + _temperature + " 800,0";
			_humPolygon = "0,0 " + _humidity + " 800,0";
			BarCharData = PicoWDataConversion.FifoBatchHistory.ToList();
			_requestDelay =Math.Abs(BarCharData[^1].Item1.Millisecond - changed.Measurements[^1].Time.Millisecond);
		
			var notification = await SendNotificationMailIfNeeded(
				changed.Measurements, "emil.clementz@icloud.com");

			if (!string.IsNullOrEmpty(notification) && notification != _notification)
			{
				_notification = notification;
			}

			await InvokeAsync(() => StateHasChanged());
		};

		await base.OnInitializedAsync();
	}
  
	public class BatchSavedArgs : EventArgs
	{
		public string TemperaturePolyLine { get; set; }
		public string HumidityPolyLine { get; set; }
		public string Time { get; set; }
		public int batchSize { get; set; }
		public List<Measurement> Measurements { get; set; }
	}
	public delegate Task AsyncEventHandler(object sender, EventArgs e);

	public static event EventHandler<BatchSavedArgs> OnBatchSaved;

	public async Task Handle(BatchSavedEvent notification, CancellationToken cancellationToken)
	{
		var polyLine = notification.Measurements
			.OrderBy(t => t.Time)
			.CacheData(CacheSize,10)
			.CreatePolyLineFromMeasurements(800, 500);

		OnBatchSaved?.Invoke(
			this, new BatchSavedArgs
				{
					TemperaturePolyLine = polyLine[0],
					HumidityPolyLine = polyLine[1],
					Time = polyLine[2],
					Measurements = notification.Measurements
				});
	}
```
And that is how the real time data is handeld.

In addition

The data is stored in a static Queue with unique items to reduce the amount of calls to the database. If you are going to have multiple users/Microcontrollers make a user state instead with the same functionality.
```C#
		public static Queue<Measurement> FifoQueue { get; set; } = new();
		public static HashSet<Measurement> UniqueSet { get; set; } = new();

		public static List<Measurement> CacheData(
			this IOrderedEnumerable<Measurement> measurements,
			int maxNumOfItems,
			int maxNumOfHistory)
		{
			if (measurements == null || !measurements.Any()) return FifoQueue.ToList();
			
			var sendHistory = (DateTime.Now, measurements.Count());
			if (UniqueBatches.Add(sendHistory))
			{
				FifoBatchHistory.Enqueue(sendHistory);
			}

			foreach (var measurement in measurements)
			{
				if (UniqueSet.Add(measurement))
				{
					FifoQueue.Enqueue(measurement);
				}
			}
			
			while (FifoBatchHistory.Count >= maxNumOfHistory)
			{
				var measurement = FifoBatchHistory.Dequeue();
				UniqueBatches.Remove(measurement);
			}

			while (FifoQueue.Count >= maxNumOfItems)
			{
				var measurement = FifoQueue.Dequeue();
				UniqueSet.Remove(measurement);
			}
			return FifoQueue.ToList();
		}

```
This method converts the measurements to polyline used in the line charts   
```C#
	public static string[] CreatePolyLineFromMeasurements(this List<Measurement> measurements, int width, int height)
		{
			DateTime referenceTime = measurements[0].Time;

			var timeScaleFactor = (measurements.Count() == 1)
				? 1
				: width / (measurements[^1].Time - referenceTime).TotalSeconds;

			var temperatureScaleFactor = height / 50;
			var humidityScaleFactor = height / 90;

			var temperatureBuilder = new StringBuilder();
			var humidityBuilder = new StringBuilder();
			var time = new StringBuilder();

			for (int i = 0; i < measurements.Count; i++)
			{
				var xValueInSeconds = (int)((measurements[i].Time - referenceTime).TotalSeconds * timeScaleFactor);

				temperatureBuilder.Append(
					$"{xValueInSeconds},{measurements[i].Temperature * temperatureScaleFactor} ");
				humidityBuilder.Append(
					$"{xValueInSeconds},{measurements[i].Humidity * humidityScaleFactor} ");

				time.Append(measurements[i].Time.ToString("mm:ss") + ", ");
			}
			var result = new[]
			{
				temperatureBuilder.ToString(),
				humidityBuilder.ToString(),
				time.ToString()
			};

			temperatureBuilder.Clear();
			humidityBuilder.Clear();
			time.Clear();

			return result;

		}
```
## Presenting the data


>Describe the presentation part. How is the dashboard built? How long is the data preserved in the database?

**The dashboard**

>DHT11

The green areas indicate the changes in temperature and humidity, 

**Automation/triggers of the data**  
The yellow and magenta lines shows the adjustable thresholds where automatic notifications are sent via mail when measurements exceeds them. the cache size "number of measurements" is adjustable. There is a second notification handler that triggers on the  BatchSaved notification published event, that logs to debug output and console.  
![image](https://github.com/em-cl/IoT-Project/assets/76754841/0389999e-1730-4b56-abc4-ef3c61c115da)

>HW-040.

The dashboard for the rotary encoder shows X time and Y number of measurements.
![image](https://github.com/em-cl/IoT-Project/assets/76754841/bd3ce31e-9370-4152-9273-281d462152a3)

**Choise of database**
The project uses a relational database created code first with generated migration scripts.
using migration scripts allows for easy testing and updates to the database on server when the software is published.
This is useful when building integration tests. Developers can easily create databases with the scripts to run separate identical databases with different data in production, test and development environments.

Entity Framework Core with a SQL relational database was used because it is fast to build and easy to maintain.
A relational database is also storage efficient if the program grows in the future. 

![image](https://github.com/em-cl/IoT-Project/assets/76754841/706d1931-d30e-414b-b145-cda8ddbd99db)

>The project uses the Unit of Work design pattern with the Repository designpattern for the data access code.

Compiled queries improve performance slightly and makes the data access code compact and easy to read. 

```C#
 private static readonly Func<CleanDbContext, int, IAsyncEnumerable<TraceLogDto>>
	        LatestTraceLogsCompiledQuery =
		        EF.CompileAsyncQuery(
			        (CleanDbContext ctx, int numberOfLogs) => ctx.TraceLogs
				        .OrderByDescending(x => x.LoggedDate)
				        .Take(numberOfLogs)
				        .Select(log => new TraceLogDto
			                {
				                TraceId = log.TraceId,
				                ComponentName = log.ComponentName,
				                LoggedDate = log.LoggedDate,
				                Message = log.Message,
			                })
				        .AsNoTracking());
```

IAsyncenumerable makes it possible to display items as they are yield returned from database, so the UI don´t freeze when the database grows. 

**How often is data saved**

Data is saved  every time a request arrives before it is displayed to the dashboard as mentioned earlier with a maximum speed of once every 3 seconds. i added this limitation because indoor temperature and humidity take a couple of seconds to change to not waste computing power.
I think less frequent measurements would also be fine for this project, but the debugging would take longer, and the dashboard will take a while to fill up while testing.  
there is no functionality in the project to periodically remove data this is suggested for continued development of the project, to save space.

>The mail notification

![image](https://github.com/em-cl/IoT-Project/assets/76754841/558912bd-3ef5-4cba-a843-eb1c96ee4359)

## Finalizing the design
>I am happy with the outcome of the project. The digital artefact and IoT device can still be improved with caseing for the sensors and more functionality like maybe a light sensor, some of the code like the values stored in memory retaining state needs to be made more user specific. In the future perhaps I will extend the project to be a content management system for multiple sensors and micro controllers in my home to make it better. It was fun try programming in MicroPython for the first time.

**Demo Video** [if image don´t open a link to the video press here!](https://youtu.be/GtIQFoNZHAY)

[![Image](https://github.com/em-cl/IoT-Project/assets/76754841/71f94ae5-8e10-479f-8852-1f20a6de6994)](https://youtu.be/GtIQFoNZHAY)

**The device IRL**
![image](https://github.com/em-cl/IoT-Project/assets/76754841/9131f7d0-50ea-48f2-a595-9bdf9bb3d1ff)









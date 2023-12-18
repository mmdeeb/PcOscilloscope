# Oscilloscope /PC
Oscilloscope using Arduino and computer
## Prepared by: Mohammed yousef mohammed deeb
A project for the subject of controllers in the faculty of Informatics Engineering at the University of Aleppo in the liberated areas
## Full project explanation video
[![Project explanation video](https://github.com/mmdeeb/PcOscilloscope/blob/master/img/vv.jpg)](https://drive.google.com/file/d/1A7uExNKH6BorsYwiNYdgPYV827Eba2JP/view?usp=share_link)

###What is an Oscilloscope?
The Oscilloscope "signal plotter" is considered one of the most important electronic circuit testing and measurement devices, as it allows us to see signals at multiple points in the circuit and thus detect if any part is working properly or not.

The Oscilloscope allows us to see the signal image and identify its shape, whether sinusoidal, square, etc.

 1- Controller: Here we used Arduino mega2560 chip

2- Computer: any computer with the software installed for display


###The controller and processes and software applied to it:

As mentioned previously, we used the Atmega2560 controller in our project, which operates at 16MHz and has an analog-to-digital converter built into its structure, allowing the controller to read and process analog signals that are common in most applications, and is characterized by the following specifications:
• 10-bit conversion accuracy, with absolute error (2LSB +)
•	Conversion time [13 - 260 uS]
•	Sampling rate up to 76.9 KSPS
•	Multiplexer for selection between 16 channels
•	Interruption upon completion of conversion

For our application to work fully, there is a sequence of processes that take place in the controller as follows:
1- Read voltage value from one of the multiplexer channels.
2- Apply analog-to-digital conversion to it.
3- Send this value to the computer via the USRT communication port

This sequence of processes includes some sub-processes that will be explained in detail when explaining the programming code.

Using the C language to program the controller with the mikroC code editor

<img src="https://github.com/mmdeeb/PcOscilloscope/blob/master/img/mikroC.jpg">


We note that the code is divided into three main parts:

1- ADC Initialization:
-	ADMUX=0b00100000: Selecting the Aref pin voltage as a reference, left aligning the output, selecting channel ch0.

-	ADCSRA=0b10000111:Enabling the converter and selecting the division ratio (here we chose a division ratio of 128 because the processor's operating frequency is 16MHz and for the converter to function properly, the pulse rate driving the converter must be within the range [50KHz - 200KHz] 16MHz/128 = 125Khz and so we have achieved the required condition).

-	ADCSRB=0b00000000: No auto-trigger source was selected.

-	DIR0=0b00000001: Configuring the register corresponding to ch0 to save power.
2-	Configuring the USART communication port:

-	UCSR0B=0b00001000: Enabling data transmission.

-	UCSR0C=0B00000110: Configuring the operating mode (Asynchronous USART) and specifying the stop bit size (1-bit) and data size (8-Bit).

-	UBRR0H=0, UBRR0L=3: Specifying the data transmission rate (250K or 230.4k).

3-	An infinite loop within which processing and transmission operations take place:

- ADCSRA.B6=1: Triggering the ADC to start conversion.

- While(ADCSRA.B4==0){}: Waiting for the conversion complete flag to become 1.

- ADCSRA.B4=1: Resetting the conversion complete flag.

- UDR0=ADCH: Sending the conversion result via the communication port.

So if we want to explain the process completely, it will be as follows:

First: The Aref is connected to a 5v power supply

Second: The signal to be displayed is read through channel CH0, which is called A0 on the Arduino chip, then it is converted to a digital value which is sent via the USART communication channel, and the rest of the processes are on the computer.

<img src="https://github.com/mmdeeb/PcOscilloscope/blob/master/img/ar.jpg">

###	The computer and software applied to it:



The language used to program the application is C# Windows forms App.
Editor: Microsoft Visual Studio 2022.

#### application interface:
<img src="https://github.com/mmdeeb/PcOscilloscope/blob/master/img/gui.jpg">

###The code has been explained in detail in the video attached above.





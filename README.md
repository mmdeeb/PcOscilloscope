# Oscilloscope/PC

![Oscilloscope Application Interface](https://github.com/mmdeeb/PcOscilloscope/blob/master/img/gui.jpg)

## Introduction
The Oscilloscope/PC project integrates the functionality of an oscilloscope with a personal computer, utilizing Arduino for signal acquisition and a C# Windows Forms application for visualization. This project is developed for the Controllers course at the Faculty of Informatics Engineering, University of Aleppo in the liberated areas, by Mohammed Yousef Mohammed Deeb.

## Project Overview
An oscilloscope, commonly referred to as a signal plotter, is essential for testing and measuring electronic circuits. It visualizes electrical signals, allowing for an assessment of the circuit's functionality. This software oscilloscope supports various signal forms, like sinusoidal and square waves, via a user-friendly PC interface.

### Components
1. **Controller**: Arduino Mega 2560, featuring:
   - Atmega2560 at 16MHz
   - Built-in analog-to-digital converter with 10-bit resolution
   - 76.9 KSPS max sampling rate
   - 16-channel multiplexer

2. **Computer**: Utilizes any computer where the software can be installed for display.

### Software and Processes
The application follows a sequence of steps to visualize the signals:
1. Reading voltage value from a multiplexer channel.
2. Converting the analog signal to a digital one.
3. Transmitting the digital data to the PC via USART.

Programming of the controller is performed using C language in mikroC.

![mikroC Interface](https://github.com/mmdeeb/PcOscilloscope/blob/master/img/mikroC.jpg)

### The Software for PC
Developed using C# Windows Forms in Microsoft Visual Studio 2022, the PC application provides a real-time display of the oscilloscope readings. 

![Application Interface](https://github.com/mmdeeb/PcOscilloscope/blob/master/img/ar.jpg)

## Full Project Explanation Video
Watch the detailed video explanation of the entire project [here](https://drive.google.com/file/d/1A7uExNKH6BorsYwiNYdgPYV827Eba2JP/view?usp=share_link).

[![Project explanation video](https://github.com/mmdeeb/PcOscilloscope/blob/master/img/vv.jpg)](https://drive.google.com/file/d/1A7uExNKH6BorsYwiNYdgPYV827Eba2JP/view?usp=share_link)

## Installation
For detailed instructions on how to set up the Oscilloscope/PC project, please refer to the video linked above.

## Acknowledgments
Special thanks to everyone involved in the development and support of this educational project.

## Contact
For more information and support, please reach out to Mohammed Yousef Mohammed Deeb.

---

This project is crafted with care by [Mohammed Mohammed Deeb](https://github.com/mmdeeb)

# IPK-first-project-2024

## Table of Contents

- [Executive Summary](#executive-summary)
- [Introduction](#introduction)
- [Theoretical Background](#theoretical-background)
- [Project Architecture and Design](#project-architecture-and-design)
- [Tools Used](#tools-used)
- [Testing](#testing)
- [Bibliography](#bibliography)

## Executive Summary
This project, **ipk24chat-client** aims to develop a robust client application enabling users to connect and communicate over a network using either TCP or UDP protocols. The primary objective was to create an efficient client that adheres to a specified protocol for message exchange, ensuring reliable communication within a chat environment.

Throughout the development process, challenges such as protocol implementation and message handling were met, however, there was not enough time for me to fully implement all the features, that`s why support of *UPD* protocol was not implemented. Yet, *TCP* protocol is fully functional. This executive summary provides an overview of creating a chat client, further detailed in the following sections.
## Introduction
This project is focused on development of a chat client that connects users to a chat server, providing real-time messaging,using set of commands.
## Theoretical Background
The development of the ipk24chat-client application centers around creating an efficient means of network communication, adhering to the custom `IPK24-CHAT` protocol. This protocol operates atop **IPv4**, leveraging the distinct characteristics of both **TCP** (Transmission Control Protocol) and **UDP** (User Datagram Protocol) to facilitate message exchange in a chat environment.

**TCP** offers a connection-oriented pathway, ensuring reliable, ordered, and error-checked transmission of data between applications. This makes **TCP** an ideal choice for scenarios where data integrity and order are paramount. On the contrary, **UDP** provides a lightweight, connectionless communication model. It excels when speed and resource efficiency take precedence over reliability, as it does not guarantee message delivery, order, or error correction.
### Problems I Faced
As you can see from Theoretical Background, *TCP* and *UDP* protocols are completely different features to implement. Despite the fact , there are some general fragments , where you can implement features for both *TCP* and *UDP* protocols, differences between them, makes the implementation very time consuming, and time is needed to realise the whole project requirements was underestimated. Also, strong influence on this human error had last year project, which was much easier.
## Project Architecture and Design
### Code simplicity
As you might notice , some sections of code are too simple or too overloaded.

Despite the fact, all the code was written on `C#` language , most of the codding habbits were taken from writing on `C` language. `C#` was chosen , based on it`s complecity and diversity, which eased some features to implement. However , there are some *Object-Orieneted-Language* features added. Fortunately , there are lots of sources of information in the internet, thanks to which it was possible to realise project in the way, you can see it.

More information about it in [Bibliography](#bibliography).
### Project Architecture
The client application is structured around a modular architecture, deviding all tasks between couple of key classes, which was operating with wide variety of methods and functions.
- `Program:` core class, where **Main** method is situated , from which program starts. All core functions and thread creating was implemented in this class. Also checks for some basic errors.
- `Arguments:` custom class, where all the arguments are stored.
- `ArgParse:` custom class, which parses all incoming arguments into `Arguments` instance.
- `AuthSemaphore:` custom class, which only represents semaphore for authentication checks.
- `UserParse:` custom class, which is used to parse incoming commands or messages from user. Contains such methods as: ___ParseCmd___ - used to parse incoming commands and build byte array,which later will be sent as relevant message to server.  Also chechs for some error cases; ___ParseMsg___ - used to form byte array for message, which will later be sent to server.
- `TCP:` core class, in which situated methods are needed for **TCP** functionality. Such as : ___Connect___ - creates client, reading and writing streams; ___SendMessage___ - used to send messages to server; ___RecieveMessage___ - used to continiuosly recieve messages from server, parse and handle them; ___CloseStreams___ - used to close opened streams, usually called right before exiting programm;
- `ResponseParser:` custom class, which parsess all incoming messages from server. Contains such methods as: ___ParseTCP___ - most of the parsing logic is happening there; ___RegexSplitMsg___ - supporting method, which is working with special regular expression.
- `Message` and `UDP:` redundant classes, which were planned to be used in **UDP** protocol implementation, but as written before, because of lack UDP implementation, these classes has no use.
## Tools Used
The client was developed on an Asus laptop, with Windows 10 operating system installed. Also in the development was involved virtual machine linux-x64 Ubuntu.  
### Key tools used

**Python3:** For Automated Testing.

**.NET 8+ SDK:** Provides the runtime and libraries for building the application, along with tools for compilation and package management.

**JetBrains Rider:** Served as the primary IDE.

**Git and Gitea:** Version control and repository hosting.

**Wireshark:** Assisted in analyzing network traffic for debugging and verifying compliance with the `IPK24-CHAT` protocol specifications.

## Testing
### UDP tests 
As UDP protocol was not implemented, there was no point in testing it.
### TCP tests  
*--------*  
**TEST OBJECTIVE:** Invalid command for the START state  
**INPUT:** "Hello"  
**EXPECTED OUTPUT:** "ERR: You must authenticate first! Type /help to view available commands!"  
**OUTPUT:** "ERR: You must authenticate first! Type /help to view available commands!"  
**OUTCOME:** _SUCCESS_  
*--------*  
**TEST OBJECTIVE:**  Writing wrong command  
**INPUT:** "/bark"  
**EXPECTED OUTPUT:** "ERR: Unknown command or wrong number of arguments! Type /help to view available commands!"  
**OUTPUT:** "ERR: Unknown command or wrong number of arguments! Type /help to view available commands!"  
**OUTCOME:**   _SUCCESS_  
*--------*  
**TEST OBJECTIVE:** Auth sent test  
**INPUT:** "/auth a b c"  
**EXPECTED OUTPUT:** "AUTH a AS c USING b\r\n" sent to server  
**OUTPUT:** "AUTH a AS c USING b\r\n" sent to server  
**OUTCOME:**  _SUCCESS_  
*--------*  
**TEST OBJECTIVE:** Msg sent test  
**INPUT:** "Hello people"  
**EXPECTED OUTPUT:** "MSG FROM c IS Hello people\r\n" sent to server  
**OUTPUT:** "MSG FROM c IS Hello people\r\n" sent to server  
**OUTCOME:**  _SUCCESS_  
*--------*  
**TEST OBJECTIVE:** BYE sent test  
**INPUT:** "BYE\r\n"  
**EXPECTED OUTPUT:** none, programm closure  
**OUTPUT:** none, programm closure  
**OUTCOME:**  _SUCCESS_  
*--------*  
##### There are not all of the tests, which were done, because the actual number of tests would take too much space.

## Bibliography
- A Matt Cone project | Mark Down Guide Basic Syntax [online]. (2024) Link : https://www.markdownguide.org/basic-syntax
- Metanit Team | Metanit C# guides [online] (2024) Link: https://metanit.com/sharp/
- Microsoft | Learn Microsoft Dotnet Documentation [online] (2024) Link: https://learn.microsoft.com/en-us/dotnet/standard
- gskinner | Regexr [online] (2024) Link: https://regexr.com
- Creative Commons | Conventional Commits [online] (2024) Link: https://www.conventionalcommits.org/en/v1.0.0/
- CommitGo | Gitea Git Project Page With Requirements [online] (2024) https://git.fit.vutbr.cz/NESFIT/IPK-Projects-2024/src/branch/master/Project%201

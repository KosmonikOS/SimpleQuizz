# SimpleQuizz

This project is an online quiz organizer that uses SignalR for real-time communication. The goal of this project is to provide a simple and intuitive platform for educators to create and administer quizzes to their students. This project was inspired by Kahoot.

Please note that this project is a prototype and currently only has a console user interface.

## Features

*   Create quizzes
*   Administer quizzes to students in real-time
*   View quiz results and statistics

## Built With

*   .NET 6
*   SignalR

## Design

This project was made to practice object-oriented design. Therefore, it was first fully designed using a UML diagram.

## Getting Started

To get started with this project, clone the repository and open it in Visual Studio. Restore the NuGet packages and build the solution.

This project consists of three main components:

<table>

<thead>

<tr>

<th>Component</th>

<th>Description</th>

</tr>

</thead>

<tbody>

<tr>

<td>Web application</td>

<td>Handles real-time communication between the quiz host and participants.</td>

</tr>

<tr>

<td>Quiz host</td>

<td>Creates and controls quizzes.</td>

</tr>

<tr>

<td>Participant</td>

<td>Connects to and takes quizzes.</td>

</tr>

</tbody>

</table>

Once the solution is built, you can begin using SimpleQuizz.

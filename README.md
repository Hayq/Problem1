# Multi-threaded Console Application

This is a multi-threaded console application that demonstrates the interaction between producers and consumers using a shared data queue. Producers generate random integers and add them to the queue, while consumers retrieve the integers and write them to an output file.

## Problem Description
You are tasked with designing a multi-threaded console application. This application will consist of two types of threads: Producers and Consumers that interact with a shared data queue and operate concurrently.

### Producers (N)
The application will support N producers, where N ranges from 1 to 10. Each producer is its own separate thread. A producer thread will go to sleep for a random duration between 0 and 100 milliseconds. Upon waking up, it generates a random integer. This integer is added to the shared data queue. However, if the addition of this integer causes the data queue to reach its maximum limit of 100 elements, the producer thread will pause and wait. It will only resume operation once the number of elements in the queue has decreased to 80 or fewer.

### Consumers (M)
The application will support M consumers, where M ranges from 1 to 10. Each consumer also operates as a separate thread. A consumer thread will sleep for a random duration between 0 and 100 milliseconds. Upon waking up, it retrieves an integer from the data queue and writes it to an output file named output.txt. Each integer in the output file is separated from the next by a comma. If the data queue is empty when a consumer tries to access it, the consumer thread will block until a new element is added to the queue by a producer.

When you start the application, you will need to input N and M, representing the number of producers and consumers, respectively. The application will then initiate all the threads. The application should display (in the console window) the current number of elements in the data queue every second. When stopping the application, all producer threads should be interrupted. However, before the application exits, it should wait for all the consumer threads to finish writing the queued data to the output file.

## How to Setup & Run

1. Install .NET 6 SDK:
   - Visit the [Download .NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) page.
   - Follow the instructions to download and install the .NET 6 SDK appropriate for your operating system.

2. Clone the repository:
   ```bash
   git clone https://github.com/your-username/multi-threaded-console-app.git

3. Navigate to the project directory:
    ```bash
    cd multi-threaded-console-app
4. Build the project:
    ```bash
    dotnet build
5. Run the project:
   ```bash
    dotnet run --project Problem1\Problem1.csproj
6. And when the application is finished the output.txt file will be in the same directory as run the command or executable.

# Create a .NET Core console app

Begin by creating a new .NET Core console project using the [.NET Core CLI](/dotnet/core/tools/).

1. Open your command-line interface (CLI) in a directory where you want to create the project. Run the following command.

    ```Shell
    dotnet new console -o GraphGettingStarted
    ```

1. Once the project is created, verify that it works by changing the current directory to the **GraphGettingStarted** directory and running the following command in your CLI.

    ```Shell
    cd GraphGettingStarted
    dotnet run
    ```

    If it works, the app should output `Hello World!`.

## Install dependencies

Before moving on, add some additional dependencies that you will use later.

- [Microsoft.Extensions.Configuration.UserSecrets](https://github.com/aspnet/extensions) to read application configuration from the [.NET development secret store](https://docs.microsoft.com/aspnet/core/security/app-secrets).
- [Azure SDK Client Library for Azure Identity](https://github.com/Azure/azure-sdk-for-net) to authenticate the user and acquire access tokens.
- [Microsoft Graph .NET Client Library](https://github.com/microsoftgraph/msgraph-sdk-dotnet) to make calls to the Microsoft Graph.

Run the following commands in your CLI to install the dependencies.

```Shell
dotnet add package Microsoft.Extensions.Configuration.UserSecrets --version 5.0.0
dotnet add package Azure.Identity --version 1.4.1
dotnet add package Microsoft.Graph --version 4.16.0
```

## Design the app

In this section you will create a simple console-based menu.

Open `Program.cs` in a text editor (such as [Visual Studio Code](https://code.visualstudio.com/)) and replace its entire contents with the following code.

```csharp
Console.WriteLine(".NET Core Graph Tutorial\n");

int choice = -1;

while (choice != 0) {
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Display access token");
    Console.WriteLine("2. View the signed in user");
    Console.WriteLine("3. View the signed in user emails");
    Console.WriteLine("4. Send an email as the signed in user");

    try
    {
        choice = int.Parse(Console.ReadLine()!);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    switch(choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            // Display access token
            break;
        case 2:
            // Get signed in user
            break;
        case 3:
            // Get messages
            break;
        case 4:
            // Send mail
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}
```

This implements a basic menu and reads the user's choice from the command line.

[Next step](03-register-app.md)
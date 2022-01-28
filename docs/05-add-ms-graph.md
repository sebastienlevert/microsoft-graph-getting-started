# Get signed in user's emails

In this exercise you will incorporate the Microsoft Graph into the application. For this application, you will use the [Microsoft Graph .NET Client Library](https://github.com/microsoftgraph/msgraph-sdk-dotnet) to make calls to Microsoft Graph.

## Get user details

1. Open **./Graph/GraphHelper.cs** and add the following function to the **GraphHelper** class.

    ```csharp
    public static async Task<User?> GetMeAsync()
    {
        try
        {
            // GET /me
            return await graphClient!.Me
                .Request()
                .Select(u => new{
                    u.DisplayName,
                    u.Mail,
                    u.MailboxSettings
                })
                .GetAsync();
        }
        catch (ServiceException ex)
        {
            Console.WriteLine($"Error getting signed-in user: {ex.Message}");
            return null;
        }
    }
    ```

1. Add the following code in `Program.cs` just after the `// Get signed in user` call to get the user and output the user's display name.

    ```csharp    
    var user = await GraphHelper.GetMeAsync();
    Console.WriteLine($"Signed in user: {user!.DisplayName}\n");
    ```

If you run the app now and select option 2, the app will output:

```shell
.NET Core Graph Tutorial

Please choose one of the following options:
0. Exit
1. Display access token
2. View the signed in user
3. View the signed in user emails
4. Send an email as the signed in user
2
To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code AVHEDWCK9 to authenticate.
Signed in user: Megan Bowen
```

## Get user's emails

1. Add the following function to the `GraphHelper` class to get emails from the user's inbox.

    ```csharp
    public static async Task<IMailFolderMessagesCollectionPage?> GetMessagesAsync(int numberOfMessages = 5)
    {
        try
        {
            // GET /me
            return await graphClient!.Me.MailFolders.Inbox.Messages
                .Request()
                .Select(u => new{
                    u.Subject,
                    u.From
                })
                .Top(numberOfMessages)
                .OrderBy("receivedDateTime desc")
                .GetAsync();
        }
        catch (ServiceException ex)
        {
            Console.WriteLine($"Error getting signed-in user messages: {ex.Message}");
            return null;
        }
    }
    ```

Consider what this code is doing.

- The URL that will be called is `/me/messages`.
- The `Top` function requests at most 5 messages.
- The `Select` function limits the fields returned for each message to just those the app will actually use.
- The `OrderBy` function sorts the results by the received date time.

## Display the results

1. Add the following just after the `// Get messages` comment in the `Program.cs` file.

    ```csharp
    var messages = await GraphHelper.GetMessagesAsync();
    
    Console.WriteLine($"Signed in user emails:\n");
    foreach(var message in messages!) {
        Console.WriteLine($"{message.From.EmailAddress.Name} ({message.From.EmailAddress.Address}): {message.Subject}");
    }
    ```

2. Save all of your changes and run the app. Choose the **View the signed in user emails** option to see a list of the user's emails.

    ```Shell
    Please choose one of the following options:
    1. Exit
    2. Display access token
    3. View the signed in user
    4. View the signed in user emails
    5. Send an email as the signed in user
    3
    To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code ATZSZ5W8L to authenticate.
    Signed in user emails:

    Microsoft (microsoft-noreply@microsoft.com): Get started with your new Microsoft 365 E5 Compliance trial
    Microsoft Audio Conferencing (maccount@microsoft.com): You now have Audio Conferencing for Microsoft Teams or Skype for Business Online – Here is your dial-in information and PIN
    Yammer (notifications@yammer.com): You have been added on Contoso Demo to Leadership
    Sales and Marketing (SalesAndMarketing@M365x55726300.onmicrosoft.com): You've joined the Sales and Marketing group
    Mark 8 Project Team (Mark8ProjectTeam@M365x55726300.onmicrosoft.com): You've joined the Mark 8 Project Team group
    Please choose one of the following options:
    1. Exit
    2. Display access token
    3. View the signed in user
    4. View the signed in user emails
    5. Send an email as the signed in user
    ```

[Next step](06-send-email.md)
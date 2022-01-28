// See https://aka.ms/new-console-template for more information
using GraphGettingStarted;

Console.WriteLine(".NET Core Graph Tutorial\n");

var appId = "e1d10ab9-736a-478e-a9b2-bdbf57ace9db";
string[] scopes = {"User.Read", "Mail.Read", "Mail.Send"};

// Initialize Graph client
GraphHelper.Initialize(appId, scopes, (code, cancellation) => {
    Console.WriteLine(code.Message);
    return Task.FromResult(0);
});

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
            var accessToken = await GraphHelper.GetAccessTokenAsync(scopes);
            Console.WriteLine($"Access token: {accessToken}\n");
            break;
        case 2:
            // Get signed in user
            var user = await GraphHelper.GetMeAsync();
            Console.WriteLine($"Signed in user: {user!.DisplayName}\n");
            break;
        case 3:
            // Get messages
            var messages = await GraphHelper.GetMessagesAsync();
            
            Console.WriteLine($"Signed in user emails:\n");
            foreach(var message in messages!) {
                Console.WriteLine($"{message.From.EmailAddress.Name} ({message.From.EmailAddress.Address}): {message.Subject}");
            }
            break;
        case 4:
            // Send mail
            var currentUser = await GraphHelper.GetMeAsync();
            await GraphHelper.SendMessageAsync("Hello from Microsoft Graph 🦒", "Welcome to the amazing world of Graph!", currentUser!.Mail);
            Console.WriteLine($"Sent email as the signed in user\n");
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}
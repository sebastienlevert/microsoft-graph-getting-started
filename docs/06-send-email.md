# Send a new email

In this section you will add the ability to send new emails from the user's mailbox.

1. Open **./Graph/GraphHelper.cs** and add the following function to the **GraphHelper** class.

    ```csharp
    public static async Task SendMessageAsync(string subject, string body, string recipientEmail)
    {
        try
        {
            // GET /me
            await graphClient!.Me.SendMail(new Message {
                Subject = subject,
                Body = new ItemBody() {
                    Content = body,
                    ContentType = BodyType.Text
                },
                ToRecipients = new List<Recipient>() { 
                    new Recipient {
                        EmailAddress = new EmailAddress {
                            Address = recipientEmail
                        }
                    }
                }
            })
            .Request()
            .PostAsync();
        }
        catch (ServiceException ex)
        {
            Console.WriteLine($"Error getting signed-in user: {ex.Message}");
        }
    }
    ```

    This code initializes an **Message** object and uses the Graph SDK to send it to the a specified email address.

4. Add the following just after the `// Send mail` comment in the `Program.cs` file.

    ```csharp
    var currentUser = await GraphHelper.GetMeAsync();
    await GraphHelper.SendMessageAsync("Hello from Microsoft Graph 🦒", "Welcome to the amazing world of Graph!", currentUser!.Mail);
    Console.WriteLine($"Sent email as the signed in user\n");
    ```

5. Save all of your changes and run the app. Choose the **Send an email as the signed in user** option. You can now see the email was sent in your **Sent items** folder.

[Next step](07-completed.md)
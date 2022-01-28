# Add Azure AD authentication

In this exercise you will extend the application from the previous exercise to support authentication with Azure AD. This is required to obtain the necessary OAuth access token to call the Microsoft Graph. In this step you will integrate the [Microsoft Authentication Library (MSAL) for .NET](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) into the application.

1. Add the following code to the `Program.cs` file immediately after the `Console.WriteLine(".NET Core Graph Tutorial\n");` line. Replace `YOUR_APP_ID_HERE` with the application ID you created in the Azure portal.

    ```csharp
    var appId = "YOUR_APP_ID_HERE";
    string[] scopes = {"User.Read", "Mail.Read", "Mail.Send"};
    ```

    Let's look at the permission scopes you just set.

    - **User.Read** will allow the app to read the signed-in user's profile to get information such as display name and email address.
    - **Mail.Read** will allow the app to read the user's emails.
    - **Mail.Send** will allow the app to send emails on behalf of the signed in user.

## Implement sign-in

In this section you will use the `DeviceCodeCredential` class to request an access token by using the [device code flow](https://docs.microsoft.com/azure/active-directory/develop/v2-oauth2-device-code).

1. Create a new directory in the **GraphTutorial** directory named **Graph**.
1. Create a new file in the **Graph** directory named **GraphHelper.cs** and add the following code to that file.

    ```csharp
    using Azure.Core;
    using Azure.Identity;
    using Microsoft.Graph;

    namespace GraphGettingStarted
    {
        public class GraphHelper
        {
            private static DeviceCodeCredential? tokenCredential;
            private static GraphServiceClient? graphClient;

            public static void Initialize(string clientId,
                                          string[] scopes,
                                          Func<DeviceCodeInfo, CancellationToken, Task> callBack)
            {
                tokenCredential = new DeviceCodeCredential(callBack, clientId);
                graphClient = new GraphServiceClient(tokenCredential, scopes);
            }

            public static async Task<string> GetAccessTokenAsync(string[] scopes)
            {
                var context = new TokenRequestContext(scopes);
                var response = await tokenCredential!.GetTokenAsync(context);
                return response.Token;
            }
        }
    }
    ```

1. Add the following `using` statement at the top of your `Program.cs` file.

    ```csharp
    using GraphGettingStarted;
    ```

2. Add the following code to the `Program.cs` file immediately after the `var appId = "YOUR_APP_ID_HERE";`and `string[] scopes = {"User.Read", "Mail.Read", "Mail.Send"}` lines.

    ```csharp
        // Initialize Graph client
    GraphHelper.Initialize(appId, scopes, (code, cancellation) => {
        Console.WriteLine(code.Message);
        return Task.FromResult(0);
    });
    ```

3. Add the following code to the `Program.cs` file immediately after the `// Display access token` line.

    ```csharp
    var accessToken = await GraphHelper.GetAccessTokenAsync(scopes);
    Console.WriteLine($"Access token: {accessToken}\n");
    ```

4. Build and run the app. The application displays a URL and device code.

    ```Shell
    PS C:\Source\GraphTutorial> dotnet run
    .NET Core Graph Tutorial

    To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code F7CG945YZ to authenticate.
    ```

    > If you encounter errors, compare your `Program.cs` with the [example on GitHub](https://github.com/sebastienlevert/microsoft-graph-getting-started/blob/main/Program.cs).

5. Open a browser and browse to the URL displayed. Enter the provided code and sign in. Once completed, return to the application and choose the **1. Display access token** option to display the access token.

[Next step](05-add-ms-graph.md)
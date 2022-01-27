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

        public static async Task<IUserMessagesCollectionPage?> GetMessagesAsync(int numberOfMessages = 5)
        {
            try
            {
                // GET /me
                return await graphClient!.Me.Messages
                    .Request()
                    .Select(u => new{
                        u.Subject,
                        u.From
                    })
                    .Top(numberOfMessages)
                    .GetAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

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
    }
}
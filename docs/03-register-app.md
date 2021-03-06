# Register the app in the portal

In this exercise you will create a new Azure AD application using the Azure Active Directory admin center.

1. Open a browser and navigate to the [Azure Active Directory admin center](https://aad.portal.azure.com) and login using a **personal account** (aka: Microsoft Account) or **Work or School Account**.

1. Select **Azure Active Directory** in the left-hand navigation, then select **App registrations** under **Manage**.

    ![A screenshot of the App registrations ](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core/tutorial/images/aad-portal-app-registrations.png)

2. Select **New registration**. On the **Register an application** page, set the values as follows.

    - Set **Name** to `.NET Core Graph Tutorial`.
    - Set **Supported account types** to **Accounts in any organizational directory and personal Microsoft accounts**.
    - Under **Redirect URI**, change the dropdown to **Public client (mobile & desktop)**, and set the value to `https://login.microsoftonline.com/common/oauth2/nativeclient`.

    ![A screenshot of the Register an application page](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core/tutorial/images/aad-register-an-app.png)

3. Select **Register**. On the **.NET Core Graph Tutorial** page, copy the value of the **Application (client) ID** and save it, you will need it in the next step.

    ![A screenshot of the application ID of the new app registration](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core/tutorial/images/aad-application-id.png)

4. Select **Authentication** under **Manage**. Locate the **Advanced settings** section and change the **Allow public client flows** toggle to **Yes**, then choose **Save**.

    ![A screenshot of the Allow public client flows toggle](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core/tutorial/images/aad-default-client-type.png)

[Next step](04-add-aad-auth.md)
using Microsoft.Maui.Storage;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace MauiPeopleApp.Views;

public partial class AuthPage : ContentPage
{
    public AuthPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RunBiometricFlowAsync();
    }

    private async Task RunBiometricFlowAsync()
    {
        TryAgainButton.IsVisible = false;

        if (Preferences.Get("AuthOk", false))
        {
            await NavigateToAppAsync();
            return;
        }

        var available = await CrossFingerprint.Current.IsAvailableAsync(allowAlternativeAuthentication: true);
        if (!available)
        {
            StatusLabel.Text = "Biometric/passcode not available. Continuing…";
            await NavigateToAppAsync();
            return;
        }

        var config = new AuthenticationRequestConfiguration(
            "Unlock",
            "Authenticate to continue")
        {
            AllowAlternativeAuthentication = true,

            // Show the "Enter Passcode" fallback (don’t blank this out)
            FallbackTitle = "Enter Passcode",

            CancelTitle = "Cancel"
        };

        var result = await CrossFingerprint.Current.AuthenticateAsync(config);

        if (result.Authenticated)
        {
            Preferences.Set("AuthOk", true);
            await NavigateToAppAsync();
        }
        else
        {
            StatusLabel.Text = result.Status switch
            {
                FingerprintAuthenticationResultStatus.Canceled => "Canceled.",
                FingerprintAuthenticationResultStatus.TooManyAttempts => "Too many attempts. Try again shortly.",
                FingerprintAuthenticationResultStatus.NotAvailable => "Auth not available.",
                _ => "Authentication failed."
            };
            TryAgainButton.IsVisible = true;
        }
    }

    private async void OnTryAgainClicked(object sender, EventArgs e)
    {
        await RunBiometricFlowAsync();
    }

    private Task NavigateToAppAsync()
    {
        Application.Current!.MainPage = new NavigationPage(new PersonListPage());
        return Task.CompletedTask;
    }
}

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using System;
using System.Windows;

public static class ToastNotificationService
{
    private static Notifier notifier;

    public static void Initialize(Window window)
    {
        notifier = new Notifier(cf =>
        {
            cf.PositionProvider = new WindowPositionProvider(
                parentWindow: window,
                corner: Corner.BottomRight,
                offsetX: -1,
                offsetY: 619);

            cf.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cf.Dispatcher = Application.Current.Dispatcher;
            cf.DisplayOptions.Width = 250;
           
        });
    }

    public static void ShowInformation(string message)
    {
        notifier?.ShowInformation(message);
    }

    public static void ShowSuccess(string message)
    {
        notifier?.ShowSuccess(message);
    }
}

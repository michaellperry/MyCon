namespace $rootnamespace$.Models;

fact Identity {
key:
    string anonymousId;

query:
    DisableToastNotification* isToastNotificationDisabled {
        DisableToastNotification d : d.identity = this
            where not d.isReenabled
    }
}

fact DisableToastNotification {
key:
    unique;
    Identity identity;

query:
    bool isReenabled {
        exists EnableToastNotification e : e.disable = this
    }
}

fact EnableToastNotification {
key:
    DisableToastNotification* disable;
}

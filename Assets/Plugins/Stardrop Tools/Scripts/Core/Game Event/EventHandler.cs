
using System;

namespace StardropTools
{
    // not sure if I should add to the namespace
    // just to make it accessible to other scripts without the Stardrop namespace
}

/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler
{
    private event Action action = delegate { };

    public void Invoke() => action?.Invoke();

    /// <summary>
    /// Subscribes a class method to this event
    /// </summary>
    public void AddListener(Action listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    /// <summary>
    /// Removes the subscription of a class method to this event
    /// </summary>
    public void RemoveListener(Action listener) => action -= listener;

    /// <summary>
    /// Removes all subscribers of this event
    /// </summary>
    public void ClearAllListeners()
    {
        if (action == null)
            return;

        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T>
{
    private event Action<T> action = delegate { };

    public void Invoke(T arg) => action?.Invoke(arg);

    public void AddListener(Action<T> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1,T2>
{
    private event Action<T1, T2> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2) => action?.Invoke(arg1, arg2);

    public void AddListener(Action<T1, T2> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3>
{
    private event Action<T1, T2, T3> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg1, arg2, arg3);

    public void AddListener(Action<T1, T2, T3> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4>
{
    private event Action<T1, T2, T3, T4> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => action?.Invoke(arg1, arg2, arg3, arg4);

    public void AddListener(Action<T1, T2, T3, T4> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5>
{
    private event Action<T1, T2, T3, T4, T5> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5);

    public void AddListener(Action<T1, T2, T3, T4, T5> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6>
{
    private event Action<T1, T2, T3, T4, T5, T6> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>;
    }
}



/// <summary>
/// An Action/Delegate wrapper with usefull methods for easy Event Subscription and Unsubscription
/// <para> Allows for the Invokation of events, removing the need for direct script reference or object cache </para>
/// </summary>
public class EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
    private event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

    public void AddListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> listener) => action -= listener;

    public void ClearAllListeners()
    {
        Delegate[] eventListeners = action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            action -= del as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>;
    }
}

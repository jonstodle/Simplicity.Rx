﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Reactive.Linq
{
    public static class CreateObservableMixins
    {
        /// <summary>
        /// Creates an observable that starts the <c>Task</c> when subscribed to
        /// </summary>
        /// <param name="This">The <c>Task</c> to convert.</param>
        /// <returns></returns>
        public static IObservable<Unit> ToColdObservable(this Task This) => Observable.FromAsync(() => This);

        /// <summary>
        /// Creates an observable that starts the <c>Task</c> when subscribed to
        /// </summary>
        /// <typeparam name="T">The type of the next signals.</typeparam>
        /// <param name="This">The <c>Task</c> to convert.</param>
        /// <returns></returns>
        public static IObservable<T> ToColdObservable<T>(this Task<T> This) => Observable.FromAsync(() => This);

        /// <summary>
        /// Creates an observable that signals every time the event with the given name is fired
        /// </summary>
        /// <returns>An observable that signals every time the event is fired.</returns>
        /// <param name="This">The object containing the event.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <typeparam name="T">The type of the class contains the event.</typeparam>
        public static IObservable<EventPattern<object>> GetEvents<T>(this T This, string eventName) => Observable.FromEventPattern(This, eventName);

        /// <summary>
        /// Creates an observable that signals every time the event with the given name is fired
        /// </summary>
        /// <returns>An observable that signals ever time the event is fired.</returns>
        /// <param name="This">The object containing the event.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <typeparam name="TEventArgs">The type of event arguments emitted by the event.</typeparam>
        public static IObservable<EventPattern<TEventArgs>> GetEvents<TEventArgs>(this object This, string eventName) => Observable.FromEventPattern<TEventArgs>(This, eventName);
    }
}
